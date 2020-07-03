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

        public List<eTaiKhoan> LayDSTaiKhoanVaNhanVienDangLamViec()
        {
            var ds = (from n in dt.tblTaiKhoan
                      where n.ENhanVien.ChucVu != 4 && n.ENhanVien.Active == true
                      select n).ToList();
            return ds;
        }
        public List<eTaiKhoan> LayDSTaiKhoanVaNhanVienDaNghiViec()
        {
            var ds = (from n in dt.tblTaiKhoan
                      where n.ENhanVien.ChucVu != 4 && n.ENhanVien.Active == false
                      select n).ToList();
            return ds;
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

        public bool DoiMatKhau(int maNV, string matKhau, string matKhauMoi)
        {
            var tk = (from n in dt.tblTaiKhoan
                      where n.ENhanVien.MaNV.Equals(maNV)
                      select n).FirstOrDefault();
            if (!tk.MatKhau.Equals(matKhau))
                return false;
            try
            {
                tk.MatKhau = matKhauMoi;
                dt.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }    
        public void ThemNhanVien(eNhanVien nv)
        {
            try
            {
                dt.tblNhanVien.Add(nv);
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public eTaiKhoan KiemTraTKTonTai(string tenTK)
        {
            var tkhoan = (from n in dt.tblTaiKhoan
                          where n.TenTK.Equals(tenTK)
                          select n).FirstOrDefault();
            return tkhoan;
        }
        public void ThemTK(eTaiKhoan tk)
        {
            try
            {
                dt.tblTaiKhoan.Add(tk);
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void XoaNV (eTaiKhoan nv)
        {
            eNhanVien nvien = (from n in dt.tblNhanVien
                         where n.MaNV == nv.MaNV
                         select n).FirstOrDefault();
            try
            {
                nvien.Active = false;
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void SuaTTNhanVien(int maNV, eNhanVien nvSua)
        {
            var nvien = (from n in dt.tblNhanVien
                         where n.MaNV == maNV
                         select n).FirstOrDefault();
            try
            {
                nvien.ChucVu = nvSua.ChucVu;
                nvien.DiaChi = nvSua.DiaChi;
                nvien.Email = nvSua.Email;
                nvien.NgaySinh = nvSua.NgaySinh;
                nvien.QueQuan = nvSua.QueQuan;
                nvien.SDT = nvSua.SDT;
                nvien.SoCMND = nvSua.SoCMND;
                nvien.TenNV = nvSua.TenNV;
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
