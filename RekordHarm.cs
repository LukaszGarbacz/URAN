using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace GF_postoje
{
    public class RekordHarm
    {
        public Maszyna maszyna;
        public Karta KK;
        public int rok;
        public int tydzien;
        public int jak_dawno;
        public string karta;
        public bool specjalny;
        public bool wykonany;
        public string sygnatura;
        public string aktualnosc;
        public string status = "Wykonany";
        public string specjalnyex;
        public string specjalnyStr;
        public string wykonanyex;
        public RekordHarm(string _rok, string _tydzien, Maszyna _maszyna, Karta _karta, string _specjalny, string _wykonany)
        {
            maszyna = _maszyna;
            rok = Convert.ToInt32(_rok);
            tydzien = Convert.ToInt32(_tydzien);
            if (_specjalny == "0")
            {
                specjalny = false;
                specjalnyStr = "Okresowy";
            }
            else
            {
                specjalny = true;
                specjalnyStr = "Specjalny";
            }
            sygnatura = _rok + "/";
            if (tydzien < 10) sygnatura = sygnatura + "0" + tydzien + "/" + maszyna.ID;
            else sygnatura = sygnatura + tydzien + "/" + maszyna.ID;
            if (specjalny) sygnatura = sygnatura + "/S";
            karta = _karta.nazwa;
            KK = _karta;
            if (_wykonany == "0") wykonany = false;
            else wykonany = true;
            specjalnyex = _specjalny;
            wykonanyex = _wykonany;
            if (!wykonany)
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;

                DateTime terazData = DateTime.Now;
                int terazTyg = cal.GetWeekOfYear(terazData, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday); //CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                int[] teraz = { terazData.Year, terazTyg };

                int ix = rok - teraz[0];
                int iy = tydzien - teraz[1];
                jak_dawno = ix * 52 + iy;

                if (jak_dawno < 0)
                {
                    aktualnosc = Math.Abs(jak_dawno).ToString() + " tyg. temu";
                    status = "Zaległy";
                }
                else if (jak_dawno == 0)
                {
                    aktualnosc = "W tym tyg.";
                    status = "Aktualny";
                }
                else
                {
                    aktualnosc = "Za " + jak_dawno.ToString() + " tyg.";
                    status = "Nadchodzący";
                }
            }

        }
    }
}
