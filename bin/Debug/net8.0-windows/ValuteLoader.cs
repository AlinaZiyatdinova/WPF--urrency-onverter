using prakt_5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace prakt_5.Data
{
    public static class ValuteLoader
    {
        public static List<Valute> LoadValutes (string XMLText)
        {
            List<Valute> valutesList = new List<Valute>();

            XmlDocument doc = new XmlDocument();
            doc.Load("valutes.xml");

            XmlNodeList valutes = doc.SelectNodes("ValCurs");
            

            foreach (XmlNode valute in valutes)
            {
                string id = valute.Attributes["ID"].Value;
                int numCode = Convert.ToInt32(valute.Attributes["NumCode"].Value);
                string charCode = valute.Attributes["CharCode"].Value;
                int nominal = Convert.ToInt32(valute.Attributes["Nominal"].Value);
                string name = valute.Attributes["Name"].Value;
                int value = Convert.ToInt32(valute.Attributes["Value"].Value);
                Valute val = new Valute(id, numCode, charCode, nominal, name, value);

                valutesList.Add(val);
            }

            return valutesList;
        }
    }
}
