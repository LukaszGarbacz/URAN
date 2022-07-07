using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.IO;

namespace Utrzymanie_Ruchu___APP_Niepruszewo
{
    public partial class FormKarty : Form
    {
        List<GF_postoje.Karta> FullList_Karty;
        string SciezkaDoDanych;
        bool admin;
        bool nowy = false;
        int selRow = 0;

        public FormKarty(string _sciezka, bool _admin)
        {
            InitializeComponent();
            SciezkaDoDanych = _sciezka;
            admin = _admin;
            FullList_Karty = new List<GF_postoje.Karta>();
            using (ReadWriteCsv.CsvFileReader CSVkarty = new ReadWriteCsv.CsvFileReader(SciezkaDoDanych + @"\ListaKart.csv"))
            {
                ReadWriteCsv.CsvRow row = new ReadWriteCsv.CsvRow();
                while (CSVkarty.ReadRow(row))
                {
                    string[] rekord = row.LineText.Split(';');
                    GF_postoje.Karta nowy = new GF_postoje.Karta(rekord);
                    FullList_Karty.Add(nowy);
                }
                Wyswietl(0);
            }
            nazwa_combo.Items.Clear();
            foreach (GF_postoje.Karta karta in FullList_Karty) nazwa_combo.Items.Add(karta.nazwa);
            nazwa_combo.SelectedIndex = 0;
            if(!admin)
            {
                karty_edycja_btn.Enabled = false;
                karty_nowa_btn.Enabled = false;
                karty_import_btn.Enabled = false;
            }
            
        }
        public void Wyswietl(int indeks)
        {
            Kolumny();
            System.Drawing.Font TenRegular = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            for (int i = 1; i < FullList_Karty[indeks].szer; i++)
            {
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                cell.Style.WrapMode = DataGridViewTriState.True;
                cell.Style.Font = TenRegular;
                DataGridViewColumn column = new DataGridViewColumn();
                column.Name = "Czynnosc_" + i.ToString();
                column.HeaderText = "Czynność " + i.ToString();            
                column.CellTemplate = cell;
                column.Width = 250;
                column.HeaderCell.Style.Font = TenRegular;
                karty_tabela.Columns.Add(column);
            }
          
            foreach (List<string> row in FullList_Karty[indeks].tabelka)
            {
                int szer = row.Count;
                object[] wiersz = new object[szer + 2];
                wiersz[0] = "[Usuń]";
                wiersz[1] = row[0];
                wiersz[2] = row[1];
                for(int i = 2; i< szer; i++)
                {
                    wiersz[i + 1] = row[i];
                }
                karty_tabela.Rows.Add(wiersz);
            }
            
        }

        private void Kolumny()
        {
            karty_tabela.Rows.Clear();
            karty_tabela.Columns.Clear();

            System.Drawing.Font TenBold = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            System.Drawing.Font TenRegular = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));

            DataGridViewCell cell = new DataGridViewTextBoxCell();
            cell.Style.WrapMode = DataGridViewTriState.True;
            cell.Style.Font = TenRegular;

            DataGridViewCell cellUsun = new DataGridViewTextBoxCell();
            cellUsun.Style.WrapMode = DataGridViewTriState.False;
            cellUsun.Style.Font = TenRegular;
            cellUsun.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewCell cellNaglowek = new DataGridViewTextBoxCell();
            cellNaglowek.Style.WrapMode = DataGridViewTriState.True;
            cellNaglowek.Style.Font = TenBold;

            DataGridViewColumn Usun = new DataGridViewColumn();           
            Usun.Name = "Usun";
            Usun.HeaderText = "";
            Usun.CellTemplate = cellUsun;
            Usun.Width = 60;
            Usun.ReadOnly = true;
            if (!nowy)Usun.Visible = false;
            karty_tabela.Columns.Add(Usun);

            DataGridViewColumn Lp = new DataGridViewColumn();
            Lp.Name = "Lp";
            Lp.HeaderText = "Lp.:";
            Lp.CellTemplate = cell;
            Lp.Width = 50;
            Lp.HeaderCell.Style.Font = TenRegular;
            Lp.ReadOnly = true;
            Lp.CellTemplate.Style.Alignment = DataGridViewContentAlignment.TopLeft;
            karty_tabela.Columns.Add(Lp);

