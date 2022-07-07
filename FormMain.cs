using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using Utrzymanie_Ruchu___APP_Niepruszewo;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using WordToPDF;
//using Excel = Microsoft.Office.Interop.Excel;



namespace GF_postoje
{
    public partial class URAN : Form
    {

        List<CellPostoj> FullList_Postoje;
        List<Pracownik> FullList_Pracownicy;
        List<Maszyna> FullList_Maszyny;
        List<CellPostoj> ShowList;
        List<Przeglad> FullList_Przeglady;
        List<Przeglad> ShowList_Przeglady;
        List<RekordHarm> FullList_Harmonogram;
        List<Karta> FullList_Karty;
        List<Wypis> FullList_Wypisy;


        string[] PracownicyNazwiska;
        string[] PracownicyNazwiska_BezArch;
        string[] Linie;
        string[] Linie_BezArch;
        string[] Nazwy_Kart;

        List<int> ShowList_Indeksy_Postoje;
        List<int> ShowList_Indeksy_Przeglady;

        public List<string> Zapis;
        public List<string> PlikiEx;

        string SciezkaDoExe;
        string SciezkaDoDanych;
        string SciezkaDoKatalogu;
        bool admin = false;
        int indeks_wyswietlania = 0;
        int zaznaczona_pozycja = 0;
        bool TrybPracy = true;
        int Editindeks = 0;
        int indekslinii = 0;
        int indeksmaszyny = 0;
        int LogDanych;
        int WyswPoDodaniu = -1;
        string dataRaportu="",mailing ="";

        public URAN()
        {
            InitializeComponent();

            SciezkaDoExe = Directory.GetCurrentDirectory();
            var fi0 = new FileInfo(SciezkaDoExe + @"\path.txt");
            bool ffi0 = fi0.Exists;

            if (!ffi0)
            {
                DataFolder_Dialog.ShowDialog();
                System.IO.File.WriteAllText(SciezkaDoExe + @"\path.txt", DataFolder_Dialog.SelectedPath);
            }
            string[] scezki = System.IO.File.ReadAllLines(SciezkaDoExe + @"\path.txt");
            SciezkaDoDanych = scezki[0] + @"\Dane";
            SciezkaDoKatalogu = scezki[0] ;
            if (scezki[1] == "a") admin = true;

            if (admin)
            {
                //MenuGl.TabPages[2].Show();
                      
                magazyn_aktualizuj_btn.Visible = true;
                czesci_generuj_btn.Visible = true;
                czesci_tylko_monitorowanecheckBox.Visible = true;
                przeglady_wykonal_combo.Visible = true;
                przeglady_lbl.Visible = true;
                park_dodaj_btn.Enabled = true;
                park_edytuj_btn.Text = "Edytuj maszynę";
                przeglady_historia_zatwierdz_btn.Visible = true;

            }
            else
            {
                //MenuGl.TabPages.RemoveAt(2);
               
                magazyn_aktualizuj_btn.Visible = false;
                czesci_generuj_btn.Visible = false;
                czesci_tylko_monitorowanecheckBox.Visible = false;
                przeglady_wykonal_combo.Visible = false;
                przeglady_lbl.Visible = false;
                park_dodaj_btn.Enabled = false;
                park_edytuj_btn.Text = "Więcej";
                przeglady_historia_zatwierdz_btn.Visible = false;

            }

            if (ZaladujDane())
            {
                
                nowy_postoj_linia_combo.Items.AddRange(Linie_BezArch);
                postoje_historia_filtry_linia_combo.Items.AddRange(Linie);
                nowy_postoj_wprowadzil_combo.Items.AddRange(PracownicyNazwiska_BezArch);
                postoje_historia_filtry_wprowadzil_combo.Items.AddRange(PracownicyNazwiska);
                czesc_pobral_comboBox.Items.AddRange(PracownicyNazwiska_BezArch);

                UstalDatyZakresu();
                Ustal_wyswietlanie();
                postoje_historia_wyswietlaj_combo.SelectedIndex = indeks_wyswietlania;
                Wyswietl();

                PlikiEx = new List<string>();


                int przewyk = przeglady_wykonal_combo.SelectedIndex;
                przeglady_wykonal_combo.Items.Clear();
                przeglady_wykonal_combo.Items.Add("Wszyscy");
                foreach (Pracownik P in FullList_Pracownicy) przeglady_wykonal_combo.Items.Add(P.Nazwisko);
                if (przewyk != -1) przeglady_wykonal_combo.SelectedIndex = przewyk;
                else przeglady_wykonal_combo.SelectedIndex = 0;

       
            }

            
        }

