using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_postoje
{
    public class Maszyna
    {
        public string ID;
        public string NazwaWys;
        public string Nazwa;
        public string Linia;
        public string Karta;
        public List<string> Podzespoly;
        public bool Archiwalne;
        public string ArchiwalneStr;






        public Maszyna (string _ID, string _NazwaWys, string _Nazwa, string _Linia, string _Archiwalne,string _Karta)
        {
           
            Podzespoly = new List<string>();
            ID = _ID;
            NazwaWys = _NazwaWys;
            Nazwa = _Nazwa;
            Linia = _Linia;
            Karta = _Karta;
            ArchiwalneStr = _Archiwalne;
            if (_Archiwalne == "0") Archiwalne = false;
            else Archiwalne = true;           
        }

        public void dodajPodzespol (string _podzespol)
        {
            Podzespoly.Add(_podzespol);
        }
        /*
        public void NadajATRPostoje( int _wystapil,int _tPostojow,int _tPracy, int _xNpr, int _xReg, int _xMod, int _xKons, int _xPzb)
        {
            atr = true;
            atrPostojow = _wystapil;
            atrCzasPostojow = _tPostojow;
            atrCzasPracy = _tPracy;
            atrIleNapraw = _xNpr;
            atrIleRegulacji = _xReg;
            atrIleKonserwacji = _xMod;
            atrIleModernizacji = _xKons;
            atrIlePrzezbrajania = _xPzb;
         }*/
    }
}
