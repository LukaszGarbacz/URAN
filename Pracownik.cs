using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_postoje
{
    public class Pracownik
    {
        public string ID;
        public string Imie;
        public string Nazwisko;
        public bool archiwalne;
        public bool kierownik;

        public Pracownik(string _ID, string _Imie, string _Nazwisko, string _archiwalne,string _kierownik)
        {
            ID = _ID;
            Imie = _Imie;
            Nazwisko = _Nazwisko;
            if (_archiwalne == "0") archiwalne = false;
            else archiwalne = true;
            if (_kierownik == "0") kierownik = false;
            else kierownik = true;
        }


    }
}
