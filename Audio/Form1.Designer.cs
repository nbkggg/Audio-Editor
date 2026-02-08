namespace Audio
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbWaveform = new System.Windows.Forms.PictureBox();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.lblVolume = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lblSelection = new System.Windows.Forms.Label();
            this.btnUndo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApplyFade = new System.Windows.Forms.Button();
            this.numFadeIn = new System.Windows.Forms.NumericUpDown();
            this.numFadeOut = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFadeIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFadeOut)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(955, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            // 
            // pbWaveform
            // 
            this.pbWaveform.BackColor = System.Drawing.Color.Black;
            this.pbWaveform.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pbWaveform.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbWaveform.Location = new System.Drawing.Point(0, 24);
            this.pbWaveform.Name = "pbWaveform";
            this.pbWaveform.Size = new System.Drawing.Size(955, 175);
            this.pbWaveform.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbWaveform.TabIndex = 1;
            this.pbWaveform.TabStop = false;
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(16, 281);
            this.tbVolume.Maximum = 200;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(243, 37);
            this.tbVolume.TabIndex = 2;
            this.tbVolume.Value = 100;
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVolume.Location = new System.Drawing.Point(12, 245);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(176, 24);
            this.lblVolume.TabIndex = 3;
            this.lblVolume.Text = "Громкость: 100%";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(745, 408);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(198, 37);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить файл";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlay.Location = new System.Drawing.Point(454, 205);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(285, 37);
            this.btnPlay.TabIndex = 6;
            this.btnPlay.Text = "Прослушать отрывок";
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // lblSelection
            // 
            this.lblSelection.AutoSize = true;
            this.lblSelection.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSelection.Location = new System.Drawing.Point(12, 211);
            this.lblSelection.Name = "lblSelection";
            this.lblSelection.Size = new System.Drawing.Size(247, 24);
            this.lblSelection.TabIndex = 8;
            this.lblSelection.Text = "Выделено: 00:00 - 00:00";
            // 
            // btnUndo
            // 
            this.btnUndo.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUndo.Location = new System.Drawing.Point(745, 205);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(198, 37);
            this.btnUndo.TabIndex = 9;
            this.btnUndo.Text = "Вернуть действие";
            this.btnUndo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(27, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Нарастание:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(27, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "Затухание:";
            // 
            // btnApplyFade
            // 
            this.btnApplyFade.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnApplyFade.Location = new System.Drawing.Point(23, 408);
            this.btnApplyFade.Name = "btnApplyFade";
            this.btnApplyFade.Size = new System.Drawing.Size(286, 37);
            this.btnApplyFade.TabIndex = 14;
            this.btnApplyFade.Text = "Создать затухание";
            this.btnApplyFade.UseVisualStyleBackColor = true;
            // 
            // numFadeIn
            // 
            this.numFadeIn.Location = new System.Drawing.Point(164, 321);
            this.numFadeIn.Name = "numFadeIn";
            this.numFadeIn.Size = new System.Drawing.Size(120, 20);
            this.numFadeIn.TabIndex = 15;
            // 
            // numFadeOut
            // 
            this.numFadeOut.Location = new System.Drawing.Point(164, 366);
            this.numFadeOut.Name = "numFadeOut";
            this.numFadeOut.Size = new System.Drawing.Size(120, 20);
            this.numFadeOut.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 457);
            this.Controls.Add(this.numFadeOut);
            this.Controls.Add(this.numFadeIn);
            this.Controls.Add(this.btnApplyFade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.lblSelection);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.tbVolume);
            this.Controls.Add(this.pbWaveform);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Audio Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFadeIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFadeOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbWaveform;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblSelection;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApplyFade;
        private System.Windows.Forms.NumericUpDown numFadeIn;
        private System.Windows.Forms.NumericUpDown numFadeOut;
    }
}

