using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class VanPhongDbContext:DbContext
    {
        public VanPhongDbContext() : base("VanPhongDbContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<eNhanVien> tblNhanVien { get; set; }
        public DbSet<eChiTietHoaDon> tblCT_HoaDon { get; set; }
        public DbSet<eHoaDon> tblHoaDon { get; set; }
        public DbSet<eHopDong> tblHopDong { get; set; }
        public DbSet<eKhachHang> tblKhachHang { get; set; }
        
        public DbSet<ePhieuYeuCauKiemTraPhong> tblPhieuYeuCauKiemTraPhong { get; set; }
        public DbSet<eTaiKhoan> tblTaiKhoan { get; set; }
        public DbSet<eVanPhong> tblVanPhong { get; set; }
    }
}
