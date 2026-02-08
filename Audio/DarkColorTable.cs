using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Audio
{
    public class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(60, 60, 60);
        public override Color MenuItemBorder => Color.FromArgb(60, 60, 60);
        public override Color MenuBorder => Color.FromArgb(30, 30, 30);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(40, 40, 40);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(40, 40, 40);
        public override Color ToolStripDropDownBackground => Color.FromArgb(30, 30, 30);
        public override Color ImageMarginGradientBegin => Color.FromArgb(30, 30, 30);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(30, 30, 30);
        public override Color ImageMarginGradientEnd => Color.FromArgb(30, 30, 30);
    }
}
