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
using System.Diagnostics;

namespace Utrzymanie_Ruchu___APP_Niepruszewo
{
    public partial class FormPrz : Form
    {
        List<GF_postoje.Pracownik> FullList_Pracownicy;      
        List<string[]> pracownicy;
        List<string[]> kierownicy;

        string SciezkaDoDanych;
        string SciezkaDoKatalogu;
        string sygnatura;
        string mycie;
        int rok, tydzien,indeks;
        bool edycja;
        GF_postoje.Maszyna Maszyna;
        GF_postoje.RekordHarm Rekord;
        GF_postoje.Karta Karta;
        GF_postoje.Przeglad Przegl;
        bool admin,status = false;



        public FormPrz(GF_postoje.RekordHarm _rekord, string _sciezka)
        {
            generuj(_sciezka);
            edycja = false;
            Rekord = _rekord;
            rok = Rekord.rok;
            tydzien = Rekord.tydzien;
            Maszyna = Rekord.maszyna;
            Karta = Rekord.KK;
            object wiersz;
            for (int i = 0; i < Karta.Wyswietl.Count; i++)
            {
                wiersz = Karta.Wyswietl[i];               
                dataGridView1.Rows.Add(wiersz);
            }
            style();
            przeglady_data_DTPicker.Value = DateTime.Now;
            sygnatura = Rekord.sygnatura;
            s_sygnatura_lbl.Text = sygnatura;
            s_linia_lbl.Text = Maszyna.Linia;
            s_maszyna_lbl.Text = Maszyna.Nazwa;
            s_karta_lbl.Text = Karta.nazwa;
            this.Text = "URAN - Przeglądy: ["+sygnatura+"] - Nowy";
        }

        public FormPrz(GF_postoje.Przeglad gotowy, int _indeks, string _sciezka, bool _admin)
        {
            Przegl = gotowy;
            sygnatura = gotowy.sygnatura;
            generuj(_sciezka);
            edycja = true;
            Maszyna = gotowy.maszyna;
            indeks = _indeks;
            admin = _admin;
            Karta = gotowy.karta;
            //GF_postoje
            object[] wiersz = new object[3];
            for (int i = 0; i < Karta.Wyswietl.Count; i++)
            {
                wiersz[0] = Karta.Wyswietl[i];
                wiersz[1] = gotowy.Wyniki[i];
                wiersz[2] = gotowy.Komentarze[i];
                dataGridView1.Rows.Add(wiersz);
            }
            style();
            przeglady_data_DTPicker.Value = gotowy.Make_Data();
            s_sygnatura_lbl.Text = sygnatura;
            s_linia_lbl.Text = Maszyna.Linia;
            s_maszyna_lbl.Text = Maszyna.Nazwa;
            s_karta_lbl.Text = Karta.nazwa;
            s_wynik_lbl.Text = gotowy.wynik;
            
            mycie_zlecono_check.Checked = true;
            if (gotowy.mycie)
            {
                mycie_wykonano_check.Checked = true;
                mycie_zlecono_check.Enabled = false;
            }
            int znajdz = -1;
            for (int i = 0; i < pracownicy.Count; i++) if (pracownicy[i][1] == gotowy.wykonał.ID) znajdz = i;
            wykonał_combo.SelectedIndex = znajdz;
            if (gotowy.status)
            {
                zatwierdzil_lbl.Enabled = true;
                zatwierdzil_combo.Enabled = false;
                wykonał_combo.Enabled = false;
                przeglady_data_DTPicker.Enabled = false;
                dataGridView1.ReadOnly = true;
                int znajdz2 = -1;
                for (int i = 0; i < kierownicy.Count; i++) if (kierownicy[i][1] == gotowy.zatwierdzil.ID) znajdz2 = i;
                zatwierdzil_combo.SelectedIndex = znajdz2;
                this.Text = "URAN - Przeglądy: [" + sygnatura + "] - Podgląd";
                button1.Visible = false;
                Zapisz_btn.Visible = false;
                docx_btn.Visible = true;
                zatwierdzony_lbl.Text = "Zatwierdzony";
                zatwierdzony_lbl.BackColor = System.Drawing.Color.PaleGreen;
                status = true;
            }
            else
            {
                this.Text = "URAN - Przeglądy: [" + sygnatura + "] - Edycja";
                if (admin && gotowy.mycie)
                {
                    zatwierdzil_lbl.Enabled = true;
                    zatwierdzil_combo.Enabled = true;                   
                    zatwierdzony_lbl.Text = "Niezatwierdzony";
                    zatwierdzony_lbl.BackColor = System.Drawing.Color.MistyRose;
                    Zapisz_btn.Text = "Zatwierdź";
                }
                else 
                {
                    zatwierdzil_lbl.Enabled = false;
                    zatwierdzil_combo.Enabled = false;
                    zatwierdzony_lbl.Text = "Oczekujący";
                    zatwierdzony_lbl.BackColor = System.Drawing.Color.LightYellow;
                    Zapisz_btn.Text = "Zapisz";
                }
            }


        }

