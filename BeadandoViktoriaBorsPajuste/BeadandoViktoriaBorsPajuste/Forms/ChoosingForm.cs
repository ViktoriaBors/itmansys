using BeadandoViktoriaBorsPajuste.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeadandoViktoriaBorsPajuste.Forms
{
    public partial class ChoosingForm : Form
    {
        internal Types Type { get; private set; }
        public ChoosingForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox1);
            DesignStaticClass.Lbl_Enter(label3);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox1);
            DesignStaticClass.Lbl_Leave(label3);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox2);
            DesignStaticClass.Lbl_Enter(label4);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox2);
            DesignStaticClass.Lbl_Leave(label4);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox3);
            DesignStaticClass.Lbl_Enter(label5);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox3);
            DesignStaticClass.Lbl_Leave(label5);
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox4);
            DesignStaticClass.Lbl_Enter(label6);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox4);
            DesignStaticClass.Lbl_Leave(label6);
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox5);
            DesignStaticClass.Lbl_Enter(label7);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox5);
            DesignStaticClass.Lbl_Leave(label7);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Type = Types.Processor;
            PartsForm ablak = new PartsForm(Type);
            DialogResult = DialogResult.OK;
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            Type = Types.Motherboard;
            PartsForm ablak = new PartsForm(Type);
            DialogResult = DialogResult.OK;
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            Type = Types.RAM;
            PartsForm ablak = new PartsForm(Type);
            DialogResult = DialogResult.OK;
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            Type = Types.GPU;
            PartsForm ablak = new PartsForm(Type);
            DialogResult = DialogResult.OK;
        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            Type = Types.PSU;
            PartsForm ablak = new PartsForm(Type);
            DialogResult = DialogResult.OK;
        }
    }
}
