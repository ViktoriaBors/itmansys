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

namespace BeadandoViktoriaBorsPajuste
{
    public partial class DatabaseCreationForm : Form
    {
        public DatabaseCreationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errorMessages = string.Empty;
           // FunctionResult databaseCreated = MySQLDatahandler.DatabaseCreation();
           // errorMessages = !databaseCreated.Result ? databaseCreated.Message + Environment.NewLine : string.Empty;
            FunctionResult storagesCreated = MySQLDatahandler.StorageTablesCreation();
            errorMessages += !storagesCreated.Result ? storagesCreated.Message + Environment.NewLine : string.Empty;
            FunctionResult PartsCreated = MySQLDatahandler.PartsTablesCreation();
            errorMessages += !PartsCreated.Result ? PartsCreated.Message + Environment.NewLine : string.Empty;

            if (errorMessages.Length == 0)
            {
                DialogResult = DialogResult.OK;
            } else
            {
                MessageBox.Show(errorMessages, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
