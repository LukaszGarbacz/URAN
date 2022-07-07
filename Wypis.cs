using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_postoje
{
    public class Wypis
    {
        public string CzescID;
        public string CzescNazwa;
        public Pracownik Pobral;
        public string Data;
        public int PobranoSzt;
        public string Opis;     
        public string Lista;

        public string godzina;
        public string zostało;
        public float wartosc;
        public bool czyMonit;
        public bool czyMin;
        public string czyMonitS;
        public string czyMinS;
        public DateTime DataGl;

        public string rok;
        public string miesiac;
        public string miesiacSkr;
        public string miesiacTrz;
        public string niceData;
        public string niceDataSkr;
        public string nazwaDok;

        public Wypis(string _Data, string _Godzina, string _CzescID, string _CzescNazwa,string _CzyMonit, string _CzyMin, Pracownik _Pracownik,  string _PobranoSzt,string _Zostało, string _Wartosc, string _Opis, string _Lista)
        {
            CzescID = _CzescID;
            CzescNazwa = _CzescNazwa;
            Pobral = _Pracownik;
            Data = _Data;
            Opis = _Opis;
            PobranoSzt = Convert.ToInt32(_PobranoSzt);
            Lista = _Lista;
            godzina = _Godzina;
            czyMonitS = _CzyMonit;
            if (_CzyMonit == "0") czyMonit = false;
            else czyMonit = true;
            czyMinS = _CzyMin;
            if (_CzyMin == "0") czyMin = false;
            else czyMin = true;
            zostało = _Zostało;
            wartosc=  float.Parse(_Wartosc);

            string[] podz = Data.Split('-');
            rok = podz[0];
            miesiacSkr = podz[1];

            

            if (podz[1] == "01")  miesiac = "Styczeń"; 
            else if (podz[1] == "02")  miesiac = "Luty"; 
            else if (podz[1] == "03")  miesiac = "Marzec"; 
            else if (podz[1] == "04")  miesiac = "Kwiecień"; 
            else if (podz[1] == "05")  miesiac = "Maj"; 
            else if (podz[1] == "06")  miesiac = "Czerwiec"; 
            else if (podz[1] == "07")  miesiac = "Lipiec"; 
            else if (podz[1] == "08")  miesiac = "Sierpień"; 
            else if (podz[1] == "09")  miesiac = "Wrzesień"; 
            else if (podz[1] == "10")  miesiac = "Październik"; 
            else if (podz[1] == "11")  miesiac = "Listopad"; 
            else if (podz[1] == "12")  miesiac = "Grudzień";

            if (podz[1] == "01") miesiacTrz = "Sty";
            else if (podz[1] == "02") miesiacTrz = "Lut";
            else if (podz[1] == "03") miesiacTrz = "Mar";
            else if (podz[1] == "04") miesiacTrz = "Kwi";
            else if (podz[1] == "05") miesiacTrz = "Maj";
            else if (podz[1] == "06") miesiacTrz = "Cze";
            else if (podz[1] == "07") miesiacTrz = "Lip";
            else if (podz[1] == "08") miesiacTrz = "Sie";
            else if (podz[1] == "09") miesiacTrz = "Wrz";
            else if (podz[1] == "10") miesiacTrz = "Paz";
            else if (podz[1] == "11") miesiacTrz = "Lis";
            else if (podz[1] == "12") miesiacTrz = "Gru";

            DataGl = new DateTime(Convert.ToInt32(podz[0]), Convert.ToInt32(podz[1]), Convert.ToInt32(podz[2]));
            niceData = podz[2] + " " + miesiac + " " + rok;
            niceDataSkr = podz[2] + " " + miesiacTrz + " " + rok;
            nazwaDok = podz[2] + miesiacTrz + rok;
        }
        
        public void Przypisz(string _lis)
        {
            Lista = _lis;
        }

    }



}

