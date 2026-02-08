using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;

namespace Audio
{
    public struct EditorState
    {
        public double Start;
        public double End;
        public int Volume;
        public double FadeIn;
        public double FadeOut;

        public EditorState(double s, double e, int v, double fi, double fo)
        {
            Start = s;
            End = e;
            Volume = v;
            FadeIn = fi;
            FadeOut = fo;
        }
    }

    public partial class Form1 : Form
    {
        private WaveStream audioReader;
        private WaveOutEvent outputDevice;
        private string currentFilePath;

        private double selectionStartSec = 0;
        private double selectionEndSec = 0;

        private double currentFadeIn = 0;
        private double currentFadeOut = 0;

        private Point startPoint;
        private Point endPoint;
        private bool isSelecting = false;
        private Rectangle selectionRect;

        private Stack<EditorState> undoStack = new Stack<EditorState>();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Аудио Редактор Pro (Safe Mode)";
            this.KeyPreview = true;

            SetupControlsAndEvents();
            ApplyDarkTheme();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                UndoAction();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private WaveStream CreateSafeReader(string file)
        {
            if (file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                return new Mp3FileReader(file);
            }
            else if (file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                return new WaveFileReader(file);
            }
            else
            {
                return new AudioFileReader(file);
            }
        }

        private void SetupControlsAndEvents()
        {
            this.Controls.Add(numFadeIn);
            this.Controls.Add(numFadeOut);
            this.Controls.Add(btnApplyFade);

            if (MainMenuStrip != null && MainMenuStrip.Items.Count > 0)
                MainMenuStrip.Items[0].Click += (s, e) => OpenFile();

            btnPlay.Click += (s, e) => PlayAudio();
            btnSave.Click += (s, e) => SaveFile();

            btnApplyFade.Click += (s, e) =>
            {
                PushStateToHistory();
                currentFadeIn = (double)numFadeIn.Value;
                currentFadeOut = (double)numFadeOut.Value;
                MessageBox.Show($"Применено!\nFade In: {currentFadeIn} сек\nFade Out: {currentFadeOut} сек");
            };

            if (this.Controls.ContainsKey("btnUndo"))
                this.Controls["btnUndo"].Click += (s, e) => UndoAction();
            else
            {
                Button dynamicUndo = new Button();
                dynamicUndo.Name = "btnUndo";
                dynamicUndo.Text = "Назад (Ctrl+Z)";
                dynamicUndo.Size = new Size(100, 30);
                dynamicUndo.Location = new Point(btnSave.Location.X + btnSave.Width + 10, btnSave.Location.Y);
                dynamicUndo.Click += (s, e) => UndoAction();
                this.Controls.Add(dynamicUndo);
            }

            tbVolume.Scroll += (s, e) => lblVolume.Text = $"Громкость: {tbVolume.Value}%";
            tbVolume.MouseDown += (s, e) => PushStateToHistory();
            tbVolume.KeyDown += (s, e) => { if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) PushStateToHistory(); };

            pbWaveform.MouseDown += PbWaveform_MouseDown;
            pbWaveform.MouseMove += PbWaveform_MouseMove;
            pbWaveform.MouseUp += PbWaveform_MouseUp;
            pbWaveform.Paint += PbWaveform_Paint;
        }

        private void PushStateToHistory()
        {
            undoStack.Push(new EditorState(selectionStartSec, selectionEndSec, tbVolume.Value, currentFadeIn, currentFadeOut));
        }

        private void UndoAction()
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
                outputDevice.Stop();

            if (undoStack.Count > 0)
            {
                EditorState lastState = undoStack.Pop();
                selectionStartSec = lastState.Start;
                selectionEndSec = lastState.End;
                tbVolume.Value = lastState.Volume;
                lblVolume.Text = $"Громкость: {tbVolume.Value}%";
                currentFadeIn = lastState.FadeIn;
                currentFadeOut = lastState.FadeOut;
                numFadeIn.Value = (decimal)currentFadeIn;
                numFadeOut.Value = (decimal)currentFadeOut;

                if (audioReader != null && pbWaveform.Width > 0)
                {
                    double totalSec = audioReader.TotalTime.TotalSeconds;
                    int x = (int)((selectionStartSec / totalSec) * pbWaveform.Width);
                    int width = (int)(((selectionEndSec - selectionStartSec) / totalSec) * pbWaveform.Width);
                    selectionRect = new Rectangle(x, 0, width, pbWaveform.Height);

                    TimeSpan tsStart = TimeSpan.FromSeconds(selectionStartSec);
                    TimeSpan tsEnd = TimeSpan.FromSeconds(selectionEndSec);
                    if (lblSelection != null)
                        lblSelection.Text = $"Выделено: {tsStart:mm\\:ss\\.ff} - {tsEnd:mm\\:ss\\.ff}";

                    pbWaveform.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("История пуста!");
            }
        }

