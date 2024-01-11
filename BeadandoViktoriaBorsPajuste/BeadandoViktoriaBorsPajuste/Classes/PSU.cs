using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    enum QualityType
    {
        Bronze,
        Silver,
        Gold
    }
    internal class PSU : Parts
    {
        int poweroutput;
        QualityType quality;

        public PSU(string id, string manufecturer, string type, int price, int warranty, QualityType quality, int poweroutput) : base(id, manufecturer, type, price, warranty)
        {
            Poweroutput = poweroutput;
            Quality = quality;
        }

        public int Poweroutput
        { 
            get => poweroutput;
            set
            {
                if (value >= 350 && value <= 1200)
                {
                    poweroutput = value;
                }
                else
                {
                    throw new ArgumentException("A teljesitmeny 350 es 1200 Watt kozott lehet");
                }
            }
        }
        internal QualityType Quality { get => quality; set => quality = value; }

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
