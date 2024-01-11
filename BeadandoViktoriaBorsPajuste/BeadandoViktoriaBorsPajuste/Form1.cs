using BeadandoViktoriaBorsPajuste.Classes;
using BeadandoViktoriaBorsPajuste.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeadandoViktoriaBorsPajuste
{
    public enum Types
    {
        Processor,
        Motherboard,
        RAM,
        GPU,
        PSU,
        X
    }

    public enum StorageTypes
    {
        mainStorage,
        secondaryStorage,
        exportStorage,
        extraStorage
    }
    public partial class Form1 : Form
    {
        List<Parts> partList;

        int chosenPartCardIndex = -1;
        public Form1()
        {
            InitializeComponent();

            partList = MySQLDatahandler.GetAllParts();

            if (partList.Count == 0)
            {
                label8.Text = "There are no computer parts yet";
                button2.Visible = false;
            }
            else
            {
                label8.Visible = false;
                Panel_Update();
            }

            /* These function were for the local setup - now we connecting to the same db
            FunctionResult result = MySQLDatahandler.DatabaseExist(); // check database exists or not
            if (result.Fresult == FunctionResultType.fatal ) // if there was a problem with database check
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0); // no clean up... :S
               
            } else
            {
                if (!result.Result) // database doesnt exists yet
                {
                    this.Hide();
                    DatabaseCreationForm ablak = new DatabaseCreationForm();
                    if (ablak.ShowDialog() == DialogResult.OK)
                    {
                        this.Show();
                    }
                }

                    partList = MySQLDatahandler.GetAllParts();

                    if (partList.Count == 0)
                    {
                        label8.Text = "There are no computer parts yet";
                        button2.Visible = false;
                    }
                    else
                    {
                        label8.Visible = false;
                        Panel_Update();
                    }
              
            }        
            */
        }

        private void Panel_Update()
        {
            flowLayoutPanel1.Controls.Clear();

            partList = MySQLDatahandler.GetAllParts();

            for (int i = 0; i < partList.Count; i++)
            {
                PartControl partCard = new PartControl(partList[i],i );
                partCard.checkBox1.CheckedChanged += Unchosen_Event;
                flowLayoutPanel1.Controls.Add(partCard);
            }
        }

        private void Unchosen_Event(object sender, EventArgs e)
        {

            if (chosenPartCardIndex > -1)
            {
                (flowLayoutPanel1.Controls[chosenPartCardIndex] as PartControl).UnChoosePart();
            }
            chosenPartCardIndex = ((sender as CheckBox).Parent as PartControl).Index;

        }

        private void pictureBox1_label_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox1);
            DesignStaticClass.Lbl_Enter(label2);
        }

        private void pictureBox1_label_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox1);
            DesignStaticClass.Lbl_Leave(label2);
        }

        private void pictureBox2_label_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox2);
            DesignStaticClass.Lbl_Enter(label3);
        }

        private void pictureBox2_label_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox2);
            DesignStaticClass.Lbl_Leave(label3);
        }

        private void pictureBox3_label_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox3);
            DesignStaticClass.Lbl_Enter(label4);
        }

        private void pictureBox3_label_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox3);
            DesignStaticClass.Lbl_Leave(label4);
        }

        private void pictureBox4_label_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox4);
            DesignStaticClass.Lbl_Enter(label5);
        }

        private void pictureBox4_label_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox4);
            DesignStaticClass.Lbl_Leave(label5);
        }

        private void pictureBox5_Lbl_MouseEnter(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Enter(pictureBox5);
            DesignStaticClass.Lbl_Enter(label6);
        }

        private void pictureBox5_Lbl_MouseLeave(object sender, EventArgs e)
        {
            DesignStaticClass.PB_Leave(pictureBox5);
            DesignStaticClass.Lbl_Leave(label6);
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            Types chosen = Types.X;
            ChoosingForm window = new ChoosingForm();

            if (window.ShowDialog() == DialogResult.OK)
            {
                chosen = window.Type;
            }

            if (chosen != Types.X)
            {
                PartsForm form = new PartsForm(chosen);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Panel_Update();
                    button2.Visible = true;
                }
                else
                {
                    MessageBox.Show("New part was not created!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (chosenPartCardIndex > -1)
            {
                PartsForm form = new PartsForm(partList[chosenPartCardIndex]);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Panel_Update();
                }
                else
                {
                    MessageBox.Show("Part was not modified!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {
                MessageBox.Show("No items were selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StorageForm form = new StorageForm(StorageTypes.mainStorage);
            form.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StorageForm form = new StorageForm(StorageTypes.secondaryStorage);
            form.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            StorageForm form = new StorageForm(StorageTypes.exportStorage);
            form.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            StorageForm form = new StorageForm(StorageTypes.extraStorage);
            form.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FunctionResult exportResult = StaticClass.ImportPartToDatabase();

            switch (exportResult.Fresult)
            {
                case FunctionResultType.ok:
                    MessageBox.Show(exportResult.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Panel_Update();
                    break;
                case FunctionResultType.error:
                    MessageBox.Show(exportResult.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Panel_Update();
                    break;
                case FunctionResultType.fatal:
                    MessageBox.Show(exportResult.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
