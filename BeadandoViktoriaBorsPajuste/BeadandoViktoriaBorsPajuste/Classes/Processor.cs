using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    enum Packaging
    {
        LGA1200,
        LGA1700,
        AM4,
        AM5

    }
    internal class Processor : Parts
    {
        Packaging package;
        int clockspeed;
        int l3size;
        int cores;

        public Processor(string id,string manufecturer, string type, int warranty, Packaging package, int clockspeed, int l3size, int cores) : base(id, manufecturer, type, 1, warranty)
        {
            Package = package;
            Clockspeed = clockspeed;
            L3size = l3size;
            Cores = cores;

            this.Price = (this.clockspeed / 100) * this.l3size * this.cores;
        }

        public Packaging Package
        {
            get => package;
            set
            {
                if ( (this.Manufacturer == ProcessorManufacturer.Intel.ToString() && (value == Packaging.LGA1700 || value == Packaging.LGA1200)) || (this.Manufacturer == ProcessorManufacturer.AMD.ToString() && (value == Packaging.AM4 || value == Packaging.AM5))  )
                {
                    package = value;
                }
                else
                {
                    throw new ArgumentException("The manufecturer and the packaging is not a match");
                }
            }
        }
        public int Clockspeed
        { 
            get => clockspeed;
            set
            {
                if (value >= 1000 && value <= 6000)
                {
                    clockspeed = value;
                }
                else
                {
                    throw new ArgumentException("The clockspeed must be between 1000 and 6000 MHz");
                }
            }
        }
        public int L3size
        { 
            get => l3size;
            set
            {
                if (value >= 2 && value <= 256)
                {
                    l3size = value;
                }
                else
                {
                    throw new ArgumentException("The L3 cache size must be between 2 and 256 Mb");
                }
            }
        }
        public int Cores
        {
            get => cores;
            set
            {
                if (value >= 1 && value <= 32)
                {
                    cores = value;
                }
                else
                {
                    throw new ArgumentException("The number of cores must be between 1 and 32");
                }
            }
        }

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
