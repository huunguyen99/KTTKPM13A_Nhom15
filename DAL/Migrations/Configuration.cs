namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.VanPhongDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.VanPhongDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.tblNhanVien.Add(new eNhanVien()
            {
                MaNV = 1,
                TenNV = "Nguyễn Văn An",
                Email = "annguyen@gmail.com",
                SDT = "0165888777",
                DiaChi = "79A Trần Bá Giao Gò Vấp",
                QueQuan = "Xã Quế Xuân 1, huyện Quế Sơn, Quảng Nam",
                NgaySinh = new DateTime(22, 02, 1999),
                SoCMND = "205888956",
                GioiTinh = true,
                ChucVu = 1,
                Active = true
            });
            context.tblTaiKhoan.Add(new eTaiKhoan()
            {
                TenTK = "annguyen",
                MatKhau = "annguyen",
                MaNV = 1
            });
            context.tblVanPhong.Add(new eVanPhong()
            {
                MaPhong = "p001",
                TenPhong = "Phòng 001",
                GiaThue = 15000000,
                DienTich = 100,
                SoBongDen = 20,
                SoMayLanh = 3,
                TangLau = 0
            });

        }
    }
}
