using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GF_postoje
{
    public class CellPostoj
    {
        public string dataStr;
        public string dataExp;
        public Maszyna maszyna;       
        public string tStartStr;
        public string tStopStr;
        public int tPraca;
        public int tPostojInt;
        public string tPostojStr;
        public string przyczyna;
        public Pracownik pracownik;
        public string mycie;
        public int mycieInt;
        public string podzespol;
        public string dzialanie;
        public string typ;
        public string niceData;
        public string rok;
        public string miesiac;
        public string pliki;
        public List<string> Ufiles;
        public string NazwaDok;

        public CellPostoj(Maszyna _maszyna, string _data, string _tStart, string _tStop, string _tPostuj, string _przyczyna, Pracownik _pracownik, string _mycie, string _podzespol, string _dzialanie, string _typ, string _pliki)
        {
            dataStr = _data;
            maszyna = _maszyna;
            tStartStr = _tStart;
            tStopStr = _tStop;
            pliki = _pliki;
            string[] NTStart = tStartStr.Split(':');
            int sH = Convert.ToInt32(NTStart[0]);
            int sM = Convert.ToInt32(NTStart[1]);
            string[] NTStop = tStopStr.Split(':');
            int kH = Convert.ToInt32(NTStop[0]);
            int kM = Convert.ToInt32(NTStop[1]);
            tPraca = (kH * 60 + kM) - (sH * 60 + sM);
            if (tPraca < 0) tPraca =1440 + tPraca;
            tPostojInt = Convert.ToInt32(_tPostuj);
            mycieInt = Convert.ToInt32(_mycie);
            if (mycieInt == 0) mycie = "Nie zlecono";
            else if (mycieInt == 1) mycie = "Zlecono";
            else mycie = "Wykonano";
            if (tPostojInt / 60 != 0) tPostojStr = (tPostojInt / 60).ToString() + "h " + (tPostojInt % 60).ToString() + "min";
            else tPostojStr = (tPostojInt % 60).ToString() + "min";
            przyczyna = _przyczyna;
            pracownik= _pracownik;
            String[] Ndata = dataStr.Split('-');
            dataExp = Ndata[2] + "." + Ndata[1];
            rok = Ndata[0];
           // miesiac = Ndata[1];
            podzespol = _podzespol;
            dzialanie = _dzialanie;
            typ = _typ;
            
            Ufiles = new List<string>();
            string[] pliczki = _pliki.Split('*');
            foreach (string S in pliczki)
            {
                string gom = @S;
                if (File.Exists(gom)) Ufiles.Add(gom);
            }
            //Ufiles = pliczki.ToList();

            LadnaData();
        }

        public void Zatwierdz()
        {
            mycieInt = 2;
            mycie = "Wykonano";
        }


        public DateTime Make_data()
        {
            String[] Ndata = dataStr.Split('-');
            string[] NTStart = tStartStr.Split(':');
            DateTime data = new DateTime(Convert.ToInt32(Ndata[0]), Convert.ToInt32(Ndata[1]), Convert.ToInt32(Ndata[2]), Convert.ToInt32(NTStart[0]), Convert.ToInt32(NTStart[1]),0);
            return data;
        }
        public DateTime Make_data2()
        {
            String[] Ndata = dataStr.Split('-');
            string[] NTStart = tStartStr.Split(':');
            DateTime data = new DateTime(Convert.ToInt32(Ndata[0]), Convert.ToInt32(Ndata[1]), Convert.ToInt32(Ndata[2]));
            return data;
        }
        public DateTime Make_tStart()
        {
            string[] NTStart = tStartStr.Split(':');
            DateTime tStart = new DateTime(2020, 1, 1, Convert.ToInt32(NTStart[0]), Convert.ToInt32(NTStart[1]), 0);
            return tStart;
        }

        public DateTime Make_tStop()
        {
            string[] NTStop= tStopStr.Split(':');
            DateTime tStop = new DateTime(2020, 1, 1, Convert.ToInt32(NTStop[0]), Convert.ToInt32(NTStop[1]), 0);
            
            return tStop;
        }
        public DateTime Make_tPostoj()
        {
            DateTime tPostoj = new DateTime(2020, 1, 1, tPostojInt / 60, tPostojInt % 60, 0);
            return tPostoj;
        }

        public void LadnaData ()
        {
            String[] Ndata = dataStr.Split('-');
            string nice = Ndata[2];
            if (Ndata[1] == "01") {nice = nice + " styczeń "; miesiac = "Styczeń"; }
            else if (Ndata[1] == "02") { nice = nice + " luty "; miesiac = "Luty"; }
                else if (Ndata[1] == "03") { nice = nice + " marzec "; miesiac = "Marzec"; }
            else if (Ndata[1] == "04") { nice = nice + " kwiecień "; miesiac = "Kwiecień"; }
            else if (Ndata[1] == "05") { nice = nice + " maj "; miesiac = "Maj"; }
            else if (Ndata[1] == "06") { nice = nice + " czerwiec "; miesiac = "Czerwiec"; }
            else if (Ndata[1] == "07") { nice = nice + " lipiec "; miesiac = "Lipiec"; }
            else if (Ndata[1] == "08") { nice = nice + " sierpień "; miesiac = "Sierpień"; }
            else if (Ndata[1] == "09") { nice = nice + " wrzesień "; miesiac = "Wrzesień"; }
            else if (Ndata[1] == "10") { nice = nice + " październik "; miesiac = "Październik"; }
            else if (Ndata[1] == "11") { nice = nice + " listopad "; miesiac = "Listopad"; }
            else if (Ndata[1] == "12") { nice = nice + " grudzień "; miesiac = "Grudzień"; }
            nice = nice + Ndata[0];
            niceData = nice;
            string miesiacTrz="";
            if (Ndata[1] == "01") miesiacTrz = "Sty";
            else if (Ndata[1] == "02") miesiacTrz = "Lut";
            else if (Ndata[1] == "03") miesiacTrz = "Mar";
            else if (Ndata[1] == "04") miesiacTrz = "Kwi";
            else if (Ndata[1] == "05") miesiacTrz = "Maj";
            else if (Ndata[1] == "06") miesiacTrz = "Cze";
            else if (Ndata[1] == "07") miesiacTrz = "Lip";
            else if (Ndata[1] == "08") miesiacTrz = "Sie";
            else if (Ndata[1] == "09") miesiacTrz = "Wrz";
            else if (Ndata[1] == "10") miesiacTrz = "Paz";
            else if (Ndata[1] == "11") miesiacTrz = "Lis";
            else if (Ndata[1] == "12") miesiacTrz = "Gru";

            NazwaDok = Ndata[2] + miesiacTrz + Ndata[0];
        }

        string plikiexp()
        {
            string ss="";
            foreach (string s in Ufiles)
            {
                ss = ss + s;
                if (Ufiles.IndexOf(s) < Ufiles.Count) ss = ss + "*";
            }
            return ss;
        }


    }
}
