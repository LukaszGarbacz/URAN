using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utrzymanie_Ruchu___APP_Niepruszewo
{
    public partial class FormCzesc : Form
    {
        List<GF_postoje.Czesc> FullList_Czesci;
        List<GF_postoje.Wypis> FullList_Wypisy;
        bool admin;
        GF_postoje.Czesc wybrana;
        int indeks;
        string SciezkaDoDanych;

        public FormCzesc(List<GF_postoje.Czesc> czesci, List<GF_postoje.Wypis> wypisy, int _indeks, bool _admin, string _path)
        {
            InitializeComponent();
            FullList_Czesci = czesci;
            FullList_Wypisy = wypisy;
            admin = _admin;
            indeks = _indeks;
            SciezkaDoDanych = _path;

            if (indeks!= -1 )
            {
                wybrana = FullList_Czesci[indeks];
                indeksTB.Enabled = false;
                indeksTB.Text = wybrana.ID;
                nazwaTB.Enabled = false;
                nazwaTB.Text = wybrana.Nazwa;
                lokalizacjaTB.Enabled = false;
                lokalizacjaTB.Text = wybrana.Lokalizacja;
                opisTB.Enabled = false;
                opisTB.Text = wybrana.opis;
                iloscUD.Enabled = false;
                iloscUD.Value = wybrana.Ilosc;
                monitUD.Enabled = false;
                monitUD.Value = wybrana.PowiadomInt;
                monitCB.Enabled = false;
                monitCB.Checked = wybrana.Obserwuj;
                ZapiszBTN.Enabled = false;

                List<GF_postoje.Wypis> show = new List<GF_postoje.Wypis>();
                foreach ( GF_postoje.Wypis w in FullList_Wypisy)
                {
                    if (w.CzescID == wybrana.ID) show.Add(w);
                }
                object[] wiersz2 = new object[3];
                foreach (GF_postoje.Wypis element2 in show)
                {
                    wiersz2[0] = element2.CzescID + "\n" + element2.CzescNazwa + "\n" + element2.Opis;
                    wiersz2[1] = element2.Data + "\n" + element2.Pobral.Nazwisko + "\n" + element2.Lista;
                    wiersz2[2] = element2.PobranoSzt;
                    tabelka_wypisy.Rows.Add(wiersz2);

                }

            }
            else
            {

                ZapiszBTN.Enabled = false;
                EdytujBTN.Enabled = false;
                int szukana = 0;
                string znaleziony;
                bool znajdz;
                do
                {
                    znajdz = false;
                    znaleziony = "PEN" + szukana.ToString("D4");
                    foreach (GF_postoje.Czesc c in FullList_Czesci)
                    {
                        if (c.ID == znaleziony)
                        {
                           
                            znajdz = true;
                            break;
                        }
                    }
                    szukana++;
                }
                while (znajdz);
                indeksTB.Text = znaleziony;
            }
        }

        private void EdytujBTN_Click(object sender, EventArgs e)
        {
            if(wybrana.ZN)
            {
                indeksTB.Enabled = false;
                nazwaTB.Enabled = false;
                lokalizacjaTB.Enabled = false;
                opisTB.Enabled = true;
                iloscUD.Enabled = false;
                if(admin)monitUD.Enabled = true;
                if(admin)monitCB.Enabled = true;
                EdytujBTN.Enabled = false;
                ZapiszBTN.Enabled = true;
            }
            else if (wybrana.PEN)
            {
                indeksTB.Enabled = false;
                nazwaTB.Enabled = true;
                lokalizacjaTB.Enabled = true;
                opisTB.Enabled = true;
                if(admin) iloscUD.Enabled = true;
                else iloscUD.Enabled = false;
                monitUD.Enabled = true;
                monitCB.Enabled = true;
                ZapiszBTN.Enabled = true;
                EdytujBTN.Enabled = false;
            }
        }

        private void ZapiszBTN_Click(object sender, EventArgs e)
        {
            string _id = indeksTB.Text;
            string _nazwa = wyczysc(nazwaTB.Text);
            string _lok = wyczysc(lokalizacjaTB.Text);
            string _opis = wyczysc(opisTB.Text);
            string _ile = iloscUD.Value.ToString();
            string _obs;
            if (monitCB.Checked) _obs = "1";
            else _obs = "0";
            string _monit = monitUD.Value.ToString();

            GF_postoje.Czesc nowa = new GF_postoje.Czesc(_id, _nazwa, _lok, _ile, _opis, _obs, _monit,"");

            if(indeks == -1)
            {
                FullList_Czesci.Add(nowa);
            }
            else
            {
                FullList_Czesci.RemoveAt(indeks);
                FullList_Czesci.Insert(indeks, nowa);
            }

            using (ReadWriteCsv.CsvFileWriter writer = new ReadWriteCsv.CsvFileWriter(SciezkaDoDanych + @"\ListaCzesci.csv"))
            {
                foreach (GF_postoje.Czesc C in FullList_Czesci)
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

            this.Close();
        }



        string wyczysc(string podaj)
        {
            string st1 = podaj.Replace(';', ':');
            string st2 = st1.Replace('"', '\'');
            string st3 = st2.Replace("\n", ".");
            string st4 = st3.Replace("\t", ". -");
            return st4;
        }


        void sprawdz()
        {
            if (nazwaTB.Text != "" && lokalizacjaTB.Text != "" ) ZapiszBTN.Enabled = true;
            else ZapiszBTN.Enabled = false;
        }

        private void lokalizacjaTB_Leave(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void nazwaTB_Leave(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void iloscUD_ValueChanged(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void nazwaTB_TextChanged(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void lokalizacjaTB_TextChanged(object sender, EventArgs e)
        {
            sprawdz();
        }

        private void monitCB_CheckedChanged(object sender, EventArgs e)
        {
            
            if ( monitCB.Checked)
            {
                monitUD.Enabled = true;
                label10.Enabled = true;
                label12.Enabled = true;
            }
            else
            {
                monitUD.Enabled = false;
                label10.Enabled = false;
                label12.Enabled = false;
            }

            sprawdz();

        }


    }
}