        private void PbWaveform_MouseDown(object sender, MouseEventArgs e)
        {
            if (audioReader == null) return;
            if (selectionEndSec > selectionStartSec) PushStateToHistory();

            isSelecting = true;
            startPoint = e.Location;
            selectionRect = new Rectangle();
            pbWaveform.Invalidate();
        }

        private void PbWaveform_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting && audioReader != null)
            {
                endPoint = e.Location;
                int x = Math.Min(startPoint.X, endPoint.X);
                int width = Math.Abs(startPoint.X - endPoint.X);
                if (x < 0) x = 0;
                if (x + width > pbWaveform.Width) width = pbWaveform.Width - x;

                selectionRect = new Rectangle(x, 0, width, pbWaveform.Height);
                double totalSeconds = audioReader.TotalTime.TotalSeconds;
                double currentStart = ((double)x / pbWaveform.Width) * totalSeconds;
                double currentEnd = ((double)(x + width) / pbWaveform.Width) * totalSeconds;

                TimeSpan tsStart = TimeSpan.FromSeconds(currentStart);
                TimeSpan tsEnd = TimeSpan.FromSeconds(currentEnd);
                if (lblSelection != null) lblSelection.Text = $"Выделено: {tsStart:mm\\:ss\\.ff} - {tsEnd:mm\\:ss\\.ff}";
                pbWaveform.Invalidate();
            }
        }

        private void PbWaveform_MouseUp(object sender, MouseEventArgs e)
        {
            if (audioReader == null) return;
            isSelecting = false;
            double totalSeconds = audioReader.TotalTime.TotalSeconds;
            double startSec = ((double)selectionRect.X / pbWaveform.Width) * totalSeconds;
            double endSec = ((double)(selectionRect.X + selectionRect.Width) / pbWaveform.Width) * totalSeconds;

            if (endSec > startSec) { selectionStartSec = startSec; selectionEndSec = endSec; }
            else { selectionStartSec = 0; selectionEndSec = totalSeconds; if (lblSelection != null) lblSelection.Text = "Весь трек"; }
        }

        private void PbWaveform_Paint(object sender, PaintEventArgs e)
        {
            if (selectionRect != null && selectionRect.Width > 0)
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(100, 0, 120, 215))) e.Graphics.FillRectangle(brush, selectionRect);
                using (Pen pen = new Pen(Color.DeepSkyBlue, 1)) e.Graphics.DrawRectangle(pen, selectionRect);
            }
        }

        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Аудио файлы|*.wav;*.mp3";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = ofd.FileName;

                if (outputDevice != null) { outputDevice.Stop(); outputDevice.Dispose(); outputDevice = null; }
                if (audioReader != null) { audioReader.Dispose(); audioReader = null; }

                try
                {
                    audioReader = CreateSafeReader(currentFilePath);

                    selectionStartSec = 0;
                    selectionEndSec = audioReader.TotalTime.TotalSeconds;
                    undoStack.Clear();

                    currentFadeIn = 0; currentFadeOut = 0;
                    numFadeIn.Value = 0; numFadeOut.Value = 0;

                    DrawWaveform(currentFilePath);
                    selectionRect = new Rectangle(0, 0, pbWaveform.Width, pbWaveform.Height);
                    if (lblSelection != null) lblSelection.Text = "Файл открыт. Выделите участок мышкой.";
                    pbWaveform.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message);
                }
            }
        }

        private void DrawWaveform(string path)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    using (WaveStream localReader = CreateSafeReader(path))
                    {
                        ISampleProvider sampleProvider = localReader.ToSampleProvider();

                        Bitmap bmp = new Bitmap(pbWaveform.Width, pbWaveform.Height);

                        int channels = localReader.WaveFormat.Channels;
                        int sampleRate = localReader.WaveFormat.SampleRate;

                        long totalSamples = (long)(localReader.TotalTime.TotalSeconds * sampleRate * channels);
                        if (totalSamples == 0) totalSamples = 1;

                        int samplesPerPixel = (int)(totalSamples / pbWaveform.Width);
                        if (samplesPerPixel < 1) samplesPerPixel = 1;

                        float[] buffer = new float[samplesPerPixel];

                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Black);
                            Pen pen = new Pen(Color.LimeGreen, 1);
                            g.DrawLine(Pens.DarkGray, 0, pbWaveform.Height / 2, pbWaveform.Width, pbWaveform.Height / 2);

                            for (int x = 0; x < pbWaveform.Width; x++)
                            {
                                int read = sampleProvider.Read(buffer, 0, samplesPerPixel);
                                if (read == 0) break;

                                float max = 0;
                                for (int i = 0; i < read; i++)
                                    if (Math.Abs(buffer[i]) > max) max = Math.Abs(buffer[i]);

                                int height = (int)(max * pbWaveform.Height);
                                g.DrawLine(pen, x, pbWaveform.Height / 2 - height / 2, x, pbWaveform.Height / 2 + height / 2);
                            }
                        }
                        pbWaveform.Invoke((MethodInvoker)(() => pbWaveform.Image = bmp));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка отрисовки волны: " + ex.Message);
                }
            });
        }

        private void PlayAudio()
        {
            if (audioReader == null) return;
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Stop();
                return;
            }

            try
            {
                outputDevice = new WaveOutEvent();
                audioReader.CurrentTime = TimeSpan.FromSeconds(selectionStartSec);

                var volumeProvider = new VolumeSampleProvider(audioReader.ToSampleProvider());
                volumeProvider.Volume = tbVolume.Value / 100f;

                outputDevice.Init(volumeProvider);

                Timer limitTimer = new Timer();
                limitTimer.Interval = 50;
                outputDevice.PlaybackStopped += (s, e) => { btnPlay.Text = "Прослушать отрывок"; limitTimer.Stop(); outputDevice.Dispose(); outputDevice = null; };
                limitTimer.Tick += (s, e) => { if (outputDevice != null && audioReader.CurrentTime.TotalSeconds >= selectionEndSec) outputDevice.Stop(); };

                outputDevice.Play();
                limitTimer.Start();
                btnPlay.Text = "Остановить";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка воспроизведения: " + ex.Message);
            }
        }

        private void SaveFile()
        {
            if (audioReader == null) return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "WAV файл|*.wav";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    double duration = selectionEndSec - selectionStartSec;
                    if (duration <= 0.1) { MessageBox.Show("Выделите участок!"); return; }

                    audioReader.CurrentTime = TimeSpan.FromSeconds(selectionStartSec);

                    var sampleProvider = audioReader.ToSampleProvider();
                    var volumeProvider = new VolumeSampleProvider(sampleProvider);
                    volumeProvider.Volume = tbVolume.Value / 100f;

                    using (var writer = new WaveFileWriter(sfd.FileName, sampleProvider.WaveFormat))
                    {
                        int sampleRate = sampleProvider.WaveFormat.SampleRate;
                        int channels = sampleProvider.WaveFormat.Channels;

                        long totalSamplesToWrite = (long)(duration * sampleRate * channels);

                        long fadeInSamples = (long)(currentFadeIn * sampleRate * channels);
                        long fadeOutSamples = (long)(currentFadeOut * sampleRate * channels);

                        float[] buffer = new float[sampleRate * channels];
                        long samplesWrittenSoFar = 0;

                        while (samplesWrittenSoFar < totalSamplesToWrite)
                        {
                            int samplesRead = volumeProvider.Read(buffer, 0, buffer.Length);
                            if (samplesRead == 0) break;

                            if (samplesWrittenSoFar + samplesRead > totalSamplesToWrite)
                                samplesRead = (int)(totalSamplesToWrite - samplesWrittenSoFar);

                            for (int i = 0; i < samplesRead; i += channels)
                            {
                                long currentSamplePos = samplesWrittenSoFar + i;
                                float fadeMultiplier = 1.0f;

                                if (currentSamplePos < fadeInSamples)
                                {
                                    fadeMultiplier = (float)currentSamplePos / fadeInSamples;
                                }
                                else if (currentSamplePos > totalSamplesToWrite - fadeOutSamples)
                                {
                                    long samplesFromEnd = totalSamplesToWrite - currentSamplePos;
                                    fadeMultiplier = (float)samplesFromEnd / fadeOutSamples;
                                }

                                for (int ch = 0; ch < channels; ch++)
                                {
                                    if (i + ch < samplesRead)
                                        buffer[i + ch] *= fadeMultiplier;
                                }
                            }

                            writer.WriteSamples(buffer, 0, samplesRead);
                            samplesWrittenSoFar += samplesRead;
                        }
                    }
                    MessageBox.Show("Файл сохранен с эффектами!");
                }
                catch (Exception ex) { MessageBox.Show("Ошибка сохранения: " + ex.Message); }
            }
        }

        private void ApplyDarkTheme()
        {
            Color darkBackground = Color.FromArgb(30, 30, 30);
            Color lightText = Color.White;
            Color buttonColor = Color.SeaGreen;

            this.BackColor = darkBackground;
            this.ForeColor = lightText;
            pbWaveform.BackColor = Color.Black;

            if (MainMenuStrip != null)
            {
                MainMenuStrip.BackColor = darkBackground;
                MainMenuStrip.ForeColor = lightText;
                MainMenuStrip.Renderer = new ToolStripProfessionalRenderer(new DarkColorTable());
                foreach (ToolStripItem item in MainMenuStrip.Items) { item.ForeColor = lightText; if (item is ToolStripMenuItem mi) foreach (ToolStripItem sub in mi.DropDownItems) sub.ForeColor = Color.Black; }
            }

            foreach (Control c in this.Controls)
            {
                if (c is Button btn)
                {
                    if (btn.Text.Contains("Назад") || btn.Name == "btnUndo") btn.BackColor = Color.FromArgb(100, 100, 100);
                    else btn.BackColor = buttonColor;
                    btn.FlatStyle = FlatStyle.Flat; btn.FlatAppearance.BorderSize = 0; btn.ForeColor = lightText; btn.Cursor = Cursors.Hand;
                }
            }
        }
    }
}