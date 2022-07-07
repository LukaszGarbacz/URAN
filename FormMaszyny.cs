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
    public partial class FormMaszyny : Form
    {
        List<GF_postoje.Maszyna> FullList_Maszyny;
        List<GF_postoje.CellPostoj> FullList_Postoje;
        List<GF_postoje.Przeglad> FullList_Przeglady;
        bool admin;
        //GF_postoje.Czesc wybrana;
        string indeks;
        string SciezkaDoDanych;
        string SciezkaDoKatalogu;
        bool kolejnosc = true;
        bool nowy = false;
        int zaznaczony;
        string folder = "";

        string[] linie;
        string[] oznaczenia;

        Font czci = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));

        public FormMaszyny(List<GF_postoje.Maszyna> _maszyny, List<GF_postoje.CellPostoj> _postoje, List<GF_postoje.Przeglad> _przeglady, string _indeks, bool _admin, string _path, string _pathKat)
        {
            InitializeComponent();
            FullList_Maszyny = _maszyny;
            FullList_Postoje = _postoje;
            FullList_Przeglady = _przeglady;
            admin = _admin;
            indeks = _indeks;
            SciezkaDoDanych = _path;
            SciezkaDoKatalogu = _pathKat;

            List<string> lin = new List<string>();
            List<string> ozn = new List<string>();
            foreach (GF_postoje.Maszyna M in FullList_Maszyny)
            {
                if (!lin.Contains(M.Linia))
                {
                    lin.Add(M.Linia);
                    ozn.Add(M.ID.Substring(0, 2));
                }
            }
            linie = lin.ToArray();
            oznaczenia = ozn.ToArray();
            LiniaCB.Items.AddRange(linie);

            
            park_ustaw_UD.Maximum = FullList_Maszyny.Count;

            if( admin)
            {
                groupBox3.Visible = true;
                Zapisz2BTN.Visible = false;
                EdytujBTN.Visible = true;
            }
            else
            {
                groupBox3.Visible = false;
                Zapisz2BTN.Visible = false;
                EdytujBTN.Visible = false;
            }

            

            Wyswietl();
        }
        private void Wyswietl()
        {

            int first;
            if (tabelka_maszynGridView.Rows.Count > 0 && tabelka_maszynGridView.SelectedRows.Count > 0) first = tabelka_maszynGridView.FirstDisplayedScrollingRowIndex;
            else first = 0;

            tabelka_maszynGridView.ScrollBars = ScrollBars.None;

            int lance = -1, lawrence = -1;
            if (tabelka_maszynGridView.SelectedRows.Count > 0) lance = tabelka_maszynGridView.SelectedRows[0].Index;
            if (tabelka_maszynGridView.Rows.Count > 0) lawrence = tabelka_maszynGridView.FirstDisplayedScrollingRowIndex;

            tabelka_maszynGridView.Rows.Clear();
            tabelka_maszynGridView.Font = czci;
            foreach (GF_postoje.Maszyna M in FullList_Maszyny)
            {
                //nie dodawać tu warunkowania
                object[] wiersz = new object[5];
                wiersz[0] = M.Linia;
                wiersz[1] = M.NazwaWys;
                wiersz[2] = M.ID;
                wiersz[3] = M.ArchiwalneStr;
                wiersz[4] = FullList_Maszyny.IndexOf(M);
                tabelka_maszynGridView.Rows.Add(wiersz);

                int ioi = tabelka_maszynGridView.Rows.Count - 1;
                if (tabelka_maszynGridView.Rows[ioi].Cells[3].Value.ToString() == "1") tabelka_maszynGridView.Rows[ioi].DefaultCellStyle.Font = new System.Drawing.Font(czci, FontStyle.Italic);
                else tabelka_maszynGridView.Rows[ioi].DefaultCellStyle.Font = new System.Drawing.Font(czci, FontStyle.Regular);


            }
          
         
            
            tabelka_maszynGridView.ScrollBars = ScrollBars.Vertical;
            


            int ind = -1;
            if (indeks == "new")
            {
                trybNowe();
            }
            else if (indeks != "done")
            {
                
                foreach (GF_postoje.Maszyna M in FullList_Maszyny)
                {
                    if (M.ID == indeks) ind = FullList_Maszyny.IndexOf(M);
                }
                if (ind != -1)
                {
                    tabelka_maszynGridView.Rows[1].Selected = true;
                    tabelka_maszynGridView.Rows[ind].Selected = true;
                    tabelka_maszynGridView.FirstDisplayedScrollingRowIndex = ind;
                }
                else
                {
                    tabelka_maszynGridView.Rows[1].Selected = true;
                    if (lance != -1 && lance< tabelka_maszynGridView.Rows.Count) tabelka_maszynGridView.Rows[lance].Selected = true;
                    else tabelka_maszynGridView.Rows[0].Selected = true;
                    if (tabelka_maszynGridView.Rows.Count > 0 && lawrence != -1) tabelka_maszynGridView.FirstDisplayedScrollingRowIndex = lawrence;
                }

            }
            else
            {
                if (lance != -1 && lance < tabelka_maszynGridView.Rows.Count) tabelka_maszynGridView.Rows[lance].Selected = true;
                tabelka_maszynGridView.FirstDisplayedScrollingRowIndex = first;
            }
            
            indeks = "done";
           
        }  

        private void tabelka_maszynGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (tabelka_maszynGridView.Rows.Count > 0 && tabelka_maszynGridView.SelectedRows.Count > 0)
            {
                zaznaczony = tabelka_maszynGridView.SelectedRows[0].Index;
                for (int i = 0; i < tabelka_maszynGridView.Rows.Count; i++)
                {
                    if (tabelka_maszynGridView.Rows[i].Cells[3].Value.ToString() == "1") tabelka_maszynGridView.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font(czci, FontStyle.Italic);
                    else tabelka_maszynGridView.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font(czci, FontStyle.Regular);
                }
                tabelka_maszynGridView.Rows[zaznaczony].DefaultCellStyle.Font = new System.Drawing.Font(czci, FontStyle.Bold);
                if (!nowy)
                {                  
                    GF_postoje.Maszyna M = FullList_Maszyny[zaznaczony];
                    indeksTB.Text = M.ID;
                    liniaTB.Text = M.Linia;
                    //if (LiniaCB.Items.Contains(M.Linia)) LiniaCB.SelectedIndex = LiniaCB.Items.IndexOf(M.Linia);
                    NazwaWysTB.Text = M.NazwaWys;
                    nazwaTB.Text = M.Nazwa;
                    KartaTB.Text = M.Karta;
                    if (M.Archiwalne) arch_checkBox.Checked = true;
                    else arch_checkBox.Checked = false;
                    Podzespoly_listView.Items.Clear();
                    foreach (string pdz in M.Podzespoly) Podzespoly_listView.Items.Add(pdz);
                    if (!kolejnosc)
                    {
                        park_ustaw_UD.Value = zaznaczony;
                        if (zaznaczony == 0) park_Up1_btn.Enabled = false;
                        else park_Up1_btn.Enabled = true;
                        if (zaznaczony == tabelka_maszynGridView.Rows.Count - 1) park_down1_btn.Enabled = false;
                        else park_down1_btn.Enabled = true;
                    }

                    WyswietlInf();
                }              
            }
        }

        void WyswietlInf()
        {
            GF_postoje.Maszyna Masz = FullList_Maszyny[zaznaczony];
            tabelka_przeglady.Rows.Clear();
            tabelka_statystyki.Rows.Clear();
            tabelka_pliki.Rows.Clear();

            {
                foreach (GF_postoje.Przeglad P in FullList_Przeglady)
                {
                    if ( P.maszyna.ID == Masz.ID)
                    {
                        string link = "-", patch = "-";
                        if (P.status)
                        {
                            string naz = P.sygnatura.Replace('/', '-');
                            string fileName = SciezkaDoKatalogu + @"\Wykonane_Przeglady\" + naz + ".docx";
                            var fi9 = new FileInfo(fileName);
                            if (fi9.Exists)
                            {
                                link = "link";
                                patch = fileName;
                            }
                        }

                        object[] wiersz = new object[5];
                        wiersz[0] = P.sygnatura;
                        wiersz[1] = P.wykonał.Imie.ElementAt(0).ToString() + ". " + P.wykonał.Nazwisko;
                        wiersz[2] = P.wynik;
                        wiersz[3] = link;
                        wiersz[4] = patch;
                        tabelka_przeglady.Rows.Add(wiersz);
                    }
                }

                int vje = 0;
                int vdw = tabelka_przeglady.Rows.Count;
                int vcz = 0;
                int vpi = 0;
                int vsz = 0; 
                int vsi = 0;
                int vos = 0;
                int vde = 0;
                int vqj = 0;
                int vqd = 0;
                int vqt = 0;
                string vqp = "0";
                int vqs = 0;
                List<string[]> pliczki = new List<string[]>();

                
                foreach (GF_postoje.CellPostoj P in FullList_Postoje)
                {
                    if (P.maszyna.ID == Masz.ID)
                    {
                        vje++;
                        if (P.dzialanie == "Naprawa") vcz++;
                        if (P.dzialanie == "Regulacja") vpi++;
                        if (P.dzialanie == "Konserwacja") vsz++;
                        if (P.dzialanie == "Przezbrajanie") vsi++;
                        if (P.dzialanie == "Modernizacja") vos++;
                        vde = vde + P.tPostojInt;
                        vqj = vqj + P.tPraca;
                        vqp = P.niceData;
                        vqs = vqs + P.Ufiles.Count;
                        foreach (string S in P.Ufiles)
                        {
                            //string fileName = SciezkaDoKatalogu + @"\Wykonane_Przeglady\" + naz + ".docx";
                            var fi0 = new FileInfo(S);
                            if (fi0.Exists)
                            {
                                string[] wrzuc = {fi0.Name, P.dataStr, "Archiwum", fi0.FullName };
                                pliczki.Add(wrzuc);
                            }
                        }
                    }
                }

                if (vje != 0) vqd = vde / vje;
                if (vje != 0) vqt = vqj / vje;

                List<string[]> staty = new List<string[]>();
                string[] jed = { "Interwencji:  ", vje.ToString(), "" };
                string[] dwa = { "Przeglądów:  ", vdw.ToString(),"" };
                string[] trz = { "", "", "" };
                string[] czt = { "Napraw: ", vcz.ToString(), "" };
                string[] pie = { "Regulacji: ", vpi.ToString(), "" };
                string[] sze = { "Konserwacji:  ", vsz.ToString(), "" };
                string[] sie = { "Przezbrojeń:  ", vsi.ToString(), "" };
                string[] osi = { "Modernizacji:  ", vos.ToString(), "" };
                string[] dzi = { "", "", "" };
                string[] des = { "Razem postoju: ", vde.ToString(), "minut" };
                string[] qje = { "Razem pracy:  ", vqj.ToString(), "minut" };
                string[] qdw = { "Średni czas posoju:  ", vqd.ToString(), "minut" };
                string[] qtr = { "Średni czas pracy:  ", vqt.ToString(), "minut" };
                string[] qcz = { "", "", "" };
                string[] qpi = { "Pierwszy wpis:  ","", vqp};
                string[] qsz = { "Powiązanych plików:  ", vqs.ToString(), "" };



                staty.Add(jed);
                staty.Add(dwa);
                staty.Add(trz);
                staty.Add(czt);
                staty.Add(pie);
                staty.Add(sze);
                staty.Add(sie);
                staty.Add(osi);
                staty.Add(dzi);
                staty.Add(des);
                staty.Add(qje);
                staty.Add(qdw);
                staty.Add(qtr);
                staty.Add(qcz);
                staty.Add(qpi);
                staty.Add(qsz);


                foreach (string[] S in staty)
                {
                    object[] wiersz = S;
                    tabelka_statystyki.Rows.Add(S);
                }

                foreach (string[] S in pliczki)
                {
                    object[] wiersz = S;
                    tabelka_pliki.Rows.Add(S);
                }

                folder = SciezkaDoKatalogu + @"\Archiwum\" + Masz.Linia + "\\" + Masz.NazwaWys;
                var fi1 = new DirectoryInfo(folder);
                if (fi1.Exists) LokalizacjaBTN.Enabled = true;
                else LokalizacjaBTN.Enabled = false;

            }
        }

        private void park_kolejnosc_btn_Click(object sender, EventArgs e)
        {
            if(kolejnosc)
            {
                kolejnosc = false;
                park_kolejnosc_btn.Text = "Zapisz";
                park_Up1_btn.Enabled = true;
                park_down1_btn.Enabled = true;
                label4.Enabled = true;
                park_ustaw_UD.Enabled = true;
                tabelka_maszynGridView.Columns[4].Visible = true;
                park_ok_btn.Enabled = true;

                Statystyki_Box.Visible = false;
                groupBox1.Visible = false;
                groupBox4.Visible = false;

                EdytujBTN.Enabled = false;
                park_dodaj_btn.Enabled = false;
                tabelka_maszynGridView.ReadOnly = false;
                park_ustaw_UD.Value = tabelka_maszynGridView.SelectedRows[0].Index;

            }
            else
            {
                kolejnosc = true;
                park_kolejnosc_btn.Text = "Zmień kolejność";
                park_Up1_btn.Enabled = false;
                park_down1_btn.Enabled = false;
                label4.Enabled = false;
                park_ustaw_UD.Enabled = false;
                tabelka_maszynGridView.Columns[4].Visible = false;
                park_ok_btn.Enabled = false;

                EdytujBTN.Enabled = true;
                park_dodaj_btn.Enabled = true;
                tabelka_maszynGridView.ReadOnly = true;
                park_ustaw_UD.Value = 0;

                Statystyki_Box.Visible = true;
                groupBox1.Visible = true;
                groupBox4.Visible = true;

                Zapisz();
            }
        }

        private void park_Up1_btn_Click(object sender, EventArgs e)
        {
            if (tabelka_maszynGridView.Rows.Count > 0 && tabelka_maszynGridView.SelectedRows.Count > 0)
            {
                int indyk = tabelka_maszynGridView.SelectedRows[0].Index;
                if (indyk > 0)
                {
                    GF_postoje.Maszyna M = FullList_Maszyny[indyk];
                    FullList_Maszyny.RemoveAt(indyk);
                    FullList_Maszyny.Insert(indyk - 1, M);
                    int lawreence2 = tabelka_maszynGridView.FirstDisplayedScrollingRowIndex;
                    Wyswietl();
                    tabelka_maszynGridView.Rows[indyk - 1].Selected = true;
                    //tabelka_maszynGridView.FirstDisplayedScrollingRowIndex = lawreence2;
                }
            }
        }

        private void park_down1_btn_Click(object sender, EventArgs e)
        {
            if (tabelka_maszynGridView.Rows.Count > 0 && tabelka_maszynGridView.SelectedRows.Count > 0)
            {
                int indyk = tabelka_maszynGridView.SelectedRows[0].Index;
                if (indyk < tabelka_maszynGridView.Rows.Count-1)
                {
                    GF_postoje.Maszyna M = FullList_Maszyny[indyk];
                    FullList_Maszyny.RemoveAt(indyk);
                    FullList_Maszyny.Insert(indyk + 1, M);
                    int lawreence2 = tabelka_maszynGridView.FirstDisplayedScrollingRowIndex;
                    Wyswietl();
                    tabelka_maszynGridView.Rows[indyk + 1].Selected = true;
                    //tabelka_maszynGridView.FirstDisplayedScrollingRowIndex = lawreence2;
                }
            }
        }

        private void park_ok_btn_Click(object sender, EventArgs e)
        {
            if (tabelka_maszynGridView.Rows.Count > 0 && tabelka_maszynGridView.SelectedRows.Count > 0)
            {
                int indyk = tabelka_maszynGridView.SelectedRows[0].Index;
                int docel = Convert.ToInt32(park_ustaw_UD.Value);
                if (indyk < tabelka_maszynGridView.Rows.Count - 1)
                {
                    GF_postoje.Maszyna M = FullList_Maszyny[indyk];
                    FullList_Maszyny.RemoveAt(indyk);
                    FullList_Maszyny.Insert(docel, M);
                    Wyswietl();
                    tabelka_maszynGridView.Rows[docel].Selected = true;
                }
            }
        }

        private void EdytujBTN_Click(object sender, EventArgs e)
        {
            EdytujBTN.Visible = false;
            ZapiszBTN.Visible = true;
            park_dodaj_btn.Enabled = false;
            park_kolejnosc_btn.Enabled = false;
            NazwaWysTB.ReadOnly = false;
            nazwaTB.ReadOnly = false;
            arch_checkBox.Enabled = true;
            LiniaCB.Enabled = true;
            tabelka_maszynGridView.Enabled = false;

            Podzespoly_listView.LabelEdit = true;
            podNowyBTN.Visible = true;
            podUsunBTN.Visible = true;
            podUpBTN.Visible = true;
            podDownBTN.Visible = true;
        }

        private void ZapiszBTN_Click(object sender, EventArgs e)
        {
            EdytujBTN.Visible = true;
            ZapiszBTN.Visible = false;
            park_dodaj_btn.Enabled = true;
            park_kolejnosc_btn.Enabled = true;
            NazwaWysTB.ReadOnly = true;
            nazwaTB.ReadOnly = true;
            arch_checkBox.Enabled = false;
            tabelka_maszynGridView.Enabled = true;

            Podzespoly_listView.LabelEdit = false;
            podNowyBTN.Visible = false;
            podUsunBTN.Visible = false;
            podUpBTN.Visible = false;
            podDownBTN.Visible = false;

            

            GF_postoje.Maszyna M = FullList_Maszyny[zaznaczony];
            M.NazwaWys = NazwaWysTB.Text;
            M.Nazwa = nazwaTB.Text;
            if (arch_checkBox.Checked)
            {
                M.Archiwalne = true;
                M.ArchiwalneStr = "1";
            }
            else
            {
                M.Archiwalne = false;
                M.ArchiwalneStr = "0";
            }
            M.Podzespoly.Clear();
            foreach (ListViewItem l in Podzespoly_listView.Items) M.dodajPodzespol(l.Text);
            Zapisz();
            Wyswietl();

        }

        private void Podzespoly_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Podzespoly_listView.SelectedIndices.Count>0)
            {
                int ten = Podzespoly_listView.SelectedIndices[0];
                podUsunBTN.Enabled = true;
                if (ten == 0) podUpBTN.Enabled = false;
                else podUpBTN.Enabled = true;
                if (ten == Podzespoly_listView.Items.Count-1) podDownBTN.Enabled = false;
                else podDownBTN.Enabled = true;
            }
            else
            {
                podUsunBTN.Enabled = false;
                podDownBTN.Enabled = false;
                podUpBTN.Enabled = false;
            }
        }

        private void podUsunBTN_Click(object sender, EventArgs e)
        {
            if (Podzespoly_listView.SelectedIndices.Count >0)
            {
                int ten = Podzespoly_listView.SelectedIndices[0];
                Podzespoly_listView.Items[ten].Remove();
            }
        }

        private void podNowyBTN_Click(object sender, EventArgs e)
        {
            Podzespoly_listView.Items.Add("new");
            Podzespoly_listView.Items[Podzespoly_listView.Items.Count-1].Selected = true;
            Podzespoly_listView.Items[Podzespoly_listView.Items.Count - 1].BeginEdit();
        }

        private void podUpBTN_Click(object sender, EventArgs e)
        {
            string[] pdzp = new string [Podzespoly_listView.Items.Count];
            foreach (ListViewItem l in Podzespoly_listView.Items) pdzp[Podzespoly_listView.Items.IndexOf(l)] = l.Text;
            int ten = Podzespoly_listView.SelectedIndices[0];
            string buf = pdzp[ten - 1];
            pdzp[ten - 1] = pdzp[ten];
            pdzp[ten] = buf;
            Podzespoly_listView.Items.Clear();
            foreach (string s in pdzp) Podzespoly_listView.Items.Add(s);        
            Podzespoly_listView.Items[ten-1].Selected = true;
        }

        private void podDownBTN_Click(object sender, EventArgs e)
        {
            string[] pdzp = new string[Podzespoly_listView.Items.Count];
            foreach (ListViewItem l in Podzespoly_listView.Items) pdzp[Podzespoly_listView.Items.IndexOf(l)] = l.Text;
            int ten = Podzespoly_listView.SelectedIndices[0];
            string buf = pdzp[ten + 1];
            pdzp[ten + 1] = pdzp[ten];
            pdzp[ten] = buf;
            Podzespoly_listView.Items.Clear();
            foreach (string s in pdzp) Podzespoly_listView.Items.Add(s);
            Podzespoly_listView.Items[ten + 1].Selected = true;
        }

        private void Podzespoly_listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string nowy = e.Label;
            if (e.Label != null)
            {
                if (e.Label.Length > 30)
                {
                    e.CancelEdit = true;
                    MessageBox.Show("Maksymalna długość nazwy podzespołu wynosi 30 znaków.");
                    return;
                }
            }
        }

        void trybNowe ()
        {
            nowy = true;
            Zapisz2BTN.Visible = true;
            Zapisz2BTN.Enabled = false;
            AnulujBTN.Visible = true;

            object[] wiersz = new object[5];
            wiersz[0] = "";
            wiersz[1] = "new";
            wiersz[3] = "1";
            tabelka_maszynGridView.Rows.Insert(0, wiersz);

            tabelka_maszynGridView.Rows[0].Selected = true;
            tabelka_maszynGridView.FirstDisplayedScrollingRowIndex = 0;
            tabelka_maszynGridView.Enabled = false;

            park_kolejnosc_btn.Enabled = false;
            park_dodaj_btn.Enabled = false;

            indeksTB.Text = "";
            liniaTB.Text = "";
            liniaTB.Visible = false;
            LiniaCB.Enabled = true;
            if (!LiniaCB.Items.Contains("nowy")) LiniaCB.Items.Add("nowy");
            LiniaCB.Visible = true;
            LiniaCB.DroppedDown = true;
            KartaTB.Text = "";
            NazwaWysTB.Text = "";
            nazwaTB.Text = "";
            Podzespoly_listView.Items.Clear();
            arch_checkBox.Checked = false;
            Statystyki_Box.Visible = false;
            groupBox1.Visible = false;
            groupBox4.Visible = false;
        }

        private void park_dodaj_btn_Click(object sender, EventArgs e)
        {
            trybNowe();
        }

        private void AnulujBTN_Click(object sender, EventArgs e)
        {
            nowy = false;
            park_kolejnosc_btn.Enabled = true;
            park_dodaj_btn.Enabled = true;
            Zapisz2BTN.Visible = false;
            AnulujBTN.Visible = false;
            EdytujBTN.Visible = true;
            liniaTB.Visible = true;
            LiniaCB.Enabled = false;
            LiniaCB.Visible = false;
            tabelka_maszynGridView.Enabled = true;

            NazwaWysTB.ReadOnly = true;
            nazwaTB.ReadOnly = true;
            arch_checkBox.Enabled = false;

            Podzespoly_listView.LabelEdit = false;
            podNowyBTN.Visible = false;
            podUsunBTN.Visible = false;
            podUpBTN.Visible = false;
            podDownBTN.Visible = false;

            indeksTB.ReadOnly = true;
            liniaTB.ReadOnly = true;

            Statystyki_Box.Visible = true;
            groupBox1.Visible = true;
            groupBox4.Visible = true;

            Wyswietl();
        }

        private void Zapisz2BTN_Click(object sender, EventArgs e)
        {
            string ar = "0";
            if (arch_checkBox.Checked) ar = "1";
            GF_postoje.Maszyna N = new GF_postoje.Maszyna(wyczysc(indeksTB.Text), wyczysc(NazwaWysTB.Text), wyczysc(nazwaTB.Text), wyczysc(liniaTB.Text), ar, "");
            foreach (ListViewItem i in Podzespoly_listView.Items) N.dodajPodzespol(wyczysc(i.Text));
            FullList_Maszyny.Insert(0, N);

            nowy = false;
            park_kolejnosc_btn.Enabled = true;
            park_dodaj_btn.Enabled = true;
            Zapisz2BTN.Visible = false;
            AnulujBTN.Visible = false;
            EdytujBTN.Visible = true;
            liniaTB.Visible = true;
            LiniaCB.Enabled = false;
            LiniaCB.Visible = false;
            tabelka_maszynGridView.Enabled = true;

            NazwaWysTB.ReadOnly = true;
            nazwaTB.ReadOnly = true;
            arch_checkBox.Enabled = false;

            Podzespoly_listView.LabelEdit = false;
            podNowyBTN.Visible = false;
            podUsunBTN.Visible = false;
            podUpBTN.Visible = false;
            podDownBTN.Visible = false;

            Statystyki_Box.Visible = true;
            groupBox1.Visible = true;
            groupBox4.Visible = true;

            indeksTB.ReadOnly = true;
            liniaTB.ReadOnly = true;

            Zapisz();

            List<string> lin = new List<string>();
            List<string> ozn = new List<string>();
            foreach (GF_postoje.Maszyna M in FullList_Maszyny)
            {
                if (!lin.Contains(M.Linia))
                {
                    lin.Add(M.Linia);
                    ozn.Add(M.ID.Substring(0, 2));
                }
            }
            linie = lin.ToArray();
            oznaczenia = ozn.ToArray();
            LiniaCB.Items.AddRange(linie);


            park_ustaw_UD.Maximum = FullList_Maszyny.Count;
            Wyswietl();

        }

        private void Zapisz()
        {
            this.Enabled = false;
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int LogDanych = Convert.ToInt32(log[0]);
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
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaPodzespolow.csv"))
            {
                foreach (GF_postoje.Maszyna M in FullList_Maszyny)
                {
                    foreach (string p in M.Podzespoly)
                    {
                        ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                        row.Add(M.ID);
                        row.Add(p);
                        writer.WriteRow(row);
                    }
                }
            }
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());
            this.Enabled = true;
        }

        private void LiniaCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LiniaCB.SelectedIndex >-1)
            {
                if (LiniaCB.SelectedItem.ToString() != "nowy")
                {
                    bool mamy;
                    int ser = 1;
                    string newID;
                    do
                    {
                        mamy = true;
                        newID = oznaczenia[LiniaCB.SelectedIndex] + ser.ToString("D3");
                        foreach (GF_postoje.Maszyna M in FullList_Maszyny) if (M.ID == newID) mamy = false;
                        ser++;
                    }
                    while (!mamy);
                    indeksTB.Text = newID;

                    NazwaWysTB.ReadOnly = false;
                    nazwaTB.ReadOnly = false;
                    arch_checkBox.Enabled = true;

                    Podzespoly_listView.LabelEdit = true;
                    podNowyBTN.Visible = true;
                    podUsunBTN.Visible = true;
                    podUpBTN.Visible = true;
                    podDownBTN.Visible = true;

                    indeksTB.ReadOnly = true;
                    liniaTB.Text = LiniaCB.Text;
                    liniaTB.Visible = true;
                    liniaTB.ReadOnly = true;
                    LiniaCB.Enabled = false;
                    LiniaCB.Visible = false;

                }
                else
                {
                    LiniaCB.Enabled = false;
                    LiniaCB.Visible = false;
                    liniaTB.ReadOnly = false;
                    liniaTB.Visible = true;
                    indeksTB.ReadOnly = false;

                    NazwaWysTB.ReadOnly = false;
                    nazwaTB.ReadOnly = false;
                    arch_checkBox.Enabled = true;

                    Podzespoly_listView.LabelEdit = true;
                    podNowyBTN.Visible = true;
                    podUsunBTN.Visible = true;
                    podUpBTN.Visible = true;
                    podDownBTN.Visible = true;
                }
            }
        }

        private void indeksTB_Leave(object sender, EventArgs e)
        {
            if (!indeksTB.ReadOnly)
            {
                bool bum = false;
                if (indeksTB.Text.Length == 5)
                {
                    indeksTB.Text = indeksTB.Text.ToUpper();
                    string oz = indeksTB.Text.Substring(0, 2);
                    string num = indeksTB.Text.Substring(2, 3);
                    if (oznaczenia.Contains(oz) || num != "001") bum = true;
                }
                else bum = true;
                if (bum)
                {
                    MessageBoxButtons przc = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show("Indeks musi składać się z dwuliterowego znacznika linii który nie był do tej pory używany oraz numeru 001 \n Na przykład: XX001", "Indeks nieprawidłowy", przc);
                    //if (result == System.Windows.Forms.DialogResult.Yes)
                    indeksTB.Text = "";
                }
            }
        }

        void sprawdz()
        {
            if (nowy)
            {
                if (indeksTB.Text.Length == 5 && liniaTB.Text != "" && NazwaWysTB.Text != "" && nazwaTB.Text != "") Zapisz2BTN.Enabled = true;
                else Zapisz2BTN.Enabled = false;
            }
        }

        private void NazwaWysTB_TextChanged(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void liniaTB_Leave(object sender, EventArgs e)
        {
            if(!liniaTB.ReadOnly)
            {
                if (linie.Contains(liniaTB.Text))
                {
                    MessageBox.Show("Nazwa linii musi być unikatowa");
                    liniaTB.Text = "";
                }
            }
        }

        private void liniaTB_TextChanged(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void indeksTB_TextChanged(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void nazwaTB_TextChanged(object sender, EventArgs e)
        {
            sprawdz();
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

        private void tabelka_przeglady_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             if (tabelka_przeglady.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "-" && e.ColumnIndex==3)
             {
                Process.Start("WINWORD.EXE", tabelka_przeglady.Rows[e.RowIndex].Cells[4].Value.ToString());
             }

        }

        private void LokalizacjaBTN_Click(object sender, EventArgs e)
        {
            var fi1 = new DirectoryInfo(folder);
            if (fi1.Exists) Process.Start(fi1.FullName);
        }

        private void tabelka_pliki_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( e.ColumnIndex == 0)
            {
                var fi2 = new FileInfo(tabelka_pliki.Rows[e.RowIndex].Cells[3].Value.ToString());
                if (fi2.Exists) Process.Start(fi2.FullName);             
            }
        }
    }
}
