using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.CRUD;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    public enum StorageNames
    {
       mainStorage,
       secondaryStorage,
       exportStorage,
       extraStorage
    }
    internal class MySQLDatahandler
    // database conenction and interaction
    {
        static MySqlConnection mainConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

        static MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);       

        public static FunctionResult DatabaseExist()
        {
            FunctionResult fcresult = new FunctionResult();
            try
            {
                mainConnection.Open();
                
                using (MySqlCommand cmd = new MySqlCommand("SHOW DATABASES LIKE @dbname", mainConnection))
                {
                    cmd.Parameters.AddWithValue("@dbname", "itmansys");
                    fcresult.Result = cmd.ExecuteScalar() != null;
                    fcresult.Fresult = FunctionResultType.ok;
                }
            }
            catch (Exception ex)
            {
                fcresult.Message = ex.Message;
                fcresult.Result = false;
                fcresult.Fresult = FunctionResultType.fatal;

            } finally 
            {
                mainConnection.Close();
            }  
            return fcresult; 
        }

        public static FunctionResult DatabaseCreation()
        {
            FunctionResult result = new FunctionResult(); 
            result.Fresult = FunctionResultType.ok;
            result.Result = true;
            result.Message = string.Empty;

            try
            {
                mainConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS " + "storagemanagerdb", mainConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = false;
                result.Fresult = FunctionResultType.fatal;
            } finally 
            {
                mainConnection.Close();
            }
            return result;

        }

        public static FunctionResult StorageTablesCreation()
        {
            FunctionResult result = new FunctionResult();
            result.Fresult = FunctionResultType.ok;
            result.Result = true;
            result.Message = string.Empty;

            try
            {
                connection.Open();
                foreach (StorageNames storageNames in Enum.GetValues(typeof(StorageNames)))
                {
                    using (MySqlCommand cmd = new MySqlCommand($"CREATE TABLE {storageNames} ( " +
                         "productID VARCHAR(50) NOT NULL, " +
                         "pieces INT(11) NOT NULL, " +
                         "dateofmod DATETIME NOT NULL, " +
                         "PRIMARY KEY (productID)) ENGINE=InnoDB", connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = false;
                result.Fresult = FunctionResultType.fatal;
            } finally { connection.Close(); }

            return result;
        }

        public static FunctionResult PartsTablesCreation()
        {
            FunctionResult result = new FunctionResult();
            result.Fresult = FunctionResultType.ok;
            result.Result = true;
            result.Message = string.Empty;

            try
            {
                connection.Open();

                // value table - parts values
                using (MySqlCommand cmd = new MySqlCommand(
                    "CREATE TABLE `valuetable` (" +
                    "   `ID` INT(11) NOT NULL AUTO_INCREMENT, " +
                    "   `descriptionID`  INT(11) NOT NULL," +
                    "   `productID` VARCHAR(50) NOT NULL," +
                    "   `basestr1` VARCHAR(50) NOT NULL," +
                    "   `basestr2` VARCHAR(50) NOT NULL," +
                    "   `baseint1` INT NOT NULL DEFAULT 0," +
                    "   `baseint2` INT NOT NULL DEFAULT 0," +
                    "   `str1` VARCHAR(50) NULL DEFAULT NULL," +
                    "   `str2` VARCHAR(50) NULL DEFAULT NULL," +
                    "   `int1` INT NULL DEFAULT NULL," +
                    "   `int2` INT NULL DEFAULT NULL," +
                    "   `int3` INT NULL DEFAULT NULL," +
                    "   `bool1` INT(1) NULL DEFAULT NULL, " +
                    "   PRIMARY KEY (ID)" +
                    ") COLLATE utf8mb4_general_ci;", connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // description table - parts propertie names
                using (MySqlCommand cmd = new MySqlCommand(
                    "CREATE TABLE `descriptiontable` (" +
                    "   `ID` INT(11) NOT NULL AUTO_INCREMENT, " +
                    "   `parttype` VARCHAR(50) NOT NULL," +
                    "   `basestr1` VARCHAR(50) NOT NULL," +
                    "   `basestr2` VARCHAR(50) NOT NULL," +
                    "   `baseint1` VARCHAR(50) NOT NULL," +
                    "   `baseint2` VARCHAR(50) NOT NULL," +
                    "   `str1` VARCHAR(50) NULL DEFAULT NULL," +
                    "   `str2` VARCHAR(50) NULL DEFAULT NULL," +
                    "   `int1` VARCHAR(50) NULL DEFAULT NULL," +
                    "   `int2` VARCHAR(50) NULL DEFAULT NULL," +
                    "   `int3` VARCHAR(50) NULL DEFAULT NULL," +
                    "   `bool1` VARCHAR(50) NULL DEFAULT NULL," +
                    "   PRIMARY KEY (ID)" +
                    ") COLLATE utf8mb4_general_ci;", connection))
                {
                    cmd.ExecuteNonQuery();
                }



                // description table upload
                using (MySqlCommand cmd = new MySqlCommand(
                         "INSERT INTO `descriptiontable` (`parttype`, `basestr1`, `basestr2`, `baseint1`, `baseint2`, `str1`, `str2`, `int1`, `int2`, `int3`, `bool1`) " +
                         "VALUES " +
                         "('Processor', 'Manufecturer', 'Type', 'Price', 'Warranty', 'Packaging', NULL, 'Clock speed', 'L3 cache size', 'Number of Cores', NULL), " +
                         "('Motherboard','Manufecturer', 'Type', 'Price', 'Warranty', 'Processor socket', 'Chipset', NULL, NULL, NULL, 'Illuminated'), " +
                         "('RAM', 'Manufecturer', 'Type', 'Price', 'Warranty', 'Generation', NULL, 'Size', 'Clock speed', 'Timing', NULL), " +
                         "('GPU', 'Manufecturer', 'Type', 'Price', 'Warranty', 'Ram', NULL, 'Size', 'Core clock speed', 'Consumption', 'Raytracing'), " +
                         "('PSU','Manufecturer', 'Type', 'Price', 'Warranty', 'Quality', NULL, 'Power output', NULL, NULL, NULL);", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = false;
                result.Fresult = FunctionResultType.fatal;
            } finally { connection.Close(); }

            return result;
        }

        public static List<string> PropertyNames(string type) 
        {
            List<string> propertyNames = new List<string>();
            try
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM `descriptiontable` WHERE parttype=@type", connection))
                {
                    cmd.Parameters.AddWithValue("@type", type);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!reader.IsDBNull(i))
                            {
                                propertyNames.Add(reader[i].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during reading from database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally { connection.Close(); }

            return propertyNames;
        }


        public static void CreateProduct(Parts newP)
        {
            connection.Open();
            string sql = "INSERT INTO `valuetable` ";
            bool sucessfull = false;
            string prodId = string.Empty;
            int descriptionId = StaticClass.TypeToDescriptionId(newP);

            if (newP is Processor)
            {
                sql += "(`id`, `descriptionID`, `productId`, `basestr1`, `basestr2`, `baseint1`, `baseint2`, `str1`, `int1`, `int2`, `int3`) " +
                        "VALUES (@id, @descriptionID, @productId, @basestr1, @basestr2, @baseint1, @baseint2, @str1, @int1, @int2, @int3)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", null);
                    cmd.Parameters.AddWithValue("@descriptionID", descriptionId);
                    cmd.Parameters.AddWithValue("@productId", StaticClass.PreProductID.Proc.ToString());
                    cmd.Parameters.AddWithValue("@basestr1", newP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", newP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", newP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", newP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (newP as Processor).Package);
                    cmd.Parameters.AddWithValue("@int1", (newP as Processor).Clockspeed);
                    cmd.Parameters.AddWithValue("@int2", (newP as Processor).L3size);
                    cmd.Parameters.AddWithValue("@int3", (newP as Processor).Cores);

                    sucessfull = cmd.ExecuteNonQuery() == 1 ? true : false;
                    prodId = StaticClass.PreProductID.Proc.ToString();
                }
            } else if (newP is Motherboard)
            {
                sql += "(`id`, `descriptionID`, `productId`, `basestr1`, `basestr2`, `baseint1`, `baseint2`, `str1`, `str2`, `bool1`) " +
                      "VALUES (@id, @descriptionID, @productId, @basestr1, @basestr2, @baseint1, @baseint2, @str1, @str2, @bool1)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", null);
                    cmd.Parameters.AddWithValue("@descriptionID", descriptionId);
                    cmd.Parameters.AddWithValue("@productId", StaticClass.PreProductID.MotherB.ToString());
                    cmd.Parameters.AddWithValue("@basestr1", newP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", newP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", newP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", newP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (newP as Motherboard).Procsocket);
                    cmd.Parameters.AddWithValue("@str2", (newP as Motherboard).Chipset);
                    cmd.Parameters.AddWithValue("@bool1", StaticClass.FromBoolToInt((newP as Motherboard).Illuminated));

                    sucessfull = cmd.ExecuteNonQuery() == 1 ? true : false;
                    prodId = StaticClass.PreProductID.MotherB.ToString();
                }
            } else  if (newP is RAM)
            {
                sql += "(`id`, `descriptionID`, `productId`, `basestr1`, `basestr2`, `baseint1`, `baseint2`, `str1`, `int1`, `int2`, `int3`) " +
                      "VALUES (@id, @descriptionID, @productId, @basestr1, @basestr2, @baseint1, @baseint2, @str1, @int1,@int2, @int3)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", null);
                    cmd.Parameters.AddWithValue("@descriptionID", descriptionId);
                    cmd.Parameters.AddWithValue("@productId", StaticClass.PreProductID.Mem.ToString());
                    cmd.Parameters.AddWithValue("@basestr1", newP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", newP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", newP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", newP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (newP as RAM).Gen);
                    cmd.Parameters.AddWithValue("@int1", (newP as RAM).Size);
                    cmd.Parameters.AddWithValue("@int2", (newP as RAM).Clockspeed);
                    cmd.Parameters.AddWithValue("@int3", (newP as RAM).Timing);

                    sucessfull = cmd.ExecuteNonQuery() == 1 ? true : false;
                    prodId = StaticClass.PreProductID.Mem.ToString();
                }
            } else  if (newP is GPU)
            {
                sql += "(`id`, `descriptionID`, `productId`, `basestr1`, `basestr2`, `baseint1`, `baseint2`, `str1`, `int1`, `int2`, `int3`, `bool1` ) " +
                      "VALUES (@id, @descriptionID, @productId, @basestr1, @basestr2, @baseint1, @baseint2, @str1, @int1,@int2, @int3, @bool1)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", null);
                    cmd.Parameters.AddWithValue("@descriptionID", descriptionId);
                    cmd.Parameters.AddWithValue("@productId", StaticClass.PreProductID.Video.ToString());
                    cmd.Parameters.AddWithValue("@basestr1", newP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", newP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", newP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", newP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (newP as GPU).Ram);
                    cmd.Parameters.AddWithValue("@int1", (newP as GPU).Size);
                    cmd.Parameters.AddWithValue("@int2", (newP as GPU).Coreclockspeed);
                    cmd.Parameters.AddWithValue("@int3", (newP as GPU).Powerconsumption);
                    cmd.Parameters.AddWithValue("@bool1", (newP as GPU).Raytracing);

                    sucessfull = cmd.ExecuteNonQuery() == 1 ? true : false;
                    prodId = StaticClass.PreProductID.Video.ToString();
                }
            } else if (newP is PSU)
            {
                sql += "(`id`, `descriptionID`, `productId`, `basestr1`, `basestr2`, `baseint1`, `baseint2`, `str1`, `int1` ) " +
                      "VALUES (@id, @descriptionID, @productId, @basestr1, @basestr2, @baseint1, @baseint2, @str1, @int1)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", null);
                    cmd.Parameters.AddWithValue("@descriptionID", descriptionId);
                    cmd.Parameters.AddWithValue("@productId", StaticClass.PreProductID.PSU.ToString());
                    cmd.Parameters.AddWithValue("@basestr1", newP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", newP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", newP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", newP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (newP as PSU).Quality);
                    cmd.Parameters.AddWithValue("@int1", (newP as PSU).Poweroutput);

                    sucessfull = cmd.ExecuteNonQuery() == 1 ? true : false;
                    prodId = StaticClass.PreProductID.PSU.ToString();
                }
            }
            connection.Close();

            if (sucessfull)
            {
                ProductID_Update(prodId); // update productID with the customize productID
            }
        }

        public static void UpdateProduct(Parts modP)
        {
            connection.Open();

            if (modP is Processor)
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE `valuetable` SET `basestr1`=@basestr1, `basestr2`=@basestr2, `baseint1`=@baseint1, `baseint2`=@baseint2, `str1`=@str1, `int1`=@int1, `int2`=@int2, `int3`=@int3 WHERE productID=@productId", connection)) 
                {
                    cmd.Parameters.AddWithValue("@productId", modP.Id);
                    cmd.Parameters.AddWithValue("@basestr1", modP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", modP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", modP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", modP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (modP as Processor).Package);
                    cmd.Parameters.AddWithValue("@int1", (modP as Processor).Clockspeed);
                    cmd.Parameters.AddWithValue("@int2", (modP as Processor).L3size);
                    cmd.Parameters.AddWithValue("@int3", (modP as Processor).Cores);
                    cmd.ExecuteNonQuery();
                }
            } else if (modP is Motherboard)
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE `valuetable` SET `basestr1`=@basestr1, `basestr2`=@basestr2, `baseint1`=@baseint1, `baseint2`=@baseint2, `str1`=@str1,`str2`=@str2, `bool1`=@bool1 WHERE productID=@productId", connection)) 
                {
                    cmd.Parameters.AddWithValue("@productId", modP.Id);
                    cmd.Parameters.AddWithValue("@basestr1", modP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", modP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", modP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", modP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (modP as Motherboard).Procsocket);
                    cmd.Parameters.AddWithValue("@str2", (modP as Motherboard).Chipset);
                    cmd.Parameters.AddWithValue("@bool1", (modP as Motherboard).Illuminated);
                    cmd.ExecuteNonQuery();
                }
            } else if (modP is RAM)
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE `valuetable` SET `basestr1`=@basestr1, `basestr2`=@basestr2, `baseint1`=@baseint1, `baseint2`=@baseint2, `str1`=@str1,  `int1`=@int1, `int2`=@int2, `int3`=@int3 WHERE productID=@productId", connection))
                {
                    cmd.Parameters.AddWithValue("@productId", modP.Id);
                    cmd.Parameters.AddWithValue("@basestr1", modP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", modP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", modP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", modP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (modP as RAM).Gen);
                    cmd.Parameters.AddWithValue("@int1", (modP as RAM).Size);
                    cmd.Parameters.AddWithValue("@int2", (modP as RAM).Clockspeed);
                    cmd.Parameters.AddWithValue("@int3", (modP as RAM).Timing);
                    cmd.ExecuteNonQuery();
                }
            } else if (modP is GPU)
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE `valuetable` SET `basestr1`=@basestr1, `basestr2`=@basestr2, `baseint1`=@baseint1, `baseint2`=@baseint2, `str1`=@str1,  `int1`=@int1, `int2`=@int2, `int3`=@int3, `bool1`=@bool1 WHERE productID=@productId", connection)) 
                {
                    cmd.Parameters.AddWithValue("@productId", modP.Id);
                    cmd.Parameters.AddWithValue("@basestr1", modP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", modP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", modP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", modP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (modP as GPU).Ram);
                    cmd.Parameters.AddWithValue("@int1", (modP as GPU).Size);
                    cmd.Parameters.AddWithValue("@int2", (modP as GPU).Coreclockspeed);
                    cmd.Parameters.AddWithValue("@int3", (modP as GPU).Powerconsumption);
                    cmd.Parameters.AddWithValue("@bool1", (modP as GPU).Raytracing);
                    cmd.ExecuteNonQuery();
                }
            } else if (modP is PSU)
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE `valuetable` SET `basestr1`=@basestr1, `basestr2`=@basestr2, `baseint1`=@baseint1, `baseint2`=@baseint2, `str1`=@str1,  `int1`=@int1 WHERE productID=@productId", connection)) 
                {
                    cmd.Parameters.AddWithValue("@productId", modP.Id);
                    cmd.Parameters.AddWithValue("@basestr1", modP.Manufacturer);
                    cmd.Parameters.AddWithValue("@basestr2", modP.Type);
                    cmd.Parameters.AddWithValue("@baseint1", modP.Price);
                    cmd.Parameters.AddWithValue("@baseint2", modP.Warranty);
                    cmd.Parameters.AddWithValue("@str1", (modP as PSU).Quality);
                    cmd.Parameters.AddWithValue("@int1", (modP as PSU).Poweroutput);
                    cmd.ExecuteNonQuery();
                }
            }

            connection.Close();
        }

        public static void ProductID_Update(string productID)
        {
            connection.Open();
            string newProductId = string.Empty;
            int id = 0;

            using (MySqlCommand cmd = new MySqlCommand("SELECT ID, productID FROM `valuetable` WHERE productId=@productId", connection))
            {
                cmd.Parameters.AddWithValue("@productId", productID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    newProductId = reader.GetString(1) + reader.GetInt32(0);
                }
                reader.Close();
            }

            using (MySqlCommand cmd = new MySqlCommand("UPDATE  `valuetable` SET  productId=@productId  WHERE ID =@id", connection)) 
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@productId", newProductId);
                cmd.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static List<Parts> GetAllParts()
        {
            List<Parts> parts = new List<Parts>();

            try
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM `valuetable` ", connection))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        switch (reader.GetInt32(1))
                        {
                            case 1: // Processor
                                parts.Add(new Processor(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(6), (Packaging)reader.GetInt32(7), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)));
                                break;
                            case 2: // Motherboard
                                parts.Add(new Motherboard(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), (ProcessorSocket)reader.GetInt32(7), (Chipset)reader.GetInt32(8), StaticClass.FromIntToBool(reader.GetInt32(12))));
                                break;
                            case 3: // RAM
                                parts.Add(new RAM(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(6), (Generation)reader.GetInt32(7), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)));
                                break;
                            case 4: // GPU
                                parts.Add(new GPU(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), (RamType)reader.GetInt32(7), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), StaticClass.FromIntToBool(reader.GetInt32(12))));
                                break;
                            case 5: // PSU
                                parts.Add(new PSU(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), (QualityType)reader.GetInt32(7), reader.GetInt32(9)));
                                break;
                        }

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during reading from database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally { connection.Close(); }
           
            return parts;
        }

        public static FunctionResult PartExitsInValueTable(string id)
        {
            FunctionResult result = new FunctionResult();
            int piece;

            try
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT  Count(*)  FROM  `valuetable` WHERE productId=@id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    piece = Convert.ToInt32(cmd.ExecuteScalar());
                    result.Result = piece > 0 ? true : false;
                    result.Message = result.Result ? "There is already a part with this product ID." : "There has not saved part with this product ID.";
                    result.Fresult = FunctionResultType.ok;
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = false;
                result.Fresult = FunctionResultType.fatal;
            }finally { connection.Close(); }
            return result;            
        }
        

        internal static List<Parts> AllPartsinGivenStorage(StorageTypes chosenStorage) 
        {
            List<Parts> parts = new List<Parts>();
            try
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT val.ID,val.descriptionID, val.productID, val.basestr1, val.basestr2, val.baseint1, val.baseint2, val.str1, val.str2, val.int1, val.int2, val.int3, val.bool1 FROM `" + chosenStorage + "` st JOIN `valuetable` val ON val.productID = st.productID", connection))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        switch (reader.GetString(1)) // description id => type of computer part
                        {
                            case "1":
                                parts.Add(new Processor(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(6), (Packaging)reader.GetInt32(7), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)));
                                break;
                            case "2":
                                parts.Add(new Motherboard(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), (ProcessorSocket)reader.GetInt32(7), (Chipset)reader.GetInt32(8), StaticClass.FromIntToBool(reader.GetInt32(12))));
                                break;
                            case "3":
                                parts.Add(new RAM(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(6), (Generation)reader.GetInt32(7), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)));
                                break;
                            case "4":
                                parts.Add(new GPU(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), (RamType)reader.GetInt32(7), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), StaticClass.FromIntToBool(reader.GetInt32(12))));
                                break;
                            case "5":
                                parts.Add(new PSU(reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), (QualityType)reader.GetInt32(7), reader.GetInt32(9)));
                                break;
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during reading from database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
           
            return parts;

        }

        internal static int NumberPartInGivenStorage(string chosenId, StorageTypes chosenStorage) 
        {
            int pieces = 0;

            try
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT pieces  FROM `" + chosenStorage + "` WHERE productID =@id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", chosenId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        pieces = reader.GetInt32(0);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during reading from database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally { connection.Close(); }
            
            return pieces;

        }

        internal static void UpdatePartPiecesInGivenStorage(StorageTypes chosenStorage, string id, int afterChangePieces, int beforeChangePieces)
        {
            connection.Open();
            bool add = afterChangePieces > beforeChangePieces; 

            if (add && beforeChangePieces == 0) // There was 0 pieces in the storage - new product is added to the storage
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO  `" + chosenStorage + "` VALUES(@productID, @pieces, @dateofmod)", connection))
                {
                    cmd.Parameters.AddWithValue("@productID", id);
                    cmd.Parameters.AddWithValue("@pieces", afterChangePieces);
                    cmd.Parameters.AddWithValue("@dateofmod", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            } else if( (add || !add ) && beforeChangePieces != 0 && afterChangePieces != 0) // we added extra or  we deleted some part but not totally - there is still some left
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE  `" + chosenStorage + "` SET  pieces=@pieces, dateofmod=@dateofmod  WHERE productID =@productID", connection))
                {
                    cmd.Parameters.AddWithValue("@productID", id);
                    cmd.Parameters.AddWithValue("@pieces", afterChangePieces);
                    cmd.Parameters.AddWithValue("@dateofmod", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            else if (!add && afterChangePieces == 0) // delete the part and now there is 0 - Delete the part from the storage totally
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM `" + chosenStorage + "`  WHERE productID =@productID", connection))
                {
                    cmd.Parameters.AddWithValue("@productID", id);
                    cmd.ExecuteNonQuery();
                }
            }
            connection.Close();
        }
    }
 
}
