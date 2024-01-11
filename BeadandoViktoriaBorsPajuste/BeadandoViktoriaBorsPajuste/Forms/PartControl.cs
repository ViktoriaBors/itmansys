using BeadandoViktoriaBorsPajuste.Classes;
using BeadandoViktoriaBorsPajuste.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace BeadandoViktoriaBorsPajuste.Forms
{
    public partial class PartControl : UserControl
    {
        public Parts ChosenPart { get; private set; }
        public int Index { get; private set; }
        bool chosen = false;
        public PartControl(Parts chosenPart, int index)
        {
            InitializeComponent();
            ChosenPart = chosenPart;
            Index = index;
            UI_Loading();
        }

        private void UI_Loading()
        {
            label1.Text = ChosenPart.ToString();
            label6.Text = ChosenPart.Manufacturer.ToString();
            label7.Text = ChosenPart.Type;
            label8.Text = ChosenPart.Price.ToString();
            label9.Text = ChosenPart.Warranty.ToString();

            PictureBox_IMG();

        }

        private void PictureBox_IMG()
        {
            if (ChosenPart is Processor)
            {
                pictureBox1.Image = Resources.processor;
            } else if (ChosenPart is Motherboard)
            {
                pictureBox1.Image = Resources.motherboard;
            }
            else if (ChosenPart is RAM)
            {
                pictureBox1.Image = Resources.ram;
            }
            else if (ChosenPart is GPU)
            {
                pictureBox1.Image = Resources.gpu;
            }
            else if (ChosenPart is PSU)
            {
                pictureBox1.Image = Resources.power_supply;
            }
        }

        public void UnChoosePart()
        {
            checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chosen = !chosen;
        }
    }
}
