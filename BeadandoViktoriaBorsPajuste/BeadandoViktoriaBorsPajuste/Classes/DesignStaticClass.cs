using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    internal class DesignStaticClass
        // Functions for design changes
    {
        public static void PB_Enter(PictureBox PB)
        {
            PB.Size = new Size(PB.Width + 4, PB.Height + 4);
            PB.Location = new Point(PB.Location.X - 2, PB.Location.Y - 2);
        }

        public static void PB_Leave(PictureBox PB)
        {
            PB.Size = new Size(PB.Width - 4, PB.Height - 4);
            PB.Location = new Point(PB.Location.X + 2, PB.Location.Y + 2);
        }

        public static void Lbl_Enter(Label lbl)
        {
            lbl.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lbl.Location = new Point(lbl.Location.X - 2, lbl.Location.Y);
        }

        public static void Lbl_Leave(Label lbl)
        {
            lbl.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            lbl.Location = new Point(lbl.Location.X + 2, lbl.Location.Y);
        }
    }
}
