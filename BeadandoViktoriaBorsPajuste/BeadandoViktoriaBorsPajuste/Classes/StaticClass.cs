using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    public  class StaticClass
    // enums, parameters, and functions crucial for widespread application use.
    {
        public enum ModificationType // when adding or deleting from storage
        {
            add,
            delete
        }
        public enum PreProductID // during creating a product, this id inserted, amd then updated with the customized id
        {
            Proc,
            MotherB,
            Mem,
            Video,
            PSU
        }

       private static string logfile = "log.txt";

        public static  string StorageNames(StorageTypes chosenStorage)
        {
            string name = string.Empty;
            switch (chosenStorage)
            {
                case StorageTypes.mainStorage:
                    name = "Main Storage";
                    break;
                case StorageTypes.secondaryStorage:
                    name = "Secondary Storage";
                    break;
                case StorageTypes.exportStorage:
                    name = "Export Storage";
                    break;
                case StorageTypes.extraStorage:
                    name = "Extra Storage";
                    break;
            }
            return name;
        }

        public static Types TypeCheck(Parts ChosenPart)
        {
            Types chosen = Types.X;

            if (ChosenPart is Processor)
            {
                chosen = Types.Processor;
            }
            else if (ChosenPart is Motherboard)
            {
                chosen = Types.Motherboard;
            }
            else if (ChosenPart is RAM)
            {
                chosen = Types.RAM;
            }
            else if (ChosenPart is GPU)
            {
                chosen = Types.GPU;
            }
            else if (ChosenPart is PSU)
            {
                chosen = Types.PSU;
            }
            return chosen;
        }

        public static int TypeToDescriptionId(Parts chosenPart)
        {
            int id = 0;
            if (chosenPart is Processor) 
            {
                id = 1;
            } else if (chosenPart is Motherboard)
            {
                id = 2;
            }
            else if (chosenPart is RAM)
            {
                id = 3;
            }
            else if (chosenPart is GPU)
            {
                id = 4;
            }
            else if (chosenPart is PSU)
            {
                id = 5;
            }
            return id;
        }

        public static bool FromIntToBool(int value)
        {
            return value > 0 ? true : false;
        }

        public static int FromBoolToInt(bool value)
        {
            return value == true ? 1 : 0;
        }
        public static void Logging(string storage,string part, ModificationType modtype, int piece)
        {
            StreamWriter wr = new StreamWriter(logfile, true, Encoding.UTF8);
            wr.WriteLine($"{DateTime.Now}: {storage} - {modtype} - {piece} - {part}");
            wr.Close();
        }

        public static FunctionResult ExportPartToCSV(Parts chosenpart)
        {
            FunctionResult result = new FunctionResult();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Part as CSV File";
            saveFileDialog.FileName = "part.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                string csvString = TypeToDescriptionId(chosenpart) + ";" + chosenpart.Id + ";" + chosenpart.Manufacturer + ";" + chosenpart.Type + ";" + chosenpart.Price + ";" + chosenpart.Warranty + ";";
                try
                {
                    StreamWriter wr = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                    if (chosenpart is Processor proc)
                    {
                        csvString += proc.Package + ";" + proc.Clockspeed + ";" + proc.L3size + ";" + proc.Cores;
                    } else if (chosenpart is Motherboard mb)
                    {
                        csvString += mb.Procsocket + ";" + mb.Chipset + ";" + mb.Illuminated;
                    }
                    else if (chosenpart is RAM ram)
                    {
                        csvString += ram.Gen + ";" + ram.Size + ";" + ram.Clockspeed + ";" + ram.Timing;
                    }
                    else if (chosenpart is GPU gpu)
                    {
                        csvString += gpu.Ram + ";" + gpu.Size + ";" + gpu.Coreclockspeed + ";" + gpu.Powerconsumption + ";" + gpu.Raytracing;
                    }
                    else if (chosenpart is PSU psu )
                    {
                        csvString += psu.Quality + ";" + psu.Poweroutput;
                    }

                    wr.WriteLine(csvString);
                    wr.Close();

                    result.Result = true;
                    result.Fresult = FunctionResultType.ok;
                    result.Message = "Part was saved to CSV file sucessfully.";
                }
                catch (Exception ex)
                {
                    result.Result = false;
                    result.Fresult = FunctionResultType.fatal;
                    result.Message = "Could not save CSV file." + Environment.NewLine + ex.Message;
                }
            } else
            {
                result.Result = false;
                result.Fresult = FunctionResultType.error;
                result.Message = "Warning: Exporting to CSV file was cancelled.";
            }
            return result;
        }

        internal static FunctionResult ImportPartToDatabase()
        {
            FunctionResult result = new FunctionResult();
            bool okToContinue = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Title = "Export CSV File to database";
            openFileDialog.FileName = "part.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog.FileName);
                    string part = sr.ReadLine();

                    string[] values = part.Split(';');
                    sr.Close();

                    // check if there is any product id like this before insert to it 
                    FunctionResult partAlredyExist = MySQLDatahandler.PartExitsInValueTable(values[1]);

                    if (!partAlredyExist.Result && partAlredyExist.Fresult == FunctionResultType.fatal) // there was a db problem
                    {
                        MessageBox.Show(partAlredyExist.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        okToContinue = false;
                    }
                    else if (partAlredyExist.Result && partAlredyExist.Fresult == FunctionResultType.ok) // it there is already a same podId part
                    {
                        if (MessageBox.Show(partAlredyExist.Message + Environment.NewLine + "Would you like to save it with the same product id to database anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            okToContinue = false;
                        }
                    }

                    if (okToContinue)
                    {
                        try
                        {
                            switch (Convert.ToInt32(values[0]))
                            {
                                case 1:
                                    MySQLDatahandler.CreateProduct(new Processor(values[1], values[2], values[3], Convert.ToInt32(values[5]), (Packaging)Enum.Parse(typeof(Packaging), values[6].ToString()), Convert.ToInt32(values[7]), Convert.ToInt32(values[8]), Convert.ToInt32(values[9])));
                                    break;
                                case 2:
                                    MySQLDatahandler.CreateProduct(new Motherboard(values[1], values[2], values[3], Convert.ToInt32(values[4]),Convert.ToInt32(values[5]), (ProcessorSocket)Enum.Parse(typeof(ProcessorSocket), values[6].ToString()), (Chipset)Enum.Parse(typeof(Chipset), values[7].ToString()), values[8] == "TRUE" ? true : false));
                                    break;
                                case 3:
                                    MySQLDatahandler.CreateProduct(new RAM(values[1], values[2], values[3], Convert.ToInt32(values[5]), (Generation)Enum.Parse(typeof(Generation), values[6].ToString()), Convert.ToInt32(values[7]), Convert.ToInt32(values[8]), Convert.ToInt32(values[9])));
                                    break;
                                case 4:
                                    MySQLDatahandler.CreateProduct(new GPU(values[1], values[2], values[3],Convert.ToInt32(values[4]), Convert.ToInt32(values[5]), (RamType)Enum.Parse(typeof(RamType), values[6].ToString()), Convert.ToInt32(values[7]), Convert.ToInt32(values[8]), Convert.ToInt32(values[9]), values[10] == "TRUE" ? true : false));
                                    break;
                                case 5:
                                    MySQLDatahandler.CreateProduct(new PSU(values[1], values[2], values[3], Convert.ToInt32(values[4]), Convert.ToInt32(values[5]), (QualityType)Enum.Parse(typeof(QualityType), values[6].ToString()), Convert.ToInt32(values[7])));
                                    break;
                            }
                            result.Result = true;
                            result.Fresult = FunctionResultType.ok;
                            result.Message = "Part was sucessfully inserted to database.";
                        }
                        catch (Exception ex)
                        {
                            result.Result = false;
                            result.Fresult = FunctionResultType.fatal;
                            result.Message = "Could not save CSV file." + Environment.NewLine + ex.Message;
                        }
                    } else
                    {
                        result.Result = false;
                        result.Fresult = FunctionResultType.error;
                        result.Message = "Could not save CSV file.";
                    }
                }
                catch (Exception ex)
                {
                    result.Result = false;
                    result.Fresult = FunctionResultType.fatal;
                    result.Message = "Could not save CSV file." + Environment.NewLine + ex.Message;
                }
            }
            else
            {
                result.Result = false;
                result.Fresult = FunctionResultType.error;
                result.Message = "Warning: Importing CSV File to database was cancelled.";
            }
            return result;
        }
    }
}
