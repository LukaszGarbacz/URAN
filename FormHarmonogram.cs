using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.Drawing.Text;

namespace Utrzymanie_Ruchu___APP_Niepruszewo
{
    public partial class FormHarmonogram : Form
    {
        List<GF_postoje.Maszyna> FullList_Maszyny;
        List<List<int>> tygodnie;
        List<string[]> FullList_harmonogram;
        List<string[]> Nawigator;
        List<string> Linie;
        List<string[]> doWykonaniaSpc;
        List<GF_postoje.RekordHarm> Harmonogram;
        List<GF_postoje.RekordHarm> NowyRok;
        List<int> lata;
        string SciezkaDoDanych;
        List<GF_postoje.Karta> Karty;
        List<string> karty_cmb;
        int rok;       
        bool admin;
        bool edycja = false;
        Graphics g;
        Pen Gr;
        Brush W;
        SolidBrush blackBrush = new SolidBrush(Color.Black);

        Font wykonany, zalegly, normalny;

        public FormHarmonogram(List<GF_postoje.Maszyna> Maszyny, string _sciezka, bool _admin, List<GF_postoje.Karta> _karty)
        {
            InitializeComponent();
            FullList_Maszyny = Maszyny;
            admin = _admin;
            Karty = _karty;
            tygodnie = new List<List<int>>();
            SciezkaDoDanych = _sciezka;
            FullList_harmonogram = new List<string[]>();
            doWykonaniaSpc = new List<string[]>();
            Linie = new List<string>();
            Harmonogram = new List<GF_postoje.RekordHarm>();
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, harmonogram_tabela, new object[] { true });
            g = wyswietlacz.CreateGraphics();
            Gr = new Pen(Color.Gray, 1);


            if (edycja) harmonogram_tabela.ReadOnly = false;
            else harmonogram_tabela.ReadOnly = true;

