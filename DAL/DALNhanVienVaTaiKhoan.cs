using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNhanVienVaTaiKhoan
    {
        VanPhongDbContext dt;
        public DALNhanVienVaTaiKhoan()
        {
            dt = new VanPhongDbContext();
        }

        public eTaiKhoan KiemTraTaiKhoan(string taiKhoan, string matKhau)
        {
            var tk = (from n in dt.tblTaiKhoan
                      where n.TenTK == taiKhoan && n.MatKhau == matKhau
                      select n).FirstOrDefault();
            return tk;
        }
        public bool DangNhap(string taiKhoan, string matKhau)
        {
            var tk = KiemTraTaiKhoan(taiKhoan, matKhau);
            if (tk == null)
                return false;
            return true;
        }
        public eNhanVien LayNhanVienDangNhap(string taiKhoan, string matKhau)
        {
            var tk = KiemTraTaiKhoan(taiKhoan, matKhau);
            if (tk == null)
                return null;
            return tk.ENhanVien;
        }
    }
}
