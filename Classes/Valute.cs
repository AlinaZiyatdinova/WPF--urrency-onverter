using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt_5.Classes
{
    public class Valute
    {
        public string ID { get; set; }
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }

        public Valute(string id, int numCode, string charCode, int nominal, string name, double value)
        {
            this.ID = id;
            this.NumCode = numCode;
            this.CharCode = charCode;
            this.Nominal = nominal;
            this.Name = name;
            this.Value = value;
        }

        public override string ToString ()
        {
            return $"{Nominal} {Name}";
        }
    }
}
