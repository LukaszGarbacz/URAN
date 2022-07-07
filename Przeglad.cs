using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
using System.Diagnostics;
using Xceed.Document.NET;

namespace GF_postoje
{
    public class Przeglad
    {
        public List<string> Wyniki;
        public List<string> Komentarze;
        public string sygnatura;
        public Maszyna maszyna;
        public Pracownik wykonał;
        public Pracownik zatwierdzil;
        public Karta karta;
        public string dataStr;
        public string wynik;
        public int wynost;
        public bool mycie;
        public string mycieStr;
        public bool status;
        public string statusStr;
        public string okres;
        public int rok, tydzien;
        public bool specjalny;
        public string WynikiStr;
        public string KomStr;
        string SciezkaDoDanych;
        public string typ;
        public string rokS, miesiac;
        public string raport;

        DateTime dataGl;

        public Przeglad(string _wyniki, string _komentarze, string _sygnatura, Maszyna _maszyna, Pracownik _wykonał, Pracownik _zatwierdzil, string _data, string _mycie, Karta _karta)
        {
            generuj(_wyniki, _komentarze, _sygnatura, _maszyna, _wykonał, _data, _mycie, _karta);
            zatwierdzil = _zatwierdzil;
            status = true;
            statusStr = "Zatwierdzony";
        }
        public Przeglad(string _wyniki, string _komentarze, string _sygnatura, Maszyna _maszyna, Pracownik _wykonał, string _data, string _mycie, Karta _karta)
        {
            generuj(_wyniki, _komentarze, _sygnatura, _maszyna, _wykonał, _data, _mycie, _karta);
            status = false;
            statusStr = "Niezatwierdzony";
            
        }
        public void generuj(string _wyniki, string _komentarze, string _sygnatura, Maszyna _maszyna, Pracownik _wykonał, string _data, string _mycie, Karta _karta)
        {
            WynikiStr = _wyniki;
            KomStr = _komentarze;
            Wyniki = new List<string>();
            Komentarze = new List<string>();
            wynost = 4;
            String[] wyn = _wyniki.Split('-');
            foreach (string w in wyn)
            {
                int wynint = Convert.ToInt32(w);
                if (wynint == 4) Wyniki.Add("Pozytywny");
                else if (wynint == 3) Wyniki.Add("Naprawiony");
                else if (wynint == 2) Wyniki.Add("Dopuszczony");
                else Wyniki.Add("Negatywny");
                if (wynint < wynost) wynost = wynint;
            }
            if (wynost == 4) wynik = "Pozytywny";
            else if (wynost == 3) wynik = "Naprawiony";
            else if (wynost == 2) wynik = "Dopuszczony";
            else wynik = "Negatywny";
            String[] kom = _komentarze.Split('*');
            foreach (string k in kom) Komentarze.Add(k);
            sygnatura = _sygnatura;
            maszyna = _maszyna;
            wykonał = _wykonał;
            dataStr = _data;
            karta = _karta;
            if (_mycie == "0")
            {
                mycie = false;
                mycieStr = "Zlecono";
            }
            else
            {
                mycie = true;
                mycieStr = "Wykonano";
            }

            string[] czas = sygnatura.Split('/');
            rok = Convert.ToInt32(czas[0]);
            tydzien = Convert.ToInt32(czas[1]);

            if (czas.Length == 4) specjalny = true;
            else specjalny = false;
            string okr;
            if (tydzien >= 1 && tydzien < 14) okr = "I";
            else if (tydzien >= 14 && tydzien < 27) okr = "II";
            else if (tydzien >= 27 && tydzien < 40) okr = "III";
            else okr = "IV";
            okres = okr + " Kwartał " + rok.ToString();
            if (specjalny) typ = "Specjalny";
            else typ = "Okresowy";

            String[] Ndata = dataStr.Split('-');
            rokS = Ndata[0];

            if (Ndata[1] == "01") miesiac = "Styczeń"; 
            else if (Ndata[1] == "02")  miesiac = "Luty"; 
            else if (Ndata[1] == "03")  miesiac = "Marzec"; 
            else if (Ndata[1] == "04")  miesiac = "Kwiecień"; 
            else if (Ndata[1] == "05")  miesiac = "Maj"; 
            else if (Ndata[1] == "06")  miesiac = "Czerwiec"; 
            else if (Ndata[1] == "07")  miesiac = "Lipiec"; 
            else if (Ndata[1] == "08")  miesiac = "Sierpień"; 
            else if (Ndata[1] == "09")  miesiac = "Wrzesień"; 
            else if (Ndata[1] == "10")  miesiac = "Październik"; 
            else if (Ndata[1] == "11")  miesiac = "Listopad"; 
            else if (Ndata[1] == "12")  miesiac = "Grudzień"; 



        }
        public DateTime Make_Data()
        {
            string[] Ndata = dataStr.Split('-');
            DateTime tStart = new DateTime(Convert.ToInt32(Ndata[0]), Convert.ToInt32(Ndata[1]), Convert.ToInt32(Ndata[2]));
            return tStart;
        }
        public bool filtr_Wynik(bool Poz, bool Nap, bool Dop, bool Neg)
        {
            if (Poz && wynost == 4) return true;
            else if (Nap && wynost == 3) return true;
            else if (Dop && wynost == 2) return true;
            else if (Neg && wynost == 1) return true;
            else return false;
        }
        public void Zatw_Mycie ()
        {
            mycie = true;
            mycieStr = "Wykonano";
        }

