using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    enum RamType
    {
        GDDR6,
        GDDR6X
    }
    internal class GPU : Parts
    {
        RamType ram;
        int size;
        int coreclockspeed;
        int powerconsumption;
        bool raytracing;

        public GPU(string id, string manufecturer, string type, int price, int warranty, RamType ram, int size, int coreclockspeed,  int powerconsumption, bool raytracing) : base(id, manufecturer, type, price, warranty)
        {
            Size = size;
            Coreclockspeed = coreclockspeed;
            Ram = ram;
            Powerconsumption = powerconsumption;
            Raytracing = raytracing;
        }

        public int Size
        { 
            get => size;
            set
            {
                if (value >= 8 && value <= 32)
                {
                    size = value;
                }
                else
                {
                    throw new ArgumentException("The size must be between 8 and 32 GB.");
                }
            }
        }
        public int Coreclockspeed 
        { 
            get => coreclockspeed;
            set
            {
                if (value >= 1800 && value <= 3200)
                {
                    coreclockspeed = value;
                }
                else
                {
                    throw new ArgumentException("The core clockspeed must be between 1800 and 3200 MHz.");
                }
            }
        }
        public int Powerconsumption
        { 
            get => powerconsumption;
            set
            {
                if (value >= 120 && value <= 650)
                {
                    powerconsumption = value;
                }
                else
                {
                    throw new ArgumentException("The power consumption must be between 120 and 650 watt");
                }
            }
        }
        public bool Raytracing { get => raytracing; set => raytracing = value; }
        internal RamType Ram { get => ram; set => ram = value; }

        public override double BruttoPrice()
        {
            return this.Price * 1.27;
        }

        public override string ToString()
        {
            return $"[{this.GetType().Name.ToUpper()}] -  {Id}: {Manufacturer} {Type}";
        }
    }
}