        public void generuj(string _sciezka)
        {
            InitializeComponent();

            FullList_Pracownicy = new List<GF_postoje.Pracownik>();
            pracownicy = new List<string[]>();
            kierownicy = new List<string[]>();

            SciezkaDoDanych = _sciezka;
            SciezkaDoKatalogu = _sciezka.Replace(@"\Dane", "");


            using (ReadWriteCsv.CsvFileReader CSVpracownicy = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaPracownikow.csv"))
            {
                ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                while (CSVpracownicy.ReadRow(row))
                {
                    GF_postoje.Pracownik nowy = new GF_postoje.Pracownik(row[0], row[1], row[2], row[3], row[4]);
                    FullList_Pracownicy.Add(nowy);
                }
            }

            foreach (GF_postoje.Pracownik element in FullList_Pracownicy)
            {
                if (element.archiwalne == false)
                {
                    pracownicy.Add(new string[] { element.Imie + " " + element.Nazwisko, element.ID });
                    wykonał_combo.Items.Add(element.Imie + " " + element.Nazwisko);
                    if (element.kierownik == true)
                    {
                        kierownicy.Add(new string[] { element.Imie + " " + element.Nazwisko, element.ID });
                        zatwierdzil_combo.Items.Add(element.Imie + " " + element.Nazwisko);
                    }
                }
            }

        }

        int Znajdz(string _ID)
        {
            int zna = -1;
            foreach (GF_postoje.Pracownik P in FullList_Pracownicy)
            {
                if (P.ID == _ID)
                {
                    zna = FullList_Pracownicy.IndexOf(P);
                    break;
                }
            }
            return zna;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void style()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    string wybor = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    if (wybor == "Naprawiony" || wybor == "Dopuszczony" || wybor == "Negatywny")
                    {
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.White;
                        dataGridView1.Rows[i].Cells[2].Style.SelectionBackColor = System.Drawing.Color.LightBlue;
                        dataGridView1.Rows[i].Cells[2].ReadOnly = false;
                       
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[2].Value = null;
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.LightGray;
                        dataGridView1.Rows[i].Cells[2].Style.SelectionBackColor = System.Drawing.Color.LightGray;
                        dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                    }

                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.LightGray;
                    dataGridView1.Rows[i].Cells[2].Style.SelectionBackColor = System.Drawing.Color.LightGray;
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                }
            }
            //if (dataGridView1.Size != dataGridView1.PreferredSize) dataGridView1.Size = dataGridView1.PreferredSize;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            style();
            kontroluj();

        }

