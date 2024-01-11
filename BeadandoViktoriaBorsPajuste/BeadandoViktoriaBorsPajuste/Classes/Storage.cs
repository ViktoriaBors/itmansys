using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    internal class Storage
    {
        // DELETE IT LATER - we are not makin storages itself - we make it by checking do we have them or not
        /*
         * foraktar, masodlagosraktar, kulfoldiraktar, extraraktar
         * ID - int
         * termekID - string
         * dbszam - int
         * valtozasIdeje - datetime
         * 
         * create code base
                CREATE TABLE `extraraktar` ( // NEV VALTOZTATAS
	                `ID` INT(11) NOT NULL,
	                `termekID` VARCHAR(50) NOT NULL DEFAULT '' COLLATE 'utf8mb4_general_ci',
	                `dbszam` INT(11) NOT NULL,
	                `valtozasIdeje` DATETIME NOT NULL,
	                PRIMARY KEY (`ID`) USING BTREE
                )
                COLLATE='utf8mb4_general_ci'
                ENGINE=InnoDB;
        Create it with for loop - enum store the name of the storages - set to storage name table
         */
        string name;
        string productId;
        uint pieces;
        DateTime recordChanged;

        public Storage(string name, string productId, uint pieces, DateTime recordChanged)
        {
            Name = name;
            ProductId = productId;
            Pieces = pieces;
            RecordChanged = recordChanged;
        }

        public string Name { get => name; set => name = value; }
        public string ProductId { get => productId; set => productId = value; }
        public uint Pieces { get => pieces; set => pieces = value; }
        public DateTime RecordChanged { get => recordChanged; set => recordChanged = value; }
    }
}