            wykonany = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            zalegly = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            normalny = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));

            wczytajodzera();


        }

        public void wczytajodzera()
        {
            Harmonogram.Clear();
            FullList_harmonogram.Clear();
            FullList_Maszyny.Clear();
            Linie.Clear();
            using (ReadWriteCsv.CsvFileReader CSVmaszyny = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaMaszyn.csv"))
            {
                ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                while (CSVmaszyny.ReadRow(row))
                {
                    if (row.Count == 5) row.Add("");
                    GF_postoje.Maszyna nowy = new GF_postoje.Maszyna(row[0], row[1], row[2], row[3], row[4], row[5]);
                    FullList_Maszyny.Add(nowy);
                }
            }
            using (ReadWriteCsv.CsvFileReader CSVharm = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv"))
            {
                ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                while (CSVharm.ReadRow(row))
                {
                    string[] rekord = row.LineText.Split(';');
                    
                    GF_postoje.RekordHarm nowy = new GF_postoje.RekordHarm(rekord[0], rekord[1], UstalIndexPoID(rekord[2]), UstalkartePoID(rekord[3]), rekord[4], rekord[5]);
                    Harmonogram.Add(nowy);
                }
            }

            foreach (GF_postoje.Maszyna M in FullList_Maszyny) if (!Linie.Contains(M.Linia)) Linie.Add(M.Linia);


            lata = new List<int>();
            foreach (GF_postoje.RekordHarm rek in Harmonogram) if (!lata.Contains(rek.rok)&&!rek.specjalny) lata.Add(rek.rok);
            string[] pojemnik = new string[lata.Count];
            for (int i = 0; i < lata.Count; i++) pojemnik[i] = lata[i].ToString();

            karty_cmb = new List<string>();
            foreach (GF_postoje.Karta K in Karty) karty_cmb.Add(K.nazwa);

            lata_combo.Items.Clear();
            lata_combo.Items.AddRange(pojemnik);
            lata_combo.SelectedIndex = -1;
            lata_combo.Text = "-";
        }

        public void Wyswietl()
        {


            harmonogram_tabela.Rows.Clear();
            Nawigator = new List<string[]>();
            object [] wiersz = new object[1];
            foreach (GF_postoje.Maszyna m in FullList_Maszyny)
            {
                if (!m.Archiwalne)
                {
                    harmonogram_tabela.Rows.Add(wiersz);
                    string[] wpis = new string[] { m.ID, FullList_Maszyny.IndexOf(m).ToString(), (harmonogram_tabela.Rows.Count - 1).ToString() };
                    Nawigator.Add(wpis);
                }

            }
            foreach(string[] wpis in Nawigator)
            {
                harmonogram_tabela.Rows[Convert.ToInt32(wpis[2])].HeaderCell.Value = FullList_Maszyny[Convert.ToInt32(wpis[1])].Linia + " - " + FullList_Maszyny[Convert.ToInt32(wpis[1])].NazwaWys;
                if(edycja)
                {
                    DataGridViewComboBoxCell komm = new DataGridViewComboBoxCell();
                    komm.Items.Add("-");
                    komm.Items.AddRange(karty_cmb.ToArray());
                    komm.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
                    komm.DisplayStyleForCurrentCellOnly = true;
                    komm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    komm.Value = FullList_Maszyny[Convert.ToInt32(wpis[1])].Karta;
                    harmonogram_tabela.Rows[Convert.ToInt32(wpis[2])].Cells[0] = komm;
                }
                else
                {
                    DataGridViewTextBoxCell komm = new DataGridViewTextBoxCell();
                    komm.Value = FullList_Maszyny[Convert.ToInt32(wpis[1])].Karta;
                    harmonogram_tabela.Rows[Convert.ToInt32(wpis[2])].Cells[0] = komm;
                }

            }

            if (edycja)
            {
                DateTime terazData = DateTime.Now;

                for (int i=1;i<13;i++)
                {
                    bool ridonly = true;
                    DateTime porownaj = new DateTime(rok,i,1);
                    if (terazData.CompareTo(porownaj) <= 0) ridonly = false;
                    for (int j =0; j<harmonogram_tabela.RowCount;j++)
                    {                   
                        int[] tygs = tygodnie[i - 1].ToArray();
                        string[] tygss = new string[tygs.Length+1];
                        tygss[0] = "-";
                        for (int k = 0; k < tygs.Length; k++) tygss[k+1] = tygs[k].ToString();

                        DataGridViewComboBoxCell komm = new DataGridViewComboBoxCell();
                        komm.Items.AddRange(tygss);
                        komm.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
                        komm.DisplayStyleForCurrentCellOnly = true;
                        komm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                        harmonogram_tabela.Rows[j].Cells[i] = komm;
                        if (ridonly) harmonogram_tabela.Rows[j].Cells[i].ReadOnly = true;

                    }
                }
            }
            

            foreach (GF_postoje.RekordHarm rek in Harmonogram)
            {
                if (rek.rok == rok && !rek.specjalny)
                {
                    int col = 1, wier = 0;
                    for (int i = 0; i < tygodnie.Count; i++)
                    {
                        if (tygodnie[i].Contains(rek.tydzien))
                        {
                            col = col + i;
                            break;
                        }
                    }
                    for (int i = 0; i < Nawigator.Count; i++)
                    {
                        if (Nawigator[i][0] == rek.maszyna.ID)
                        {
                            wier = Convert.ToInt32(Nawigator[i][2]);
                            break;
                        }
                    }
                    if (rek.wykonany) harmonogram_tabela.Rows[wier].Cells[col].Style.Font = wykonany;
                    else if (rek.status == "Zaległy")
                    {
                        if (edycja)
                        {
                            DataGridViewComboBoxCell komm = new DataGridViewComboBoxCell();
                            komm.Items.AddRange("-", rek.tydzien.ToString());
                            komm.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
                            komm.DisplayStyleForCurrentCellOnly = true;
                            komm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                            harmonogram_tabela.Rows[wier].Cells[col] = komm;                           
                            harmonogram_tabela.Rows[wier].Cells[col].ReadOnly = false;
                        }
                        harmonogram_tabela.Rows[wier].Cells[col].Style.Font = zalegly;
                        harmonogram_tabela.Rows[wier].Cells[col].Style.ForeColor = System.Drawing.Color.Firebrick;
                        
                    }
                    harmonogram_tabela.Rows[wier].Cells[col].Value = rek.tydzien.ToString();
                }
            }
            rysuj();
            
        }

        public void Oblicz_tygodnie()
        {
            tygodnie.Clear();
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            for (int i = 1; i < 13; i++)
            {
                List<int> tygs = new List<int>();
                int dni = cal.GetDaysInMonth(rok, i);
                for (int j = 1; j <= dni; j++)
                {
                    DateTime data = new DateTime(rok, i, j);
                    int obecny = cal.GetWeekOfYear(data, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
                    if (cal.GetDayOfWeek(data) == DayOfWeek.Monday) tygs.Add(obecny);
                }
                tygodnie.Add(tygs);
            }


        }

        private void harmonogram_tabela_SelectionChanged(object sender, EventArgs e)
        {
            //int sRow = harmonogram_tabela.SelectedCells[0].RowIndex;
            //int sCol = harmonogram_tabela.SelectedCells[0].ColumnIndex;
            //if(edycja) rysuj();
        }

        int suma;
        int[] sumaTyg, sumaMies, m_x, m_y, t_x, t_y;

        void zapisz()
        {
            foreach (string[] wpis in Nawigator)
            {
                FullList_Maszyny[Convert.ToInt32(wpis[1])].Karta = harmonogram_tabela.Rows[Convert.ToInt32(wpis[2])].Cells[0].Value.ToString(); 
            }

            List<GF_postoje.RekordHarm> Harmonogram_kopia;
            Harmonogram_kopia = new List<GF_postoje.RekordHarm>();

            for (int i = 1; i < 53; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    for (int l = 0; l < harmonogram_tabela.Rows.Count; l++)
                    {
                        if (harmonogram_tabela.Rows[l].Cells[j].Value != null)
                        {
                            if (harmonogram_tabela.Rows[l].Cells[j].Value.ToString() == i.ToString())
                            {
                                
                                if (harmonogram_tabela.Rows[l].Cells[j].Style.Font != wykonany)
                                {
                                    string kar = harmonogram_tabela.Rows[l].Cells[0].Value.ToString();
                                    if (kar != "" && kar != "-")
                                    {
                                        GF_postoje.RekordHarm nowy = new GF_postoje.RekordHarm(rok.ToString(), i.ToString(), UstalIndexPoID(Nawigator[l][0]), UstalkartePoID(kar), "0", "0");
                                        Harmonogram_kopia.Add(nowy);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            List<int> wywal = new List<int>();          
            foreach (GF_postoje.RekordHarm rek in Harmonogram)
            {
                if (rek.rok == rok && !rek.specjalny && !rek.wykonany) wywal.Add(Harmonogram.IndexOf(rek));
                
            }
            for(int i = wywal.Count-1; i>=0;i--) Harmonogram.RemoveAt(wywal[i]);
                          
            Harmonogram.AddRange(Harmonogram_kopia);
            Sortuj();
        }

        private void btn_anuluj_Click(object sender, EventArgs e)
        {
            wczytajodzera();
            edycja = false;
            btn_anuluj.Enabled = false;
            btn_generuj.Enabled = false;
            btn_zapisz.Enabled = false;
            btn_anuluj.Visible = false;
            btn_generuj.Visible = false;
            btn_zapisz.Visible = false;
            btn_edytuj.Enabled = false;
            harmonogram_tabela.ReadOnly = true;
            g.FillRectangle(Brushes.White, 0, 0, 722, 120);
            harmonogram_tabela.Rows.Clear();
 
        }

        private void harmonogram_tabela_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (edycja) rysuj();
        }

        private void btn_zapisz_Click(object sender, EventArgs e)
        {
            zapisz();
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int LogDanych = Convert.ToInt32(log[0]);
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv"))
            {

                foreach (GF_postoje.RekordHarm rek in Harmonogram)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.Add(rek.rok.ToString());
                    row.Add(rek.tydzien.ToString());
                    row.Add(rek.maszyna.ID);
                    row.Add(rek.karta);
                    row.Add(rek.specjalnyex);
                    row.Add(rek.wykonanyex);
                    writer.WriteRow(row);
                }
            }
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaMaszyn.csv"))
            {

                foreach (GF_postoje.Maszyna M in FullList_Maszyny)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.Add(M.ID);
                    row.Add(M.NazwaWys);
                    row.Add(M.Nazwa);
                    row.Add(M.Linia);
                    row.Add(M.ArchiwalneStr);
                    row.Add(M.Karta);
                    writer.WriteRow(row);
                }
            }
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
            edycja = false;
            btn_anuluj.Enabled = false;
            btn_generuj.Enabled = false;
            btn_zapisz.Enabled = false;
            btn_anuluj.Visible = false;
            btn_generuj.Visible = false;
            btn_zapisz.Visible = false;
            btn_edytuj.Enabled = true;
            harmonogram_tabela.ReadOnly = true;
            Wyswietl();
        }
       
        private void btn_edycaj_Click(object sender, EventArgs e)
        {
            edycja = true;
            btn_anuluj.Enabled = true;
            btn_generuj.Enabled = true;
            btn_zapisz.Enabled = true;
            btn_anuluj.Visible = true;
            btn_generuj.Visible = true;
            btn_zapisz.Visible = true;
            btn_edytuj.Enabled = false;
            harmonogram_tabela.ReadOnly = false;
            Wyswietl();
        }

        private void btn_generuj_Click(object sender, EventArgs e)
        {
            NowyRok = new List<GF_postoje.RekordHarm>();
            lata_combo.SelectedIndex = lata_combo.Items.Count-1;
            foreach (GF_postoje.RekordHarm rek in Harmonogram)
            {
                if (rek.rok == rok && !rek.specjalny)
                {
                    GF_postoje.RekordHarm nowy = new GF_postoje.RekordHarm((rek.rok + 1).ToString(), rek.tydzien.ToString(), rek.maszyna, rek.KK, "0", "0");
                    NowyRok.Add(nowy);
                }
            }
            foreach (GF_postoje.RekordHarm comb in NowyRok) Harmonogram.Add(comb);


            lata.Add(rok + 1);
            string[] pojemnik = new string[lata.Count];
            for (int i = 0; i < lata.Count; i++) pojemnik[i] = lata[i].ToString();
            lata_combo.Items.Clear();
            lata_combo.Items.AddRange(pojemnik);
            lata_combo.SelectedIndex = lata_combo.Items.Count - 1;
        }

        

        void rysuj()
        {
            suma = 0;
            sumaTyg = new int[52];
            sumaMies = new int[12];
            m_x = new int[12];
            m_y = new int[12];
            t_x = new int[52];
            t_y = new int[52];
            for (int j = 1; j < 13; j++) sumaMies[j - 1] = 0;
            //FullList_harmonogram.Clear();
            for (int i = 1; i < 53; i++)
            {
                sumaTyg[i - 1] = 0;
                for (int j = 1; j < 13; j++)
                {
                    for (int l = 0; l < harmonogram_tabela.Rows.Count; l++)
                    {
                        if (harmonogram_tabela.Rows[l].Cells[j].Value != null)
                        {
                            if (harmonogram_tabela.Rows[l].Cells[j].Value.ToString() == i.ToString())
                            {
                                //string wyk = "0";
                               // if (harmonogram_tabela.Rows[l].Cells[j].Style.Font == wykonany) wyk = "1";
                                //FullList_harmonogram.Add(new string[] { rok.ToString(), i.ToString(), Nawigator[l][0], harmonogram_tabela.Rows[l].Cells[0].Value.ToString(), "0", wyk });
                                suma = suma + 1;
                                sumaTyg[i - 1] = sumaTyg[i - 1] + 1;
                                sumaMies[j - 1] = sumaMies[j - 1] + 1;
                            }
                        }
                    }
                }
            }
           
            g.FillRectangle(Brushes.White, 0, 0, 722, 120);
            float wsp_x = (float)80 / sumaMies.Max();
            for (int i = 0; i < 12; i++)
            {
                m_x[i] = (int)((sumaMies[i] * wsp_x));
                m_y[i] = (i * 60) + 30;
            }
            for (int i = 0; i < 12; i++)
            {
                //g.DrawLine(Gr, m_y[i], m_x[i], m_y[i + 1], m_x[i + 1]);
                g.DrawRectangle(Gr, m_y[i] - 11, 120 - m_x[i], 42, m_x[i]);
                g.DrawString("("+sumaMies[i].ToString()+")", normalny, Brushes.Black, m_y[i], 106 - m_x[i]);
;
            }

            float wsp_x2 = sumaTyg.Max() / (float)30;
            float wsp_y2 = 722 / (float)52;
            for (int i = 0; i < 52; i++)
            {
                t_x[i] = (int)(60 - (sumaTyg[i] * wsp_x));
                t_y[i] = (int)(i * wsp_y2) + 7;
            }

            //for (int i = 0; i < 51; i++) g.DrawLine(Gr, t_y[i], t_x[i], t_y[i + 1], t_x[i + 1]);

            Font cc = new System.Drawing.Font("Arial", 13,FontStyle.Bold);


            int razem = sumaTyg.Sum();


            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            using (cc)
            {
                const int dx = 60;
                int x = 1, y = 120;
                DrawRotatedTextAt(g, -90, "STYCZEŃ",
                    x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "LUTY",
                    x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "MARZEC",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "KWIECIEŃ",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "MAJ",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "CZERWIEC",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "LIPIEC",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "SIERPIEŃ",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "WRZESIEŃ",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "PAŹDZIERNIK",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "LISTOPAD",
                   x, y, cc, Brushes.Black);
                x += dx;
                DrawRotatedTextAt(g, -90, "GRUDZIEŃ",
                   x, y, cc, Brushes.Black);
                x += dx;
            }
            Font cs = new System.Drawing.Font("Arial", 18, FontStyle.Bold);
            g.DrawString(lata_combo.Text, cs, Brushes.Black, 0, 0);
            Font cz = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            g.DrawString("Zaplanowano " + razem.ToString()+" przeglądów", cz, Brushes.Black, 60, 0);

        }

       

        private void DrawRotatedTextAt(Graphics gr, float angle, string txt, int x, int y, Font the_font, Brush the_brush)
        {
            // Save the graphics state.
            System.Drawing.Drawing2D.GraphicsState state = gr.Save();
            gr.ResetTransform();

            // Rotate.
            gr.RotateTransform(angle);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            gr.TranslateTransform(x, y, System.Drawing.Drawing2D.MatrixOrder.Append);

            // Draw the text at the origin.
            gr.DrawString(txt, the_font, the_brush, 0, 0);

            // Restore the graphics state.
            gr.Restore(state);
        }

        private void lata_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (edycja) zapisz();
            if (admin && lata_combo.Text != "-") btn_edytuj.Enabled = true;
            else btn_edytuj.Enabled = false;
            rok = Convert.ToInt32(lata_combo.Items[lata_combo.SelectedIndex]);
            Oblicz_tygodnie();
            Wyswietl();
            
        }
        public GF_postoje.Maszyna UstalIndexPoID(string _ID)
        {
            int indexx = -1;
            foreach (GF_postoje.Maszyna M in FullList_Maszyny)
            {
                if (M.ID == _ID)
                {
                    indexx = FullList_Maszyny.IndexOf(M);
                    break;
                }
            }
            return FullList_Maszyny[indexx];
        }

        public GF_postoje.Karta UstalkartePoID(string _ID)
        {
            int ixK = -1;
            foreach (GF_postoje.Karta K in Karty)
            {
                if (K.nazwa == _ID)
                {
                    ixK = Karty.IndexOf(K);
                    break;
                }
            }
            return Karty[ixK];
        }
        int UstalIndexMaszynyPoCombo(string _linia, string _maszyna)
        {
            int index = -1;
            foreach (GF_postoje.Maszyna M in FullList_Maszyny)
            {
                if (M.Linia == _linia && M.NazwaWys == _maszyna)
                {
                    index = FullList_Maszyny.IndexOf(M);
                    break;
                }
            }
            return index;
        }




        void pokazSpc()
        {
            przeglady_spc_listView.Items.Clear();
            doWykonaniaSpc.Clear();
            foreach (GF_postoje.RekordHarm rek in Harmonogram)
            {
                if (!rek.wykonany && rek.specjalny)
                {
                    int ind = Harmonogram.IndexOf(rek);
                    doWykonaniaSpc.Add(new string[] {rek.sygnatura, rek.maszyna.Linia, rek.maszyna.NazwaWys, rek.karta, rek.aktualnosc, ind.ToString() });
                }
            }
            foreach (string[] spc in doWykonaniaSpc)
            {
                ListViewItem hwiersz = new ListViewItem(spc);// new string[] { spc[0], spc[1], spc[2], spc[3] });
                
                przeglady_spc_listView.Items.Add(hwiersz);
            }
            
            for(int i=0;i< przeglady_spc_listView.Items.Count;i++)
            {
                przeglady_spc_listView.Items[i].Font = normalny;               
                if (Harmonogram[Convert.ToInt32(doWykonaniaSpc[i][5])].status == "Zaległy") przeglady_spc_listView.Items[i].BackColor = Color.LightPink;
            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {

                spc_linia_combo.Items.Clear();
                spc_karta_combo.Items.Clear();
                spc_maszyna_combo.Items.Clear();
                spc_linia_combo.Items.AddRange(Linie.ToArray());
                spc_karta_combo.Items.AddRange(karty_cmb.ToArray());
                spc_linia_combo.SelectedIndex = -1;
                spc_maszyna_combo.SelectedIndex = -1;
                spc_karta_combo.SelectedIndex = -1;
                spc_termin_combo.SelectedIndex = -1;
                spc_co_combo.SelectedIndex = 1;
                spc_powtorz_combo.SelectedIndex = 1;
                spc_cykl_check.Checked = false;
                spc_maszyna_combo.Enabled = false;


                if (admin)
                    {
                    spc_linia_combo.Enabled = true;
                    spc_karta_combo.Enabled = true;
                    spc_termin_combo.Enabled = true;
                    spc_co_combo.Enabled = true;
                    spc_powtorz_combo.Enabled = true;
                    spc_cykl_check.Enabled = true;
                    spc_termin_combo.SelectedIndex = 1;
                    usun_btn.Enabled = true;
                    spc_karta_lbl.Enabled = true;
                    spc_linia_lbl.Enabled = true;
                    spc_maszyna_lbl.Enabled = true;
                    scp_termin_lbl.Enabled = true;
                }
                else
                {
                    spc_linia_combo.Enabled = false;
                    spc_karta_combo.Enabled = false;
                    spc_termin_combo.Enabled = false;
                    spc_co_combo.Enabled = false;
                    spc_powtorz_combo.Enabled = false;
                    spc_cykl_check.Enabled = false;
                    usun_btn.Enabled = false;
                    spc_karta_lbl.Enabled = false;
                    spc_linia_lbl.Enabled = false;
                    spc_maszyna_lbl.Enabled = false;
                    scp_termin_lbl.Enabled = false;
                }
                pokazSpc();
            }
            else
            { 
                wczytajodzera();
                edycja = false;
                btn_anuluj.Enabled = false;
                btn_generuj.Enabled = false;
                btn_zapisz.Enabled = false;
                btn_anuluj.Visible = false;
                btn_generuj.Visible = false;
                btn_zapisz.Visible = false;
                btn_edytuj.Enabled = false;
                harmonogram_tabela.ReadOnly = true;
                g.FillRectangle(Brushes.White, 0, 0, 722, 120);
                harmonogram_tabela.Rows.Clear();
                
            }
        }
        private void spc_cykl_check_CheckedChanged(object sender, EventArgs e)
        {
            if (spc_cykl_check.Checked)
            {
                spc_co_combo.Visible = true;
                co_lbl.Visible = true;
                spc_powtorz_combo.Visible = true;
                powt_lbl.Visible = true;         
            }
            else
            {
                spc_co_combo.Visible = false;
                co_lbl.Visible = false;
                spc_powtorz_combo.Visible = false;
                powt_lbl.Visible = false;
                spc_co_combo.SelectedIndex = 1;
                spc_powtorz_combo.SelectedIndex = 1;
            }
            PrzyciskDodaj();
        }



        private void spc_linia_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (spc_linia_combo.SelectedIndex >= 0)
            {
                string wybr = spc_linia_combo.SelectedItem.ToString();
                List<string> Maszyny = new List<string>();
                foreach (GF_postoje.Maszyna M in FullList_Maszyny) if (wybr == M.Linia && !M.Archiwalne) Maszyny.Add(M.NazwaWys);
                spc_maszyna_combo.Items.Clear();
                spc_maszyna_combo.Items.AddRange(Maszyny.ToArray());
                spc_maszyna_combo.Enabled = true;
                spc_maszyna_combo.SelectedIndex = -1;
                spc_maszyna_combo.Focus();
                spc_maszyna_combo.PerformLayout();
            }
            else
            {
                spc_maszyna_combo.Enabled = false;
            }
            PrzyciskDodaj();
        }

        

        private void spc_maszyna_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrzyciskDodaj();
        }

        private void spc_karta_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrzyciskDodaj();
        }

        private void spc_termin_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrzyciskDodaj();
        }

        private void spc_co_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrzyciskDodaj();
        }

        private void spc_powtorz_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrzyciskDodaj();
        }

        void PrzyciskDodaj()
        {
            if(admin)
            {
                if (spc_linia_combo.SelectedIndex >= 0 && spc_maszyna_combo.SelectedIndex >= 0 && spc_karta_combo.SelectedIndex >= 0) spc_dodaj_btn.Enabled = true;
                else spc_dodaj_btn.Enabled = false;
            }
        }
        private void spc_dodaj_btn_Click(object sender, EventArgs e)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            DateTime datater = DateTime.Now;
            int obecnyTyg = cal.GetWeekOfYear(datater, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int obecnyRok = datater.Year;
            int indMaszyny = UstalIndexMaszynyPoCombo(spc_linia_combo.Text, spc_maszyna_combo.Text);
            GF_postoje.Maszyna M = FullList_Maszyny[indMaszyny];
            GF_postoje.Karta kartt = UstalkartePoID( spc_karta_combo.Text);

            int termin;
            if (spc_termin_combo.SelectedIndex == 0) termin = 0;
            else if (spc_termin_combo.SelectedIndex == 1) termin = 1;
            else if (spc_termin_combo.SelectedIndex == 2) termin = 2;
            else termin = 4;

            int skok;
            if (spc_co_combo.SelectedIndex == 0) skok = 1;
            else if (spc_co_combo.SelectedIndex == 1) skok = 2;
            else if (spc_co_combo.SelectedIndex == 2) skok = 4;
            else if (spc_co_combo.SelectedIndex == 3) skok = 13;
            else if (spc_co_combo.SelectedIndex == 4) skok = 26;
            else skok = 52;

            int powt;
            if (!spc_cykl_check.Checked) powt = 1;
            else if (spc_powtorz_combo.SelectedIndex == 0) powt = 2;
            else if (spc_powtorz_combo.SelectedIndex == 1) powt = 3;
            else  powt = 4;


            for (int i=0; i<(skok*powt);i=i+skok)
            {
                int rokk = obecnyRok;
                int tygg = obecnyTyg + termin +i;
                while (tygg > 52)
                {
                    rokk = rokk + 1;
                    tygg = tygg - 52;
                }
                GF_postoje.RekordHarm nju = new GF_postoje.RekordHarm(rokk.ToString(), tygg.ToString(), M, kartt, "1", "0");
                int sertuj = 100000;
                int insr = Harmonogram.Count;
                foreach (GF_postoje.RekordHarm H in Harmonogram)
                {
                    if (!H.wykonany)
                    {
                        int roz = nju.jak_dawno - H.jak_dawno;
                        if (roz < sertuj)
                        {
                            sertuj = roz;
                            insr = Harmonogram.IndexOf(H)+1;

                        }
                    }
                }
                Harmonogram.Insert(insr, nju);
            }
            Sortuj();
            pokazSpc();
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int LogDanych = Convert.ToInt32(log[0]);
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv"))
            {

                foreach (GF_postoje.RekordHarm rek in Harmonogram)
                {
                    ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                    row.Add(rek.rok.ToString());
                    row.Add(rek.tydzien.ToString());
                    row.Add(rek.maszyna.ID);
                    row.Add(rek.karta);
                    row.Add(rek.specjalnyex);
                    row.Add(rek.wykonanyex);
                    writer.WriteRow(row);
                }
            }
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
            spc_linia_combo.SelectedIndex = -1;
            spc_maszyna_combo.SelectedIndex = -1;
            spc_karta_combo.SelectedIndex = -1;
            spc_termin_combo.SelectedIndex = -1;
            spc_co_combo.SelectedIndex = 1;
            spc_powtorz_combo.SelectedIndex = 1;
            spc_cykl_check.Checked = false;
            spc_maszyna_combo.Enabled = false;
        }
        private void usun_btn_Click(object sender, EventArgs e)
        {
            
            if (przeglady_spc_listView.SelectedIndices != null)
            {
                if(przeglady_spc_listView.SelectedIndices.Count > 0)
                {
                    Harmonogram.RemoveAt(Convert.ToInt32(doWykonaniaSpc[przeglady_spc_listView.SelectedIndices[0]][5]));
                    pokazSpc();
                    string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
                    int LogDanych = Convert.ToInt32(log[0]);
                    using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\HarmonogramPrzegladow.csv"))
                    {

                        foreach (GF_postoje.RekordHarm rek in Harmonogram)
                        {
                            ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                            row.Add(rek.rok.ToString());
                            row.Add(rek.tydzien.ToString());
                            row.Add(rek.maszyna.ID);
                            row.Add(rek.karta);
                            row.Add(rek.specjalnyex);
                            row.Add(rek.wykonanyex);
                            writer.WriteRow(row);
                        }
                    }
                    LogDanych = LogDanych + 1;
                    System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
                }
            }
            
        }

        bool Spr(GF_postoje.RekordHarm xx, GF_postoje.RekordHarm yy)
        {
            if (xx.rok > yy.rok) return true;
            else if (xx.rok == yy.rok)
            {
                if (xx.tydzien > yy.tydzien) return true;
                else if (xx.tydzien == yy.tydzien)
                {
                    if (!xx.specjalny && yy.specjalny) return true;
                    else if (xx.specjalny && !yy.specjalny) return false;
                    else
                    {
                        if (FullList_Maszyny.IndexOf(xx.maszyna) > FullList_Maszyny.IndexOf(yy.maszyna) )return true;
                        else return false;
                    }
                }
                else return false;
            }
            else  return false;
        }

        void Sortuj()
        {
            List<GF_postoje.RekordHarm> Harm_kopia = new List<GF_postoje.RekordHarm>();
            Harm_kopia.AddRange(Harmonogram);

            int dl = Harmonogram.Count;
            Harmonogram.Clear();

            Harmonogram.Add(Harm_kopia[0]);
            Harm_kopia.RemoveAt(0);
            for(int i=1;i<dl;i++)
            {
                int wynik = 0;
                foreach (GF_postoje.RekordHarm rek in Harmonogram)
                {
                    if (!Spr(Harm_kopia[0], rek)) break;
                    wynik++;
                }
                Harmonogram.Insert(wynik,Harm_kopia[0]);
                Harm_kopia.RemoveAt(0);
            }

        }

    }
}
