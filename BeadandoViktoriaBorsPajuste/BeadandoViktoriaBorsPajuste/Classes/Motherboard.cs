using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadandoViktoriaBorsPajuste.Classes
{
    enum ProcessorSocket
    {
        LGA1200,
        LGA1700,
        AM4,
        AM5
    }

    enum Chipset
    {
        H510,
        H610,
        B660,
        Z690,
        B760,
        Z790,
        B450,
        X470,
        B550,
        X570,
        B650,
        X670
    }

    internal class Motherboard : Parts
    {
        ProcessorSocket procsocket;
        Chipset chipset;
        bool illuminated;

        public Motherboard(string id, string manufecturer, string type, int price, int warranty, ProcessorSocket procsocket, Chipset chipset, bool illuminated) : base(id,manufecturer, type, price, warranty)
        {
            Procsocket = procsocket;
            Chipset = chipset;
            Illuminated = illuminated;
        }

        public ProcessorSocket Procsocket
        {
            get => procsocket;
            set => procsocket = value;
        }

        public Chipset Chipset
        {
            get => chipset;
            set
            {
                if ( (this.Procsocket == ProcessorSocket.LGA1200 && (value == Chipset.H510  || value == Chipset.H610))
                    || (this.Procsocket == ProcessorSocket.LGA1700 && (value == Chipset.B660 || value == Chipset.Z690 || value == Chipset.B760 || value == Chipset.Z790))
                    || (this.Procsocket == ProcessorSocket.AM4 && (value == Chipset.B450 || value == Chipset.X470 || value == Chipset.B550 || value == Chipset.X570))
                    || (this.Procsocket == ProcessorSocket.AM5 && (value == Chipset.B650 || value == Chipset.X670))
                    )
                {
                    chipset = value;
                }
                else
                {
                    throw new ArgumentException("The processor socket and chipset type are not a match");
                }
            }
        }

        public bool Illuminated { get => illuminated; set => illuminated = value; }

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
