using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_postoje
{
    public class Karta
    {
        public int lp,szer;
        public string nazwa;
        public List<List <string>> tabelka;
        public List<string> Wyswietl;
        public string[] row;
        public Karta(string [] tresc)
        {
            Rozkoduj(tresc);
        }

        public void Podmien(string[] tresc)
        {
            Rozkoduj(tresc);
        }
        private void Rozkoduj(string[] tresc)
        {
            row = tresc;
            int _lp = 1;
            nazwa = tresc[0];
            tabelka = new List<List<string>>();
            List<string> rekord = new List<string>();
            for (int i = 1; i < tresc.Length; i++)
            {
                if (tresc[i].StartsWith(">"))
                {
                    if (_lp != 1)
                    {
                        string[] kopia = rekord.ToArray();
                        tabelka.Add(kopia.ToList());
                        rekord.Clear();
                    }
                    rekord.Add(_lp.ToString());
                    string[] bez = tresc[i].Split('>');
                    rekord.Add(bez[1]);
                    _lp = _lp + 1;
                }
                else
                {
                    rekord.Add(tresc[i]);
                }
                if (i == tresc.Length - 1)
                {
                    string[] kopia = rekord.ToArray();
                    tabelka.Add(kopia.ToList());
                }
                lp = _lp;
            }
            szer = 0;
            foreach (List<string> row in tabelka)
            {
                if (row.Count > szer) szer = row.Count;
            }
            szer = szer - 1;
            Wyswietl = new List<string>();
            foreach (List<string> row in tabelka)
            {
                string ss = row[0]+". "+row[1];
                for(int j=2;j<row.Count;j++)
                {
                    if(j==2) ss = ss + "\n\n";
                    ss = ss + row[j];
                    if(j< row.Count-1) ss = ss + "\n";
                }
                Wyswietl.Add(ss);
            }
           
        }

    }
}
