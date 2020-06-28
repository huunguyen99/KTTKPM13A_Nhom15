using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BUS
{
    public class BUSVanPhong
    {
        DALVanPhong dt;
        public BUSVanPhong()
        {
            dt = new DALVanPhong();
        }
        

        public List<eVanPhong> LayDanhSachPhong()
        {
            return dt.LayDanhSachPhong();
        }
        public List<eVanPhong> LayDSVanPhongDangChoThue()
        {
            return dt.LayDSVanPhongDangChoThue();
        }
        public List<eVanPhong> LayDSVanPhongTrong()
        {
            return dt.LayDSVanPhongTrong();
        }
        public bool TraPhong(string maPhong)
        {
            return dt.TraPhong(maPhong);
        }
        public bool Xoa_TaiSuDung_Phong(eVanPhong p, bool tinhTrangPhong)
        {
            return dt.Xoa_TaiSuDung_Phong(p, tinhTrangPhong);
        }
        public bool ThemPhong(eVanPhong p)
        {
            return dt.ThemPhong(p);
        }
        public bool SuaPhong(eVanPhong p, eVanPhong phSua)
        {
            return dt.SuaPhong(p, phSua);
        }

        public List<eVanPhong> LayDSVanPHongDenHanThanhToan()
        {
            return dt.LayDSVanPHongDenHanThanhToan();
        }
    }
}
