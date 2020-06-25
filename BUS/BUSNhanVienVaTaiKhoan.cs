using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BUS
{
    public class BUSNhanVienVaTaiKhoan
    {
        DALNhanVienVaTaiKhoan dt;
        public BUSNhanVienVaTaiKhoan()
        {
            dt = new DALNhanVienVaTaiKhoan();
        }    
        public eTaiKhoan KiemTraTaiKhoan(string taiKhoan, string matKhau)
        {
            return dt.KiemTraTaiKhoan(taiKhoan, matKhau);
        }
        public bool DangNhap(string taiKhoan, string matKhau)
        {
            return dt.DangNhap(taiKhoan, matKhau);
        }
        public eNhanVien LayNhanVienDangNhap(string taiKhoan, string matKhau)
        {
            return dt.LayNhanVienDangNhap(taiKhoan, matKhau);
        }
        public bool DoiMatKhau(int maNV, string matKhau, string matKhauMoi)
        {
            return dt.DoiMatKhau(maNV, matKhau, matKhauMoi);
        }
        public List<eTaiKhoan> LayDSTaiKhoanVaNhanVien()
        {
            return dt.LayDSTaiKhoanVaNhanVien();
        }

        public void ThemNhanVien(eNhanVien nv)
        {
            dt.ThemNhanVien(nv);
        }
        public eTaiKhoan KiemTraTKTonTai(string tenTK)
        {
            return dt.KiemTraTKTonTai(tenTK);
        }
        public void ThemTK(eTaiKhoan tk)
        {
            dt.ThemTK(tk);
        }

        public void XoaNV(eTaiKhoan nv)
        {
            dt.XoaNV(nv);
        }
        public void SuaTTNhanVien(int maNV, eNhanVien nvSua)
        {
            dt.SuaTTNhanVien(maNV, nvSua);
        }
    }
}
