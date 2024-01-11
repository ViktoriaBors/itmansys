using BeadandoViktoriaBorsPajuste.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BeadandoViktoriaBorsPajuste.Classes.StaticClass;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace BeadandoViktoriaBorsPajuste.Forms
{
    public partial class PartPiecesInStorageForm : Form
    {
        
        List<Parts> partlist;
        int beforeChangePieces;
        int afterChangePieces;

        ModificationType Modification = ModificationType.add;
        public StorageTypes ChosenStorage { get; private set; }
        public Parts ChosenPart { get; private set; }
        public PartPiecesInStorageForm(StorageTypes chosenStorage, ModificationType modification, string prodId) // adding 
        {
            InitializeComponent();
            this.Text = StaticClass.StorageNames(chosenStorage);
            Modification = modification;
            ChosenStorage = chosenStorage;
            partlist = MySQLDatahandler.GetAllParts();

            if (partlist.Count == 0)
            {
                comboBox1.Visible = false;
                label1.Text = "There is no computer parts. Please create/import some.";
                label1.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                button1.Enabled = false;
            } else
            {
                comboBox1.Visible = true;
                comboBox1.DataSource = partlist; 
                comboBox1.SelectedItem = prodId != string.Empty ? partlist.Find(p => p.Id == prodId) : partlist[0];
            }

            button1.Text = "Add to " + this.Text;
        }

        public PartPiecesInStorageForm(StorageTypes chosenStorage, ModificationType modification, Parts chosenPart) // deleting 
        {
            InitializeComponent();
            this.Text = StaticClass.StorageNames(chosenStorage);
            Modification = modification;
            ChosenStorage = chosenStorage;
            ChosenPart = chosenPart;

            comboBox1.Items.Add(chosenPart);
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = false;

            button1.Text = "Delete from " + this.Text;

            beforeChangePieces = MySQLDatahandler.NumberPartInGivenStorage(ChosenPart.Id, ChosenStorage);
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = beforeChangePieces;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                Parts chosen = comboBox1.SelectedItem as Parts;

                try
                {
                    MySQLDatahandler.UpdatePartPiecesInGivenStorage(ChosenStorage, chosen.Id, afterChangePieces, beforeChangePieces);
                    StaticClass.Logging(StaticClass.StorageNames(ChosenStorage), chosen.ToString(), Modification, Math.Abs(beforeChangePieces - afterChangePieces));
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            else
            {
                MessageBox.Show("There is no chosen items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                Parts chosen = comboBox1.SelectedItem as Parts;
                beforeChangePieces = MySQLDatahandler.NumberPartInGivenStorage(chosen.Id, ChosenStorage);
                numericUpDown1.Value = 1 ;

                afterChangePieces = Modification == ModificationType.add ? beforeChangePieces + (int)numericUpDown1.Value : beforeChangePieces - (int)numericUpDown1.Value;

                resultLbl.Text = "Pieces now: " + beforeChangePieces;
                result2lbl.Text = "Pieces after change: " + afterChangePieces;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            afterChangePieces = Modification == ModificationType.add ? beforeChangePieces + (int)(sender as NumericUpDown).Value : beforeChangePieces - (int)(sender as NumericUpDown).Value;
            result2lbl.Text = "Pieces after change: " + afterChangePieces;            
        }
    }
}
