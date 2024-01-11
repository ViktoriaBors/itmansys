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
using static BeadandoViktoriaBorsPajuste.Classes.StaticClass;

namespace BeadandoViktoriaBorsPajuste.Forms
{
    public partial class PartsForm : Form
    {
        public Parts ChosenPart { get; private set; }

        private Types chosen;
        private List<string> Fields { get; }
        public PartsForm(Types type)
        {
            InitializeComponent();
            Fields = MySQLDatahandler.PropertyNames(type.ToString().ToLower());
            chosen = type;
            this.Text = "Create new " + chosen;
            button1.Text = "Create";

            if (Fields.Count  != 0)
            {
                UI_Loading(chosen);
            }
           
        }

        public PartsForm(Parts PartsToModify)
        {
            InitializeComponent();
            ChosenPart = PartsToModify;
            chosen = StaticClass.TypeCheck(PartsToModify);
            Fields = MySQLDatahandler.PropertyNames(chosen.ToString().ToLower());
            this.Text = ChosenPart.ToString();
            button1.Text = "Save changes";

            if (Fields.Count != 0)
            {
                UI_Loading(chosen);
            }
        }

        private void UI_Loading(Types type)
        {
            switch (type)
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
            comboBox1.DataSource = Enum.GetValues(typeof(PSUManufacturer));

            str1Lbl.Text = Fields[6];
            comboBox2.DataSource = Enum.GetValues(typeof(QualityType));
            str1Mertekegyseglbl.Visible = false;

            int1lbl.Text = Fields[7];
            int1Mertekegyseglbl.Text = "Watt";
            numericUpDown3.Minimum = 350;
            numericUpDown3.Maximum = 1200;

            str2Lbl.Visible = false;
            str2MertekegysegLbl.Visible = false; 
            comboBox3.Visible = false;

            int2Lbl.Visible = false;
            int2MertekegysegLbl.Visible=false;
            numericUpDown4.Visible = false;

            int3Lbl.Visible = false;    
            int3Mertekegyseglbl.Visible=false;
            numericUpDown5.Visible = false;

            checkBox1.Visible = false;


            if (ChosenPart != null) // modify
            {
                comboBox1.SelectedItem = (PSUManufacturer)Enum.Parse(typeof(PSUManufacturer), ChosenPart.Manufacturer); 
                textBox2.Text = ChosenPart.Type;
                numericUpDown1.Value = ChosenPart.Price;
                numericUpDown2.Value = ChosenPart.Warranty;
                comboBox2.SelectedItem = (ChosenPart as PSU).Quality;
                numericUpDown3.Value = (ChosenPart as PSU).Poweroutput;
            }
        }

        private void UI_GPU()
        {
            checkBox1.Visible=false;

            comboBox1.DataSource = Enum.GetValues(typeof(GPUManufacturer));

            str1Lbl.Text = Fields[6];
            comboBox2.DataSource = Enum.GetValues(typeof(RamType));
            str1Mertekegyseglbl.Visible = false;

            str2Lbl.Visible = false;
            comboBox3.Visible = false;
            str2MertekegysegLbl.Visible = false;

            int1lbl.Text = Fields[7];
            int1Mertekegyseglbl.Text = "GB";
            numericUpDown3.Minimum = 8;
            numericUpDown3.Maximum = 32;

            int2Lbl.Text = Fields[8];
            int2Lbl.Location = new Point(397,190);
            int2MertekegysegLbl.Text = "MHz";
            numericUpDown4.Minimum = 1800;
            numericUpDown4.Maximum = 3200;
          

            int3Lbl.Text = Fields[9];
            int3Mertekegyseglbl.Text = "Watt";
            numericUpDown5.Minimum = 120;
            numericUpDown5.Maximum = 650;

            if (ChosenPart != null) // modify
            {
                comboBox1.SelectedItem = (GPUManufacturer)Enum.Parse(typeof(GPUManufacturer), ChosenPart.Manufacturer); 
                textBox2.Text = ChosenPart.Type;
                numericUpDown1.Value = ChosenPart.Price;
                numericUpDown2.Value = ChosenPart.Warranty;
                comboBox2.SelectedItem = (ChosenPart as GPU).Ram;
                numericUpDown3.Value = (ChosenPart as GPU).Size;
                numericUpDown4.Value = (ChosenPart as GPU).Coreclockspeed;
                numericUpDown5.Value = (ChosenPart as GPU).Powerconsumption;
            }
        }

