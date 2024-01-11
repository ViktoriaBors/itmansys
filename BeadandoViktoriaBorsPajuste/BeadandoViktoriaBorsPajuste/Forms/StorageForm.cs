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
    public partial class StorageForm : Form
    {
        public StorageTypes chosenStorage { get; private set; }

        List<Parts> partlist;
        public StorageForm(StorageTypes chosenStorage)
        {
            InitializeComponent();
            this.chosenStorage = chosenStorage;
            this.Text = StaticClass.StorageNames(chosenStorage);

            partlist = MySQLDatahandler.AllPartsinGivenStorage(chosenStorage);
            if (partlist.Count == 0)
            {
                label1.Text ="The " + this.Text + " does not consist any parts yet.";
            } else
            {
                label1.Text = "The " + this.Text + " consist of the following parts:";
                DataGridView_Update(partlist);
            }
            dataGridView1.Focus();
        }

        private void DataGridView_Update(List<Parts> list)
        {
            dataGridView1.Rows.Clear();
            foreach(Parts part in list) // update coloumn/rows manually
            {
                int pieces = MySQLDatahandler.NumberPartInGivenStorage(part.Id, chosenStorage);

                object[] rowData = { part.Id, part.Manufacturer, part.Type, part.Price, pieces };
                dataGridView1.Rows.Add(rowData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string prodId = dataGridView1.SelectedCells.Count > 0 ?  partlist[dataGridView1.SelectedCells[0].RowIndex].Id : string.Empty;

            PartPiecesInStorageForm form = new PartPiecesInStorageForm(chosenStorage, ModificationType.add, prodId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                partlist = MySQLDatahandler.AllPartsinGivenStorage(chosenStorage);
                DataGridView_Update(partlist);
            }           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex > -1)
            {
                PartPiecesInStorageForm form = new PartPiecesInStorageForm(chosenStorage, ModificationType.delete, partlist[dataGridView1.SelectedCells[0].RowIndex]);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Sucessfull update", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    partlist = MySQLDatahandler.AllPartsinGivenStorage(chosenStorage);
                    DataGridView_Update(partlist);
                }
            } else
            {
                MessageBox.Show("No items were selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double sumOfBruttoPrices = partlist.Sum(part => part.BruttoPrice());
            int sumOfPrices = partlist.Sum(part => part.Price);

            MessageBox.Show("The whole value in the " + this.Text + ": " + sumOfPrices + " HUF (" + sumOfBruttoPrices + " HUF with taxes)", "Storage Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                string searchText = (sender as TextBox).Text.ToLower();
                List<Parts> newPartList = partlist.Where(part => part.Id.ToLower().Contains(searchText.ToLower()) || part.Type.ToLower().Contains(searchText.ToLower())).ToList();
                DataGridView_Update(newPartList);
            } else
            {
                DataGridView_Update(partlist);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Parts> newPartList = partlist.Where(part => part.Price >= numericUpDown1.Value && part.Price <= numericUpDown2.Value).ToList();
            DataGridView_Update(newPartList);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            DataGridView_Update(partlist);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = numericUpDown1.Value + 1;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                PartDetailForm form = new PartDetailForm(partlist[dataGridView1.SelectedCells[0].RowIndex]);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("No items were selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