        public void Aktualizuj(string _wyniki, string _komentarze, Pracownik _wykonał, Pracownik _zatwierdzil, string _data)
        {
            generuj(_wyniki, _komentarze, sygnatura, maszyna, _wykonał, _data, "1", karta);
            zatwierdzil = _zatwierdzil;
            status = true;
            statusStr = "Zatwierdzony";
        }

        public string[] Eksport ()
        {
            List<string> eks = new List<string>();
            eks.Add(sygnatura);
            eks.Add(maszyna.ID);
            eks.Add(wykonał.ID);
            if (status) eks.Add(zatwierdzil.ID);
            else eks.Add("");
            eks.Add(dataStr);
            if (mycie) eks.Add("1");
            else eks.Add("0");
            eks.Add(karta.nazwa);
            eks.Add(WynikiStr);
            eks.Add(KomStr);
            return eks.ToArray();
        }
        public void doPliku(string _path)
        {
            SciezkaDoDanych = _path;
            string szablon = SciezkaDoDanych + @"\templates\template_prz.docx";
            string naz = sygnatura.Replace('/', '-');
            string zapis = SciezkaDoDanych + @"\Wykonane_Przeglady\" + naz + ".docx";
            var doc = DocX.Load(szablon);

            Formatting ff = new Formatting();
            ff.Size = 10D;
            ff.Bold = true;
           

            Table t = doc.Tables[0];


            DateTime teraz = DateTime.Now;
            DateTime wtedy = Make_Data();
            t.Rows[0].Cells[1].Paragraphs.First().Append(sygnatura, ff);
            t.Rows[0].Cells[3].Paragraphs.First().Append(maszyna.Linia, ff);
            t.Rows[1].Cells[1].Paragraphs.First().Append(maszyna.NazwaWys, ff);
            t.Rows[1].Cells[3].Paragraphs.First().Append(typ, ff);
            t.Rows[3].Cells[1].Paragraphs.First().Append(wykonał.Imie+" "+wykonał.Nazwisko, ff);
            t.Rows[3].Cells[3].Paragraphs.First().Append(wtedy.ToShortDateString(), ff);
            t.Rows[4].Cells[1].Paragraphs.First().Append(zatwierdzil.Imie + " " + zatwierdzil.Nazwisko, ff);
            t.Rows[4].Cells[3].Paragraphs.First().Append(teraz.ToShortDateString(), ff);
            t.Rows[6].Cells[1].Paragraphs.First().Append("Wykonano", ff);
            t.Rows[6].Cells[3].Paragraphs.First().Append(karta.nazwa, ff);
            t.Rows[7].Cells[1].Paragraphs.First().Append(wynik, ff);


            Formatting fR = new Formatting();
            fR.Size = 10D;
            fR.Bold = false;
            Formatting fB = new Formatting();
            fB.Size = 10D;
            fB.Bold = true;



            Table t2 = doc.AddTable(1, 4);
            var szer = new float[] {30, 230f, 70f, 175f};
            t2.SetWidths(szer);
            t2.Alignment = Alignment.left;


            t2.Rows[0].Cells[0].Paragraphs.First().Append("L.p.", ff);
            t2.Rows[0].Cells[1].Paragraphs.First().Append("Opis czynności kontrolnej", ff);
            t2.Rows[0].Cells[2].Paragraphs.First().Append("Wynik", ff);
            t2.Rows[0].Cells[3].Paragraphs.First().Append("Uwagi/zalecenia pokontrolne", ff);

            int lp = 1;
            foreach (List<string> row in karta.tabelka)
            {
                var r = t2.InsertRow();
                r.Cells[0].Paragraphs.First().Append(lp.ToString()+".", fB);
                r.Cells[1].Paragraphs.First().Append(row[1], fB);
                for (int i =2; i<row.Count; i++ )
                {
                    r.Cells[1].InsertParagraph();
                    r.Cells[1].Paragraphs[i - 1].Append(row[i],fR);
                }
                r.Cells[2].Paragraphs.First().Append(Wyniki[lp-1], fR);
                r.Cells[3].Paragraphs.First().Append(Komentarze[lp - 1], fR);
                lp++;
            }

            doc.InsertTable(t2);
            doc.SaveAs(zapis);
        }

    }
}
