using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    enum Generation
    {
        DDR4,
        DDR5
    }

    internal class RAM : Parts
    {
        Generation gen;
        int size;
        int clockspeed;
        int timing;

        public RAM(string id, string manufecturer, string type,  int warranty, Generation gen, int size, int clockspeed, int timing) : base(id, manufecturer, type, 1, warranty)
        {
            Size = size;
            Gen = gen;
            Clockspeed = clockspeed;
            Timing = timing;

            this.Price = (this.clockspeed / 10) * this.size * 2;
        }

        public int Size
        { 
            get => size; 
            set
            {
                if (value >= 8 && value <= 128)
                {
                    size = value;
                }
                else
                {
                    throw new ArgumentException("The size must be between 8 and 128 GB.");
                }
            }
        }
        public int Clockspeed
        { 
            get => clockspeed; 
            set
            {
                if ( (this.Gen == Generation.DDR4 && (value >= 3000 && value <= 4400)) || (this.Gen == Generation.DDR5 && (value >= 5200 && value <= 8000)))
                {
                    clockspeed = value;
                }
                else
                {
                    if (this.Gen == Generation.DDR4)
                    {
                        throw new ArgumentException("The clockspeed must be between 3000 and 4400 MHz.");
                    } else if (this.Gen == Generation.DDR5)
                    {
                        throw new ArgumentException("The clockspeed must be between 5200 and 8000 MHz.");
                    } else
                    {
                        throw new ArgumentException("The clockspeed must be between 3000 and 8000 MHz.");
                    }                    
                }
            }
        }
        public int Timing
        { 
            get => timing;
            set
            {
                if (value >= 8 && value <= 60)
                {
                    timing = value;
                }
                else
                {
                    throw new ArgumentException("The timing/latency must be 8 and 60 CL");
                }
            }
        }
        internal Generation Gen 
        { 
            get => gen; 
            set => gen = value; 
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
