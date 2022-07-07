using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_postoje
{
    public class Czesc
    {
        public string ID;
        public string Nazwa;
        public string Lokalizacja;
        public int Ilosc;
        public bool Obserwuj;
        public string Obs;
        public string opis;
        public string Powiadom;
        public int PowiadomInt;
        public bool ZN = false;
        public bool PEN = false;
        public string Wartosc;
        public float cena;

        public Czesc(string _ID, string _Nazwa, string _Lokalizacja, string _Ilosc, string _Opis, string _obs, string _powiadom, string _wartosc)
        {
            ID = _ID;
            Nazwa = _Nazwa;
            Lokalizacja = _Lokalizacja;
            if (_Ilosc != "") Ilosc = Convert.ToInt32(_Ilosc);
            else Ilosc = 0;
            opis = _Opis;
            Obs = _obs;
            if (_obs == "1") Obserwuj = true;
            else Obserwuj = false;
            Powiadom = _powiadom;
            PowiadomInt = Convert.ToInt32(_powiadom);
            if (ID.Contains("ZN") || ID.Contains("ZZ")) ZN = true;
            else if (ID.Contains("PEN")) PEN = true;
            if (_wartosc == "") Wartosc = "0"; 
            else Wartosc = _wartosc;
            cena = float.Parse(Wartosc);
  

        }

        public void aktualizuj(string _ID, string _Nazwa, string _Lokalizacja, string _Ilosc, string _Warto)
        {
            ID = _ID;
            Nazwa = _Nazwa;
            Lokalizacja = _Lokalizacja;
            if (_Ilosc != "") Ilosc = Convert.ToInt32(_Ilosc);
            else Ilosc = 0;
            if (_Warto == "") Wartosc = "0";
            else Wartosc = _Warto;
            cena = float.Parse(Wartosc);
        }

        public void zmien (int pobrano)
        {
            Ilosc = Ilosc - pobrano;
        }

        public void oddaj(int oddano)
        {
            Ilosc = Ilosc + oddano;
        }

        public void zmienOpis(string op)
        {
            opis = op;
        }

        public void zmienObs(string _obs)
        {
            Obs = _obs;
            if (_obs == "1") Obserwuj = true;
            else Obserwuj = false;
        }

        public void zmienPow(string _powiadom)
        {
            Powiadom = _powiadom;
            PowiadomInt = Convert.ToInt32(_powiadom);
        }
       
    }
}
