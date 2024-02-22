using prakt_5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            doc.LoadXml(XMLText);

            var valutes = doc.DocumentElement;

                foreach (XmlNode valute in valutes)
                {
                    string id = valute.Attributes["ID"].InnerText;
                    List<String> list = new List<string>();

                    for (int i = 0; i < 5; i++)
                    {
                        foreach (XmlNode property in valute)
                        {
                            list.Add(property.InnerText);
                        }
                    }
                    int numCode = Convert.ToInt32(list[0]);
                    string charCode = list[1];
                    int nominal = Convert.ToInt32(list[2]);
                    string name = list[3];
                    double value = Convert.ToDouble(list[4]);
                    valutesList.Add(new Valute(id, numCode, charCode, nominal, name, value));
                    list.Clear();
                }
            return valutesList;
        }
    }
}
