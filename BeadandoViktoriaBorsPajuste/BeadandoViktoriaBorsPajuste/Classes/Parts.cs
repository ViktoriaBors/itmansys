using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    enum MohterboardManufacturer
    {      
        ASRock,
        ASUS,
        MSI,
        Gigabyte    
    }

    enum ProcessorManufacturer
    {
        AMD,
        Intel,
    }

    enum RAMManufacturer
    {
        Apacer,
        Crucial,
        Corsair,
        Hynix,
        Kingstone,
        Gskill,
    }

    enum GPUManufacturer
    {
        AMD,
        Intel,
        NVIDIA
    }

    enum PSUManufacturer
    {
        Corsair,
        Chieftech,
        CoolerMaster,
        DeepCool,
        FSP,
        EVGA,
        Zalman
    }
    public abstract class Parts
    {
        string id;
        string manufacturer;
        string type;
        int price;
        int warranty; // in month

        protected Parts( string id, string manufacturer, string type, int price, int warranty)
        {
            Id = id;
            Manufacturer = manufacturer;
            Type = type;
            Price = price;
            Warranty = warranty;
        }

        public string Id { get => id; set => id = value; }

        public string Manufacturer
        { 
            get => manufacturer;
            set 
            {
                if (value.Length >= 3)
                {
                    manufacturer = value;
                } else
                {
                    throw new ArgumentException("The manufacturer must be a minimum of 3 characters long.");
                }
            } 
        }
        public string Type
        { 
            get => type;
            set
            {
                if (value.Length >= 3)
                {
                    type = value;
                }
                else
                {
                    throw new ArgumentException("The type must be a minimum of 3 characters long.");
                }
            }
        }
        public int Price
        {
            get => price;
            set 
            {
                if (value > 0)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentException("The price cannot be 0");
                }
            }
        }
        public int Warranty 
        { 
            get => warranty;
            set
            {
                if (value >= 0)
                {
                    warranty = value;
                }
                else
                {
                    throw new ArgumentException("The warranty cannot be negative number");
                }
            }
        }

        public abstract double BruttoPrice();

        public override string ToString()
        {
            return $"{id}: {Manufacturer} - {Type}";
        }
    }
}