        private void UI_RAM()
        {

            checkBox1.Visible = false;

            comboBox1.DataSource = Enum.GetValues(typeof(RAMManufacturer));

            label3.Text = "Price is generated";
            numericUpDown1.Visible = false;
            label4.Visible = false;

            str1Lbl.Text = Fields[6];
            comboBox2.DataSource = Enum.GetValues(typeof(Generation));
            str1Mertekegyseglbl.Visible = false;

            str2Lbl.Visible = false;
            comboBox3.Visible = false;
            str2MertekegysegLbl.Visible = false;

            int1lbl.Text = Fields[7];
            int1Mertekegyseglbl.Text = "GB";
            numericUpDown3.Minimum = 8;
            numericUpDown3.Maximum = 128;

            int2Lbl.Text = Fields[8];
            int2MertekegysegLbl.Text = "MHz";

            int3Lbl.Text = Fields[9];
            int3Mertekegyseglbl.Text = "CL";
            numericUpDown5.Minimum = 8;
            numericUpDown5.Maximum = 60;

            if (ChosenPart != null) // modify
            {
                comboBox1.SelectedItem = (RAMManufacturer)Enum.Parse(typeof(RAMManufacturer), ChosenPart.Manufacturer); ;
                textBox2.Text = ChosenPart.Type;

                label3.Text = "Price is generated";
                numericUpDown1.Visible = true;
                label4.Visible = true;
                numericUpDown1.Minimum = 1;
                numericUpDown1.Maximum = 10000000;
                numericUpDown1.Value = ChosenPart.Price;
                numericUpDown1.Enabled = false;

                numericUpDown2.Value = ChosenPart.Warranty;
                comboBox2.SelectedItem = (ChosenPart as RAM).Gen;
                numericUpDown3.Value = (ChosenPart as RAM).Size;
                numericUpDown4.Value = (ChosenPart as RAM).Clockspeed;
                numericUpDown5.Value = (ChosenPart as RAM).Timing;
            }
        }

        private void UI_Motherboard()
        {
            comboBox1.DataSource = Enum.GetValues(typeof(MohterboardManufacturer));

            str1Lbl.Text = Fields[6];
            comboBox2.DataSource = Enum.GetValues(typeof(ProcessorSocket));
            str1Mertekegyseglbl.Visible = false;

            str2Lbl.Text = Fields[7];
            str2MertekegysegLbl.Visible = false;

            int1lbl.Visible = false;
            int1Mertekegyseglbl.Visible=false;
            numericUpDown3.Visible = false;

            int2Lbl.Visible = false;
            int2MertekegysegLbl.Visible = false;
            numericUpDown4.Visible = false; 

            int3Lbl.Visible = false;
            int3Mertekegyseglbl.Visible = false;
            numericUpDown5.Visible = false;

            checkBox1.Text = Fields[8];
            checkBox1.Location = new Point(15, 208);

            if (ChosenPart != null) // modify
            {
                comboBox1.SelectedItem = (MohterboardManufacturer)Enum.Parse(typeof(MohterboardManufacturer), ChosenPart.Manufacturer);
                textBox2.Text = ChosenPart.Type;
                numericUpDown1.Value = ChosenPart.Price;
                numericUpDown2.Value = ChosenPart.Warranty;
                comboBox2.SelectedItem = (ChosenPart as Motherboard).Procsocket;
                comboBox3.SelectedItem = (ChosenPart as Motherboard).Chipset;
                checkBox1.Checked = (ChosenPart as Motherboard).Illuminated;
            }

        }

