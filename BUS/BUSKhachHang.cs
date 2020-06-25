using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BUS
{
    public class BUSKhachHang
    {
        DALKhachHang dt;
        public BUSKhachHang()
        {
            dt = new DALKhachHang();
        }
        public List<eKhachHang> LayDSKhachHangDangThue(string maPhong)
        {
            return dt.LayDSKhachHangDangThue(maPhong);
        }

        public List<eKhachHang> LayDSKhachHangKhongConThue()
        {
            return dt.LayDSKhachHangKhongConThue();
        }
        public List<eKhachHang> LayDSTatCaKhachHang()
        {
            return dt.LayDSTatCaKhachHang();
        }
        public bool ThemKH(eKhachHang kh)
        {
            return dt.ThemKH(kh);
        }

        public bool XoaKH(eKhachHang kh)
        {
            return dt.XoaKH(kh);
        }
        public bool SuaKH(eKhachHang khCanSua, eKhachHang khSua)
        {
            return dt.SuaKH(khCanSua, khSua);
        }
    }
}