        private void kontroluj()
        {
            int wynik = 4;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int wynik_1;
                if (dataGridView1[1, i].Value != null)
                {
                    if (dataGridView1[1, i].Value.ToString() == "Pozytywny") wynik_1 = 4;
                    else if (dataGridView1[1, i].Value.ToString() == "Naprawiony" && dataGridView1[2, i].Value != null) wynik_1 = 3;
                    else if (dataGridView1[1, i].Value.ToString() == "Dopuszczony" && dataGridView1[2, i].Value != null) wynik_1 = 2;
                    else if (dataGridView1[1, i].Value.ToString() == "Negatywny" && dataGridView1[2, i].Value != null) wynik_1 = 1;
                    else wynik_1 = 0;
                }
                else wynik_1 = 0;
                if (wynik_1 < wynik) wynik = wynik_1;
                
                if(wynik == 4)
                {
                    s_wynik_lbl.BackColor = System.Drawing.Color.LightGreen;
                    s_wynik_lbl.Text = "Pozytywny";
                }
                else if (wynik == 3)
                {
                    s_wynik_lbl.BackColor = System.Drawing.Color.Cyan;
                    s_wynik_lbl.Text = "Naprawiony";
                }
                else if (wynik == 2)
                {
                    s_wynik_lbl.BackColor = System.Drawing.Color.Yellow;
                    s_wynik_lbl.Text = "Dopuszczony";
                }
                else if (wynik == 1)
                {
                    s_wynik_lbl.BackColor = System.Drawing.Color.Pink;
                    s_wynik_lbl.Text = "Negatywny";
                }
                else
                {
                    s_wynik_lbl.BackColor = System.Drawing.Color.Pink;
                    s_wynik_lbl.Text = "Niekompletny";
                }
            }
            if (admin)
            {
                if (mycie_wykonano_check.Checked)
                {
                    Zapisz_btn.Text = "Zatwierdź";
                    if (wynik != 0 && mycie_zlecono_check.Checked && wykonał_combo.SelectedIndex != -1 && zatwierdzil_combo.SelectedIndex != -1) Zapisz_btn.Enabled = true;
                    else Zapisz_btn.Enabled = false;
                }
                else
                {
                    Zapisz_btn.Text = "Zapisz";
                    if (wynik != 0 && mycie_zlecono_check.Checked && wykonał_combo.SelectedIndex != -1 ) Zapisz_btn.Enabled = true;
                    else Zapisz_btn.Enabled = false;
                }
                if (wynik != 0 && mycie_zlecono_check.Checked && wykonał_combo.SelectedIndex != -1 && zatwierdzil_combo.SelectedIndex != -1) Zapisz_btn.Enabled = true;
            }
            else
            {
                if (wynik != 0 && mycie_zlecono_check.Checked && wykonał_combo.SelectedIndex != -1) Zapisz_btn.Enabled = true;
                else Zapisz_btn.Enabled = false;
            }
            if (mycie_zlecono_check.Checked == true) mycie = "0";
            if (mycie_zlecono_check.Checked == true && mycie_wykonano_check.Checked == true) mycie = "1";
        }

        private void mycie_zlecono_check_CheckedChanged(object sender, EventArgs e)
        {
            kontroluj();
        }

        private void wykonał_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            kontroluj();
        }

        private void zatwierdzil_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            kontroluj();
        }

        private void docx_btn_Click(object sender, EventArgs e)
        {
            string naz = sygnatura.Replace('/', '-');           
            string fileName = SciezkaDoKatalogu + @"\Wykonane_Przeglady\" + naz + ".docx";
            Process.Start("WINWORD.EXE", fileName);
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!status)
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1[1, i].Value == null) dataGridView1[1, i].Value = "Pozytywny";

            }
        }

        private void Zapisz_btn_Click(object sender, EventArgs e)
        {
            List<string[]> Przeglady;
            List<string[]> Harm;
            List<string> indeksy;
            Przeglady = new List<string[]>();
            indeksy = new List<string>();
            Harm = new List<string[]>();
            using (ReadWriteCsv.CsvFileReader CSVindeksy = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaMaszyn.csv"))
            {
                ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                while (CSVindeksy.ReadRow(row)) indeksy.Add(row[0]);
            }

            using (ReadWriteCsv.CsvFileReader CSVprzeglady = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaPrzegladow.csv"))
            {
                ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                while (CSVprzeglady.ReadRow(row))
                {
                    Przeglady.Add(new string[] { row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7],row[8] });
                }
            }
            if (!edycja)
            {
                using (ReadWriteCsv.CsvFileReader CSVharmonogram = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv"))
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    while (CSVharmonogram.ReadRow(row))
                    {
                        Harm.Add(new string[] { row[0], row[1], row[2], row[3], row[4], row[5]});
                    }                   
                }
                foreach (string[] r in Harm)
                {
                    if (r[5] == "0")
                    {
                        string syg = r[0] + "/";
                        if (Convert.ToInt32(r[1]) < 10) syg = syg + "0" + r[1] + "/" + r[2];
                        else syg = syg + r[1] + "/" + r[2];
                        if (r[4] == "1") syg = syg + "/S";
                        if (syg == sygnatura)
                        {
                            r[5] = "1";
                            break;
                        }
                    }
                }
                using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv"))
                {
                    foreach (String[] H in Harm)
                    {
                        ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                        row.Add(H[0]);
                        row.Add(H[1]);
                        row.Add(H[2]);
                        row.Add(H[3]);
                        row.Add(H[4]);
                        row.Add(H[5]);
                        writer.WriteRow(row);
                    }
                }

            }
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int LogDanych = Convert.ToInt32(log[0]);

            
            string a6 = "", a7 = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "Pozytywny") a6 = a6 + "4";
                    else if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "Dopuszczony") a6 = a6 + "2";
                    else if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "Naprawiony") a6 = a6 + "3";
                    else a6 = a6 + "1";
                    if (i < dataGridView1.Rows.Count - 1) a6 = a6 + "-";
                }
                if (dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    string aa7 = dataGridView1.Rows[i].Cells[2].Value.ToString().Replace('*', '+');                    
                    a7 = a7 + aa7;
                }
                else a7 = a7 + "";
                if (i < dataGridView1.Rows.Count - 1) a7 = a7 + "*";
            }



            string dat;
            int dziennn = przeglady_data_DTPicker.Value.Day;           
            if (dziennn < 10) dat = "0" + dziennn.ToString();
            else dat = dziennn.ToString();
            int miesss = przeglady_data_DTPicker.Value.Month;
            if (miesss < 10) dat = "0" + miesss.ToString() + "-" + dat;
            else dat = miesss.ToString() + "-" + dat;
            dat = przeglady_data_DTPicker.Value.Year.ToString() + "-" + dat;




            string a0 = sygnatura;
            string a1 = Maszyna.ID;
            string a2 = pracownicy[wykonał_combo.SelectedIndex][1];
            string a3;
            if (zatwierdzil_combo.SelectedIndex >= 0) a3= kierownicy[zatwierdzil_combo.SelectedIndex][1];
            else a3 = "";
            string a4 = dat;
            string a5 = mycie;
            string a69 = Karta.nazwa;
            if (!edycja)
            {
                int indeks = indeksy.IndexOf(Maszyna.ID);
                int insertuj = -1;
                if (Przeglady.Count != 0)
                {
                    for (int i = 0; i < Przeglady.Count; i++)
                    {
                        String[] podziel = Przeglady[i][0].Split('/');
                        int rrr = Convert.ToInt32(podziel[0]);
                        int ttt = Convert.ToInt32(podziel[1]);
                        int iii = indeksy.IndexOf(podziel[2]);
                        if (rrr < rok)
                        {
                            insertuj = i;
                            break;
                        }
                        else if (rrr == rok)
                        {
                            if (ttt < tydzien)
                            {
                                insertuj = i;
                                break;
                            }
                            else if (ttt == tydzien)
                            {
                                if (iii > indeks)
                                {
                                    insertuj = i;
                                    break;
                                }
                            }
                        }
                        if (i == Przeglady.Count - 1 && insertuj == -1) insertuj = Przeglady.Count;
                    }
                    Przeglady.Insert(insertuj, new string[] { a0, a1, a2, a3, a4, a5,a69 , a6, a7 });
                }
                else Przeglady.Insert(0, new string[] { a0, a1, a2, a3, a4, a5, a69, a6, a7 });
            }

            else
            {
                Przeglady.RemoveAt(indeks);
                Przeglady.Insert(indeks, new string[] { a0, a1, a2, a3, a4, a5, a69, a6, a7 });
                if (a3 != "")
                {
                    Przegl.Aktualizuj(a6, a7, FullList_Pracownicy[Znajdz(pracownicy[wykonał_combo.SelectedIndex][1])], FullList_Pracownicy[Znajdz(kierownicy[zatwierdzil_combo.SelectedIndex][1])],a4);
                    Przegl.doPliku(SciezkaDoKatalogu);
                }
                
            }
            

            
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaPrzegladow.csv"))
            {
                foreach (String[] P in Przeglady)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.Add(P[0]);
                    row.Add(P[1]);
                    row.Add(P[2]);
                    row.Add(P[3]);
                    row.Add(P[4]);
                    row.Add(P[5]);
                    row.Add(P[6]);
                    row.Add(P[7]);
                    row.Add(P[8]);
                    writer.WriteRow(row);
                }
            }
            
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
            this.Close();
        }

    }
}