        private void UI_Processor()
        {
            comboBox1.DataSource = Enum.GetValues(typeof(ProcessorManufacturer));

            label3.Text = "Price is generated";
            numericUpDown1.Visible = false;
            label4.Visible = false;

            str1Lbl.Text = Fields[6];
            str1Mertekegyseglbl.Visible = false;


            str2Lbl.Visible = false;
            str2MertekegysegLbl.Visible = false;
            comboBox3.Visible = false;

            int1lbl.Text = Fields[7];
            int1Mertekegyseglbl.Text = "mHz";
            numericUpDown3.Minimum = 1000;
            numericUpDown3.Maximum = 6000;
            numericUpDown3.Value = 1000;

            int2Lbl.Text = Fields[8];
            int2MertekegysegLbl.Text = "Mb";
            int2Lbl.Location = new Point(397, 190);
            numericUpDown4.Minimum = 2;
            numericUpDown4.Maximum = 256;
            numericUpDown4.Value = 2;

            int3Lbl.Text = Fields[9];
            int3Mertekegyseglbl.Text = "pcs";
            numericUpDown5.Minimum = 1;
            numericUpDown5.Maximum = 32;
            numericUpDown5.Value = 1;

            checkBox1.Visible = false;

            if (ChosenPart != null) // modify
            {
                comboBox1.SelectedItem = (ProcessorManufacturer)Enum.Parse(typeof(ProcessorManufacturer), ChosenPart.Manufacturer);
                textBox2.Text = ChosenPart.Type;
                label3.Text = "Price is generated";
                numericUpDown1.Visible = true;
                numericUpDown1.Minimum = 1;
                numericUpDown1.Maximum = 10000000;
                label4.Visible = true;
                numericUpDown1.Value = ChosenPart.Price;
                numericUpDown1.Enabled = false;

                numericUpDown2.Value = ChosenPart.Warranty;
                comboBox2.SelectedItem = (ChosenPart as Processor).Package;
                numericUpDown3.Value = (ChosenPart as Processor).Clockspeed;
                numericUpDown4.Value = (ChosenPart as Processor).L3size;
                numericUpDown5.Value = (ChosenPart as Processor).Cores;
            } 
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chosen == Types.Processor)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        comboBox2.Items.Clear();
                        comboBox2.Items.Add(Packaging.AM4);
                        comboBox2.Items.Add(Packaging.AM5);
                        break;
                    case 1:
                        comboBox2.Items.Clear();
                        comboBox2.Items.Add(Packaging.LGA1200);
                        comboBox2.Items.Add(Packaging.LGA1700);                       
                        break;
                }
                comboBox2.SelectedIndex = 0;
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ChosenPart == null) // create
            {
                try
                {
                    if (chosen == Types.Processor)
                    {
                        MySQLDatahandler.CreateProduct(new Processor(string.Empty,comboBox1.SelectedItem.ToString(), textBox2.Text, (int)numericUpDown2.Value,(Packaging)comboBox2.SelectedItem, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value));
                    }
                    else if (chosen == Types.Motherboard)
                    {
                        MySQLDatahandler.CreateProduct(new Motherboard(string.Empty, comboBox1.SelectedItem.ToString(), textBox2.Text, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (ProcessorSocket)comboBox2.SelectedItem,(Chipset)comboBox3.SelectedItem, checkBox1.Checked)); ;
                    }
                    else if (chosen == Types.RAM)
                    {
                        MySQLDatahandler.CreateProduct(new RAM(string.Empty, comboBox1.SelectedItem.ToString(), textBox2.Text,  (int)numericUpDown2.Value, (Generation)comboBox2.SelectedItem, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value)); ;
                    }
                    else if (chosen == Types.GPU)
                    {
                        MySQLDatahandler.CreateProduct(new GPU(string.Empty, comboBox1.SelectedItem.ToString(), textBox2.Text, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (RamType)comboBox2.SelectedItem, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, checkBox1.Checked));
                    }
                    else if (chosen == Types.PSU)
                    {
                        MySQLDatahandler.CreateProduct(new PSU(string.Empty, comboBox1.SelectedItem.ToString(), textBox2.Text, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (QualityType)comboBox2.SelectedItem, (int)numericUpDown3.Value));
                    }
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else // modify
            {
                try
                {
                    string id = ChosenPart.Id.ToString();
                    if (chosen == Types.Processor)
                    {   // new PArt need to be called to make sure the price updated according to the generation                     
                        ChosenPart = new Processor(id, comboBox1.SelectedItem.ToString(), textBox2.Text, (int)numericUpDown2.Value, (Packaging)comboBox2.SelectedItem, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value);
                        MySQLDatahandler.UpdateProduct(ChosenPart);
                    }
                    else if (chosen == Types.Motherboard)
                    {
                        ChosenPart.Manufacturer = comboBox1.SelectedItem.ToString();
                        ChosenPart.Type = textBox2.Text;
                        ChosenPart.Price = (int)numericUpDown1.Value;
                        ChosenPart.Warranty = (int)numericUpDown2.Value;
                        (ChosenPart as Motherboard).Procsocket = (ProcessorSocket)comboBox2.SelectedItem;
                        (ChosenPart as Motherboard).Chipset = (Chipset)comboBox3.SelectedItem;
                        (ChosenPart as Motherboard).Illuminated = checkBox1.Checked;
                        MySQLDatahandler.UpdateProduct(ChosenPart);
                    }
                    else if (chosen == Types.RAM)
                    {
                        ChosenPart = new RAM(id, comboBox1.SelectedItem.ToString(), textBox2.Text, (int)numericUpDown2.Value, (Generation)comboBox2.SelectedItem, (int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value);
                        MySQLDatahandler.UpdateProduct(ChosenPart);
                    }
                    else if (chosen == Types.GPU)
                    {
                        ChosenPart.Manufacturer = comboBox1.SelectedItem.ToString();
                        ChosenPart.Type = textBox2.Text;
                        ChosenPart.Price = (int)numericUpDown1.Value;
                        ChosenPart.Warranty = (int)numericUpDown2.Value;
                        (ChosenPart as GPU).Ram = (RamType)comboBox2.SelectedItem;
                        (ChosenPart as GPU).Size = (int)numericUpDown3.Value;
                        (ChosenPart as GPU).Coreclockspeed = (int)numericUpDown4.Value;
                        (ChosenPart as GPU).Powerconsumption = (int)numericUpDown5.Value;
                        (ChosenPart as GPU).Raytracing = checkBox1.Checked;
                        MySQLDatahandler.UpdateProduct(ChosenPart);
                    }
                    else if (chosen == Types.PSU)
                    {
                        ChosenPart.Manufacturer = comboBox1.SelectedItem.ToString();
                        ChosenPart.Type = textBox2.Text;
                        ChosenPart.Price = (int)numericUpDown1.Value;
                        ChosenPart.Warranty = (int)numericUpDown2.Value;
                        (ChosenPart as PSU).Quality = (QualityType)comboBox2.SelectedItem;
                        (ChosenPart as PSU).Poweroutput = (int)numericUpDown3.Value;
                        MySQLDatahandler.UpdateProduct(ChosenPart);
                    }
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chosen == Types.Motherboard)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        comboBox3.Items.Clear();
                        comboBox3.Items.Add(Chipset.H510);
                        comboBox3.Items.Add(Chipset.H610);
                        break;
                    case 1:
                        comboBox3.Items.Clear();
                        comboBox3 .Items.Add(Chipset.B660);
                        comboBox3.Items.Add(Chipset.Z690);
                        comboBox3.Items.Add(Chipset.B760);
                        comboBox3.Items.Add(Chipset.Z790);
                        break;
                    case 2:
                        comboBox3.Items.Clear();
                        comboBox3.Items.Add(Chipset.B450);
                        comboBox3.Items.Add(Chipset.B550);
                        comboBox3.Items.Add(Chipset.X470);
                        comboBox3.Items.Add(Chipset.X570);
                        break;
                    case 3:
                        comboBox3.Items.Clear();
                        comboBox3.Items.Add(Chipset.B650);
                        comboBox3.Items.Add(Chipset.X670);
                        break;
                }
                comboBox3.SelectedIndex = 0;
            } else if (chosen == Types.RAM)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        numericUpDown4.Minimum = 3000;
                        numericUpDown4.Value = 3000;
                        numericUpDown4.Maximum = 4400;
                        break;
                    case 1:
                        numericUpDown4.Minimum = 5200;
                        numericUpDown4.Value = 5200;
                        numericUpDown4.Maximum = 8000;
                        break;
                }
            }


        }
    }
}