        public bool ZaladujDane()
        {
            var fi1 = new FileInfo(SciezkaDoDanych + @"\ListaPracownikow.csv");
            bool ffi1 = fi1.Exists;
            var fi2 = new FileInfo(SciezkaDoDanych + @"\ListaMaszyn.csv");
            bool ffi2 = fi2.Exists;
            var fi7 = new FileInfo(SciezkaDoDanych + @"\ListaPodzespolow.csv");
            bool ffi7 = fi7.Exists;
            var fi3 = new FileInfo(SciezkaDoDanych + @"\ListaPostojow.csv");
            bool ffi3 = fi3.Exists;
            var fi4 = new FileInfo(SciezkaDoDanych + @"\ListaPrzegladow.csv");
            bool ffi4 = fi4.Exists;
            var fi5 = new FileInfo(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv");
            bool ffi5 = fi5.Exists;
            var fi6 = new FileInfo(SciezkaDoDanych + @"\ListaKart.csv");
            bool ffi6 = fi6.Exists;
            var fi8 = new FileInfo(SciezkaDoDanych + @"\ListaCzesci.csv");
            bool ffi8 = fi8.Exists;
            var fi9 = new FileInfo(SciezkaDoDanych + @"\ListaWypisow.csv");
            bool ffi9 = fi9.Exists;

            if (ffi1 && ffi2 && ffi3 && ffi4 && ffi5 && ffi6 && ffi7  && ffi8 && ffi9)
            {
                FullList_Postoje = new List<CellPostoj>();
                FullList_Pracownicy = new List<Pracownik>();
                FullList_Maszyny = new List<Maszyna>();
                FullList_Przeglady = new List<Przeglad>();
                FullList_Harmonogram = new List<RekordHarm>();
                FullList_Karty = new List<Karta>();
                FullList_Czesci = new List<Czesc>();
                FullList_Wypisy = new List<Wypis>();

                using (ReadWriteCsv.CsvFileReader CSVpracownicy = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaPracownikow.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVpracownicy.ReadRow(row))
                    {
                        Pracownik nowy = new Pracownik(row[0], row[1], row[2], row[3], row[4]);
                        FullList_Pracownicy.Add(nowy);
                    }
                }
                using (ReadWriteCsv.CsvFileReader CSVmaszyny = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaMaszyn.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVmaszyny.ReadRow(row))
                    {
                        if (row.Count == 5) row.Add("");
                        Maszyna nowy = new Maszyna(row[0], row[1], row[2], row[3], row[4], row[5]);
                        FullList_Maszyny.Add(nowy);
                    }
                }
                using (ReadWriteCsv.CsvFileReader CSVpodzespoly = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaPodzespolow.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVpodzespoly.ReadRow(row))
                    {
                        int ixZ = UstalIndexPoID(1, row[0]);
                        FullList_Maszyny[ixZ].dodajPodzespol(row[1]);
                    }
                }
                using (ReadWriteCsv.CsvFileReader CSVpostoje = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaPostojow.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVpostoje.ReadRow(row))
                    {
                        string[] rekord = row.LineText.Split(';');
                        int ixM = UstalIndexPoID(1, row[0]);
                        int ixP = UstalIndexPoID(2, row[6]);
                        //string myc;
                        //if (rekord.Length == 8) myc = rekord[7];
                        //else myc = "0";
                        CellPostoj nowy = new CellPostoj(FullList_Maszyny[ixM], rekord[1], rekord[2], rekord[3], rekord[4], rekord[5], FullList_Pracownicy[ixP], rekord[7], rekord[8], rekord[9], rekord[10], rekord[11]);

                        FullList_Postoje.Add(nowy);
                    }
                }
                using (ReadWriteCsv.CsvFileReader CSVkarty = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaKart.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVkarty.ReadRow(row))
                    {
                        string[] rekord = row.LineText.Split(';');
                        Karta nowy = new Karta(rekord);
                        FullList_Karty.Add(nowy);
                    }
                }
                using (ReadWriteCsv.CsvFileReader CSVprzeglady = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaPrzegladow.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVprzeglady.ReadRow(row))
                    {
                        int ixM = UstalIndexPoID(1, row[1]);
                        int ixPw = UstalIndexPoID(2, row[2]);
                        int ixK = UstalIndexPoID(3, row[6]);
                        Przeglad nowy;
                        if (row[3] == "")
                        {
                            nowy = new Przeglad(row[7], row[8], row[0], FullList_Maszyny[ixM], FullList_Pracownicy[ixPw], row[4], row[5], FullList_Karty[ixK]);
                        }
                        else
                        {
                            int ixPz = UstalIndexPoID(2, row[3]);
                            nowy = new Przeglad(row[7], row[8], row[0], FullList_Maszyny[ixM], FullList_Pracownicy[ixPw], FullList_Pracownicy[ixPz], row[4], row[5], FullList_Karty[ixK]);
                        }

                        FullList_Przeglady.Add(nowy);
                    }
                }
                using (ReadWriteCsv.CsvFileReader CSVharmonogram = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVharmonogram.ReadRow(row))
                    {
                        int ixM = UstalIndexPoID(1, row[2]);
                        int ixK = UstalIndexPoID(3, row[3]);
                        RekordHarm nowy = new RekordHarm(row[0], row[1], FullList_Maszyny[ixM], FullList_Karty[ixK], row[4], row[5]);
                        FullList_Harmonogram.Add(nowy);
                    }
                }
                using (ReadWriteCsv.CsvFileReader CSVczesci = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaCzesci.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVczesci.ReadRow(row))
                    {
                        Czesc nowy;
                        if ( row.Count ==8) nowy = new Czesc(row[0], row[1], row[2], row[3],row[4], row[5], row[6], row[7]);
                        else  nowy = new Czesc(row[0], row[1], row[2], row[3], row[4], row[5], row[6], "");
                        FullList_Czesci.Add(nowy);
                    }
                }

                using (ReadWriteCsv.CsvFileReader CSVwypisy = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaWypisow.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVwypisy.ReadRow(row))
                    {
                        int ixP = UstalIndexPoID(2, row[6]);
                        Wypis nowy = new Wypis( row[0],row[1], row[2], row[3], row[4], row[5], FullList_Pracownicy[ixP], row[7], row[8], row[9], row[10],row[11]);
                        FullList_Wypisy.Add(nowy);
                    }
                }


                ShowList = new List<CellPostoj>();
                ShowList_Przeglady = new List<Przeglad>();

                ShowList_Indeksy_Postoje = new List<int>();
                ShowList_Indeksy_Przeglady = new List<int>();

                PracownicyNazwiska = new string[FullList_Pracownicy.Count];
                for (int i = 0; i < FullList_Pracownicy.Count; i++) PracownicyNazwiska[i] = FullList_Pracownicy[i].Nazwisko;

                List<string> Prac = new List<string>();
                for (int i = 0; i < FullList_Pracownicy.Count; i++) if (FullList_Pracownicy[i].archiwalne == false) Prac.Add(FullList_Pracownicy[i].Nazwisko);
                PracownicyNazwiska_BezArch = new string[Prac.Count];
                for (int i = 0; i < Prac.Count; i++) PracownicyNazwiska_BezArch[i] = Prac[i];


                List<string> LinieN = new List<string>();
                LinieN.Add(FullList_Maszyny[0].Linia);
                for (int i = 1; i < FullList_Maszyny.Count; i++)
                {
                    string iddd = FullList_Maszyny[i].Linia;
                    bool wgraj = true;
                    foreach (string czy in LinieN) if (czy == iddd) wgraj = false;
                    if (wgraj) LinieN.Add(iddd);
                }
                Linie = new string[LinieN.Count];
                for (int i = 0; i < LinieN.Count; i++) Linie[i] = LinieN[i];

                List<string> LinieN2 = new List<string>();
                bool finde = true;
                int iter = 0;
                while (finde)
                {
                    if (FullList_Maszyny[iter].Archiwalne == false)
                    {
                        LinieN2.Add(FullList_Maszyny[iter].Linia);
                        finde = false;
                    }
                }
                for (int i = iter; i < FullList_Maszyny.Count; i++)
                {
                    string iddd = FullList_Maszyny[i].Linia;
                    bool wgraj = true;
                    foreach (string czy in LinieN2) if (czy == iddd) wgraj = false;
                    if (wgraj && FullList_Maszyny[i].Archiwalne == false) LinieN2.Add(iddd);
                }
                Linie_BezArch = new string[LinieN2.Count];
                for (int i = 0; i < LinieN2.Count; i++) Linie_BezArch[i] = LinieN2[i];

                Nazwy_Kart = new string[FullList_Karty.Count];
                for (int i = 0; i < FullList_Karty.Count; i++) Nazwy_Kart[i] = FullList_Karty[i].nazwa;

                string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
                LogDanych = Convert.ToInt32(log[0]);

                string[] datas = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\DaneProgramu.csv");
                dataaktu = datas[0];
                czesc_aktualizacja_lbl.Text = "Ostatnia aktualizacja: " + dataaktu;
                dataRaportu = datas[1];
                mailing = datas[2];
                dzien_dobry();


                //przygotuj park
                park_okres_Combo.SelectedIndex = 0;
                park_wyswietlaj_Combo.SelectedIndex = 0;
                List<string> lata = new List<string>();
                foreach (CellPostoj P in FullList_Postoje) if (lata.Contains(P.rok) == false) lata.Add(P.rok);
                park_rok_combo.Items.Clear();
                park_rok_combo.Items.AddRange(lata.ToArray());
                park_rok_combo.SelectedIndex = 0;

                zalaczDialog.InitialDirectory = SciezkaDoDanych;



                return true;
            }
            else
            {
                postoje_edytor_Box.Enabled = false;
                postoje_historia_Box.Enabled = false;

                return false;
            }
        }

        public void SprawdzLog()
        {
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int logg = Convert.ToInt32(log[0]);
            if (logg != LogDanych) ZaladujDane();

        }

        private void URAN_Activated(object sender, EventArgs e)
        {
            SprawdzLog();
            WczytajPrzeglady();
            UstalDatyZakresu();
            dzien_dobry();
            Wyswietl();
        }

        void dzien_dobry()
        {
            string[] datas = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\DaneProgramu.csv");
            dataRaportu = datas[1];
            string[] sklad = dataRaportu.Split('.');
            DateTime poru = new DateTime(Convert.ToInt32(sklad[2]), Convert.ToInt32(sklad[1]), Convert.ToInt32(sklad[0]));
            DateTime nal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (nal.CompareTo(poru) > 0 && DateTime.Now.Hour > 7)
            {
                DateTime przedZap = DateTime.Now;
                dataRaportu = przedZap.Day.ToString() + "." + przedZap.Month.ToString() + "." + przedZap.Year.ToString();
                zapiszDaneProgramu();
                string efekt = generuj_raport(poru.Year, poru.Month, poru.Day);
                zrobBackup();
                if (efekt != "fail")
                {
                    string ef2 = efekt.Replace(".docx", ".pdf");
                    string zal = ef2 ;

                    Word2Pdf objWorPdf = new Word2Pdf();
                    objWorPdf.InputLocation = efekt;
                    objWorPdf.OutputLocation = zal;
                    objWorPdf.Word2PdfCOnversion();

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("maintenance.uran@gmail.com", "iwrgiogaxlzqfrgs"),
                        EnableSsl = true,
                    };
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("maintenance.uran@gmail.com"),
                        Subject = "Raport Dobowy " + poru.ToLongDateString(),
                        Body = "<p>Wygenerowano automatycznie z programu URAN</p><p></p><blockquote><h4>[Uran] by Łukasz Garbacz</h4><h4>l.garbacz@green-factory.com</h4><h4>maintenance.uran@gmail.com</h4></blockquote>",
                        IsBodyHtml = true,
                    };
                    mailMessage.To.Add(mailing);
                    var attachment = new Attachment(zal);
                    mailMessage.Attachments.Add(attachment);
                    smtpClient.Send(mailMessage);



                }

            }
        }

        void zapiszDaneProgramu()
        {
            string[] zappp = { dataaktu, dataRaportu, mailing };
            System.IO.File.WriteAllLines(SciezkaDoDanych + @"\DaneProgramu.csv", zappp);
        }

        void zrobBackup()
        {
            string nazw = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            string backpath = SciezkaDoKatalogu + @"\Backup\" + nazw;
            Directory.CreateDirectory(backpath);
            File.Copy(SciezkaDoDanych + @"\DaneProgramu.csv", backpath + @"\DaneProgramu.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaPracownikow.csv", backpath + @"\ListaPracownikow.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaMaszyn.csv", backpath + @"\ListaMaszyn.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaPodzespolow.csv", backpath + @"\ListaPodzespolow.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaPostojow.csv", backpath + @"\ListaPostojow.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaPrzegladow.csv", backpath + @"\ListaPrzegladow.csv", true);
            File.Copy(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv", backpath + @"\HarmonogramPrzegladow.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaKart.csv", backpath + @"\ListaKart.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaWypisow.csv", backpath + @"\ListaWypisow.csv", true);
            File.Copy(SciezkaDoDanych + @"\ListaCzesci.csv", backpath + @"\ListaCzesci.csv", true);
            File.Copy(SciezkaDoDanych + @"\Log.txt", backpath + @"\Log.txt", true);

        }

        string wyczysc(string podaj)
        {
            char[] cha = { '\n', '\t' };
            string st1 = podaj.Replace(';', ':');
            string st2 = st1.Replace('"', '\'');
            string st3 = st2.Replace('\n', '>');
            string st4 = st3.Replace('\t', ' ');
            string st5 = st4.Trim(cha);

            //string st6 = st5.
            return st5;
        }

        void EksportKartDoDocx()
        {
            foreach (Maszyna Ms in FullList_Maszyny)
            {
                //Maszyna Ms = FullList_Maszyny[13];
                if (Ms.Karta != "")
                {
                    int finde = 0;
                    foreach (Karta KKK in FullList_Karty)
                    {
                        if (KKK.nazwa == Ms.Karta)
                        {
                            finde = FullList_Karty.IndexOf(KKK);
                            break;
                        }
                    }
                    Karta K = FullList_Karty[finde];
                    string szablon = SciezkaDoKatalogu + @"\templates\template_pEx.docx";
                    string naz = K.nazwa;
                    string zapis = SciezkaDoKatalogu + @"\Eksport\" + naz + ".docx";
                    var doc = DocX.Load(szablon);

                    Xceed.Document.NET.Formatting ff = new Xceed.Document.NET.Formatting();
                    ff.Size = 10D;
                    ff.Bold = true;

                    Xceed.Document.NET.Formatting ffs = new Xceed.Document.NET.Formatting();
                    ffs.Size = 14D;
                    ffs.Bold = true;


                    Table t = doc.Tables[0];

                    t.Rows[0].Cells[1].Paragraphs.First().Append(K.nazwa, ffs);
                    t.Rows[1].Cells[1].Paragraphs.First().Append(Ms.Linia, ffs);
                    t.Rows[2].Cells[1].Paragraphs.First().Append(Ms.NazwaWys, ffs);
                    t.Rows[3].Cells[1].Paragraphs.First().Append(Ms.Nazwa, ffs);

                    Xceed.Document.NET.Formatting fR = new Xceed.Document.NET.Formatting();
                    fR.Size = 12D;
                    fR.Bold = false;
                    Xceed.Document.NET.Formatting fB = new Xceed.Document.NET.Formatting();
                    fB.Size = 12D;
                    fB.Bold = true;



                    Table t2 = doc.AddTable(1, 4);
                    var szer = new float[] { 35, 300f, 110f, 270f };
                    t2.SetWidths(szer);
                    t2.Alignment = Alignment.left;


                    t2.Rows[0].Cells[0].Paragraphs.First().Append("L.p.", ffs);
                    t2.Rows[0].Cells[1].Paragraphs.First().Append("Opis czynności kontrolnej", ffs);
                    t2.Rows[0].Cells[2].Paragraphs.First().Append("Wynik", ffs);
                    t2.Rows[0].Cells[3].Paragraphs.First().Append("Uwagi/zalecenia pokontrolne", ffs);

                    int lp = 1;
                    foreach (List<string> row in K.tabelka)
                    {
                        var r = t2.InsertRow();
                        r.Cells[0].Paragraphs.First().Append(lp.ToString() + ".", fB);
                        r.Cells[1].Paragraphs.First().Append(row[1], fB);
                        for (int i = 2; i < row.Count; i++)
                        {
                            r.Cells[1].InsertParagraph();
                            r.Cells[1].Paragraphs[i - 1].Append(row[i], fR);
                        }

                        lp++;
                    }

                    doc.InsertTable(t2);
                    doc.SaveAs(zapis);
                }
            }
        }

        public int UstalIndexPoID(int lista, string _ID)
        {
            int index = -1;
            if (lista == 1)
            {
                foreach (Maszyna M in FullList_Maszyny)
                {
                    if (M.ID == _ID)
                    {
                        index = FullList_Maszyny.IndexOf(M);
                        break;
                    }
                }
                return index;
            }
            else if (lista == 2)
            {
                foreach (Pracownik P in FullList_Pracownicy)
                {
                    if (P.ID == _ID)
                    {
                        index = FullList_Pracownicy.IndexOf(P);
                        break;
                    }
                }
                return index;
            }
            else if (lista == 3)
            {
                foreach (Karta K in FullList_Karty)
                {
                    if (K.nazwa == _ID)
                    {
                        index = FullList_Karty.IndexOf(K);
                        break;
                    }
                }
                return index;
            }
            else return -1;
        }

        int UstalIndexMaszynyPoCombo(string _linia, string _maszyna)
        {
            int index = -1;
            foreach (Maszyna M in FullList_Maszyny)
            {
                if (M.Linia == _linia && M.NazwaWys == _maszyna)
                {
                    index = FullList_Maszyny.IndexOf(M);
                    break;
                }
            }
            return index;
        }

        int UstalIndexPracownikaPoCombo(string _nazwisko)
        {
            int index = -1;
            foreach (Pracownik P in FullList_Pracownicy)
            {
                if (P.Nazwisko == _nazwisko)
                {
                    index = FullList_Pracownicy.IndexOf(P);
                    break;
                }
            }
            return index;
        }



        // Postoje


        public void UstalDatyZakresu()
        {
            DateTime teraz = DateTime.Now;
            if(TrybPracy) nowy_postoj_data_DTPicker.MaxDate = new DateTime(teraz.Year, teraz.Month, teraz.Day, 23, 59, 59);
            if(TrybPracy) nowy_postoj_data_DTPicker.Value = new DateTime(teraz.Year, teraz.Month, teraz.Day);
            if (FullList_Postoje.Count > 0)
            {
                postoje_historia_Od_DTPicker.MinDate = FullList_Postoje[FullList_Postoje.Count - 1].Make_data();
                postoje_historia_Od_DTPicker.MaxDate = FullList_Postoje[0].Make_data();
                postoje_historia_Od_DTPicker.Value = FullList_Postoje[FullList_Postoje.Count - 1].Make_data();
                postoje_historia_Do_DTPicker.MinDate = FullList_Postoje[FullList_Postoje.Count - 1].Make_data();
                postoje_historia_Do_DTPicker.MaxDate = FullList_Postoje[0].Make_data();
                postoje_historia_Do_DTPicker.Value = FullList_Postoje[0].Make_data();
            }
            if (admin)
            {
                postoje_historia_filtry_Box.Height = 275;
                postoje_historia_filtry_wprowadzil_checkBox.Visible = true;
                postoje_historia_filtry_wprowadzil_checkBox.Checked = false;
                postoje_historia_filtry_wprowadzil_lbl.Visible = true;
                postoje_historia_zatwierdz_btn.Visible = true;

            }
            else
            {
                postoje_historia_filtry_Box.Height = 225;
                postoje_historia_filtry_wprowadzil_checkBox.Visible = false;
                postoje_historia_filtry_wprowadzil_checkBox.Checked = false;
                postoje_historia_filtry_wprowadzil_lbl.Visible = false;
                postoje_historia_zatwierdz_btn.Visible = false;
            }

            Raport_dobowy_dateTimePicker.Value = teraz;

        }

        public void Wyswietl()
        {
            DateTime najnowsze = DateTime.Today.AddDays(-1);
            DateTime tydzien = DateTime.Now.AddDays(-7);
            DateTime miesiac = DateTime.Now.AddDays(-30);
            DateTime Od = new DateTime(postoje_historia_Od_DTPicker.Value.Year, postoje_historia_Od_DTPicker.Value.Month, postoje_historia_Od_DTPicker.Value.Day);
            DateTime Do = new DateTime(postoje_historia_Do_DTPicker.Value.Year, postoje_historia_Do_DTPicker.Value.Month, postoje_historia_Do_DTPicker.Value.Day);

            int skrol = tabelka_postojow.FirstDisplayedScrollingRowIndex;
            int toten = 0;
            if (tabelka_postojow.Rows.Count > 0 && tabelka_postojow.SelectedRows.Count > 0) toten = tabelka_postojow.SelectedRows[0].Index;
            tabelka_postojow.ScrollBars = ScrollBars.None;

                ShowList.Clear();
            ShowList_Indeksy_Postoje.Clear();

            foreach (CellPostoj element in FullList_Postoje)
            {
                bool WarunekDaty = false;
                bool WarunekLinii = false;
                bool WarunekMaszyny = false;
                bool WarunekPracownika = false;
                bool WarunekMycia = false;
                bool WarunekDzialania = false;
                bool WarunekTypu = false;

                if (indeks_wyswietlania == 0 && element.Make_data().CompareTo(najnowsze) > 0) WarunekDaty = true;
                if (indeks_wyswietlania == 1 && element.Make_data().CompareTo(tydzien) > 0) WarunekDaty = true;
                if (indeks_wyswietlania == 2 && element.Make_data().CompareTo(miesiac) > 0) WarunekDaty = true;
                if (indeks_wyswietlania == 3) WarunekDaty = true;
                if (indeks_wyswietlania == 4 && element.Make_data2().CompareTo(Do) <= 0 && element.Make_data2().CompareTo(Od) >= 0) WarunekDaty = true;
                if (postoje_historia_filtry_linia_checkBox.Checked)
                {
                    if (postoje_historia_filtry_linia_combo.Text == element.maszyna.Linia) WarunekLinii = true;
                }
                else WarunekLinii = true;
                if (postoje_historia_filtry_maszyna_checkBox.Checked)
                {
                    if (postoje_historia_filtry_maszyna_combo.Text == element.maszyna.NazwaWys) WarunekMaszyny = true;
                }
                else WarunekMaszyny = true;
                if (postoje_historia_filtry_wprowadzil_checkBox.Checked)
                {
                    if (postoje_historia_filtry_wprowadzil_combo.Text == element.pracownik.Nazwisko) WarunekPracownika = true;
                }
                else WarunekPracownika = true;
                if (postoje_historia_filtry_mycie_checkBox.Checked)
                {
                    if (postoje_historia_filtry_mycie_combo.SelectedIndex == element.mycieInt) WarunekMycia = true;
                }
                else WarunekMycia = true;
                if (postoje_historia_filtry_dzialanie_checkBox.Checked)
                {
                    if (postoje_historia_filtry_dzialanie_combo.Text == element.dzialanie) WarunekDzialania = true;
                }
                else WarunekDzialania = true;
                if (postoje_historia_filtry_typ_checkBox.Checked)
                {
                    if (postoje_historia_filtry_typ_combo.Text == element.typ) WarunekTypu = true;
                }
                else WarunekTypu = true;

                if (WarunekDaty && WarunekLinii && WarunekMaszyny && WarunekPracownika && WarunekMycia && WarunekDzialania && WarunekTypu)
                {
                    ShowList.Add(element);
                    ShowList_Indeksy_Postoje.Add(FullList_Postoje.IndexOf(element));
                }

            }



            tabelka_postojow.Rows.Clear();
            object[] wiersz = new object[6];
            foreach (CellPostoj element in ShowList)
            {
                string ataczment = "";
                if (element.Ufiles.Count > 0) ataczment = "                                                             [Załączników: " + element.Ufiles.Count.ToString()+"]" + "\n";
                ataczment = ataczment + element.przyczyna;

                wiersz[0] = element.niceData + "\n" + element.tStartStr + " - " + element.tStopStr + "\n" + "\n" + element.pracownik.Imie + " " + element.pracownik.Nazwisko;            
                wiersz[2] = element.maszyna.Linia + "\n"+ element.maszyna.NazwaWys + "\n"+element.podzespol;
                wiersz[3] = ataczment;
                wiersz[4] = "Postój: " + "\n" + "Mycie: " + "\n" + "Działanie: " + "\n" +"Uwaga: ";
                wiersz[5] = element.tPostojStr + "\n" + element.mycie + "\n" + element.dzialanie + "\n" +element.typ;
               

                tabelka_postojow.Rows.Add(wiersz);



            }
            if (ShowList.Count == 0)
            {
                postoje_historia_edytuj_btn.Enabled = false;
                postoje_historia_eksportuj_btn.Enabled = false;

            }
            else
            {
                postoje_historia_edytuj_btn.Enabled = true;
                postoje_historia_eksportuj_btn.Enabled = true;

            }
            tabelka_postojow.ScrollBars = ScrollBars.Vertical;
            if (tabelka_postojow.Rows.Count> skrol  && skrol != -1) tabelka_postojow.FirstDisplayedScrollingRowIndex = skrol;
            if (tabelka_postojow.Rows.Count > toten) tabelka_postojow.Rows[toten].Selected = true;

           
            if (WyswPoDodaniu != -1)
            {
                if (!ShowList_Indeksy_Postoje.Contains(WyswPoDodaniu)) postoje_historia_wyswietlaj_combo.SelectedIndex = 3;
            }
            /*
           foreach (CellPostoj element in ShowList)
           {
               if (element.mycieInt == 1)
               {
                   int ind = ShowList.IndexOf(element);
                   tabelka_postojow.Rows[ind].DefaultCellStyle.BackColor = System.Drawing.Color.Pink;
               }
           }*/
            /*
            postoj_filtry_pokaz_btn.Text = "Pokaż";
            Point ruk = new Point(982, 483);
            postoje_historia_filtry_Box.Location = ruk;
            Size wil = new Size(264, 51);
            postoje_historia_filtry_Box.Size = wil;*/

            GrupboksUstaw();
            WczytajPark2();
            ustalListy();
            WyswietlCz();
        }

        public void Ustal_wyswietlanie()
        {
            DateTime najnowsze = DateTime.Today.AddDays(-1);
            DateTime tydzien = DateTime.Now.AddDays(-7);
            DateTime miesiac = DateTime.Now.AddDays(-30);
            DateTime Od = new DateTime(postoje_historia_Od_DTPicker.Value.Year, postoje_historia_Od_DTPicker.Value.Month, postoje_historia_Od_DTPicker.Value.Day);
            DateTime Do = new DateTime(postoje_historia_Do_DTPicker.Value.Year, postoje_historia_Do_DTPicker.Value.Month, postoje_historia_Do_DTPicker.Value.Day);

            int najn = 0, tydz = 0, mies = 0, wszystko = 0, zakres = 0;
            foreach (CellPostoj post in FullList_Postoje)
            {
                bool WarunekMycia = false;
                bool WarunekLinii = false;
                bool WarunekMaszyny = false;
                bool WarunekPracownika = false;
                bool WarunekDzialania = false;
                bool WarunekTypu = false;
                bool wyrok = false;


                if (postoje_historia_filtry_dzialanie_checkBox.Checked)
                {
                    if (postoje_historia_filtry_dzialanie_combo.Text == post.dzialanie) WarunekDzialania = true;
                }
                else WarunekDzialania = true;
                if (postoje_historia_filtry_typ_checkBox.Checked)
                {
                    if (postoje_historia_filtry_typ_combo.Text == post.typ) WarunekTypu = true;
                }
                else WarunekTypu = true;
                if (postoje_historia_filtry_mycie_checkBox.Checked)
                {
                    if (postoje_historia_filtry_mycie_combo.SelectedIndex == post.mycieInt) WarunekMycia = true;
                }
                else WarunekMycia = true;
                if (postoje_historia_filtry_linia_checkBox.Checked)
                {
                    if (postoje_historia_filtry_linia_combo.Text == post.maszyna.Linia) WarunekLinii = true;
                }
                else WarunekLinii = true;
                if (postoje_historia_filtry_maszyna_checkBox.Checked)
                {
                    if (postoje_historia_filtry_maszyna_combo.Text == post.maszyna.NazwaWys) WarunekMaszyny = true;
                }
                else WarunekMaszyny = true;
                if (postoje_historia_filtry_wprowadzil_checkBox.Checked)
                {
                    if (postoje_historia_filtry_wprowadzil_combo.Text == post.pracownik.Nazwisko) WarunekPracownika = true;
                }
                else WarunekPracownika = true;

                if (WarunekLinii && WarunekMaszyny && WarunekPracownika && WarunekMycia && WarunekDzialania && WarunekTypu) wyrok = true;

                if (post.Make_data().CompareTo(najnowsze) > 0 && wyrok) najn++;
                if (post.Make_data().CompareTo(tydzien) > 0 && wyrok) tydz++;
                if (post.Make_data().CompareTo(miesiac) > 0 && wyrok) mies++;
                if (wyrok) wszystko++;
                if (post.Make_data2().CompareTo(Do) <= 0 && post.Make_data2().CompareTo(Od) >= 0 && wyrok) zakres++;

            }


            string[] opcje_wysw = new string[5];

            opcje_wysw[0] = "Najnowsze (" + najn.ToString() + ")";
            opcje_wysw[1] = "Ostatni tydzień (" + tydz.ToString() + ")";
            opcje_wysw[2] = "Ostatni miesiąc (" + mies.ToString() + ")";
            opcje_wysw[3] = "Wszystko (" + wszystko.ToString() + ")";
            opcje_wysw[4] = "Zakres (" + zakres.ToString() + ")";
            postoje_historia_wyswietlaj_combo.Items.Clear();
            postoje_historia_wyswietlaj_combo.Items.AddRange(opcje_wysw);
            postoje_historia_wyswietlaj_combo.SelectedIndex = indeks_wyswietlania;

            if (postoj_filtry_pokaz_btn.Text == "Pokaż")
            {
                Point ruk = new Point(982, 483);
                postoje_historia_filtry_Box.Location = ruk;
                Size wil = new Size(264, 51);
                postoje_historia_filtry_Box.Size = wil;
            }
        }

        public void ZapiszPostojeDoPliku()
        {

            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaPostojow.csv"))
            {

                foreach (CellPostoj P in FullList_Postoje)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.Add(P.maszyna.ID);
                    row.Add(P.dataStr);
                    row.Add(P.tStartStr);
                    row.Add(P.tStopStr);
                    row.Add(P.tPostojInt.ToString());
                    row.Add(wyczysc(P.przyczyna));
                    row.Add(P.pracownik.ID);
                    row.Add(P.mycieInt.ToString());
                    row.Add(P.podzespol);
                    row.Add(P.dzialanie);
                    row.Add(P.typ);
                    row.Add(P.pliki);
                    writer.WriteRow(row);
                }
            }
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
            UstalDatyZakresu();
            Ustal_wyswietlanie();


        }

        private void SprawdzNowyPostoj()
        {
            bool godzina;
            int SH = nowy_postoj_tStart_DTPicker.Value.Hour;
            int SM = nowy_postoj_tStart_DTPicker.Value.Minute;
            int KH = nowy_postoj_tStop_DTPicker.Value.Hour;
            int KM = nowy_postoj_tStop_DTPicker.Value.Minute;
            if (SH == 0 && SM == 0 && KH == 0 && KM == 0) godzina = false;
            else godzina = true;

            if (nowy_postoj_wprowadzil_combo.Text.Length != 0 && nowy_postoj_linia_combo.Text.Length != 0 && nowy_postoj_maszyna_combo.Text.Length != 0 && nowy_postoj_przyczyna_textBox.Text.Length != 0 && godzina && torba_CheckBox.Checked)
            {
                nowy_postoj_dodaj_btn.Enabled = true;
            }
            else nowy_postoj_dodaj_btn.Enabled = false;
        }

        void GrupboksUstaw()
        {
            if (postoj_filtry_pokaz_btn.Text == "Ukryj")
            {
                if (admin)
                {
                    Point ruk = new Point(982, 217);
                    postoje_historia_filtry_Box.Location = ruk;
                    Size wil = new Size(264, 317);
                    postoje_historia_filtry_Box.Size = wil;
                }
                else
                {
                    Point ruk = new Point(982, 277);
                    postoje_historia_filtry_Box.Location = ruk;
                    Size wil = new Size(264, 257);
                    postoje_historia_filtry_Box.Size = wil;
                }
            }
            else
            {
                Point ruk = new Point(982, 483);
                postoje_historia_filtry_Box.Location = ruk;
                Size wil = new Size(264, 51);
                postoje_historia_filtry_Box.Size = wil;
            }
        }

        private void nowy_postoj_dodaj_btn_Click(object sender, EventArgs e)
        {

            SprawdzLog();
            string[] tekst = nowy_postoj_przyczyna_textBox.Lines;
            string bezenterow = "";
            foreach (string line in tekst) bezenterow = bezenterow + line + " ";

            int czas_postoju = nowy_postoj_tPostoj_DTPicker.Value.Hour * 60 + nowy_postoj_tPostoj_DTPicker.Value.Minute;
            int IxM = UstalIndexMaszynyPoCombo(nowy_postoj_linia_combo.Text, nowy_postoj_maszyna_combo.Text);
            int IxP = UstalIndexPracownikaPoCombo(nowy_postoj_wprowadzil_combo.Text);

            int dziennn = nowy_postoj_data_DTPicker.Value.Day;
            string dzi;
            if (dziennn < 10)
            {
                dzi = "0" + dziennn.ToString();
            }
            else dzi = dziennn.ToString();

            int miesss = nowy_postoj_data_DTPicker.Value.Month;
            string mie;
            if (miesss < 10)
            {
                mie = "0" + miesss.ToString();
            }
            else mie = miesss.ToString();

            string dat = nowy_postoj_data_DTPicker.Value.Year.ToString() + "-" + mie + "-" + dzi;
            string mycko;
            if (mycie_zlecono_check.Checked == true) mycko = "1";
            else mycko = "0";

            string ss = "";
            foreach (ListViewItem L in zalaczniki_ListView.Items)
            {
                if (!Directory.Exists(SciezkaDoKatalogu + @"\Archiwum\" + FullList_Maszyny[IxM].Linia)) Directory.CreateDirectory(SciezkaDoKatalogu + @"\Archiwum\" + FullList_Maszyny[IxM].Linia);
                if (!Directory.Exists(SciezkaDoKatalogu + @"\Archiwum\" + FullList_Maszyny[IxM].Linia + @"\" + FullList_Maszyny[IxM].NazwaWys)) Directory.CreateDirectory(SciezkaDoKatalogu + @"\Archiwum\" + FullList_Maszyny[IxM].Linia + @"\" + FullList_Maszyny[IxM].NazwaWys);
                if (!File.Exists(SciezkaDoKatalogu + @"\Archiwum\" + FullList_Maszyny[IxM].Linia + @"\" + FullList_Maszyny[IxM].NazwaWys + @"\" + L.Text)) File.Copy(L.ImageKey.ToString(), SciezkaDoKatalogu + @"\Archiwum\"+ FullList_Maszyny[IxM].Linia +@"\"+ FullList_Maszyny[IxM].NazwaWys+ @"\" + L.Text,true);
                L.ImageKey = SciezkaDoKatalogu + @"\Archiwum\" + FullList_Maszyny[IxM].Linia + @"\" + FullList_Maszyny[IxM].NazwaWys + @"\" + L.Text;

                

                ss = ss + L.ImageKey.ToString();
                if (zalaczniki_ListView.Items.IndexOf(L) < zalaczniki_ListView.Items.Count) ss = ss + "*";
            }

            CellPostoj nowy = new CellPostoj(
                    FullList_Maszyny[IxM],
                    dat,
                    nowy_postoj_tStart_DTPicker.Value.ToShortTimeString(),
                    nowy_postoj_tStop_DTPicker.Value.ToShortTimeString(),
                    czas_postoju.ToString(),
                    bezenterow,
                    FullList_Pracownicy[IxP],
                    mycko,
                    nowy_postoj_podzespol_combo.Text,
                    nowy_postoj_dzialanie_combo.Text,
                    nowy_postoj_typ_combo.Text,
                    ss
                    ); 
           



            if (TrybPracy == false) FullList_Postoje.RemoveAt(Editindeks);

            int insertuj = 0;
            bool end = true;
            while (end)
            {
                if (FullList_Postoje.Count == 0) end = false;
                else if (nowy.Make_data() >= FullList_Postoje[insertuj].Make_data()) end = false;
                else if (insertuj == FullList_Postoje.Count - 1)
                {
                    end = false;
                    insertuj++;
                }
                else insertuj++;
            }
            FullList_Postoje.Insert(insertuj, nowy);


            postoje_historia_filtry_linia_checkBox.Checked = false;
            postoje_historia_filtry_wprowadzil_checkBox.Checked = false;

            if (TrybPracy == false)
            {
                postoje_edytor_Box.Text = "Dodaj postój";
                tabelka_postojow.Enabled = true;
                postoje_historia_wyswietlaj_combo.Enabled = true;
                postoje_historia_filtry_linia_checkBox.Enabled = true;
                if (postoje_historia_filtry_linia_checkBox.Checked) postoje_historia_filtry_maszyna_checkBox.Enabled = true;
                postoje_historia_filtry_wprowadzil_checkBox.Enabled = true;
                postoje_historia_filtry_linia_combo.Enabled = true;
                postoje_historia_filtry_maszyna_combo.Enabled = true;
                postoje_historia_filtry_wprowadzil_combo.Enabled = true;
                postoje_historia_Od_DTPicker.Enabled = true;
                postoje_historia_Do_DTPicker.Enabled = true;
                postoje_historia_edytuj_btn.Enabled = true;
                postoje_historia_eksportuj_btn.Enabled = true;
                nowy_postoj_usun_btn.Visible = false;
                nowy_postoj_dodaj_btn.Text = "Dodaj";
                TrybPracy = true;
            }
            WyswPoDodaniu = insertuj;
            Wyswietl();
            ZapiszPostojeDoPliku();

            DateTime teraz = DateTime.Now;
            nowy_postoj_data_DTPicker.MaxDate = new DateTime(teraz.Year, teraz.Month, teraz.Day, 23, 59, 59);
            nowy_postoj_data_DTPicker.Value = DateTime.Today;
            nowy_postoj_linia_combo.SelectedIndex = -1;
            nowy_postoj_maszyna_combo.SelectedIndex = -1;
            nowy_postoj_wprowadzil_combo.SelectedIndex = -1;
            nowy_postoj_podzespol_combo.SelectedIndex = -1;
            nowy_postoj_dzialanie_combo.SelectedIndex = -1;
            nowy_postoj_typ_combo.SelectedIndex = -1;
            nowy_postoj_podzespol_combo.Enabled = false;
            nowy_postoj_maszyna_combo.Enabled = false;
            nowy_postoj_podzespol_lbl.Enabled = false;
            nowy_postoj_maszyna_lbl.Enabled = false;
            nowy_postoj_tStart_DTPicker.Value = DateTime.Today;
            nowy_postoj_tStop_DTPicker.Value = DateTime.Today;
            nowy_postoj_tPostoj_DTPicker.Value = DateTime.Today;
            nowy_postoj_przyczyna_textBox.Text = "";
            mycie_zlecono_check.Checked = false;
            mycie_zlecono_check.Enabled = true;
            zalaczniki_ListView.Items.Clear();
            torba_CheckBox.Checked = false;
            SprawdzNowyPostoj();

            int zazn = ShowList_Indeksy_Postoje.IndexOf(WyswPoDodaniu);
            tabelka_postojow.Rows[zazn].Selected = true;
            WyswPoDodaniu = -1;

            pliki_ListView.Visible = true;

        }

        private void nowy_postoj_usun_btn_Click(object sender, EventArgs e)
        {
            SprawdzLog();
            FullList_Postoje.RemoveAt(Editindeks);
            postoje_edytor_Box.Text = "Dodaj postój";
            tabelka_postojow.Enabled = true;
            postoje_historia_wyswietlaj_combo.Enabled = true;
            postoje_historia_filtry_linia_checkBox.Enabled = true;
            if (postoje_historia_filtry_linia_checkBox.Checked) postoje_historia_filtry_maszyna_checkBox.Enabled = true;
            postoje_historia_filtry_wprowadzil_checkBox.Enabled = true;
            postoje_historia_filtry_linia_combo.Enabled = true;
            postoje_historia_filtry_maszyna_combo.Enabled = true;
            postoje_historia_filtry_wprowadzil_combo.Enabled = true;
            postoje_historia_Od_DTPicker.Enabled = true;
            postoje_historia_Do_DTPicker.Enabled = true;
            postoje_historia_edytuj_btn.Enabled = true;
            postoje_historia_eksportuj_btn.Enabled = true;
            nowy_postoj_usun_btn.Visible = false;
            nowy_postoj_dodaj_btn.Text = "Dodaj";
            nowy_postoj_data_DTPicker.Value = DateTime.Today;
            nowy_postoj_linia_combo.SelectedIndex = -1;
            nowy_postoj_maszyna_combo.SelectedIndex = -1;
            nowy_postoj_wprowadzil_combo.SelectedIndex = -1;
            nowy_postoj_dzialanie_combo.SelectedIndex = -1;
            nowy_postoj_typ_combo.SelectedIndex = -1;
            nowy_postoj_podzespol_combo.SelectedIndex = -1;
            nowy_postoj_podzespol_combo.Enabled = false;
            nowy_postoj_maszyna_combo.Enabled = false;
            nowy_postoj_tStart_DTPicker.Value = DateTime.Today;
            nowy_postoj_tStop_DTPicker.Value = DateTime.Today;
            nowy_postoj_tPostoj_DTPicker.Value = DateTime.Today;
            nowy_postoj_przyczyna_textBox.Text = "";
            mycie_zlecono_check.Checked = false;
            mycie_zlecono_check.Enabled = true;
            SprawdzNowyPostoj();
            TrybPracy = true;
            Wyswietl();
            ZapiszPostojeDoPliku();

        }

        private void postoje_historia_edytuj_btn_Click(object sender, EventArgs e)
        {
            TrybPracy = false;
            nowy_postoj_data_DTPicker.Value = FullList_Postoje[Editindeks].Make_data();
            nowy_postoj_linia_combo.SelectedItem = FullList_Postoje[Editindeks].maszyna.Linia;
            nowy_postoj_maszyna_combo.SelectedIndex = nowy_postoj_maszyna_combo.Items.IndexOf(FullList_Postoje[Editindeks].maszyna.NazwaWys);
            nowy_postoj_podzespol_combo.SelectedItem = FullList_Postoje[Editindeks].podzespol;
            nowy_postoj_dzialanie_combo.SelectedItem = FullList_Postoje[Editindeks].dzialanie;
            nowy_postoj_typ_combo.SelectedItem = FullList_Postoje[Editindeks].typ;
            nowy_postoj_tStart_DTPicker.Value = FullList_Postoje[Editindeks].Make_tStart();
            nowy_postoj_tStop_DTPicker.Value = FullList_Postoje[Editindeks].Make_tStop();
            nowy_postoj_tPostoj_DTPicker.Value = FullList_Postoje[Editindeks].Make_tPostoj();
            nowy_postoj_przyczyna_textBox.Text = FullList_Postoje[Editindeks].przyczyna;
            nowy_postoj_wprowadzil_combo.SelectedIndex = nowy_postoj_wprowadzil_combo.Items.IndexOf(FullList_Postoje[Editindeks].pracownik.Nazwisko);
            torba_CheckBox.Checked = true;

            if (FullList_Postoje[Editindeks].mycieInt == 0)
            {
                mycie_zlecono_check.Checked = false;
                mycie_zlecono_check.Enabled = true;
            }
            else if (FullList_Postoje[Editindeks].mycieInt == 1)
            {
                mycie_zlecono_check.Checked = true;
                mycie_zlecono_check.Enabled = true;
            }
            else
            {
                mycie_zlecono_check.Checked = true;
                mycie_zlecono_check.Enabled = false;
            }
            zalaczniki_ListView.Items.Clear();
            foreach (string S in FullList_Postoje[Editindeks].Ufiles)
            {
                string[] dziel = S.Split('\\');
                string klucz = dziel[dziel.Length - 1];
                zalaczniki_ListView.Items.Add(klucz, S);
            }



            tabelka_postojow.Enabled = false;

            postoje_historia_wyswietlaj_combo.Enabled = false;
            postoje_historia_filtry_linia_checkBox.Enabled = false;
            postoje_historia_filtry_maszyna_checkBox.Enabled = false;
            postoje_historia_filtry_wprowadzil_checkBox.Enabled = false;
            postoje_historia_filtry_linia_combo.Enabled = false;
            postoje_historia_filtry_maszyna_combo.Enabled = false;
            postoje_historia_filtry_wprowadzil_combo.Enabled = false;
            postoje_historia_Od_DTPicker.Enabled = false;
            postoje_historia_Do_DTPicker.Enabled = false;
            postoje_historia_edytuj_btn.Enabled = false;
            postoje_historia_eksportuj_btn.Enabled = false;
            nowy_postoj_usun_btn.Visible = true;
            nowy_postoj_dodaj_btn.Text = "Zapisz";
            postoje_edytor_Box.Text = "Edytuj postój";
           

            postoje_pictureBox.ImageLocation = "";
            postoje_pictureBox.Visible = false;
            pliki_ListView.Visible = false;
        }

        private void postoje_historia_eksportuj_btn_Click(object sender, EventArgs e)
        {
            Eksport_Dialog.ShowDialog();
        }

        private void Eksport_Dialog_FileOk(object sender, CancelEventArgs e)
        {
            string myStream;
            myStream = Eksport_Dialog.FileName;
            string[] spraw = myStream.Split('\\');
            string[] spraw2 = spraw[spraw.Length - 1].Split('.');

            if (spraw2[0] != "" && spraw2[1] == "docx")
            {
                string fileName = myStream;

                string szablon = SciezkaDoKatalogu + @"\templates\template_pos.docx";
                var doc = DocX.Load(szablon);

                Xceed.Document.NET.Formatting ff = new Xceed.Document.NET.Formatting();
                ff.Size = 9D;
                ff.StyleName = "Normalny";



                Table t = doc.AddTable(1, 8);
                t.Alignment = Alignment.center;
                t.Design = TableDesign.LightShading;

                var szer = new float[] { 50f, 55f, 58f, 80f, 95f, 195f, 85f, 59f };
                t.SetWidths(szer);


                t.Rows[0].Cells[0].Paragraphs.First().Append("Data", ff);
                t.Rows[0].Cells[1].Paragraphs.First().Append("Praca", ff);
                t.Rows[0].Cells[2].Paragraphs.First().Append("Postój", ff);
                t.Rows[0].Cells[3].Paragraphs.First().Append("Linia", ff);
                t.Rows[0].Cells[4].Paragraphs.First().Append("Maszyna", ff);
                t.Rows[0].Cells[5].Paragraphs.First().Append("Przyczyna", ff);
                t.Rows[0].Cells[6].Paragraphs.First().Append("Wprowadził", ff);
                t.Rows[0].Cells[7].Paragraphs.First().Append("Mycie*", ff);

                int tRazem = 0;

                foreach (CellPostoj p in ShowList)
                {
                    tRazem = tRazem + p.tPostojInt;

                    var r = t.InsertRow(1);
                    r.Cells[0].Paragraphs.First().Append(p.dataExp, ff);
                    r.Cells[1].Paragraphs.First().Append(p.tStartStr + " - " + p.tStopStr, ff);
                    r.Cells[2].Paragraphs.First().Append(p.tPostojInt.ToString(), ff);
                    r.Cells[3].Paragraphs.First().Append(p.maszyna.Linia, ff);
                    r.Cells[4].Paragraphs.First().Append(p.maszyna.NazwaWys, ff);
                    r.Cells[5].Paragraphs.First().Append(p.przyczyna, ff);
                    r.Cells[6].Paragraphs.First().Append(p.pracownik.Imie + "\n" + p.pracownik.Nazwisko, ff);
                    string skrot;
                    if (p.mycieInt == 0) skrot = "nZ";
                    else if (p.mycieInt == 1) skrot = "Z";
                    else skrot = "W";
                    r.Cells[7].Paragraphs.First().Append(skrot, ff);
                }

                doc.InsertParagraph("\tData:", false, ff);
                doc.InsertParagraph("\t\tOd:\t\t" + ShowList[ShowList.Count - 1].dataStr, false, ff);
                doc.InsertParagraph("\t\tDo:\t\t" + ShowList[0].dataStr, false, ff);
                bool napisz = false;
                string mycieEXP = "\t\tMycie:\t\t";
                string dzialanieEXP = "\t\tDziałanie:\t\t";
                string typEXP = "\t\tTyp:\t\t";
                string liniaEXP = "\t\tLinia:\t\t";
                string MaszynaEXP = "\t\tMaszyna:\t\t";
                string PracownikEXP = "\t\tWprowadził:\t";
                doc.InsertParagraph("\tFiltry:", false, ff);


                if (postoje_historia_filtry_dzialanie_checkBox.Checked)
                {
                    dzialanieEXP = dzialanieEXP + postoje_historia_filtry_dzialanie_combo.SelectedItem;
                    doc.InsertParagraph(dzialanieEXP, false, ff);
                    napisz = true;
                }
                if (postoje_historia_filtry_typ_checkBox.Checked)
                {
                    typEXP = typEXP + postoje_historia_filtry_typ_combo.SelectedItem;
                    doc.InsertParagraph(typEXP, false, ff);
                    napisz = true;
                }
                if (postoje_historia_filtry_mycie_checkBox.Checked)
                {
                    mycieEXP = mycieEXP + postoje_historia_filtry_mycie_combo.SelectedItem;
                    doc.InsertParagraph(mycieEXP, false, ff);
                    napisz = true;
                }
                if (postoje_historia_filtry_linia_checkBox.Checked)
                {
                    liniaEXP = liniaEXP + postoje_historia_filtry_linia_combo.SelectedItem;
                    doc.InsertParagraph(liniaEXP, false, ff);
                    napisz = true;
                }

                if (postoje_historia_filtry_maszyna_checkBox.Checked)
                {

                    MaszynaEXP = MaszynaEXP + postoje_historia_filtry_maszyna_combo.SelectedItem;
                    doc.InsertParagraph(MaszynaEXP, false, ff);
                    napisz = true;
                }

                if (postoje_historia_filtry_wprowadzil_checkBox.Checked)
                {
                    PracownikEXP = PracownikEXP + postoje_historia_filtry_wprowadzil_combo.SelectedItem;
                    doc.InsertParagraph(PracownikEXP, false, ff);
                    napisz = true;
                }

                if (!napisz) doc.InsertParagraph("\t\tBrak", false, ff);
                doc.InsertParagraph("", false, ff);



                doc.InsertTable(t);
                doc.InsertParagraph("", false, ff);
                doc.InsertParagraph("Łączny czas postoju maszyn: " + tRazem.ToString() + " minut", false);
                doc.InsertParagraph("", false, ff);
                doc.InsertParagraph("_____________________________________________", false, ff);
                doc.InsertParagraph("* nZ - nie zlecono, Z - zlecono, W - wykonano", false, ff);
                doc.SaveAs(fileName);
                Process.Start("WINWORD.EXE", fileName);
            }
            Eksport_Dialog.FileName = ".docx";
        }

        private void tabelka_postojow_SelectionChanged(object sender, EventArgs e)
        {
            if (tabelka_postojow.Rows.Count > 0 && tabelka_postojow.SelectedRows.Count > 0)
            {
                zaznaczona_pozycja = tabelka_postojow.SelectedRows[0].Index;
                Editindeks = ShowList_Indeksy_Postoje[zaznaczona_pozycja];
                foreach (DataGridViewRow wiersz in tabelka_postojow.Rows)
                {
                    if (wiersz.Selected)
                    {
                        wiersz.Height = 88;
                        wiersz.Resizable = DataGridViewTriState.True;
                        wiersz.MinimumHeight = 88;
                    }
                    else
                    {
                        wiersz.MinimumHeight = 72;
                        wiersz.Height = 72;
                        wiersz.Resizable = DataGridViewTriState.False;
                    }

                }
                if (FullList_Postoje[Editindeks].maszyna.Archiwalne || FullList_Postoje[Editindeks].pracownik.archiwalne) postoje_historia_edytuj_btn.Enabled = false;
                else postoje_historia_edytuj_btn.Enabled = true;
                if (FullList_Postoje[Editindeks].mycieInt == 1) postoje_historia_zatwierdz_btn.Enabled = true;
                else postoje_historia_zatwierdz_btn.Enabled = false;

                //postoje_pictureBox.Image.
                if (FullList_Postoje[Editindeks].Ufiles.Count > 0)
                {
                    pliki_ListView.Items.Clear();

                    foreach (string S in FullList_Postoje[Editindeks].Ufiles)
                    {
                        string[] dziel = S.Split('\\');
                        string klucz = dziel[dziel.Length - 1];
                        string[] form = klucz.Split('.');
                        string format = form[form.Length - 1];
                        pliki_ListView.Items.Add(klucz);
                        pliki_ListView.Items[pliki_ListView.Items.Count - 1].Tag = format;
                        pliki_ListView.Items[pliki_ListView.Items.Count - 1].ImageKey = S;
                        //ikonki.Images.Add(System.Drawing.Image.FromFile(S));
                        // ikonki.Images.Add(klucz, System.Drawing.Image.FromFile(S));
                    }
                    pliki_ListView.Items[0].Selected = true;

                    // pliki_ListView.LargeImageList = ikonki;
                    //postoje_pictureBox.Image = System.Drawing.Image.FromFile(FullList_Postoje[Editindeks].Ufiles[0]);
                }
                else
                {
                    postoje_pictureBox.ImageLocation = "";
                    postoje_pictureBox.Visible = false;
                    pliki_ListView.Items.Clear();
                }



            }
        }

        private void postoje_historia_filtry_wprowadzil_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (postoje_historia_filtry_wprowadzil_checkBox.Checked)
            {
                postoje_historia_filtry_wprowadzil_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_wprowadzil_lbl.Font, FontStyle.Bold);
                postoje_historia_filtry_wprowadzil_combo.Visible = true;
            }
            else
            {
                postoje_historia_filtry_wprowadzil_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_wprowadzil_lbl.Font, FontStyle.Regular);
                postoje_historia_filtry_wprowadzil_combo.Visible = false;
            }
            Ustal_wyswietlanie();
            Wyswietl();
        }

        private void postoje_historia_filtry_dzialanie_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (postoje_historia_filtry_dzialanie_checkBox.Checked)
            {
                postoje_historia_filtry_dzialanie_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_dzialanie_lbl.Font, FontStyle.Bold);
                postoje_historia_filtry_dzialanie_combo.Visible = true;
            }
            else
            {
                postoje_historia_filtry_dzialanie_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_dzialanie_lbl.Font, FontStyle.Regular);
                postoje_historia_filtry_dzialanie_combo.Visible = false;
            }
            Ustal_wyswietlanie();
            Wyswietl();
        }

        private void postoje_historia_filtry_dzialanie_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ustal_wyswietlanie();
        }

        private void postoje_historia_filtry_typ_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (postoje_historia_filtry_typ_checkBox.Checked)
            {
                postoje_historia_filtry_typ_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_typ_lbl.Font, FontStyle.Bold);
                postoje_historia_filtry_typ_combo.Visible = true;
            }
            else
            {
                postoje_historia_filtry_typ_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_typ_lbl.Font, FontStyle.Regular);
                postoje_historia_filtry_typ_combo.Visible = false;
            }
            Ustal_wyswietlanie();
            Wyswietl();
        }

        private void postoje_historia_filtry_typ_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ustal_wyswietlanie();
        }

        private void postoje_historia_filtry_mycie_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (postoje_historia_filtry_mycie_checkBox.Checked)
            {
                postoje_historia_filtry_mycie_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_mycie_lbl.Font, FontStyle.Bold);
                postoje_historia_filtry_mycie_combo.Visible = true;
            }
            else
            {
                postoje_historia_filtry_mycie_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_mycie_lbl.Font, FontStyle.Regular);
                postoje_historia_filtry_mycie_combo.Visible = false;
            }
            Ustal_wyswietlanie();
            Wyswietl();
        }

        private void postoje_historia_filtry_linia_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (postoje_historia_filtry_linia_checkBox.Checked)
            {
                postoje_historia_filtry_linia_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_wprowadzil_lbl.Font, FontStyle.Bold);
                postoje_historia_filtry_linia_combo.Visible = true;
                postoje_historia_filtry_maszyna_checkBox.Enabled = true;
            }
            else
            {
                postoje_historia_filtry_linia_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_wprowadzil_lbl.Font, FontStyle.Regular);
                postoje_historia_filtry_linia_combo.Visible = false;
                postoje_historia_filtry_maszyna_checkBox.Checked = false;
                postoje_historia_filtry_maszyna_checkBox.Enabled = false;
            }
            Ustal_wyswietlanie();
            Wyswietl();

        }

        private void postoje_historia_filtry_maszyna_checkBox_CheckedChanged(object sender, EventArgs e)
        {

            if (postoje_historia_filtry_maszyna_checkBox.Checked)
            {
                postoje_historia_filtry_maszyna_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_maszyna_lbl.Font, FontStyle.Bold);
                postoje_historia_filtry_maszyna_combo.Visible = true;
            }
            else
            {
                postoje_historia_filtry_maszyna_lbl.Font = new System.Drawing.Font(postoje_historia_filtry_maszyna_lbl.Font, FontStyle.Regular);
                postoje_historia_filtry_maszyna_combo.Visible = false;
            }
            Ustal_wyswietlanie();
            Wyswietl();

        }

        private void nowy_postoj_linia_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indeks = nowy_postoj_linia_combo.SelectedIndex;
            if (indeks != -1)
            {
                indekslinii = indeks;
                nowy_postoj_maszyna_combo.Items.Clear();
                foreach (Maszyna M in FullList_Maszyny)
                {
                    if (nowy_postoj_linia_combo.Text == M.Linia && M.Archiwalne == false) nowy_postoj_maszyna_combo.Items.AddRange(new object[] { M.NazwaWys });
                }

                nowy_postoj_maszyna_combo.Enabled = true;
                nowy_postoj_maszyna_lbl.Enabled = true;

                nowy_postoj_podzespol_combo.Items.Clear();
                nowy_postoj_podzespol_lbl.Enabled = false;
                nowy_postoj_podzespol_combo.Enabled = false;

                if (TrybPracy) nowy_postoj_maszyna_combo.DroppedDown = true;
            }
            else nowy_postoj_maszyna_combo.Enabled = false;
            SprawdzNowyPostoj();

        }

        private void nowy_postoj_maszyna_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            indeksmaszyny = nowy_postoj_maszyna_combo.SelectedIndex;

            int ixM = UstalIndexMaszynyPoCombo(nowy_postoj_linia_combo.Text, nowy_postoj_maszyna_combo.Text);
            if (ixM != -1)
            {
                if (FullList_Maszyny[ixM].Podzespoly.Count != 0)
                {
                    string[] popz = FullList_Maszyny[ixM].Podzespoly.ToArray();
                    nowy_postoj_podzespol_combo.Items.Clear();
                    nowy_postoj_podzespol_combo.Items.AddRange(popz);
                    nowy_postoj_podzespol_lbl.Enabled = true;
                    nowy_postoj_podzespol_combo.Enabled = true;
                    if (TrybPracy) nowy_postoj_podzespol_combo.DroppedDown = true;
                }
                else
                {
                    nowy_postoj_podzespol_combo.Items.Clear();                  
                    nowy_postoj_podzespol_lbl.Enabled = false;
                    nowy_postoj_podzespol_combo.Enabled = false;
                    if (TrybPracy) nowy_postoj_wprowadzil_combo.DroppedDown = true;

                }
            }
            SprawdzNowyPostoj();
        }

        private void nowy_postoj_podzespol_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrybPracy) nowy_postoj_wprowadzil_combo.DroppedDown = true;
            
        }

        private void nowy_postoj_przyczyna_textBox_TextChanged(object sender, EventArgs e)

        {
            SprawdzNowyPostoj();
        }

        private void nowy_postoj_wprowadzil_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SprawdzNowyPostoj();
        }

        private void postoje_historia_wyswietlaj_combo_TextUpdate(object sender, EventArgs e)
        {
            postoje_historia_wyswietlaj_combo.Text = postoje_historia_wyswietlaj_combo.Items[indeks_wyswietlania].ToString();
        }

        private void postoje_historia_wyswietlaj_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            indeks_wyswietlania = postoje_historia_wyswietlaj_combo.SelectedIndex;
            if (indeks_wyswietlania == 4)
            {
                postoje_historia_Od_DTPicker.Visible = true;
                postoje_historia_Do_DTPicker.Visible = true;
                postoje_historia_Od_lbl.Visible = true;
                postoje_historia_Do_lbl.Visible = true;
                postoje_zastosuj_btn.Visible = true;
            }
            else
            {
                postoje_historia_Od_DTPicker.Visible = false;
                postoje_historia_Do_DTPicker.Visible = false;
                postoje_historia_Od_lbl.Visible = false;
                postoje_historia_Do_lbl.Visible = false;
                postoje_zastosuj_btn.Visible = false;
            }
            Wyswietl();
            tabelka_postojow.Focus();
        }

        private void postoje_historia_filtry_linia_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indeks = postoje_historia_filtry_linia_combo.SelectedIndex;
            postoje_historia_filtry_maszyna_combo.Items.Clear();
            foreach (Maszyna M in FullList_Maszyny)
            {
                if (postoje_historia_filtry_linia_combo.Text == M.Linia) postoje_historia_filtry_maszyna_combo.Items.Add(M.NazwaWys);

            }
            postoje_historia_filtry_maszyna_combo.SelectedIndex = 0;
            Ustal_wyswietlanie();

        }

        private void postoje_historia_Od_DTPicker_ValueChanged(object sender, EventArgs e)
        {
          

        }

        private void postoje_zastosuj_btn_Click(object sender, EventArgs e)
        {
              Ustal_wyswietlanie();
        }

        private void postoje_historia_Do_DTPicker_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void postoje_historia_filtry_mycie_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ustal_wyswietlanie();
        }

        private void postoje_historia_filtry_maszyna_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ustal_wyswietlanie();
        }

        private void postoje_historia_filtry_wprowadzil_combo_SelectedIndexChanged(object sender, EventArgs e)
        {

            Ustal_wyswietlanie();
        }

        private void nowy_postoj_tStart_DTPicker_ValueChanged(object sender, EventArgs e)
        {
            SprawdzNowyPostoj();
        }

        private void nowy_postoj_tStop_DTPicker_ValueChanged(object sender, EventArgs e)
        {
            SprawdzNowyPostoj();
        }

        private void postoje_historia_zatwierdz_btn_Click(object sender, EventArgs e)
        {
            SprawdzLog();
            FullList_Postoje[Editindeks].Zatwierdz();
            ZapiszPostojeDoPliku();
            Wyswietl();
            if (ShowList.Count == 0) postoje_historia_zatwierdz_btn.Enabled = false;
        }

        private void postoje_zalacz_btn_Click(object sender, EventArgs e)
        {
            DialogResult ten; 
            ten = zalaczDialog.ShowDialog();
            
        }

        private void zalaczDialog_FileOk(object sender, CancelEventArgs e)
        {
            string[] myStream = zalaczDialog.FileNames;
            foreach (string _path in myStream)
            {
                string[] dziel = _path.Split('\\');
                string klucz = dziel[dziel.Length - 1];
                zalaczniki_ListView.Items.Add(klucz, _path);
                PlikiEx.Add(_path);
            }

        }

        private void pliki_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pliki_ListView.SelectedItems.Count > 0 && pliki_ListView.Items.Count > 0)
            {
                int item = pliki_ListView.SelectedItems[0].Index;

                if (File.Exists(pliki_ListView.Items[item].ImageKey))
                {
                    string[] formaty = { "png", "PNG","jpg","JPG","jpeg","JPEG","GIF","gif","BMP","bmp"};
                    
                    if (formaty.Contains(pliki_ListView.Items[item].Tag.ToString()))
                    {
                        postoje_pictureBox.Load(pliki_ListView.Items[item].ImageKey);
                        if(TrybPracy) postoje_pictureBox.Visible = true;
                    }
                    else
                    {
                        postoje_pictureBox.ImageLocation = "";
                        postoje_pictureBox.Visible = false;
                    }
                }
            }
        }      

        private void postoj_filtry_pokaz_btn_Click(object sender, EventArgs e)
        {
            if (postoj_filtry_pokaz_btn.Text == "Pokaż")
            {
                if (admin)
                {
                    postoj_filtry_pokaz_btn.Text = "Ukryj";
                    Point ruk = new Point(982, 217);
                    postoje_historia_filtry_Box.Location = ruk;
                    Size wil = new Size(264, 317);
                    postoje_historia_filtry_Box.Size = wil;
                }
                else
                {
                    postoj_filtry_pokaz_btn.Text = "Ukryj";
                    Point ruk = new Point(982, 277);
                    postoje_historia_filtry_Box.Location = ruk;
                    Size wil = new Size(264, 257);
                    postoje_historia_filtry_Box.Size = wil;
                }

            }
            else
            {
                postoj_filtry_pokaz_btn.Text = "Pokaż";
                Point ruk = new Point(982, 483);
                postoje_historia_filtry_Box.Location = ruk;
                Size wil = new Size(264, 51);
                postoje_historia_filtry_Box.Size = wil;
            }
        }

        private void torba_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SprawdzNowyPostoj();
        }

        private void pliki_ListView_DoubleClick(object sender, EventArgs e)
        {
            if (pliki_ListView.Items.Count > 0 && pliki_ListView.SelectedItems.Count > 0)
            {
                int too = pliki_ListView.SelectedItems[0].Index;
                if (File.Exists(pliki_ListView.Items[too].ImageKey)) Process.Start(pliki_ListView.Items[too].ImageKey);
            }
        }

        private void postoje_pictureBox_Click(object sender, EventArgs e)
        {
            if (pliki_ListView.Items.Count > 0 && pliki_ListView.SelectedItems.Count > 0)
            {
                int too = pliki_ListView.SelectedItems[0].Index;
                if (File.Exists(pliki_ListView.Items[too].ImageKey)) Process.Start(pliki_ListView.Items[too].ImageKey);
            }
        }

        private void Wykaz_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string naz = "Lista_Kontrolna_Skrzynki_Narzędziowej";
            string fileName = SciezkaDoKatalogu + @"\Files\" + naz + ".docx";
            Process.Start("WINWORD.EXE", fileName);
        }




       




        // Przeglądy

        List<string[]> doWykonania;
        int wiecej = 0, Wyk_indeks = -1, Zaznaczony = 0, EditZaznaczony = 0;
        bool f_Poz = true, f_Nap = true, f_Dop = true, f_Neg = true, f_Okr = true, f_Spc = true, f_Zat = true, f_Ocz = true, f_Nie = true;
        string findSyg = "";




        public void WczytajPrzeglady()
        {
            if (admin)
            {
                //przeglady_historia_mycie_btn.Visible = true;
            }
            else
            {
                //przeglady_historia_mycie_btn.Visible = false;

            }
            przeglady_harmonogram_listView.Items.Clear();
            doWykonania = new List<string[]>();
            int doZrb = 0;
            /*
            Karta nowa = new Karta(new string[] { "-", "-" });
            RekordHarm nowy = new RekordHarm("2020","50",FullList_Maszyny[1],nowa,"1","1");
            FullList_Harmonogram.Add(nowy);
            */
            if (FullList_Harmonogram.Count > 0)
            {
                foreach (RekordHarm rek in FullList_Harmonogram)
                {
                    if (!rek.wykonany)
                    {
                        if (rek.jak_dawno <= wiecej)
                        {
                            int ind = FullList_Harmonogram.IndexOf(rek);
                            doWykonania.Add(new string[] { rek.maszyna.NazwaWys, rek.maszyna.Linia, rek.aktualnosc, ind.ToString() });

                        }
                        if (rek.status == "Aktualny" || rek.status == "Zaległy") doZrb++;
                    }
                }
                if (!admin) MenuGl.TabPages[1].Text = "Przeglądy(" + doZrb.ToString() + ")";
                foreach (string[] harmonogram in doWykonania)
                {
                    int indyk = Convert.ToInt32(harmonogram[3]);

                    if (FullList_Harmonogram[indyk].specjalny)
                    {
                        ListViewItem hwiersz = new ListViewItem(new string[] { "[SPC] " + harmonogram[0], harmonogram[1], harmonogram[2] });
                        przeglady_harmonogram_listView.Items.Add(hwiersz);
                        przeglady_harmonogram_listView.Items[przeglady_harmonogram_listView.Items.Count - 1].BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        ListViewItem hwiersz2 = new ListViewItem(new string[] { harmonogram[0], harmonogram[1], harmonogram[2] });
                        //przeglady_harmonogram_listView.Items[przeglady_harmonogram_listView.Items.Count].ForeColor = Color.Black;
                        przeglady_harmonogram_listView.Items.Add(hwiersz2);
                        if (FullList_Harmonogram[indyk].maszyna.Linia == "Urządzenia" || FullList_Harmonogram[indyk].maszyna.Linia == "Infrastruktura" || FullList_Harmonogram[indyk].maszyna.Linia == "Instalacje" )
                        {
                            przeglady_harmonogram_listView.Items[przeglady_harmonogram_listView.Items.Count - 1].BackColor = Color.LightSkyBlue;
                        }
                    }


                }
                przeglady_harmonogram_wykonaj_btn.Enabled = false;
                przeglady_harmonogram_s_typ_lbl.Text = "";
                przeglady_harmonogram_s_karta_lbl.Text = "";
                przeglady_harmonogram_s_sygnatura_lbl.Text = "";
                przeglady_harmonogram_s_status_lbl.Text = "";
                Wyk_indeks = -1;

                List<string> okresy = new List<string>();
                foreach (Przeglad p in FullList_Przeglady) if (!okresy.Contains(p.okres)) okresy.Add(p.okres);
                okresy.Add("Wszystko");
               
                string aktual = historia_wysw_combo.Text;
                historia_wysw_combo.Items.Clear();
                historia_wysw_combo.Items.AddRange(okresy.ToArray());
                //int indaktual = historia_wysw_combo.Items.IndexOf(aktual);
                // historia_wysw_combo.SelectedIndex = indaktual;

                if (historia_wysw_combo.Items.Contains(aktual)) historia_wysw_combo.SelectedIndex = historia_wysw_combo.Items.IndexOf(aktual);
                else historia_wysw_combo.SelectedIndex = 0;

                /*
                if (findSyg != "")
                {
                    string znalazl = "Nie";
                    foreach (Przeglad p in FullList_Przeglady)
                    {
                        if (p.sygnatura == findSyg)
                        {
                            znalazl = p.okres;
                            break;
                        }
                    }
                    if (znalazl != "Nie") historia_wysw_combo.SelectedIndex = historia_wysw_combo.Items.IndexOf(znalazl);
                    int indaktual = historia_wysw_combo.Items.IndexOf(aktual);
                    historia_wysw_combo.SelectedIndex = indaktual;
                }
                else
                {
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    Calendar cal = dfi.Calendar;
                    DateTime terazData = DateTime.Now;
                    int terazTyg = cal.GetWeekOfYear(terazData, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    string okr;
                    if (terazTyg >= 1 && terazTyg < 14) okr = "I";
                    else if (terazTyg >= 14 && terazTyg < 27) okr = "II";
                    else if (terazTyg >= 27 && terazTyg < 40) okr = "III";
                    else okr = "IV";
                    okr = okr + " Kwartał " + terazData.Year.ToString();
                    //if (historia_wysw_combo.Items.Contains(okr)) historia_wysw_combo.SelectedIndex = historia_wysw_combo.Items.IndexOf(okr);
                    //else historia_wysw_combo.SelectedIndex = historia_wysw_combo.Items.Count - 1;
                }

                */
            }

            

        }

        private void ShowPrzeglady()
        {
            ZaznaczFiltry();

            ShowList_Przeglady.Clear();
            ShowList_Indeksy_Przeglady.Clear();
            int doZtw = 0;
            foreach (Przeglad p in FullList_Przeglady)
            {
                bool spr_wyn = p.filtr_Wynik(f_Poz, f_Nap, f_Dop, f_Neg);
                bool spr_typ = false;
                if ((p.specjalny && f_Spc) || (!p.specjalny && f_Okr)) spr_typ = true;
                bool spr_sta = false;
                if ((p.mycie && !p.status && f_Nie) || (p.status && f_Zat) || (!p.mycie && !p.status && f_Ocz)) spr_sta = true;
                bool term = false;
                if (historia_wysw_combo.SelectedIndex == historia_wysw_combo.Items.Count - 1) term = true;
                else if (historia_wysw_combo.Text == p.okres) term = true;
                bool czlo = false;
                if (przeglady_wykonal_combo.Text == "Wszyscy" || przeglady_wykonal_combo.Text == p.wykonał.Nazwisko) czlo = true;
                if (spr_wyn && spr_typ && spr_sta && term && czlo)
                {
                    ShowList_Przeglady.Add(p);
                    ShowList_Indeksy_Przeglady.Add(FullList_Przeglady.IndexOf(p));
                }
                if (p.mycie && !p.status) doZtw++;


            }
            if (admin) MenuGl.TabPages[1].Text = "Przeglądy(" + doZtw.ToString() + ")";

            int znajda = 0;

            int lance = przeglady_historia_tabela.FirstDisplayedScrollingRowIndex;
            int lorenc = 0;
            if (przeglady_historia_tabela.Rows.Count>0) lorenc = przeglady_historia_tabela.SelectedRows[0].Index;


            przeglady_historia_tabela.Rows.Clear();
            object[] wiersz = new object[8];
            int lacznie = 0;
            foreach (Przeglad p in ShowList_Przeglady)
            {
                if (p.sygnatura == findSyg) znajda = ShowList_Przeglady.IndexOf(p);
                wiersz[0] = p.sygnatura;
                wiersz[1] = p.maszyna.Linia + " - " + p.maszyna.NazwaWys;             
                wiersz[2] = p.wykonał.Nazwisko;
                wiersz[3] = p.dataStr;
                wiersz[4] = p.wynik;
                wiersz[5] = p.mycieStr;
                wiersz[6] = p.statusStr;
                wiersz[7] = FullList_Przeglady.IndexOf(p);
                przeglady_historia_tabela.Rows.Add(wiersz);
                Style(przeglady_historia_tabela.Rows.Count-1);
                lacznie++;
            }
            if (lance>=0 && lance< przeglady_historia_tabela.Rows.Count) przeglady_historia_tabela.FirstDisplayedScrollingRowIndex = lance;

            if (ShowList_Przeglady.Count != 0 && ShowList_Przeglady.Count> lorenc) przeglady_historia_tabela.Rows[lorenc].Selected = true;
            findSyg = "";

            przeglady_elementow_lbl.Text = "Elementów: " + lacznie.ToString();


        }

        private void Style(int i)
        {
            if (przeglady_historia_tabela.Rows[i].Cells[4].Value.ToString() == "Pozytywny")
            {
                przeglady_historia_tabela.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.PaleGreen;
                przeglady_historia_tabela.Rows[i].Cells[4].Style.SelectionBackColor = System.Drawing.Color.PaleGreen;
            }
            else if (przeglady_historia_tabela.Rows[i].Cells[4].Value.ToString() == "Naprawiony")
            {
                przeglady_historia_tabela.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.PaleTurquoise;
                przeglady_historia_tabela.Rows[i].Cells[4].Style.SelectionBackColor = System.Drawing.Color.PaleTurquoise;

            }
            else if (przeglady_historia_tabela.Rows[i].Cells[4].Value.ToString() == "Dopuszczony")
            {
                przeglady_historia_tabela.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.LightYellow;
                przeglady_historia_tabela.Rows[i].Cells[4].Style.SelectionBackColor = System.Drawing.Color.LightYellow;

            }
            else
            {
                przeglady_historia_tabela.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.MistyRose;
                przeglady_historia_tabela.Rows[i].Cells[4].Style.SelectionBackColor = System.Drawing.Color.MistyRose;

            }
            if (przeglady_historia_tabela.Rows[i].Cells[6].Value.ToString() == "Zatwierdzony")
            {
                przeglady_historia_tabela.Rows[i].Cells[6].Style.BackColor = System.Drawing.Color.PaleGreen;
                przeglady_historia_tabela.Rows[i].Cells[6].Style.SelectionBackColor = System.Drawing.Color.PaleGreen;
            }
            else
            {
                przeglady_historia_tabela.Rows[i].Cells[6].Style.BackColor = System.Drawing.Color.MistyRose;
                przeglady_historia_tabela.Rows[i].Cells[6].Style.SelectionBackColor = System.Drawing.Color.MistyRose;
            }
            if (przeglady_historia_tabela.Rows[i].Cells[5].Value.ToString() == "Zlecono")
            {
                przeglady_historia_tabela.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.MistyRose;
                przeglady_historia_tabela.Rows[i].Cells[5].Style.SelectionBackColor = System.Drawing.Color.MistyRose;
                przeglady_historia_tabela.Rows[i].Cells[6].Value = "Oczekujący";
                przeglady_historia_tabela.Rows[i].Cells[6].Style.BackColor = System.Drawing.Color.LightYellow;
                przeglady_historia_tabela.Rows[i].Cells[6].Style.SelectionBackColor = System.Drawing.Color.LightYellow;
            }
            else
            {
                przeglady_historia_tabela.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.PaleGreen;
                przeglady_historia_tabela.Rows[i].Cells[5].Style.SelectionBackColor = System.Drawing.Color.PaleGreen;
            }          
            przeglady_historia_tabela.Rows[i].Cells[1].Style.Font = new System.Drawing.Font(przeglady_harmonogram_listView.Font, FontStyle.Regular);
            przeglady_historia_tabela.Rows[i].Cells[2].Style.Font = new System.Drawing.Font(przeglady_harmonogram_listView.Font, FontStyle.Regular);
            przeglady_historia_tabela.Rows[i].Cells[3].Style.Font = new System.Drawing.Font(przeglady_harmonogram_listView.Font, FontStyle.Regular);
            przeglady_historia_tabela.Rows[i].Cells[4].Style.Font = new System.Drawing.Font(przeglady_historia_tabela.Rows[i].Cells[1].Style.Font, FontStyle.Regular);
            przeglady_historia_tabela.Rows[i].Cells[5].Style.Font = new System.Drawing.Font(przeglady_historia_tabela.Rows[i].Cells[1].Style.Font, FontStyle.Regular);
            przeglady_historia_tabela.Rows[i].Cells[6].Style.Font = new System.Drawing.Font(przeglady_historia_tabela.Rows[i].Cells[1].Style.Font, FontStyle.Regular);
             
        }

        public void ZaznaczFiltry()
        {
            if (f_Poz) przeglady_historia_filtry_wyniki.SetItemChecked(0, true);
            else przeglady_historia_filtry_wyniki.SetItemChecked(0, false);
            if (f_Nap) przeglady_historia_filtry_wyniki.SetItemChecked(1, true);
            else przeglady_historia_filtry_wyniki.SetItemChecked(1, false);
            if (f_Dop) przeglady_historia_filtry_wyniki.SetItemChecked(2, true);
            else przeglady_historia_filtry_wyniki.SetItemChecked(2, false);
            if (f_Neg) przeglady_historia_filtry_wyniki.SetItemChecked(3, true);
            else przeglady_historia_filtry_wyniki.SetItemChecked(3, false);
            if (f_Okr) przeglady_historia_filtry_typ.SetItemChecked(0, true);
            else przeglady_historia_filtry_typ.SetItemChecked(0, false);
            if (f_Spc) przeglady_historia_filtry_typ.SetItemChecked(1, true);
            else przeglady_historia_filtry_typ.SetItemChecked(1, false);
            if (f_Zat) przeglady_historia_filtry_status.SetItemChecked(0, true);
            else przeglady_historia_filtry_status.SetItemChecked(0, false);
            if (f_Ocz) przeglady_historia_filtry_status.SetItemChecked(1, true);
            else przeglady_historia_filtry_status.SetItemChecked(1, false);
            if (f_Nie) przeglady_historia_filtry_status.SetItemChecked(2, true);
            else przeglady_historia_filtry_status.SetItemChecked(2, false);
        }

        private void historia_wysw_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Sum = 0, Okr = 0, Spc = 0, Poz = 0, Npr = 0, Dop = 0, Neg = 0, Ztw = 0, Ocz = 0, Nzt = 0;
            foreach (Przeglad p in FullList_Przeglady)
            {
                bool term = false;
                if (historia_wysw_combo.SelectedIndex == historia_wysw_combo.Items.Count - 1) term = true;
                else if (historia_wysw_combo.Text == p.okres) term = true;
                if (term)
                {
                    Sum++;
                    if (p.specjalny) Spc++;
                    else Okr++;
                    if (p.wynost == 1) Neg++;
                    else if (p.wynost == 2) Dop++;
                    else if (p.wynost == 3) Npr++;
                    else Poz++;
                    if (p.mycie && !p.status) Nzt++;
                    else if (!p.mycie && !p.status) Ocz++;
                    else Ztw++;
                }
            }
            przeglady_historia_filtry_ilosc_lbl.Text = "[" + Sum.ToString() + "] Przeglądów";
            przeglady_historia_filtry_typ.Items[0] = "[" + Okr.ToString() + "] Okresowy";
            przeglady_historia_filtry_typ.Items[1] = "[" + Spc.ToString() + "] Specjalny";
            przeglady_historia_filtry_wyniki.Items[0] = "[" + Poz.ToString() + "] Pozytywny";
            przeglady_historia_filtry_wyniki.Items[1] = "[" + Npr.ToString() + "] Naprawiony";
            przeglady_historia_filtry_wyniki.Items[2] = "[" + Dop.ToString() + "] Dopuszczony";
            przeglady_historia_filtry_wyniki.Items[3] = "[" + Neg.ToString() + "] Negatywny";
            przeglady_historia_filtry_status.Items[0] = "[" + Ztw.ToString() + "] Zatwierdzony";
            przeglady_historia_filtry_status.Items[1] = "[" + Ocz.ToString() + "] Oczekujący";
            przeglady_historia_filtry_status.Items[2] = "[" + Nzt.ToString() + "] Niezatwierdzony";
            ShowPrzeglady();
        }

        private void przeglady_harmonogram_harmonogram_btn_Click(object sender, EventArgs e)
        {
            using (Utrzymanie_Ruchu___APP_Niepruszewo.FormHarmonogram f2 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormHarmonogram(FullList_Maszyny, SciezkaDoDanych, admin, FullList_Karty))
            {
                f2.ShowDialog(this);
            }
        }

        private void przeglady_historia_mycie_btn_Click(object sender, EventArgs e)
        {

            FullList_Przeglady[EditZaznaczony].Zatw_Mycie();           
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaPrzegladow.csv"))
            {
                foreach (Przeglad P in FullList_Przeglady)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.AddRange(P.Eksport());
                    writer.WriteRow(row);
                }
            }
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int LogDanych = Convert.ToInt32(log[0]);
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
            ShowPrzeglady();

        }

        private void przeglady_historia_zatwierdz_btn_Click(object sender, EventArgs e)
        {
            if (FullList_Przeglady[EditZaznaczony].wynost == 4)
            {
                Przeglad p = FullList_Przeglady[EditZaznaczony];
                FullList_Przeglady[EditZaznaczony].Aktualizuj(p.WynikiStr, p.KomStr, p.wykonał, FullList_Pracownicy[0], p.dataStr);
                FullList_Przeglady[EditZaznaczony].doPliku(SciezkaDoKatalogu);
            }
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaPrzegladow.csv"))
            {
                foreach (Przeglad P in FullList_Przeglady)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.AddRange(P.Eksport());
                    writer.WriteRow(row);
                }
            }
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int LogDanych = Convert.ToInt32(log[0]);
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
            ShowPrzeglady();
        }

        private void przeglady_historia_tabela_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (admin && e.ColumnIndex == 6)
            {
                foreach(Przeglad p in FullList_Przeglady)
                {
                    if (p.wynost == 4 && p.mycie && !p.status)
                    {
                        p.Aktualizuj(p.WynikiStr, p.KomStr, p.wykonał, FullList_Pracownicy[0], p.dataStr);
                        p.doPliku(SciezkaDoKatalogu);
                    }
                }
                
                using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaPrzegladow.csv"))
                {
                    foreach (Przeglad P in FullList_Przeglady)
                    {
                        ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                        row.AddRange(P.Eksport());
                        writer.WriteRow(row);
                    }
                }
                string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
                int LogDanych = Convert.ToInt32(log[0]);
                LogDanych = LogDanych + 1;
                System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
                WczytajPrzeglady();
            }
        }

        private void przeglady_historia_otworz_btn_Click(object sender, EventArgs e)
        {
            if (FullList_Przeglady[EditZaznaczony].status)
            {
                string naz = FullList_Przeglady[EditZaznaczony].sygnatura.Replace('/', '-');
                string fileName = SciezkaDoKatalogu + @"\Wykonane_Przeglady\" + naz + ".docx";
                Process.Start("WINWORD.EXE", fileName);
            }
        }

        private void przeglady_historia_wyswietl_btn_Click(object sender, EventArgs e)
        {
            //findSyg = FullList_Przeglady[EditZaznaczony].sygnatura;
            using (Utrzymanie_Ruchu___APP_Niepruszewo.FormPrz f2 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormPrz(FullList_Przeglady[EditZaznaczony], EditZaznaczony, SciezkaDoDanych, admin))
            {
                f2.ShowDialog(this);
            }
        }

        private void przeglady_harmonogram_wiecej_btn_Click(object sender, EventArgs e)
        {
            wiecej = wiecej + 1;
            przeglady_harmonogram_zwin_btn.Enabled = true;
            WczytajPrzeglady();

        }             

        private void przeglady_historia_tabela_SelectionChanged(object sender, EventArgs e)
        {
            if (przeglady_historia_tabela.SelectedRows.Count > 0 && przeglady_historia_tabela.Rows.Count > 0)
            {
                Zaznaczony = przeglady_historia_tabela.SelectedRows[0].Index;
                EditZaznaczony = Convert.ToInt32(przeglady_historia_tabela.Rows[Zaznaczony].Cells[7].Value);
               

                if (FullList_Przeglady[EditZaznaczony].status) przeglady_historia_otworz_btn.Enabled = true;
                else przeglady_historia_otworz_btn.Enabled = false;

                if (FullList_Przeglady[EditZaznaczony].mycie) przeglady_historia_mycie_btn.Enabled = false;
                else przeglady_historia_mycie_btn.Enabled = true;

                if (FullList_Przeglady[EditZaznaczony].mycie && !FullList_Przeglady[EditZaznaczony].status && FullList_Przeglady[EditZaznaczony].wynost == 4) przeglady_historia_zatwierdz_btn.Enabled = true;
                else przeglady_historia_zatwierdz_btn.Enabled = false;
            }
        }

        private void przeglady_wykonal_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPrzeglady();
        }

        private void przeglady_historia_filtry_typ_SelectedIndexChanged(object sender, EventArgs e)
        {
            przeglady_historia_filtry_typ.ClearSelected();
            f_Okr = przeglady_historia_filtry_typ.GetItemChecked(0);
            f_Spc = przeglady_historia_filtry_typ.GetItemChecked(1);
            ShowPrzeglady();
        }

        private void przeglady_historia_filtry_wyniki_SelectedIndexChanged(object sender, EventArgs e)
        {
            przeglady_historia_filtry_wyniki.ClearSelected();
            f_Poz = przeglady_historia_filtry_wyniki.GetItemChecked(0);
            f_Nap = przeglady_historia_filtry_wyniki.GetItemChecked(1);
            f_Dop = przeglady_historia_filtry_wyniki.GetItemChecked(2);
            f_Neg = przeglady_historia_filtry_wyniki.GetItemChecked(3);
            ShowPrzeglady();
        }

        private void przeglady_historia_filtry_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            przeglady_historia_filtry_status.ClearSelected();
            f_Zat = przeglady_historia_filtry_status.GetItemChecked(0);
            f_Ocz = przeglady_historia_filtry_status.GetItemChecked(1);
            f_Nie = przeglady_historia_filtry_status.GetItemChecked(2);
            ShowPrzeglady();
        }

        private void przeglady_harmonogram_zwin_btn_Click(object sender, EventArgs e)
        {
            wiecej = 0;
            przeglady_harmonogram_zwin_btn.Enabled = false;
            WczytajPrzeglady();
        }

        private void przeglady_harmonogram_listView_SelectedIndexChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (przeglady_harmonogram_listView.SelectedItems.Count != 0)
            {
                int zaz = przeglady_harmonogram_listView.SelectedItems[0].Index;
                Wyk_indeks = Convert.ToInt32(doWykonania[zaz][3]);
                przeglady_harmonogram_wykonaj_btn.Enabled = true;

                przeglady_harmonogram_s_typ_lbl.Text = FullList_Harmonogram[Wyk_indeks].specjalnyStr;
                przeglady_harmonogram_s_karta_lbl.Text = FullList_Harmonogram[Wyk_indeks].karta;
                przeglady_harmonogram_s_sygnatura_lbl.Text = FullList_Harmonogram[Wyk_indeks].sygnatura;
                //findSyg = FullList_Harmonogram[Wyk_indeks].sygnatura;
                if (FullList_Harmonogram[Wyk_indeks].status == "Zaległy")
                {
                    this.przeglady_harmonogram_s_status_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                    this.przeglady_harmonogram_s_status_lbl.ForeColor = System.Drawing.Color.Firebrick;
                }
                else
                {
                    this.przeglady_harmonogram_s_status_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                    this.przeglady_harmonogram_s_status_lbl.ForeColor = System.Drawing.SystemColors.ControlText;
                }
                przeglady_harmonogram_s_status_lbl.Text = FullList_Harmonogram[Wyk_indeks].status;


            }
            else
            {
                przeglady_harmonogram_wykonaj_btn.Enabled = false;
                przeglady_harmonogram_s_typ_lbl.Text = "";
                przeglady_harmonogram_s_karta_lbl.Text = "";
                przeglady_harmonogram_s_sygnatura_lbl.Text = "";
                przeglady_harmonogram_s_status_lbl.Text = "";
                Wyk_indeks = -1;
            }
        }

        private void przeglady_harmonogram_wykonaj_btn_Click(object sender, EventArgs e)
        {
            using (Utrzymanie_Ruchu___APP_Niepruszewo.FormPrz f2 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormPrz(FullList_Harmonogram[Wyk_indeks], SciezkaDoDanych))
            {
                f2.ShowDialog(this);
            }
        }

        private void przeglady_harmonogram_karty_btn_Click(object sender, EventArgs e)
        {
            using (Utrzymanie_Ruchu___APP_Niepruszewo.FormKarty f2 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormKarty(SciezkaDoDanych, admin))
            {
                f2.ShowDialog(this);
            }
        }

        private void przeglady_eksportuj_btn_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            string myStream;
            myStream = saveFileDialog2.FileName;
            string[] spraw = myStream.Split('\\');
            string[] spraw2 = spraw[spraw.Length - 1].Split('.');

            if (spraw2[0] != "" && spraw2[1] == "docx")
            {
                string fileName = myStream;

                string szablon = SciezkaDoKatalogu + @"\templates\template_lpr.docx";
                var doc = DocX.Load(szablon);

                Xceed.Document.NET.Formatting ff = new Xceed.Document.NET.Formatting();
                ff.Size = 9D;
                ff.StyleName = "Normalny";
                Xceed.Document.NET.Formatting ff1 = new Xceed.Document.NET.Formatting() { Size = 10D };
                Xceed.Document.NET.Formatting ff2 = new Xceed.Document.NET.Formatting() { Size = 11D };
                Xceed.Document.NET.Formatting ff9 = new Xceed.Document.NET.Formatting() { Size = 12D };
                Xceed.Document.NET.Formatting ff3 = new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true };
                Xceed.Document.NET.Formatting ff4 = new Xceed.Document.NET.Formatting() { Size = 9D, Italic = true };
                Xceed.Document.NET.Formatting ff5 = new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true };


                Table tR = doc.AddTable(1, 5);
                tR.Alignment = Alignment.center;
                tR.Design = TableDesign.LightShading;
                var szer3 = new float[] { 80f, 70f, 60f, 75f, 195f };
                tR.SetWidths(szer3);
                tR.Rows[0].Cells[0].Paragraphs.First().Append("Sygnatura/data", ff);
                tR.Rows[0].Cells[1].Paragraphs.First().Append("Maszyna", ff);
                tR.Rows[0].Cells[2].Paragraphs.First().Append("Wykonał", ff);
                tR.Rows[0].Cells[3].Paragraphs.First().Append("Wynik/status", ff);
                tR.Rows[0].Cells[4].Paragraphs.First().Append("Uwagi", ff);
                foreach (Przeglad R in ShowList_Przeglady)
                {
                    var r = tR.InsertRow(tR.RowCount);
                    r.Cells[0].Paragraphs.First().Append(R.sygnatura + "\n" +"  "+ R.Make_Data().ToShortDateString(), ff1);
                    r.Cells[1].Paragraphs.First().Append(R.maszyna.Linia + "\n" + R.maszyna.NazwaWys, ff);
                    r.Cells[2].Paragraphs.First().Append(R.wykonał.Imie + " " + R.wykonał.Nazwisko, ff1);
                    r.Cells[3].Paragraphs.First().Append(R.wynik , ff5);
                    r.Cells[3].InsertParagraph();
                    r.Cells[3].Paragraphs[1].Append(R.statusStr, ff);
                    string kom = "";
                    foreach (string S in R.Komentarze) if (S != "") kom = kom + "\"" + S + "\"" + "\n";
                    r.Cells[4].Paragraphs.First().Append(kom, ff1);
                }

                
                doc.InsertParagraph("Przeglądy wykonane w okresie: " + historia_wysw_combo.Text + ", przez: " + przeglady_wykonal_combo.Text+".", false, ff9);
                doc.InsertParagraph("Razem: " + ShowList_Przeglady.Count.ToString() + " przeglądów.", false, ff9);
                doc.InsertParagraph("", false, ff);

                doc.InsertTable(tR);


                doc.SaveAs(fileName);
                Process.Start("WINWORD.EXE", fileName);

            }

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            EksportKartDoDocx();
        }





        //Park Maszynowy


        float c8 = 8.00F;
        float c9 = 9.00F;
        float c10 = 10.00F;
        float c11 = 11.00F;
        float c12 = 12.00F;
        float c13 = 13.00F;

        List<int[]> statLinia = new List<int[]>();
        List<int[]> statMaszyna = new List<int[]>();

        List<string[]> FullList_Park = new List<string[]>();
        List<string[]> ShowList_Park = new List<string[]>();

        

        
        public void WczytajPark2()
        {
            park_tabelka_GridView.Columns[1].HeaderCell.Style.Font = new System.Drawing.Font(park_tabelka_GridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, c10);
            park_tabelka_GridView.Columns[2].HeaderCell.Style.Font = new System.Drawing.Font(park_tabelka_GridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, c10);
            park_tabelka_GridView.Columns[3].HeaderCell.Style.Font = new System.Drawing.Font(park_tabelka_GridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, c10);
            park_archiwalne_Check.Enabled = false;
            park_lacznie_Box.Visible = false;
            park_tabelka_GridView.Columns[3].Visible = true; ;

            if (park_wyswietlaj_Combo.SelectedIndex == 0) //                                                          <-- Maszyny
            {
                ParkNaglowki();
                park_archiwalne_Check.Enabled = true;
                List<int[]> atrybuty = new List<int[]>();
                if (park_okres_Combo.SelectedIndex == 2) atrybuty = ObliczAtrybuty2("all", "all");                                    //Wszystko
                if (park_okres_Combo.SelectedIndex == 0) atrybuty = ObliczAtrybuty2(park_rok_combo.Text, "all");                      //Rok
                if (park_okres_Combo.SelectedIndex == 1) atrybuty = ObliczAtrybuty2(park_rok_combo.Text, park_miesiac_combo.Text);    //Miesiac
                if (atrybuty.Count-1 == FullList_Maszyny.Count)
                {

                    park_tabelka_GridView.ScrollBars = ScrollBars.None;
                    park_tabelka_GridView.Rows.Clear();

                    object[] wiersz2 = new object[11];
                    foreach (int[] A in atrybuty)
                    {
                        if (!(!park_archiwalne_Check.Checked && A[8] == 1))
                        {
                            if (atrybuty.IndexOf(A) != 0)
                            {
                                float PRNapraw = 0;
                                float PRBlOp = 0;
                                float godz = (float)A[3] / (float)60;
                                if (A[3] > 0) PRNapraw = ((float)A[4] / A[3]) * 100;
                                if (A[2] > 0) PRBlOp = (float)A[5] / A[2] * 100;
                                
                                wiersz2[0] = A[0];
                                wiersz2[1] = FullList_Maszyny[A[0]].ID;
                                wiersz2[2] = FullList_Maszyny[A[0]].Linia;
                                wiersz2[3] = FullList_Maszyny[A[0]].NazwaWys;
                                wiersz2[4] = A[1];
                                wiersz2[5] = A[2];
                                wiersz2[6] = Math.Round(godz,1);
                                wiersz2[7] = Math.Round(PRNapraw,1);
                                wiersz2[8] = Math.Round(PRBlOp,1);
                                wiersz2[9] = A[6];
                                wiersz2[10] = A[7];
                                park_tabelka_GridView.Rows.Add(wiersz2);
                            }
                        }
                    }

                    park_tabelka_GridView.ScrollBars = ScrollBars.Vertical;
                }
            }

            else if (park_wyswietlaj_Combo.SelectedIndex == 1)//                                                      <-- Linie
            {
                ParkNaglowki();
                List<int[]> atrybuty = new List<int[]>();
                List<string> nzwLinie = new List<string>();
                List<int[]> Obliczone = new List<int[]>();

                if (park_okres_Combo.SelectedIndex == 2) atrybuty = ObliczAtrybuty2("all", "all");                                    //Wszystko
                if (park_okres_Combo.SelectedIndex == 0) atrybuty = ObliczAtrybuty2(park_rok_combo.Text, "all");                      //Rok
                if (park_okres_Combo.SelectedIndex == 1) atrybuty = ObliczAtrybuty2(park_rok_combo.Text, park_miesiac_combo.Text);    //Miesiac
                if (atrybuty.Count-1 == FullList_Maszyny.Count)
                {
                    foreach (int[] A in atrybuty)
                    {
                        if (atrybuty.IndexOf(A) != 0)
                        {
                            if (nzwLinie.Contains(FullList_Maszyny[A[0]].Linia))
                            {
                                int zmi = nzwLinie.IndexOf(FullList_Maszyny[A[0]].Linia);
                                Obliczone[zmi][0] = Obliczone[zmi][0] + A[1];
                                Obliczone[zmi][1] = Obliczone[zmi][1] + A[2];
                                Obliczone[zmi][2] = Obliczone[zmi][2] + A[3];
                                Obliczone[zmi][3] = Obliczone[zmi][3] + A[4];
                                Obliczone[zmi][4] = Obliczone[zmi][4] + A[5];
                                Obliczone[zmi][5] = Obliczone[zmi][5] + A[6];
                                Obliczone[zmi][6] = Obliczone[zmi][6] + A[7];
                            }
                            else
                            {
                                nzwLinie.Add(FullList_Maszyny[A[0]].Linia);
                                int[] nowy = new int[7];
                                nowy[0] = A[1];
                                nowy[1] = A[2];
                                nowy[2] = A[3];
                                nowy[3] = A[4];
                                nowy[4] = A[5];
                                nowy[5] = A[6];
                                nowy[6] = A[7];
                                Obliczone.Add(nowy);
                            }
                        }
                    }
                    park_tabelka_GridView.ScrollBars = ScrollBars.None;
                    park_tabelka_GridView.Rows.Clear();
                    object[] wiersz2 = new object[11];

                    foreach (string xlin in nzwLinie)
                    {
                        int zmi = nzwLinie.IndexOf(xlin);
                        float PRNapraw = 0;
                        float PRBlOp = 0;
                        float godz = (float)Obliczone[zmi][2] / (float)60;
                        if (Obliczone[zmi][2] > 0) PRNapraw = ((float)Obliczone[zmi][3] / Obliczone[zmi][2]) * 100;
                        if (Obliczone[zmi][1] > 0) PRBlOp = (float)Obliczone[zmi][4] / Obliczone[zmi][1] * 100;

                        wiersz2[0] = zmi;
                        wiersz2[1] = "";
                        wiersz2[2] = xlin;
                        wiersz2[3] = "";
                        wiersz2[4] = Obliczone[zmi][0];
                        wiersz2[5] = Obliczone[zmi][1];
                        wiersz2[6] = Math.Round(godz, 1); 
                        wiersz2[7] = Math.Round(PRNapraw, 1);
                        wiersz2[8] = Math.Round(PRBlOp, 1);
                        wiersz2[9] = Obliczone[zmi][5];
                        wiersz2[10] = Obliczone[zmi][6];
                        park_tabelka_GridView.Rows.Add(wiersz2);
                    }
                    park_tabelka_GridView.ScrollBars = ScrollBars.Vertical;
                }

            }

            else if (park_wyswietlaj_Combo.SelectedIndex == 2) //                                                    <-- Wszystko
            {
                ParkNaglowki();
                List<int[]> atrybuty = new List<int[]>();
                int post = 0, minu = 0, pracy = 0, przegl = 0, inno = 0, npr = 0, blop = 0;
                if (park_okres_Combo.SelectedIndex == 2) atrybuty = ObliczAtrybuty2("all", "all");                                    //Wszystko
                if (park_okres_Combo.SelectedIndex == 0) atrybuty = ObliczAtrybuty2(park_rok_combo.Text, "all");                      //Rok
                if (park_okres_Combo.SelectedIndex == 1) atrybuty = ObliczAtrybuty2(park_rok_combo.Text, park_miesiac_combo.Text);    //Miesiac
                if (atrybuty.Count-1 == FullList_Maszyny.Count)
                {
                    foreach (int[] A in atrybuty)
                    {
                        if (atrybuty.IndexOf(A) != 0)
                        {
                            post = post + A[1];
                            minu = minu + A[2];
                            pracy = pracy + A[3];
                            npr = npr + A[4];
                            blop = blop + A[5];
                            inno = inno + A[6];
                            przegl = przegl + A[7];
                        }
                    }

                    float PRNapraw = 0;
                    float PRBlOp = 0;
                    float godz = (float)pracy / (float)60;
                    if (pracy > 0) PRNapraw = ((float)npr / pracy) * 100;
                    if (minu > 0) PRBlOp = (float)blop / minu * 100;

                    park_tabelka_GridView.Rows.Clear();
                    object[] wiersz3 = new object[11];
                    wiersz3[0] = "";
                    wiersz3[1] = "";
                    wiersz3[2] = "";
                    wiersz3[3] = "Suma za okres";
                    wiersz3[4] = post;
                    wiersz3[5] = minu;
                    wiersz3[6] = Math.Round(godz, 1);
                    wiersz3[7] = Math.Round(PRNapraw, 1);
                    wiersz3[8] = Math.Round(PRBlOp, 1);
                    wiersz3[9] = inno;
                    wiersz3[10] = przegl;
                    park_tabelka_GridView.Rows.Add(wiersz3);

                }
            }
            else if (park_wyswietlaj_Combo.SelectedIndex == 3) //                                                     <-- Magazyn 
            {
                park_tabelka_GridView.Columns[1].HeaderText = "Indeks";
                park_tabelka_GridView.Columns[2].HeaderText = "Lokalizacja";
                park_tabelka_GridView.Columns[3].HeaderText = "Część";
                park_tabelka_GridView.Columns[3].Width = 300;
                park_tabelka_GridView.Columns[4].HeaderText = "Stan";
                park_tabelka_GridView.Columns[5].HeaderText = "Cena";
                park_tabelka_GridView.Columns[6].HeaderText = "Wartość";
                park_tabelka_GridView.Columns[7].Visible = false;
                park_tabelka_GridView.Columns[8].Visible = false;
                park_tabelka_GridView.Columns[9].HeaderText = "Pobrań";
                park_tabelka_GridView.Columns[10].HeaderText = "Wartość   pobrań";
                park_tabelka_GridView.Columns[11].Visible = false;
                park_lacznie_Box.Visible = true;

                List<int[]> atrybuty = new List<int[]>();
                if (park_okres_Combo.SelectedIndex == 2) atrybuty = ObliczAtrybuty4("all", "all");                                    //Wszystko
                if (park_okres_Combo.SelectedIndex == 0) atrybuty = ObliczAtrybuty4(park_rok_combo.Text, "all");                      //Rok
                if (park_okres_Combo.SelectedIndex == 1) atrybuty = ObliczAtrybuty4(park_rok_combo.Text, park_miesiac_combo.Text);    //Miesiac
                if (atrybuty.Count - 1 == FullList_Czesci.Count)
                {
                    park_tabelka_GridView.ScrollBars = ScrollBars.None;
                    park_tabelka_GridView.Rows.Clear();                 

                    foreach (int[] A in atrybuty)
                    {
                        if (atrybuty.IndexOf(A) != 0)
                        {
                            float cena = (float)A[2] / 100;
                            float wartosc = (float)A[3] / 100;
                            float wartPob = (float)A[6] / 100;
                            object[] wiersz4 = new object[11];
                            wiersz4[0] = A[0];
                            wiersz4[1] = FullList_Czesci[A[0]].ID;
                            wiersz4[2] = FullList_Czesci[A[0]].Lokalizacja;
                            wiersz4[3] = FullList_Czesci[A[0]].Nazwa;
                            wiersz4[4] = A[1];
                            wiersz4[5] = Math.Round(cena, 2);
                            wiersz4[6] = Math.Round(wartosc, 2);
                            wiersz4[9] = A[4];
                            wiersz4[10] = Math.Round(wartPob, 2);
                            park_tabelka_GridView.Rows.Add(wiersz4);
                        }
                        else
                        {

                            float wartosc = (float)A[3] / 100;
                            float wartPob = (float)A[6] / 100;

                            lbl1A.Text = "Indeksów:";
                            lbl1B.Text = FullList_Czesci.Count.ToString();
                            lbl2A.Text = "Artykułów:";
                            lbl2B.Text = A[1].ToString();
                            lbl3A.Text = "Wartosc magazynu:";
                            lbl3B.Text = wartosc.ToString("0.00") + "zł";
                            lbl4A.Text = "Pobrań:";
                            lbl4B.Text = A[4].ToString();
                            lbl5A.Text = "Wartosc pobrań:";
                            lbl5B.Text = wartPob.ToString("0.00") + "zł";

                        }
                    }
                    park_tabelka_GridView.ScrollBars = ScrollBars.Vertical;
                }
            }
            else if (park_wyswietlaj_Combo.SelectedIndex == 4) //                                                     <-- Raport roczny
            {
                {
                    park_tabelka_GridView.Columns[1].HeaderText = "Rok";
                    park_tabelka_GridView.Columns[2].HeaderText = "Miesiąc";
                    park_tabelka_GridView.Columns[3].Visible = false;
                    park_tabelka_GridView.Columns[4].HeaderText = "Interwencji";
                    park_tabelka_GridView.Columns[5].HeaderText = "Minut postoju";
                    park_tabelka_GridView.Columns[6].HeaderText = "Godzin pracy";
                    park_tabelka_GridView.Columns[7].Visible = false;
                    park_tabelka_GridView.Columns[8].Visible = false;
                    park_tabelka_GridView.Columns[9].HeaderText = "Przeglądów";
                    park_tabelka_GridView.Columns[10].HeaderText = "Pobrań";
                    park_tabelka_GridView.Columns[11].Visible = true;
                    park_tabelka_GridView.Columns[11].HeaderText = "Wartość pobrań";
                }
                string roczek = park_rok_combo.Text;
                List<string> Miesi = new List<string>();
                Miesi.Add("Styczeń");
                Miesi.Add("Luty");
                Miesi.Add("Marzec");
                Miesi.Add("Kwiecień");
                Miesi.Add("Maj");
                Miesi.Add("Czerwiec");
                Miesi.Add("Lipiec");
                Miesi.Add("Sierpień");
                Miesi.Add("Wrzesień");
                Miesi.Add("Październik");
                Miesi.Add("Listopad");
                Miesi.Add("Grudzień");

                List<int[]> WynikiAtr = new List<int[]>();

                foreach (string Mi in Miesi)
                {
                    int[] buforek = new int[7];

                    List<int[]> atrybuty2 = new List<int[]>();
                    atrybuty2 = ObliczAtrybuty2(roczek, Mi);
                    int[] posData = atrybuty2[0];

                    List<int[]> atrybuty4 = new List<int[]>();
                    atrybuty4 = ObliczAtrybuty4(roczek, Mi);
                    int[] pobData = atrybuty4[0];


                    buforek[0] = Miesi.IndexOf(Mi);
                    buforek[1] = posData[1];
                    buforek[2] = posData[2];
                    buforek[3] = posData[3];
                    buforek[4] = posData[7];
                    buforek[5] = pobData[4];
                    buforek[6] = pobData[6];

                    WynikiAtr.Add(buforek);
                }
                park_tabelka_GridView.ScrollBars = ScrollBars.None;
                park_tabelka_GridView.Rows.Clear();

                foreach (int[] atr in WynikiAtr)
                {
                    float wartPob = (float)atr[6] / 100;
                    float godz = (float)atr[3] / (float)60;

                    object[] wiersz5 = new object[12];
                    wiersz5[0] = "";
                    wiersz5[1] = "";
                    wiersz5[2] = Miesi[atr[0]];
                    wiersz5[4] = atr[1];
                    wiersz5[5] = atr[2];
                    wiersz5[6] = Math.Round(godz, 1); 
                    wiersz5[9] = atr[4];
                    wiersz5[10] = atr[5];
                    wiersz5[11] = Math.Round(wartPob, 2);
                    if (WynikiAtr.IndexOf(atr) == 0) wiersz5[1] = roczek.ToString();
                    park_tabelka_GridView.Rows.Add(wiersz5);
                }
                park_tabelka_GridView.ScrollBars = ScrollBars.Vertical;
            }
            park_elementow_lbl.Text = "Elementów: " + park_tabelka_GridView.Rows.Count;

        }

        void ParkNaglowki()
        {
            park_tabelka_GridView.Columns[1].HeaderText = "ID";
            park_tabelka_GridView.Columns[2].HeaderText = "Linia";
            park_tabelka_GridView.Columns[3].HeaderText = "Maszyna";
            park_tabelka_GridView.Columns[3].Width = 200;
            park_tabelka_GridView.Columns[4].HeaderText = "Interwencji";
            park_tabelka_GridView.Columns[5].HeaderText = "Minut postoju";
            park_tabelka_GridView.Columns[6].HeaderText = "Godzin pracy";
            park_tabelka_GridView.Columns[7].Visible = true;
            park_tabelka_GridView.Columns[8].Visible = true;
            park_tabelka_GridView.Columns[9].HeaderText = "Modernizacji";
            park_tabelka_GridView.Columns[10].HeaderText = "Przeglądów";
            park_tabelka_GridView.Columns[11].Visible = false;
        }

        public List<int[]> ObliczAtrybuty2(string inrok, string inmies)
        {
            List<int[]> atrybuty = new List<int[]>();
            int razyAl = 0, postojuAl = 0, pracyAl = 0, przeglAl = 0;
            foreach (Maszyna M in FullList_Maszyny)
            {
                int razy = 0, postoju = 0, pracy = 0, naprawy = 0, bladOp = 0, moder = 0, przegl = 0, arch = 0;
                if (M.Archiwalne) arch = 1;
                foreach (CellPostoj p in FullList_Postoje)
                {
                    if (M.ID == p.maszyna.ID)
                    {
                        bool wyrok = false;
                        if (inrok != "all" && inmies == "all" && inrok == p.rok) wyrok = true;
                        if (inrok != "all" && inmies != "all" && inrok == p.rok && inmies == p.miesiac) wyrok = true;
                        if (inrok == "all") wyrok = true;
                        if (wyrok)
                        {
                            razy = razy + 1;
                            postoju = postoju + p.tPostojInt;
                            pracy = pracy + p.tPraca;
                            if (p.dzialanie == "Naprawa") naprawy = naprawy + p.tPraca;
                            if (p.typ == "Błąd operatora") bladOp = bladOp + p.tPostojInt;
                            if (p.dzialanie == "Modernizacja") moder = moder + 1;
                        }
                    }
                }
                foreach (Przeglad P in FullList_Przeglady)
                {
                    if (M == P.maszyna)
                    {
                        bool wyrok = false;
                        if (inrok != "all" && inmies == "all" && inrok == P.rokS) wyrok = true;
                        if (inrok != "all" && inmies != "all" && inrok == P.rokS && inmies == P.miesiac) wyrok = true;
                        if (inrok == "all") wyrok = true;
                        if (wyrok) przegl++;
                    }
                }

                int[] dodaj = new int[10];
                dodaj[0] = FullList_Maszyny.IndexOf(M);
                dodaj[1] = razy;
                razyAl = razyAl + razy;
                dodaj[2] = postoju; //minut
                postojuAl = postojuAl + postoju;
                dodaj[3] = pracy;  //minut
                pracyAl = pracyAl + pracy;
                dodaj[4] = naprawy;  //minut
                dodaj[5] = bladOp;  //minut
                dodaj[6] = moder;
                dodaj[7] = przegl;
                przeglAl = przeglAl + przegl;
                dodaj[8] = arch;

                atrybuty.Add(dodaj);
            }
            int[] podsumowanie = { 0, razyAl, postojuAl, pracyAl, 0, 0, 0, przeglAl, 0, 0 };
            atrybuty.Insert(0, podsumowanie);
            return atrybuty;
        }

        public List<int[]> ObliczAtrybuty4(string inrok, string inmies)
        {
            List<int[]> atrybuty = new List<int[]>();
            int sztukAll = 0, pobranAll = 0, pobranSztAll = 0;
            float wartoscAll = 0, wartPobAll = 0;

            foreach (Czesc C in FullList_Czesci)
            {
                int sztuk = 0, pobran = 0, pobranSzt = 0;
                float cena = 0, wartosc = 0, wartPob = 0;


                sztuk = C.Ilosc;
                cena = C.cena;
                wartosc = sztuk * cena;

                sztukAll = sztukAll + sztuk;
                wartoscAll = wartoscAll + wartosc;



                foreach (Wypis W in FullList_Wypisy)
                {
                    if (C.ID == W.CzescID)
                    {
                        bool wyrok = false;
                        if (inrok != "all" && inmies == "all" && inrok == W.rok) wyrok = true;
                        if (inrok != "all" && inmies != "all" && inrok == W.rok && inmies == W.miesiac) wyrok = true;
                        if (inrok == "all") wyrok = true;
                        if (wyrok)
                        {
                            pobran++;
                            pobranAll++;
                            pobranSzt = pobranSzt + W.PobranoSzt;
                            pobranSztAll = pobranSztAll + W.PobranoSzt;
                            wartPob = wartPob + W.wartosc;
                            wartPobAll = wartPobAll + W.wartosc;
                        }
                    }
                }

                int[] dodaj = new int[7];
                dodaj[0] = FullList_Czesci.IndexOf(C);
                dodaj[1] = sztuk;
                dodaj[2] = (int)Math.Round(cena * 100);
                dodaj[3] = (int)Math.Round(wartosc * 100);
                dodaj[4] = pobran;
                dodaj[5] = pobranSzt;
                dodaj[6] = (int)Math.Round(wartPob * 100);

                atrybuty.Add(dodaj);
            }
            int[] podsumowanie = { -1, sztukAll, 0, (int)Math.Round(wartoscAll * 100), pobranAll, pobranSztAll, (int)Math.Round(wartPobAll * 100) };
            atrybuty.Insert(0, podsumowanie);
            return atrybuty;
        }

        private void park_wyswietlaj_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            WczytajPark2();
        }

        private void park_archiwalne_Check_CheckedChanged(object sender, EventArgs e)
        {
            WczytajPark2();
        }

        private void park_okres_Combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (park_okres_Combo.SelectedIndex == 0)
            {
                park_rok_combo.Visible = true;
                park_label5.Visible = true;
                park_miesiac_combo.Visible = false;
                park_label6.Visible = false;                         
                //park_rok_combo.DroppedDown = true;
            }
            else if (park_okres_Combo.SelectedIndex == 1)
            {
                park_rok_combo.Visible = true;
                park_label5.Visible = true;
                park_miesiac_combo.Visible = true;
                park_label6.Visible = true;              
                park_rok_combo.DroppedDown = true;
            }
            else if (park_okres_Combo.SelectedIndex == 2)
            {
                park_rok_combo.Visible = false;
                park_label5.Visible = false;
                park_miesiac_combo.Visible = false;
                park_label6.Visible = false;                
            }
            else if (park_okres_Combo.SelectedIndex == 3)
            {
                park_rok_combo.Visible = false;
                park_label5.Visible = false;
                park_miesiac_combo.Visible = false;
                park_label6.Visible = false;             
            }
            WczytajPark2();
        }

        private void park_rok_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            park_miesiac_combo.Items.Clear();
            List<string> miesiace = new List<string>();
            foreach (CellPostoj P in FullList_Postoje)
            {
                if (miesiace.Contains(P.miesiac) == false && P.rok == park_rok_combo.Text)
                {
                    miesiace.Add(P.miesiac);
                    park_miesiac_combo.Items.Add(P.miesiac);
                }
            }
            if (park_okres_Combo.SelectedIndex == 1) park_miesiac_combo.DroppedDown = true;

            WczytajPark2();
        }

        private void park_miesiac_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            WczytajPark2();
        }

        private void park_wyswietlaj_Combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (park_wyswietlaj_Combo.SelectedIndex == 4)
            {
                park_okres_Combo.SelectedIndex = 0;
                park_okres_Combo.Enabled = false;
            }
            else park_okres_Combo.Enabled = true;
            WczytajPark2();
        }

        private void park_edytuj_btn_Click(object sender, EventArgs e)
        {
            string wybind = "zero";
            if (park_tabelka_GridView.SelectedRows.Count != 0 && park_wyswietlaj_Combo.Text == "Maszyny") wybind = FullList_Maszyny[Convert.ToInt32(park_tabelka_GridView.SelectedRows[0].Cells[0].Value)].ID; ;
            using (Utrzymanie_Ruchu___APP_Niepruszewo.FormMaszyny f3 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormMaszyny(FullList_Maszyny, FullList_Postoje, FullList_Przeglady, wybind, admin, SciezkaDoDanych, SciezkaDoKatalogu))
            {
                f3.ShowDialog(this);

            }
        }

        private void park_dodaj_btn_Click(object sender, EventArgs e)
        {
            using (Utrzymanie_Ruchu___APP_Niepruszewo.FormMaszyny f3 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormMaszyny(FullList_Maszyny, FullList_Postoje, FullList_Przeglady, "new", admin, SciezkaDoDanych, SciezkaDoKatalogu))
            {
                f3.ShowDialog(this);

            }
        }

        private void raport_dobowy_generuj_btn_Click(object sender, EventArgs e)
        {
            string aha = generuj_raport(Raport_dobowy_dateTimePicker.Value.Year, Raport_dobowy_dateTimePicker.Value.Month, Raport_dobowy_dateTimePicker.Value.Day);
            //string fileName = SciezkaDoKatalogu + @"\Raporty\" + naz + ".docx";
            if (aha != "fail")Process.Start("WINWORD.EXE", aha);
        }

        private void park_eksportuj_btn_Click(object sender, EventArgs e)
        {
            saveFileDialog3.ShowDialog();
        }

        private void saveFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            string myStream;
            myStream = saveFileDialog3.FileName;
            string[] spraw = myStream.Split('\\');
            string[] spraw2 = spraw[spraw.Length - 1].Split('.');

            if (spraw2[0] != "" && spraw2[1] == "docx")
            {
                string fileName = myStream;

                string szablon = SciezkaDoKatalogu + @"\templates\template_dat.docx";
                var doc = DocX.Load(szablon);

                Xceed.Document.NET.Formatting ff = new Xceed.Document.NET.Formatting();
                ff.Size = 9D;
                ff.StyleName = "Normalny";
                Xceed.Document.NET.Formatting ff1 = new Xceed.Document.NET.Formatting() { Size = 10D };
                Xceed.Document.NET.Formatting ff2 = new Xceed.Document.NET.Formatting() { Size = 11D };
                Xceed.Document.NET.Formatting ff9 = new Xceed.Document.NET.Formatting() { Size = 12D };
                Xceed.Document.NET.Formatting ff3 = new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true };
                Xceed.Document.NET.Formatting ff4 = new Xceed.Document.NET.Formatting() { Size = 9D, Italic = true };
                Xceed.Document.NET.Formatting ff5 = new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true };


                List<int> widz = new List<int>();
                for (int i = 0; i < park_tabelka_GridView.Columns.Count; i++) if (park_tabelka_GridView.Columns[i].Visible) widz.Add(i);

                Table tR = doc.AddTable(park_tabelka_GridView.Rows.Count+1, widz.Count);


                for (int i=0; i<widz.Count;i++)
                {
                    tR.Rows[0].Cells[i].Paragraphs.First().Append(park_tabelka_GridView.Columns[widz[i]].HeaderText, ff);
                }
                for (int i = 0; i < park_tabelka_GridView.Rows.Count; i++)
                {
                    for (int j = 0; j < widz.Count; j++)
                    {
                        if (park_tabelka_GridView.Rows[i].Cells[widz[j]].Value != null) tR.Rows[i+1].Cells[j].Paragraphs.First().Append(park_tabelka_GridView.Rows[i].Cells[widz[j]].Value.ToString(), ff);
                    }
                }
                doc.InsertParagraph(" ", false, ff9);
                doc.InsertParagraph("Treść raportu: " + park_wyswietlaj_Combo.Text, false, ff9);
                doc.InsertParagraph("Dane statystyczne za okres : " + park_okres_Combo.Text.ToString() + ".", false, ff9);
                if (park_okres_Combo.Text.ToString() == "Rok") doc.InsertParagraph("Zebrane w: " + park_rok_combo.Text.ToString() + " roku.", false, ff9);
                else if (park_okres_Combo.Text.ToString() == "Miesiąc") doc.InsertParagraph("Zebrane w: " + park_miesiac_combo.Text.ToString() + " "+ park_rok_combo.Text.ToString() + " roku.", false, ff9);
                doc.InsertParagraph("", false, ff);

                doc.InsertTable(tR);


                doc.SaveAs(fileName);
                Process.Start("WINWORD.EXE", fileName);

            }

        }

        private string generuj_raport(int _rok, int _mies, int _dzien)
        {
            DateTime dlaDaty = new DateTime(_rok, _mies, _dzien);

            List<CellPostoj> Postoje = new List<CellPostoj>();
            List<Przeglad> Przeglady = new List<Przeglad>();
            List<Wypis> Pobrania = new List<Wypis>();

            int PosEl = 0, PosPracy = 0, PosMin = 0;
            foreach (CellPostoj C in FullList_Postoje)
            {
                DateTime tme = C.Make_data();
                if (tme.Year == dlaDaty.Year && tme.Month == dlaDaty.Month && tme.Day == dlaDaty.Day)
                {
                    Postoje.Add(C);
                    PosEl++;
                    PosPracy = PosPracy + C.tPraca;
                    PosMin = PosMin + C.tPostojInt;
                }
            }

            //PosPracy = PosPracy / 60;
            int PosPracy2;//= PosPracy % 60;
            PosPracy = Math.DivRem(PosPracy, 60,out PosPracy2);

            int PrzEl = 0;
            foreach (Przeglad P in FullList_Przeglady)
            {
                DateTime tme = P.Make_Data();
                if (tme.Year == dlaDaty.Year && tme.Month == dlaDaty.Month && tme.Day == dlaDaty.Day)
                {
                    Przeglady.Add(P);
                    PrzEl++;
                }
            }

            int PobEl = 0, PobCz = 0;
            float PobWar = 0;
            foreach (Wypis W in FullList_Wypisy)
            {
                DateTime tme = W.DataGl;
                if (tme.Year == dlaDaty.Year && tme.Month == dlaDaty.Month && tme.Day == dlaDaty.Day)
                {
                    PobCz = PobCz + W.PobranoSzt;
                    Pobrania.Add(W);
                    PobEl++;
                    PobWar = PobWar + W.wartosc;
                }
            }


            if (Postoje.Count > 0)
            {

                string myStream;
                myStream = SciezkaDoKatalogu + @"\Raporty\Raport_Dobowy_" + Postoje[0].NazwaDok + ".docx";
                string fileName = myStream;
                string szablon = SciezkaDoKatalogu + @"\templates\template_rap.docx";
                var doc = DocX.Load(szablon);


                Xceed.Document.NET.Formatting ff = new Xceed.Document.NET.Formatting() { Size = 9D };
                Xceed.Document.NET.Formatting ff1 = new Xceed.Document.NET.Formatting() { Size = 10D };
                Xceed.Document.NET.Formatting ff2 = new Xceed.Document.NET.Formatting() { Size = 11D };
                Xceed.Document.NET.Formatting ff9 = new Xceed.Document.NET.Formatting() { Size = 12D };
                Xceed.Document.NET.Formatting ff3 = new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true };
                Xceed.Document.NET.Formatting ff4 = new Xceed.Document.NET.Formatting() { Size = 9D, Italic = true };
                Xceed.Document.NET.Formatting ff5 = new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true };
                Xceed.Document.NET.Formatting ff6 = new Xceed.Document.NET.Formatting() { Size = 9D, UnderlineStyle = UnderlineStyle.singleLine };
                Xceed.Document.NET.Formatting ff7 = new Xceed.Document.NET.Formatting() { Size = 11D, UnderlineStyle = UnderlineStyle.singleLine, Bold = true };
                Xceed.Document.NET.Formatting ff8 = new Xceed.Document.NET.Formatting() { Size = 9D, Italic = true, FontColor = Color.Brown };
                Xceed.Document.NET.Formatting ff11 = new Xceed.Document.NET.Formatting() { Size = 9D, Bold = true};
                Xceed.Document.NET.Formatting ff10 = new Xceed.Document.NET.Formatting() { Size = 14D, Bold = true };
                Xceed.Document.NET.Formatting ff12 = new Xceed.Document.NET.Formatting() { Size = 10D, Bold = true };

                Table tP = doc.AddTable(1, 5);
                tP.Alignment = Alignment.center;
                tP.Design = TableDesign.LightShading;
                var szer = new float[] { 90f, 100f, 220f, 65f, 80f };
                tP.SetWidths(szer);
                tP.Rows[0].Cells[0].Paragraphs.First().Append("Dane", ff);
                tP.Rows[0].Cells[1].Paragraphs.First().Append("Maszyna", ff);
                tP.Rows[0].Cells[2].Paragraphs.First().Append("Przyczyna", ff);
                tP.Rows[0].Cells[3].Paragraphs.First().Append("", ff);
                tP.Rows[0].Cells[4].Paragraphs.First().Append("Szczegóły", ff);
                foreach (CellPostoj P in Postoje)
                {
                    var r = tP.InsertRow(tP.RowCount);
                    r.Cells[0].Paragraphs.First().Append(P.niceData + "\n" + P.tStartStr + " - " + P.tStopStr + "\n" + "\n" + P.pracownik.Imie + " " + P.pracownik.Nazwisko, ff);
                    r.Cells[1].Paragraphs.First().Append(P.maszyna.Linia + "\n" + P.maszyna.NazwaWys + "\n" + P.podzespol, ff);
                    string ataczment = "";
                    if (P.Ufiles.Count > 0) ataczment = "[Załączników: " + P.Ufiles.Count.ToString() + "]" + "\n";
                    r.Cells[2].Paragraphs.First().Append(ataczment + P.przyczyna, ff1);
                    r.Cells[3].Paragraphs.First().Append("Postuj: " + "\n" + "Mycie: " + "\n" + "Działanie: " + "\n" + "Uwaga: ", ff4);
                    r.Cells[3].Paragraphs[0].Alignment = Alignment.right;
                    r.Cells[4].Paragraphs.First().Append(P.tPostojStr + "\n" + P.mycie + "\n" + P.dzialanie + "\n" + P.typ, ff11);
                }

                Table tR = doc.AddTable(1, 5);
                tR.Alignment = Alignment.center;
                tR.Design = TableDesign.LightShading;
                var szer3 = new float[] { 80f, 70f, 60f, 75f, 195f };
                tR.SetWidths(szer3);
                tR.Rows[0].Cells[0].Paragraphs.First().Append("Sygnatura", ff);
                tR.Rows[0].Cells[1].Paragraphs.First().Append("Maszyna", ff);
                tR.Rows[0].Cells[2].Paragraphs.First().Append("Wykonał", ff);
                tR.Rows[0].Cells[3].Paragraphs.First().Append("Wynik", ff);
                tR.Rows[0].Cells[4].Paragraphs.First().Append("Uwagi", ff);
                foreach (Przeglad R in Przeglady)
                {
                    var r = tR.InsertRow(tR.RowCount);
                    r.Cells[0].Paragraphs.First().Append(R.sygnatura, ff1);
                    r.Cells[1].Paragraphs.First().Append(R.maszyna.Linia + "\n" + R.maszyna.NazwaWys, ff);
                    r.Cells[2].Paragraphs.First().Append(R.wykonał.Imie + " " + R.wykonał.Nazwisko, ff1);
                    r.Cells[3].Paragraphs.First().Append(R.wynik, ff5);
                    string kom = "";
                    foreach (string S in R.Komentarze) if (S != "") kom = kom + "\"" + S + "\"" + "\n";
                    r.Cells[4].Paragraphs.First().Append(kom, ff);
                }

                Table tW = doc.AddTable(1, 4);
                tW.Alignment = Alignment.center;
                tW.Design = TableDesign.LightShading;
                var szer2 = new float[] { 60f, 180f, 140f, 80f };
                tW.SetWidths(szer2);
                tW.Rows[0].Cells[0].Paragraphs.First().Append("ID", ff);
                tW.Rows[0].Cells[1].Paragraphs.First().Append("Część", ff);
                tW.Rows[0].Cells[2].Paragraphs.First().Append("Szczegóły", ff);
                tW.Rows[0].Cells[3].Paragraphs.First().Append("Pobrano", ff);
                foreach (Wypis w in Pobrania)
                {
                    var r = tW.InsertRow(tW.RowCount);
                    if (w.czyMonit) r.Cells[0].Paragraphs.First().Append(w.CzescID, ff7);
                    else r.Cells[0].Paragraphs.First().Append(w.CzescID, ff3);
                    if (w.czyMonit) r.Cells[1].Paragraphs.First().Append(w.CzescNazwa, ff6);
                    else r.Cells[1].Paragraphs.First().Append(w.CzescNazwa, ff);
                    r.Cells[1].InsertParagraph();
                    r.Cells[1].Paragraphs[1].Append(" - " + w.Opis, ff);
                    r.Cells[2].Paragraphs.First().Append(w.niceData + "  " + w.godzina + "\n" + w.Pobral.Imie + " " + w.Pobral.Nazwisko, ff);
                    r.Cells[2].InsertParagraph();
                    r.Cells[2].Paragraphs[1].Append("-" + w.wartosc.ToString("0.00") + "zł", ff4);
                    if (w.czyMin)
                    {
                        r.Cells[3].Paragraphs.First().Append(w.PobranoSzt.ToString() + "szt.", new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true, FontColor = System.Drawing.Color.Brown });
                        r.Cells[3].InsertParagraph();
                        r.Cells[3].Paragraphs[1].Append("(" + w.zostało + ")", ff8);
                    }
                    else
                    {
                        r.Cells[3].Paragraphs.First().Append(w.PobranoSzt.ToString() + "szt.", ff5);
                        r.Cells[3].InsertParagraph();
                        r.Cells[3].Paragraphs[1].Append("(" + w.zostało + ")", ff4);
                    }
                    r.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    r.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    r.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                    r.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                }


                doc.Headers.Odd.Tables[0].Rows[0].Cells[1].Paragraphs[3].Append(Postoje[0].niceData, new Xceed.Document.NET.Formatting() { Size = 16D });
                doc.Headers.Odd.Tables[0].Rows[0].Cells[1].Paragraphs[2].Alignment = Alignment.center;


                doc.InsertParagraph("", false, ff);
                doc.InsertParagraph("\t1. Postoje", false, ff10);
                doc.InsertParagraph("\t      Odnotowano: " + PosEl.ToString() + " Działań, " + PosMin.ToString() + " minut postoju, oraz " + PosPracy.ToString() + " godzin i " + PosPracy2.ToString() + " minut pracy.", false, ff1);
                doc.InsertParagraph("", false, ff);
                doc.InsertTable(tP);
                doc.InsertParagraph("", false, ff);
                doc.InsertParagraph("", false, ff);
                doc.InsertParagraph("\t2. Przeglądy", false, ff10);
                doc.InsertParagraph("\t      Wykonano " + PrzEl.ToString() + " przeglądów prewencyjnych.", false, ff1);
                doc.InsertParagraph("", false, ff);
                doc.InsertTable(tR);
                doc.InsertParagraph("", false, ff);
                doc.InsertParagraph("", false, ff);
                doc.InsertParagraph("\t3. Pobrania magazynowe", false, ff10);
                doc.InsertParagraph("\t      Wypisano " + PobEl.ToString() + " pobrań magazynowych. Pobrano " + PobCz.ToString() + " części o łącznej wartości " + PobWar.ToString("0.00") + " zł.", false, ff1);
                doc.InsertParagraph("", false, ff);
                doc.InsertTable(tW);

                doc.SaveAs(fileName);            


                //Process.Start("WINWORD.EXE", fileName);
                return myStream;
            }
            return "fail";
        }










        // magazyn techniczny

        List<Czesc> FullList_Czesci;
        List<Czesc> ShowList_Czesci;

        string fraza = "";

        
        int zazn = -1;
        int zazn2 = -1;
        string zaznID;
        bool zmiana = false;
        string dataaktu;
        string nowaLista;

        System.Drawing.Font czci = new System.Drawing.Font("Yu Gothic UI", 8.25F, (System.Drawing.FontStyle.Italic| System.Drawing.FontStyle.Bold), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
        System.Drawing.Font czci2 = new System.Drawing.Font("Yu Gothic UI", 9F, (System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold), System.Drawing.GraphicsUnit.Point, ((byte)(238)));


        void WyswietlCz()
        {

            DateTime teraz = DateTime.Now;
            DateTime minus = teraz.AddDays(-1);
            czesc_data_dateTimePicker.MinDate = minus;
            DateTime plus = teraz.AddDays(+1);
            czesc_data_dateTimePicker.MaxDate = plus;
            czesc_data_dateTimePicker.Value = teraz;

            int tuskrol = tabelka_czesci.FirstDisplayedScrollingRowIndex;
            int tuzaz = 0;
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0) tuzaz = tabelka_czesci.SelectedRows[0].Index;
            int tuskrol2 = tabelka_wypisy.FirstDisplayedScrollingRowIndex;
            int tuzaz2 = 0;
            if (tabelka_wypisy.Rows.Count > 0 && tabelka_wypisy.SelectedRows.Count > 0) tuzaz2 = tabelka_wypisy.SelectedRows[0].Index;

            tabelka_czesci.ScrollBars = ScrollBars.None;
            tabelka_czesci.Rows.Clear();
            tabelka_wypisy.Rows.Clear();
            ShowList_Czesci = new List<Czesc>(); 
            
            foreach (Czesc element in FullList_Czesci)
            {
                bool wyrok = false;
                if (fraza != "")
                {
                    if (element.Nazwa.ToLower().Contains(fraza.ToLower())) wyrok = true;
                    else if (element.ID.ToLower().Contains(fraza.ToLower())) wyrok = true;
                    else if (element.Lokalizacja.ToLower().Contains(fraza.ToLower())) wyrok = true;
                    else if (element.opis.ToLower().Contains(fraza.ToLower())) wyrok = true;
                }
                else wyrok = true;
                bool wyrok2 = false;
                if (!czesci_niezerowe_checkBox.Checked)
                {
                    if (element.Ilosc > 0) wyrok2 = true;
                }
                else wyrok2 = true;                
                bool wyrok3 = false;
                if (czesci_tylko_monitorowanecheckBox.Checked)
                {
                    if (element.Obserwuj) wyrok3 = true;
                }
                else wyrok3 = true;
                bool wyrok4 = false;
                if (!czesci_ZN_checkBox.Checked)
                {
                    if (element.PEN) wyrok4 = true;
                }
                else wyrok4 = true;
                bool wyrok5 = false;
                if (!czesci_PEN_checkBox.Checked)
                {
                    if (element.ZN) wyrok5 = true;
                }
                else wyrok5 = true;
               
                if (wyrok && wyrok2 && wyrok3 && wyrok4 && wyrok5) ShowList_Czesci.Add(element);
                
            }
            object[] wiersz = new object[6];
            foreach (Czesc element in ShowList_Czesci)
            {
                wiersz[1] = element.ID;
                wiersz[2] = element.Nazwa;
                wiersz[3] = element.Lokalizacja;
                wiersz[4] = element.Wartosc + "zł    ";
                wiersz[5] = "  "+element.Ilosc;
                tabelka_czesci.Rows.Add(wiersz);

                if (element.Obserwuj && admin)
                {
                    tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font(tabelka_czesci.DefaultCellStyle.Font.FontFamily, c9, FontStyle.Bold);
                    tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].Cells[4].Style.Font = czci;
                    if (element.Ilosc == 0 && element.PowiadomInt == 0)
                    {
                        tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightPink;
                        tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].Cells[5].Style.SelectionBackColor = Color.Red;
                    }
                    else if (element.Ilosc == element.PowiadomInt + 1 || element.Ilosc == element.PowiadomInt)
                    {
                        tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].Cells[5].Style.BackColor = Color.PaleGoldenrod;
                        tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].Cells[5].Style.SelectionBackColor = Color.Yellow;
                    }
                    else if (element.Ilosc < element.PowiadomInt)
                    {
                        tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightPink;
                        tabelka_czesci.Rows[tabelka_czesci.Rows.Count - 1].Cells[5].Style.SelectionBackColor = Color.Red;
                    }
                }

            }
            tabelka_czesci.ScrollBars = ScrollBars.Vertical;
            if (tabelka_czesci.Rows.Count > tuskrol && tuskrol != -1) tabelka_czesci.FirstDisplayedScrollingRowIndex = tuskrol;
            if (tabelka_czesci.Rows.Count > tuzaz) tabelka_czesci.Rows[tuzaz].Selected = true;

            czesc_elementow_lbl.Text = "Elementów: " + ShowList_Czesci.Count.ToString();

            object[] wiersz2 = new object[4];

            int wystapil = 0;
            float wartoRazem = 0; 
            foreach (Wypis element2 in FullList_Wypisy)
            {
                if (czesci_pobrania_combo.SelectedItem.ToString() == element2.Lista || czesci_pobrania_combo.SelectedItem.ToString() == "Wszystko")
                {
                    wiersz2[0] = element2.CzescID + "\n" + element2.CzescNazwa + "\n" + " - " + element2.Opis;
                    wiersz2[1] = element2.niceDataSkr + "\n" + element2.Pobral.Nazwisko + "\n" + " > " + element2.Lista;
                    wiersz2[2] = "Pobrano: "+element2.PobranoSzt + "          \n" +"Zostało: "+element2.zostało + "          \n" + " - "+element2.wartosc + "zł          ";
                    wiersz2[3] = FullList_Wypisy.IndexOf(element2);
                    tabelka_wypisy.Rows.Add(wiersz2);


                    if (admin)
                    {
                        if (element2.czyMonit) tabelka_wypisy.Rows[tabelka_wypisy.Rows.Count - 1].Cells[0].Style.Font = czci2;
                        if (element2.czyMin) tabelka_wypisy.Rows[tabelka_wypisy.Rows.Count - 1].Cells[2].Style.SelectionBackColor = Color.LightPink;
                    }
                    wystapil++;
                    wartoRazem = wartoRazem + element2.wartosc;
                }

            }
            wypis_elementow_lbl.Text = "Elementów: " + wystapil.ToString();
            wypis_wartosc_lbl.Text = "Razem wartość: " + wartoRazem.ToString("0.00") + " zł";

            if (tabelka_wypisy.Rows.Count > tuskrol2 && tuskrol2 != -1) tabelka_wypisy.FirstDisplayedScrollingRowIndex = tuskrol2;
            if (tabelka_wypisy.Rows.Count > tuzaz2) tabelka_wypisy.Rows[tuzaz2].Selected = true;

            int zaznIX = 0;
            foreach (Czesc element in ShowList_Czesci)
            {
                if (element.ID == zaznID)
                {
                    zaznIX = ShowList_Czesci.IndexOf(element);
                    break;
                }
            }
            if (tabelka_czesci.Rows.Count > zaznIX)
            {
                //tabelka_czesci.Rows[zaznIX].Selected = true;
                //tabelka_czesci.FirstDisplayedScrollingRowIndex = zaznIX;
            }
            if (tabelka_czesci.Rows.Count == 0)
            {
                czesci_wiecej_groupBox.Visible = false;
                czesci_pobierz_groupBox.Visible = false;
               // czesci_szukaj_lbl.Visible = false;
                //czesci_szukaj_textBox.Visible = false;

            }
            else
            {
                czesci_wiecej_groupBox.Visible = true;
                czesci_pobierz_groupBox.Visible = true;
                //czesci_szukaj_lbl.Visible = true;
               // czesci_szukaj_textBox.Visible = true;
            }

        }

        void zapiszCz()
        {
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaCzesci.csv"))
            {
                foreach (Czesc C in FullList_Czesci)
                {

                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.Add(C.ID);
                    row.Add(C.Nazwa);
                    row.Add(C.Lokalizacja);
                    row.Add(C.Ilosc.ToString());
                    row.Add(wyczysc(C.opis));
                    row.Add(C.Obs);
                    row.Add(C.Powiadom);
                    row.Add(C.Wartosc);
                    writer.WriteRow(row);
                }
            }
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaWypisow.csv"))
            {
                foreach (Wypis W in FullList_Wypisy)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.Add(W.Data);
                    row.Add(W.godzina);
                    row.Add(W.CzescID);
                    row.Add(W.CzescNazwa);
                    row.Add(W.czyMonitS);
                    row.Add(W.czyMinS);
                    row.Add(W.Pobral.ID);
                    row.Add(W.PobranoSzt.ToString());
                    row.Add(W.zostało);
                    row.Add(W.wartosc.ToString("0.00"));
                    row.Add(wyczysc(W.Opis));
                    row.Add(W.Lista);
                    writer.WriteRow(row);
                }
            }
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
        }

        void ustalListy()
        {
            List<string> listy = new List<string>();
            listy.Add("Wszystko");
            listy.Add("Nowe");

            {
                foreach(Wypis w in FullList_Wypisy)
                {
                    if (!listy.Contains(w.Lista)) listy.Add(w.Lista);
                }
            }

            int szukana = 1;
            string znaleziony;
            bool znajdz;
            do
            {
                znajdz = false;
                znaleziony = "Lista" + szukana.ToString("D4");
                foreach (Wypis w in FullList_Wypisy)
                {
                    if (w.Lista == znaleziony)
                    {

                        znajdz = true;
                        break;
                    }
                }
                szukana++;
            }
            while (znajdz);
            nowaLista = znaleziony;
            czesci_pobrania_combo.Items.Clear();
            czesci_pobrania_combo.Items.AddRange(listy.ToArray());
            czesci_pobrania_combo.SelectedIndex = 1;

        }

        void sprawdz()
        {
            if (czesc_pobral_comboBox.SelectedIndex > -1 && czesc_pobierz_ile_UpDown.Value != 0)
            {
                czesc_pobierz_btn.Enabled = true;
            }
            else czesc_pobierz_btn.Enabled = false;
        }

        void pobierz_pon()
        {
            foreach (Wypis W in FullList_Wypisy)
            {
                if (W.Lista == "Nowe")
                {
                    foreach (Czesc C in FullList_Czesci)
                    {
                        if (C.ID == W.CzescID && C.ZN)
                        {
                            int dc = FullList_Czesci.IndexOf(C);
                            int ille = Convert.ToInt32(W.PobranoSzt);
                            FullList_Czesci[dc].zmien(ille);
                        }
                    }

                }
            }
        }

        private void tabelka_wypisy_SelectionChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow wiersz in tabelka_wypisy.Rows)
            {
                if (wiersz.Selected)
                {
                    wiersz.Height = 70;
                    wiersz.Resizable = DataGridViewTriState.True;
                    wiersz.MinimumHeight = 70;
                }
                else
                {
                    wiersz.MinimumHeight = 35;
                    wiersz.Height = 35;
                    wiersz.Resizable = DataGridViewTriState.False;
                }

            }

            if (tabelka_wypisy.Rows.Count > 0 && tabelka_wypisy.SelectedRows.Count > 0)
            {
                int zzzna = tabelka_wypisy.SelectedRows[0].Index;
                zazn2 = Convert.ToInt32(tabelka_wypisy.Rows[zzzna].Cells[3].Value);


                if (czesci_pobrania_combo.SelectedItem.ToString() != "Nowe" && czesci_pobrania_combo.SelectedItem.ToString() != "Wszystko")
                {
                    czesci_otworz_btn.Enabled = true;
                }
                else czesci_otworz_btn.Enabled = false;
                if (czesci_pobrania_combo.SelectedItem.ToString() == "Nowe")
                {
                    czesci_generuj_btn.Enabled = true;
                }
                else czesci_generuj_btn.Enabled = false;
                if (FullList_Wypisy[zazn2].Lista == "Nowe")
                {
                    czesci_cofnij_btn.Enabled = true;
                }
                else
                {
                    czesci_cofnij_btn.Enabled = false;
                }
            }
            else
            {
                czesci_otworz_btn.Enabled = false;
                czesci_cofnij_btn.Enabled = false;
                czesci_generuj_btn.Enabled = false;
            }

        }      

        private void czesc_pobierz_ile_UpDown_ValueChanged(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void czesc_dodaj_btn_Click(object sender, EventArgs e)
        {
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
            {
                zazn = tabelka_czesci.SelectedRows[0].Index;
                zaznID = ShowList_Czesci[zazn].ID;
            }
            using (Utrzymanie_Ruchu___APP_Niepruszewo.FormCzesc f2 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormCzesc(FullList_Czesci,FullList_Wypisy,-1,admin, SciezkaDoDanych))
            {
                f2.ShowDialog(this);
            }
        }

        private void czesc_pobral_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {   
            sprawdz();
        }

        private void tabelka_czesci_SelectionChanged(object sender, EventArgs e)
        {
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
            {
                zazn = tabelka_czesci.SelectedRows[0].Index;
                czesc_pobierz_czesc_TextBox.Text = ShowList_Czesci[zazn].ID + "\n" + ShowList_Czesci[zazn].Nazwa;
                czesc_pobierz_ile_UpDown.Value = 0;
                czesc_pobierz_ile_UpDown.Maximum = ShowList_Czesci[zazn].Ilosc;
                czesc_pobral_comboBox.SelectedIndex = -1;

                czesc_opis_dod_textBox.Text = ShowList_Czesci[zazn].opis;
                czesci_monit_UpDown.Value = ShowList_Czesci[zazn].PowiadomInt;
                if (ShowList_Czesci[zazn].Obserwuj) czesci_monit_checkBox.Checked = true;
                else czesci_monit_checkBox.Checked = false;

            }
        }

        private void czesci_eksportuj_btn_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string myStream;
            myStream = saveFileDialog1.FileName;
            string[] spraw = myStream.Split('\\');
            string[] spraw2 = spraw[spraw.Length - 1].Split('.');

            if (spraw2[0] != "" && spraw2[1] == "docx")
            {
                string fileName = myStream;

                string szablon = SciezkaDoKatalogu + @"\templates\template_cze.docx";
                var doc = DocX.Load(szablon);

                Xceed.Document.NET.Formatting ff = new Xceed.Document.NET.Formatting();
                ff.Size = 9D;
                ff.StyleName = "Normalny";



                Table t = doc.AddTable(1, 4);
                t.Alignment = Alignment.center;
                t.Design = TableDesign.LightShading;

                var szer = new float[] { 60f, 250f, 80f, 40f};
                t.SetWidths(szer);


                t.Rows[0].Cells[0].Paragraphs.First().Append("ID", ff);
                t.Rows[0].Cells[1].Paragraphs.First().Append("Część", ff);
                t.Rows[0].Cells[2].Paragraphs.First().Append("Lokalizacja", ff);
                t.Rows[0].Cells[3].Paragraphs.First().Append("Ilość", ff);

                foreach (Czesc c in ShowList_Czesci)
                {
                    var r = t.InsertRow(t.RowCount);
                    r.Cells[0].Paragraphs.First().Append(c.ID, ff);
                    r.Cells[1].Paragraphs.First().Append(c.Nazwa, ff);
                    if (c.opis != "") r.Cells[1].Paragraphs[0].AppendLine(c.opis);
                    r.Cells[2].Paragraphs.First().Append(c.Lokalizacja, ff);
                    r.Cells[3].Paragraphs.First().Append(c.Ilosc.ToString(), ff);
                }

                doc.InsertParagraph("", false, ff);
                doc.InsertTable(t);
                

                doc.SaveAs(fileName);
                Process.Start("WINWORD.EXE", fileName);

            }

        }

        private void czesci_pobrania_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            WyswietlCz();
        }

        private void czesci_generuj_btn_Click(object sender, EventArgs e)
        {
            foreach (Wypis w in FullList_Wypisy)
            {
                if (w.Lista == "Nowe") w.Przypisz(nowaLista);
            }

            zapiszCz();

            string myStream;
            myStream = SciezkaDoKatalogu + @"\Listy_Pobran\" + nowaLista + ".docx";

            string fileName = myStream;

            string szablon = SciezkaDoKatalogu + @"\templates\template_pob.docx";
            var doc = DocX.Load(szablon);

            Xceed.Document.NET.Formatting ff = new Xceed.Document.NET.Formatting();
            ff.Size = 9D;

            Xceed.Document.NET.Formatting ff2 = new Xceed.Document.NET.Formatting();
            ff2.Size = 11D;

            Xceed.Document.NET.Formatting ff3 = new Xceed.Document.NET.Formatting();
            ff3.Size = 11D;
            ff3.Bold = true;

            Xceed.Document.NET.Formatting ff4 = new Xceed.Document.NET.Formatting();
            ff4.Size = 9D;
            ff4.Italic = true;
            
            Xceed.Document.NET.Formatting ff5 = new Xceed.Document.NET.Formatting();
            ff5.Size = 11D;
            ff5.Bold = true;

            Xceed.Document.NET.Formatting ff6 = new Xceed.Document.NET.Formatting();
            ff6.Size = 9D;
            ff6.UnderlineStyle = UnderlineStyle.singleLine;           

            Xceed.Document.NET.Formatting ff7 = new Xceed.Document.NET.Formatting();
            ff7.Size = 11D;
            ff7.UnderlineStyle = UnderlineStyle.singleLine;
            ff7.Bold = true;

            Xceed.Document.NET.Formatting ff8 = new Xceed.Document.NET.Formatting();
            ff8.Size = 9D;
            ff8.Italic = true;
            ff8.FontColor = Color.Brown;
            //ff8.


            Table t = doc.AddTable(1, 5);
            t.Alignment = Alignment.center;
            t.Design = TableDesign.LightShading;

            var szer = new float[] { 60f, 180f, 100f, 80f,20f};
            t.SetWidths(szer);


            t.Rows[0].Cells[0].Paragraphs.First().Append("ID", ff);
            t.Rows[0].Cells[1].Paragraphs.First().Append("Część", ff);
            t.Rows[0].Cells[2].Paragraphs.First().Append("Szczegóły", ff);
            t.Rows[0].Cells[3].Paragraphs.First().Append("Pobrano", ff);


            int elemelek = 0;
            float wrobelek = 0;
            DateTime pozny = DateTime.Now;
            DateTime wczesny = DateTime.Now;


            foreach (Wypis w in FullList_Wypisy)
            {
                if (w.Lista == nowaLista)
                {
                    if(elemelek==0)
                    {
                        pozny = w.DataGl;
                        wczesny = w.DataGl;
                    }
                    else
                    {
                        if (pozny.CompareTo(w.DataGl) < 0) pozny = w.DataGl;
                        if (wczesny.CompareTo(w.DataGl) > 0) wczesny = w.DataGl;
                    }
                    elemelek++;
                    wrobelek = wrobelek + w.wartosc;

                    var r = t.InsertRow(t.RowCount);
                    if(w.czyMonit) r.Cells[0].Paragraphs.First().Append(w.CzescID, ff7);
                    else r.Cells[0].Paragraphs.First().Append(w.CzescID, ff3);
                    if (w.czyMonit) r.Cells[1].Paragraphs.First().Append(w.CzescNazwa, ff6);
                    else r.Cells[1].Paragraphs.First().Append(w.CzescNazwa, ff);
                    r.Cells[1].InsertParagraph();
                    r.Cells[1].Paragraphs[1].Append(" - " + w.Opis, ff);
                    r.Cells[2].Paragraphs.First().Append(w.niceData +"  "+w.godzina+"\n" + w.Pobral .Imie+" "+ w.Pobral.Nazwisko, ff);
                    r.Cells[2].InsertParagraph();
                    r.Cells[2].Paragraphs[1].Append("-" + w.wartosc.ToString("0.00") + "zł", ff4);
                    if (w.czyMin)
                    {
                        r.Cells[3].Paragraphs.First().Append(w.PobranoSzt.ToString() + "szt.", new Xceed.Document.NET.Formatting() { Size = 11D, Bold = true, FontColor = System.Drawing.Color.Brown });
                        r.Cells[3].InsertParagraph();
                        r.Cells[3].Paragraphs[1].Append("(" + w.zostało + ")", ff8);
                    }
                    else
                    {
                        r.Cells[3].Paragraphs.First().Append(w.PobranoSzt.ToString() + "szt.", ff5);
                        r.Cells[3].InsertParagraph();
                        r.Cells[3].Paragraphs[1].Append("(" + w.zostało + ")", ff4);
                    }

                    r.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    r.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    r.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                    r.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                    // if (w.czyMin)// r.Cells[3].FillColor = Color.LightPink;

                }
            }

            doc.Headers.Odd.Tables[0].Rows[0].Cells[1].InsertParagraph();
            doc.Headers.Odd.Tables[0].Rows[0].Cells[1].Paragraphs[2].Append(nowaLista, new Xceed.Document.NET.Formatting() { Size = 16D });
            doc.Headers.Odd.Tables[0].Rows[0].Cells[1].Paragraphs[2].Alignment = Alignment.center;



            doc.InsertParagraph("\t\t ID:\t\t" + nowaLista, false, ff3);
            doc.InsertParagraph("\t\t Za okres: \t" + wczesny.ToShortDateString() + " - " + pozny.ToShortDateString(), false, ff2);
            doc.InsertParagraph("\t\t Elementów:\t" + elemelek.ToString(), false, ff2);
            doc.InsertParagraph("\t\t Wartość:\t" + wrobelek.ToString("0.00") + "zł", false, ff2);

            doc.InsertParagraph("", false, ff);
            doc.InsertTable(t);

            
            doc.SaveAs(fileName);
            Process.Start("WINWORD.EXE", fileName);

        }

        private void pasek1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Kopiuj ID")
            {
                Clipboard.SetDataObject(ShowList_Czesci[zazn].ID);
            }
            else if (e.ClickedItem.Text == "Kopiuj nazwę")
            {
                Clipboard.SetDataObject(ShowList_Czesci[zazn].Nazwa);
            }
            else if (e.ClickedItem.Text == "Karta części")
            {
                if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
                {
                    zazn = tabelka_czesci.SelectedRows[0].Index;
                    zaznID = ShowList_Czesci[zazn].ID;
                }
                int dc = FullList_Czesci.IndexOf(ShowList_Czesci[zazn]);
                using (Utrzymanie_Ruchu___APP_Niepruszewo.FormCzesc f2 = new Utrzymanie_Ruchu___APP_Niepruszewo.FormCzesc(FullList_Czesci, FullList_Wypisy, dc, admin, SciezkaDoDanych))
                {
                    f2.ShowDialog(this);
                }
            }

           
        }

        private void pasek1_Opening(object sender, CancelEventArgs e)
        {
            pasek1.Items[0].Text = ShowList_Czesci[zazn].ID;

        }

        private void czesc_opis_dod_textBox_TextChanged(object sender, EventArgs e)
        {
            if(ShowList_Czesci[zazn].opis != czesc_opis_dod_textBox.Text)
            {
                
                zmiana = true;
            }
        }

        private void czesci_otworz_btn_Click(object sender, EventArgs e)
        {

            if (czesci_pobrania_combo.SelectedItem.ToString() != "Nowe" && czesci_pobrania_combo.SelectedItem.ToString() != "Wszystko")
            {
                string fileName = SciezkaDoKatalogu + @"\Listy_Pobran\" + czesci_pobrania_combo.SelectedItem.ToString() + ".docx";
                Process.Start("WINWORD.EXE", fileName);
            }
        }

        private void czesci_cofnij_btn_Click(object sender, EventArgs e)
        {
            int usn = -1;
            foreach (Czesc C in FullList_Czesci)
            {
                if (FullList_Wypisy[zazn2].CzescID == C.ID)
                {
                    usn = FullList_Czesci.IndexOf(C);
                    break;
                }
            }
            FullList_Czesci[usn].oddaj(FullList_Wypisy[zazn2].PobranoSzt);
            FullList_Wypisy.RemoveAt(zazn2);
            zapiszCz();
            WyswietlCz();

            if (ShowList_Czesci.Contains(FullList_Czesci[usn]))
            {
                tabelka_czesci.Rows[ShowList_Czesci.IndexOf(FullList_Czesci[usn])].Selected = true;
                tabelka_czesci.FirstDisplayedScrollingRowIndex = ShowList_Czesci.IndexOf(FullList_Czesci[usn]);
            }

        }

        private void czesci_zmien_btn_Click(object sender, EventArgs e)
        {
            if (czesci_zmien_btn.Text == "Zapisz")
            {

                czesc_opis_dod_textBox.ReadOnly = true;
                czesci_monit_checkBox.Enabled = false;
                czesci_monit_UpDown.Enabled = false;
                label10.Enabled = false;
                label12.Enabled = false;
                czesci_zmien_btn.Text = "Zmień";
                tabelka_czesci.Enabled = true;

                int dc = FullList_Czesci.IndexOf(ShowList_Czesci[zazn]);

                string[] tekst = czesc_opis_dod_textBox.Lines;
                string bezenterow = "";
                foreach (string line in tekst) bezenterow = bezenterow + line + " ";
                FullList_Czesci[dc].zmienOpis(bezenterow);

                if (czesci_monit_checkBox.Checked)
                {
                    FullList_Czesci[dc].zmienObs("1");
                    FullList_Czesci[dc].zmienPow(czesci_monit_UpDown.Value.ToString());
                }
                else
                {
                    FullList_Czesci[dc].zmienObs("0");
                    FullList_Czesci[dc].zmienPow("0");
                }

                if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
                {
                    zazn = tabelka_czesci.SelectedRows[0].Index;
                    zaznID = ShowList_Czesci[zazn].ID;
                }

                zapiszCz();
                WyswietlCz();
            }
            else 
            {
                czesc_opis_dod_textBox.ReadOnly = false;                
                czesci_zmien_btn.Text = "Zapisz";
                tabelka_czesci.Enabled = false;
                if (admin) czesci_monit_checkBox.Enabled = true;
                czesc_opis_dod_textBox.Focus();


            }
        }
       
        private void park_tabelka_GridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //WczytajPark2();
        }

        private void tabelka_wypisy_DoubleClick(object sender, EventArgs e)
        {
            int usn = -1;
            foreach (Czesc C in FullList_Czesci)
            {
                if (FullList_Wypisy[zazn2].CzescID == C.ID)
                {
                    usn = FullList_Czesci.IndexOf(C);
                    break;
                }
            }
            if (ShowList_Czesci.Contains(FullList_Czesci[usn]))
            {
                tabelka_czesci.Rows[ShowList_Czesci.IndexOf(FullList_Czesci[usn])].Selected = true;
                tabelka_czesci.FirstDisplayedScrollingRowIndex = ShowList_Czesci.IndexOf(FullList_Czesci[usn]);
            }
            else
            {
                czesci_szukaj_textBox.Text = "";
                czesci_tylko_monitorowanecheckBox.Checked = false;
                czesci_niezerowe_checkBox.Checked = true;
                czesci_PEN_checkBox.Checked = true;
                czesci_ZN_checkBox.Checked = true;
                if (ShowList_Czesci.Contains(FullList_Czesci[usn]))
                {
                    tabelka_czesci.Rows[ShowList_Czesci.IndexOf(FullList_Czesci[usn])].Selected = true;
                    tabelka_czesci.FirstDisplayedScrollingRowIndex = ShowList_Czesci.IndexOf(FullList_Czesci[usn]);
                }
            }
        }

        private void czesci_monit_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (czesci_zmien_btn.Text == "Zapisz")
            {
                if (czesci_monit_checkBox.Checked && admin)
                {
                    czesci_monit_UpDown.Enabled = true;
                    label10.Enabled = true;
                    label12.Enabled = true;
                }
                else
                {
                    czesci_monit_UpDown.Enabled = false;
                    czesci_monit_UpDown.Value = 0;
                    label10.Enabled = false;
                    label12.Enabled = false;
                }
            }
        }

        private void czesc_pobierz_btn_Click(object sender, EventArgs e)
        {
            if (zazn > -1 && czesc_pobral_comboBox.SelectedIndex > -1 && czesc_pobierz_ile_UpDown.Value != 0)
            {
                int PP = 0;
                foreach (Pracownik P in FullList_Pracownicy) if (P.Nazwisko == czesc_pobral_comboBox.SelectedItem.ToString()) PP = FullList_Pracownicy.IndexOf(P);
                string[] tekst = czesc_opis_textBox.Lines;
                string bezenterow = "";
                foreach (string line in tekst) bezenterow = bezenterow + line + " ";
                string datata = czesc_data_dateTimePicker.Value.ToShortDateString();              
                string dattt;
                if (datata.Contains('-')) dattt = datata;
                else
                {
                    string[] datpo = datata.Split('.');
                    dattt = datpo[2] + "-" + datpo[1] + "-" + datpo[0];
                }
                DateTime ter = DateTime.Now;
                string goddd = ter.Hour + ":" + ter.Minute;
                string czymin = "0";
                int stanik = ShowList_Czesci[zazn].Ilosc - Convert.ToInt32(czesc_pobierz_ile_UpDown.Value);
                if (ShowList_Czesci[zazn].Obserwuj) if (ShowList_Czesci[zazn].PowiadomInt >= stanik) czymin = "1";
                float warttt = ShowList_Czesci[zazn].cena * Convert.ToInt32(czesc_pobierz_ile_UpDown.Value);

                Wypis nofy = new Wypis(
                    dattt,
                    goddd,
                    ShowList_Czesci[zazn].ID,
                    ShowList_Czesci[zazn].Nazwa,
                    ShowList_Czesci[zazn].Obs,
                    czymin,
                    FullList_Pracownicy[PP],
                    czesc_pobierz_ile_UpDown.Value.ToString(),
                    stanik.ToString(),
                    warttt.ToString("0.00"),
                    bezenterow,
                    "Nowe");
                FullList_Wypisy.Insert(0, nofy);

                int dc = FullList_Czesci.IndexOf(ShowList_Czesci[zazn]);
                int ille = Convert.ToInt32(czesc_pobierz_ile_UpDown.Value);
                FullList_Czesci[dc].zmien(ille);

                czesc_pobral_comboBox.SelectedIndex = -1;
                czesc_pobierz_ile_UpDown.Value = 0;
                czesc_opis_textBox.Text = "";

                czesci_pobrania_combo.SelectedIndex = 1;

                zapiszCz();
                WyswietlCz();
            }
        }

        private void czesci_szukaj_textBox_TextChanged(object sender, EventArgs e)
        {
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
            {
                zazn = tabelka_czesci.SelectedRows[0].Index;
                zaznID = ShowList_Czesci[zazn].ID;
            }
            fraza = czesci_szukaj_textBox.Text;

            WyswietlCz();
        }

        private void czesci_niezerowe_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
            {
                zazn = tabelka_czesci.SelectedRows[0].Index;
                zaznID = ShowList_Czesci[zazn].ID;
            }
            WyswietlCz();
        }

        private void czesci_tylko_monitorowanecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
            {
                zazn = tabelka_czesci.SelectedRows[0].Index;
                zaznID = ShowList_Czesci[zazn].ID;
            }
            WyswietlCz();
        }

        private void czesci_ZN_chckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
            {
                zazn = tabelka_czesci.SelectedRows[0].Index;
                zaznID = ShowList_Czesci[zazn].ID;
            }
            WyswietlCz();
        }

        private void czesci_PEN_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tabelka_czesci.Rows.Count > 0 && tabelka_czesci.SelectedRows.Count > 0)
            {
                zazn = tabelka_czesci.SelectedRows[0].Index;
                zaznID = ShowList_Czesci[zazn].ID;
            }
            WyswietlCz();
        }

        private void magazyn_aktualizuj_btn_Click(object sender, EventArgs e)
        {
            openFileDialog_imp.ShowDialog();
        }

        private void openFileDialog_imp_FileOk(object sender, CancelEventArgs e)
        {

            string myStream = openFileDialog_imp.FileName;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader xmlReader = XmlReader.Create(myStream, settings);

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (xmlReader.HasAttributes)
                    {
                        int go = -1;
                        foreach (Czesc c in FullList_Czesci)
                        {
                            if (xmlReader.GetAttribute("Kod") == c.ID)
                            {
                                go = FullList_Czesci.IndexOf(c);
                                break;
                            }
                        }
                        if (go == -1)
                        {
                            string Ilo2 = xmlReader.GetAttribute("Ilość");
                            string wart = xmlReader.GetAttribute("Wartość");
                            float wyn = 0;
                            if (Ilo2 != "" && wart != "")
                            {
                                int Ilo = Convert.ToInt32(Ilo2);
                                string war4 = wart.Replace('.', ',');
                                float war3 = float.Parse(war4);
                                wyn = war3 / (float)Ilo;
                            }

                            Czesc nowy = new Czesc(xmlReader.GetAttribute("Kod"), xmlReader.GetAttribute("Nazwa"), xmlReader.GetAttribute("Lokalizacja"), xmlReader.GetAttribute("Ilość"), "", "0", "0", wyn.ToString("0.00"));
                            FullList_Czesci.Add(nowy);
                        }
                        else
                        {
                            string Ilo2 = xmlReader.GetAttribute("Ilość");
                            string wart = xmlReader.GetAttribute("Wartość");
                            float wyn = 0;
                            if (Ilo2 != "" && wart != "")
                            {
                                int Ilo = Convert.ToInt32(Ilo2);
                                string war4 = wart.Replace('.', ',');
                                float war3 = float.Parse(war4);
                                wyn = war3 / (float)Ilo;
                            }

                            FullList_Czesci[go].aktualizuj(xmlReader.GetAttribute("Kod"), xmlReader.GetAttribute("Nazwa"), xmlReader.GetAttribute("Lokalizacja"), xmlReader.GetAttribute("Ilość"), wyn.ToString("0.00"));
                        }

                    }
                }
            }

            pobierz_pon();
            DateTime teraz = DateTime.Now;
            dataaktu = teraz.ToShortDateString();
            zapiszDaneProgramu();
            zapiszCz();
            WyswietlCz();

        }

        private void button225_Click(object sender, EventArgs e)
        {
            //funkcja do przerabiania danych

            List<string[]> pszepisz = new List<string[]>();
            using (ReadWriteCsv.CsvFileReader CSVwypisy = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaWypisow3.csv"))
            {
                ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                while (CSVwypisy.ReadRow(row))
                {
                    //int ixP = UstalIndexPoID(2, row[6]);
                    string[] nowy = { row[0], row[1], row[2], row[3], row[4], row[5], row[6] };
                    pszepisz.Add(nowy);
                }
            }

            List<string[]> ex = new List<string[]>();
            foreach (string[] W in pszepisz)
            {
                string[] powi = new string[12];
                if (W[0].Contains('-')) powi[0] = W[0];
                else
                {
                    string[] datpo = W[0].Split('.');
                    powi[0] = datpo[2] + "-" + datpo[1] + "-" + datpo[0];
                }
                powi[1] = "00:00";
                powi[2] = W[1];
                powi[3] = W[2];
                powi[4] = "0"; // czy obserwowane
                powi[5] = "0"; //czy osiągnięto stan min.
                powi[6] = W[3];
                powi[7] = W[4];
                powi[8] = "0"; //zostało
                powi[9] = "0"; //Wartosc
                powi[10] = W[5];
                powi[11] = W[6];


                ex.Add(powi);                    
            }

            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaWypisow2.csv"))
            {
                foreach (string[] S in ex)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.AddRange(S);                   
                    writer.WriteRow(row);
                }
            }
            
        }


    }
}