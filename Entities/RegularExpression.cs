using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entities
{
    public static class RegularExpression
    {
        public static Boolean KtraEmail(this string s)
        {
            return Regex.Match(s, @"^([A-Za-z][A-Za-z0-9]+[@][g][m][a][i][l][.][c][o][m])$").Success;
        }

        public static Boolean KtraTenTK(this string s)
        {
            return Regex.Match(s, @"^([A-Za-z][A-Za-z0-9]{4,})$").Success;
        }
        public static Boolean KtraSDT(this string s)
        {
            return Regex.Match(s, @"^[0][13579]\d{8}$").Success;
        }

        public static Boolean KtraSCMND(this string s)
        {
            return Regex.Match(s, @"^(([1-9]\d{8})|([1-9]\d{11}))$").Success;
        }
        public static Boolean KTraMaPhong(this string s)
        {
            return Regex.Match(s, @"^[p]\d{3,4}$").Success;
        }
        public static Boolean KTraTangLau(this string s)
        {
            return Regex.Match(s, @"^\d{1,2}$").Success;
        }
        public static Boolean KTraDienTich(this string s)
        {
            return Regex.Match(s, @"^\d{3}$").Success;
        }
        public static Boolean KTraSoBongDen(this string s)
        {
            return Regex.Match(s, @"^\d{2}$").Success;
        }
        public static Boolean KTraSoMayLanh(this string s)
        {
            return Regex.Match(s, @"^[3-9]$").Success;
        }

        public static Boolean KTraTien(this string s)
        {
            return Regex.Match(s, @"^[1-9]\d{0,7}$").Success;
        }
    }
}
