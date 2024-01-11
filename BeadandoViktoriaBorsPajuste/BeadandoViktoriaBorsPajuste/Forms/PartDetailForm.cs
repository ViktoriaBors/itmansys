using BeadandoViktoriaBorsPajuste.Classes;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace BeadandoViktoriaBorsPajuste.Forms
{
    public partial class PartDetailForm : Form
    {
        public Parts ChosenPart { get;private set; }

        private Types chosen;
        private List<string> Fields { get; }
        public PartDetailForm(Parts PartsInfo)
        {
            InitializeComponent();
            ChosenPart = PartsInfo;
            this.Text = "Detailed information about: " + ChosenPart.ToString();
            ChosenPart = PartsInfo;
            button1.Text = "Close";
            chosen = StaticClass.TypeCheck(PartsInfo);
            Fields = MySQLDatahandler.PropertyNames(chosen.ToString().ToLower());

            if (Fields.Count != 0)
            {
                UI_Loading(chosen);
            } else
            {
                UI_Loading(Types.X);
            }

        }

        private void UI_Loading(Types chosen)
        {
            // base information
            manufacturerLbl.Text = ChosenPart.Manufacturer;
            typeLbl.Text = ChosenPart.Type;
            priceLbl.Text = ChosenPart.Price.ToString();
            warrantyLbl.Text = ChosenPart.Warranty.ToString();

            switch (chosen)
            {
                case Types.X:
                    UI_X();
                    break;
                case Types.Processor:
                    UI_Processor();
                    break;
                case Types.Motherboard:
                    UI_Motherboard();
                    break;
                case Types.RAM:
                    UI_RAM();
                    break;
                case Types.GPU:
                    UI_GPU();
                    break;
                case Types.PSU:
                    UI_PSU();
                    break;
            }
        }

        private void UI_PSU()
        {
            str1Lbl.Text = Fields[6];  
            str1ResLbl.Text = (ChosenPart as PSU).Quality.ToString();

            int1Lbl.Text = Fields[7];
            int1ResLbl.Text = (ChosenPart as PSU).Poweroutput.ToString() + " Watt";

            checkBox1.Visible = false;

            str2Lbl.Visible = false;
            str2ResLbl.Visible = false;
            int2Lbl.Visible = false;
            int2ResLbl.Visible = false;
            int3Lbl.Visible = false;
            int3ResLbl.Visible = false;

        }

        private void UI_GPU()
        {
            str1Lbl.Text = Fields[6];
            str1ResLbl.Text = (ChosenPart as GPU).Ram.ToString();

            int1Lbl.Text = Fields[7];
            int1ResLbl.Text = (ChosenPart as GPU).Size.ToString() + " GB";
            int2Lbl.Text = Fields[8];
            int2ResLbl.Text = (ChosenPart as GPU).Coreclockspeed.ToString() + " mHz";
            int3Lbl.Text = Fields[9];
            int3ResLbl.Text = (ChosenPart as GPU).Powerconsumption.ToString() + " Watt";

            checkBox1.Text = Fields[10];
            checkBox1.Checked = (ChosenPart as GPU).Raytracing;

            str2Lbl.Visible = false;
            str2ResLbl.Visible = false;
        }

        private void UI_RAM()
        {
            str1Lbl.Text = Fields[6];
            str1ResLbl.Text = (ChosenPart as RAM).Gen.ToString();

            int1Lbl.Text = Fields[7];
            int1ResLbl.Text = (ChosenPart as RAM).Size.ToString() + " GB";
            int2Lbl.Text = Fields[8];
            int2ResLbl.Text = (ChosenPart as RAM).Clockspeed.ToString() + " mHz";
            int3Lbl.Text = Fields[9];
            int3ResLbl.Text = (ChosenPart as RAM).Timing.ToString() + " CL";

            checkBox1.Visible = false;
            str2Lbl.Visible = false;
            str2ResLbl.Visible = false;
        }

        private void UI_Motherboard()
        {
            str1Lbl.Text = Fields[6];
            str1ResLbl.Text = (ChosenPart as Motherboard).Procsocket.ToString();

            str2Lbl.Text = Fields[7];
            str2ResLbl.Text = (ChosenPart as Motherboard).Chipset.ToString();

            checkBox1.Text = Fields[8];
            checkBox1.Checked = (ChosenPart as Motherboard).Illuminated;
            checkBox1.Location = new Point(18, 77);

            int1Lbl.Visible = false;
            int1ResLbl.Visible = false;
            int2Lbl.Visible = false;
            int2ResLbl.Visible = false;
            int3Lbl.Visible = false;
            int3ResLbl.Visible = false;
        }

        private void UI_Processor()
        {
            str1Lbl.Text = Fields[6];
            str1ResLbl.Text = (ChosenPart as Processor).Package.ToString();

            int1Lbl.Text = Fields[7];
            int1ResLbl.Text = (ChosenPart as Processor).Clockspeed.ToString() + " mHz";
            int2Lbl.Text = Fields[8];
            int2ResLbl.Text = (ChosenPart as Processor).L3size.ToString() + " MB";
            int3Lbl.Text = Fields[9];
            int3ResLbl.Text = (ChosenPart as Processor).Cores.ToString() + ((ChosenPart as Processor).Cores > 1 ? " pieces" : " piece");

            checkBox1.Visible = false;
            str2Lbl.Visible = false;
            str2ResLbl.Visible = false;
        }

        private void UI_X()
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }

            this.Text = "Uknown part was chosen";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FunctionResult exportResult = StaticClass.ExportPartToCSV(ChosenPart);

            switch (exportResult.Fresult)
            {
                case FunctionResultType.ok:
                    MessageBox.Show(exportResult.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case FunctionResultType.error:
                    MessageBox.Show(exportResult.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case FunctionResultType.fatal:
                    MessageBox.Show(exportResult.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }           
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(sender as PictureBox);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave((sender as PictureBox));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

    }
}