            DataGridViewColumn Naglowek = new DataGridViewColumn();
            Naglowek.Name = "Naglowek";
            Naglowek.HeaderText = "Nagłówek";
            Naglowek.CellTemplate = cellNaglowek;
            Naglowek.Width = 350;
            Naglowek.HeaderCell.Style.Font = TenBold;
            karty_tabela.Columns.Add(Naglowek);
        }

        private void FormKarty_Load(object sender, EventArgs e)
        {

        }

        private void nazwa_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(nazwa_combo.SelectedIndex!=-1) Wyswietl(nazwa_combo.SelectedIndex);
        }

        private void karty_edycja_btn_Click(object sender, EventArgs e)
        {
            karty_edycja_box.Visible = true;
            karty_box.Enabled = false;
            karty_edycja_nazwa_textBox.Enabled = false;
            karty_edycja_nazwa_textBox.Text = FullList_Karty[nazwa_combo.SelectedIndex].nazwa;
            karty_tabela.ReadOnly = false;
        }

        private void karty_anuluj_btn_Click(object sender, EventArgs e)
        {
            karty_edycja_box.Visible = false;
            karty_box.Enabled = true;
            karty_edycja_nazwa_textBox.Text = "";
            karty_tabela.ReadOnly = true;
            if (nowy) nazwa_combo.SelectedIndex = 0;
            nowy = false;

            karty_dodaj_polecenie_btn.Visible = false;
            skopiuj_combo.Visible = false;
            label3.Visible = false;
            skopiuj_combo.Items.Clear();

            Wyswietl(nazwa_combo.SelectedIndex);
        }

        private void karty_tabela_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Sprawdz();
        }

        private void karty_dodaj_czynnosc_btn_Click(object sender, EventArgs e)
        {
            System.Drawing.Font TenRegular = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            cell.Style.WrapMode = DataGridViewTriState.True;
            cell.Style.Font = TenRegular;
            DataGridViewColumn column = new DataGridViewColumn();
            column.Name = "Czynnosc_" + (karty_tabela.ColumnCount - 2).ToString();
            column.HeaderText = "Czynność " + (karty_tabela.ColumnCount - 2).ToString();
            column.CellTemplate = cell;
            column.Width = 250;
            column.HeaderCell.Style.Font = TenRegular;
            karty_tabela.Columns.Add(column);
        }

        private void karty_edycja_nazwa_textBox_TextChanged(object sender, EventArgs e)
        {
            Sprawdz();
        }

        private void Sprawdz()
        {
            bool test = true;
            if (karty_edycja_nazwa_textBox.Text == "") test = false;
            if (karty_tabela.RowCount==0) test = false;
            for (int i=0;i<karty_tabela.RowCount;i++)
            {
                if (!(karty_tabela.Rows[i].Cells[2].Value != null)) test = false;
            }
            if (test) karty_zapisz_btn.Enabled = true;
            else karty_zapisz_btn.Enabled = false;
        }

        private void karty_zapisz_btn_Click(object sender, EventArgs e)
        {
            List<string> tresc = new List<string>();
            bool test = true;

            if (karty_edycja_nazwa_textBox.Text == "") test = false;
            else if (karty_edycja_nazwa_textBox.Text == nazwa_combo.SelectedText) tresc.Add(karty_edycja_nazwa_textBox.Text);
            else
            {
                foreach(GF_postoje.Karta karta in FullList_Karty)
                {
                    int edyto = FullList_Karty.IndexOf(karta);
                    if (karty_edycja_nazwa_textBox.Text == karta.nazwa && edyto != nazwa_combo.SelectedIndex) test = false;
                }
                if (test)
                {
                    string wartosc = karty_edycja_nazwa_textBox.Text;
                    string wartosc2 = wartosc.Replace('>', '-');
                    string wartosc3 = wartosc2.Replace(';', ':');
                    string wartosc4 = wartosc3.Replace('"', '\'');
                    tresc.Add(wartosc4);
                }
            }

            for (int r =0;r<karty_tabela.RowCount;r++)
            {
                for(int c=2;c<karty_tabela.ColumnCount;c++)
                {
                    if (karty_tabela.Rows[r].Cells[c].Value != null)
                    {
                        string wartosc = karty_tabela.Rows[r].Cells[c].Value.ToString();
                        string wartosc2 = wartosc.Replace('>', '-');
                        string wartosc3 = wartosc2.Replace(';', ':');
                        string wartosc4 = wartosc3.Replace('"', '\'');
                        if (c == 2) wartosc4 = ">" + wartosc4;
                        tresc.Add(wartosc4);
                    }
                    else if (c == 2) test = false;
                }
            }

            if (test)
            {
                if (nowy)
                {
                    GF_postoje.Karta nowa = new GF_postoje.Karta(tresc.ToArray());
                    FullList_Karty.Insert(0, nowa);

                    karty_edycja_box.Visible = false;
                    karty_box.Enabled = true;
                    karty_edycja_nazwa_textBox.Text = "";
                    karty_tabela.ReadOnly = true;
                    nazwa_combo.Items.Clear();
                    foreach (GF_postoje.Karta karta in FullList_Karty) nazwa_combo.Items.Add(karta.nazwa);
                    nazwa_combo.SelectedIndex = 0;
                    nowy = false;

                    karty_dodaj_polecenie_btn.Visible = false;
                    skopiuj_combo.Visible = false;
                    label3.Visible = false;
                    skopiuj_combo.Items.Clear();

                    Wyswietl(0);

                }
                else
                {
                    FullList_Karty[nazwa_combo.SelectedIndex].Podmien(tresc.ToArray());
                    karty_edycja_box.Visible = false;
                    karty_box.Enabled = true;
                    karty_edycja_nazwa_textBox.Text = "";
                    karty_tabela.ReadOnly = true;
                    int indeks = nazwa_combo.SelectedIndex;
                    nazwa_combo.Items.Clear();
                    foreach (GF_postoje.Karta karta in FullList_Karty) nazwa_combo.Items.Add(karta.nazwa);
                    nazwa_combo.SelectedIndex = indeks;
                    Wyswietl(nazwa_combo.SelectedIndex);
                }
                string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
                int LogDanych = Convert.ToInt32(log[0]);
                using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaKart.csv"))
                {
                    foreach (GF_postoje.Karta karta in FullList_Karty)
                    {
                        ReadWriteCsv.CsvRow Row = new ReadWriteCsv.CsvRow();
                        foreach (string str in karta.row) Row.Add(str);
                        writer.WriteRow(Row);
                    }
                }
                LogDanych = LogDanych + 1;
                System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());



            }

        }

        private void karty_nowa_btn_Click(object sender, EventArgs e)
        {
            nowy = true;
            karty_dodaj_polecenie_btn.Visible = true;
            skopiuj_combo.Visible = true;
            karty_edycja_nazwa_textBox.Enabled = true;
            label3.Visible = true;
            nazwa_combo.SelectedIndex = -1;
            karty_edycja_box.Visible = true;
            karty_box.Enabled = false;
            karty_edycja_nazwa_textBox.Text = "";
            karty_tabela.ReadOnly = false;
            skopiuj_combo.Items.Clear();
            skopiuj_combo.Items.Add("[pusta karta]");
            foreach (GF_postoje.Karta karta in FullList_Karty) skopiuj_combo.Items.Add(karta.nazwa);
            skopiuj_combo.SelectedIndex = 0;
           
            Kolumny();
            karty_tabela.Rows.Add();
            karty_tabela.Rows[0].Cells[1].Value = "1";
        }

        private void karty_dodaj_polecenie_btn_Click(object sender, EventArgs e)
        {
            object wiersz = "[Unuń]";
            if (selRow<karty_tabela.RowCount )karty_tabela.Rows.Insert(selRow+1,wiersz);
            else karty_tabela.Rows.Insert(karty_tabela.RowCount, wiersz);
            for (int i = 0; i < karty_tabela.RowCount; i++) karty_tabela.Rows[i].Cells[1].Value = (i + 1).ToString();
            if (karty_tabela.RowCount == 1) karty_tabela.Rows[0].Cells[0].Value = "";
            else for (int i = 0; i < karty_tabela.RowCount; i++) karty_tabela.Rows[i].Cells[0].Value = "[Usuń]";

        }

        private void karty_tabela_Click(object sender, EventArgs e)
        {
            selRow = karty_tabela.SelectedCells[0].RowIndex;
            int selKol = karty_tabela.SelectedCells[0].ColumnIndex;
            if (nowy && selKol == 0 && karty_tabela.RowCount > 1)
            {
                karty_tabela.Rows.RemoveAt(selRow);
                for (int i = 0; i < karty_tabela.RowCount; i++) karty_tabela.Rows[i].Cells[1].Value = (i + 1).ToString();
            }
            if (nowy && karty_tabela.RowCount == 1) karty_tabela.Rows[0].Cells[0].Value = "";
        }

        private void skopiuj_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skopiuj_combo.SelectedIndex >= 1)
            {
                Wyswietl(skopiuj_combo.SelectedIndex - 1);
                karty_edycja_nazwa_textBox.Text = FullList_Karty[skopiuj_combo.SelectedIndex - 1].nazwa + "[kopia]";
            }
            else
            {
                Kolumny();
                karty_tabela.Rows.Add();
                karty_tabela.Rows[0].Cells[1].Value = "1";
            }
        }

        private void karty_import_btn_Click(object sender, EventArgs e)
        {
            openFileDialog_imp.ShowDialog();
        }    

        private void openFileDialog_imp_FileOk(object sender, CancelEventArgs e)
        {
            string [] myStream = openFileDialog_imp.FileNames;
            foreach ( string _path in myStream)
            {
                var plik = new FileInfo(_path);
                string[] nazw = plik.Name.Split('.');
                if (plik.Exists && plik.Extension == ".docx" && !nazwa_combo.Items.Contains(nazw[0]))
                {
                    var doc2 = DocX.Load(_path);
                    List<Table> tab = doc2.Tables;
                    List<string> nowa = new List<string>();                   
                    nowa.Add(nazw[0]);
                    foreach (Table t in tab)
                    {
                        int ddd = t.RowCount;
                        for (int i = 0; i < ddd; i++)
                        {
                            string sss = t.Rows[i].Cells[1].Paragraphs[0].Text;
                            if (!sss.Contains("Mycie i dezynfekcja"))
                            {
                                for (int j = 0; j < t.Rows[i].Cells[1].Paragraphs.Count; j++)
                                {
                                    string akapit = t.Rows[i].Cells[1].Paragraphs[j].Text;
                                    if (akapit != "Opis czynności kontrolnej")
                                    {
                                        if (j == 0) nowa.Add(">" + akapit);
                                        else if (akapit != "") nowa.Add(akapit);
                                    }
                                }
                            }
                        }
                    }
                    GF_postoje.Karta odczytana = new GF_postoje.Karta(nowa.ToArray());
                    FullList_Karty.Add(odczytana);
                }
            }
            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaKart_Eksport.csv"))
            {
                foreach (GF_postoje.Karta odcz in FullList_Karty)
                {
                    ReadWriteCsv.CsvRow Row = new ReadWriteCsv.CsvRow();
                    foreach (string str in odcz.row) Row.Add(str);
                    writer.WriteRow(Row);
                }
            }
            string[] log = System.IO.File.ReadAllLines(SciezkaDoDanych + @"\Log.txt");
            int LogDanych = Convert.ToInt32(log[0]);
            LogDanych = LogDanych + 1;
            System.IO.File.WriteAllText(SciezkaDoDanych + @"\Log.txt", LogDanych.ToString());

            nazwa_combo.Items.Clear();
            foreach (GF_postoje.Karta karta in FullList_Karty) nazwa_combo.Items.Add(karta.nazwa);
            nazwa_combo.SelectedIndex = nazwa_combo.Items.Count-1;

        }
    }
}
