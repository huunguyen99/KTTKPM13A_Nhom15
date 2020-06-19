namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.eChiTietHoaDons",
                c => new
                {
                    MaCTHD = c.Int(nullable: false, identity: true),
                    MaHoaDon = c.Int(nullable: false),
                    TienDien = c.Decimal(nullable: true, precision: 18, scale: 2),
                    TienNuoc = c.Decimal(nullable: true, precision: 18, scale: 2),
                    TienGuiXe = c.Decimal(nullable: true, precision: 18, scale: 2),
                    PhiBaoTri = c.Decimal(nullable: true, precision: 18, scale: 2),
                    PhiVeSinh = c.Decimal(nullable: true, precision: 18, scale: 2),
                    PhiThangMay = c.Decimal(nullable: true, precision: 18, scale: 2),
                    PhiBaoVe = c.Decimal(nullable: true, precision: 18, scale: 2),
                    TienPhong = c.Decimal(nullable: true, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.MaCTHD)
                .ForeignKey("dbo.eHoaDons", t => t.MaHoaDon, cascadeDelete: false)
                .Index(t => t.MaHoaDon);

            CreateTable(
                "dbo.eHoaDons",
                c => new
                {
                    MaHoaDon = c.Int(nullable: false, identity: true),
                    MaNV = c.Int(nullable: true),
                    MaHopDong = c.Int(nullable: true),
                    NgayLapHoaDon = c.DateTime(nullable: true),
                    NgayCanLap = c.DateTime(nullable: true),
                    NgayThanhToan = c.DateTime(nullable: true),
                    TinhTrangHD = c.Boolean(nullable: true),
                })
                .PrimaryKey(t => t.MaHoaDon)
                .ForeignKey("dbo.eHopDongs", t => t.MaHopDong, cascadeDelete: false)
                .ForeignKey("dbo.eNhanViens", t => t.MaNV, cascadeDelete: false)
                .Index(t => t.MaNV)
                .Index(t => t.MaHopDong);

            CreateTable(
                "dbo.eHopDongs",
                c => new
                {
                    MaHopDong = c.Int(nullable: false, identity: true),
                    MaNV = c.Int(nullable: true),
                    MaKH = c.Int(nullable: true),
                    MaPhieuKTra = c.Int(nullable: true),
                    TienCoc = c.Decimal(nullable: true, precision: 18, scale: 2),
                    NgayTao = c.DateTime(nullable: true),
                    NgayThue = c.DateTime(nullable: true),
                    NgayTra = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.MaHopDong)
                .ForeignKey("dbo.eKhachHangs", t => t.MaKH, cascadeDelete: false)
                .ForeignKey("dbo.eNhanViens", t => t.MaNV, cascadeDelete: false)
                .ForeignKey("dbo.ePhieuYeuCauKiemTraPhongs", t => t.MaPhieuKTra, cascadeDelete: false)
                .Index(t => t.MaNV)
                .Index(t => t.MaKH)
                .Index(t => t.MaPhieuKTra);

            CreateTable(
                "dbo.eKhachHangs",
                c => new
                {
                    MaKH = c.Int(nullable: false, identity: true),
                    TenKH = c.String(),
                    NgaySinh = c.DateTime(nullable: true),
                    SoCMND = c.String(),
                    SDT = c.String(),
                    Email = c.String(),
                    DiaChi = c.String(),
                    GioiTinh = c.Boolean(nullable: true),
                    Active = c.Boolean(nullable: true),
                })
                .PrimaryKey(t => t.MaKH);

            CreateTable(
                "dbo.eNhanViens",
                c => new
                {
                    MaNV = c.Int(nullable: false, identity: true),
                    TenNV = c.String(),
                    NgaySinh = c.DateTime(nullable: true),
                    SoCMND = c.String(),
                    SDT = c.String(),
                    Email = c.String(),
                    DiaChi = c.String(),
                    QueQuan = c.String(),
                    GioiTinh = c.Boolean(nullable: true),
                    ChucVu = c.Int(nullable: true),
                    Active = c.Boolean(nullable: true),
                })
                .PrimaryKey(t => t.MaNV);

            CreateTable(
                "dbo.ePhieuYeuCauKiemTraPhongs",
                c => new
                {
                    MaPhieuKTra = c.Int(nullable: false, identity: true),
                    MaPhong = c.String(maxLength: 128),
                    MaNVTuVan = c.Int(nullable: true),
                    MaNVKyThuat = c.Int(nullable: true),
                    NgayTao = c.DateTime(nullable: true),
                    TinhTrangPhong = c.Boolean(nullable: true),
                    TrangThaiPhieu = c.Boolean(nullable: true),
                    GhiChu = c.String(),
                    ENhanVien_MaNV = c.Int(),
                })
                .PrimaryKey(t => t.MaPhieuKTra)
                .ForeignKey("dbo.eNhanViens", t => t.ENhanVien_MaNV)
                .ForeignKey("dbo.eVanPhongs", t => t.MaPhong)
                .Index(t => t.MaPhong)
                .Index(t => t.ENhanVien_MaNV);

            CreateTable(
                "dbo.eVanPhongs",
                c => new
                {
                    MaPhong = c.String(nullable: false, maxLength: 128),
                    TenPhong = c.String(),
                    GiaThue = c.Decimal(nullable: true, precision: 18, scale: 2),
                    TangLau = c.Int(nullable: true),
                    DienTich = c.Double(nullable: true),
                    SoBongDen = c.Int(nullable: true),
                    SoMayLanh = c.Int(nullable: true),
                    TinhTrang = c.Boolean(nullable: true),
                })
                .PrimaryKey(t => t.MaPhong);

            CreateTable(
                "dbo.eTaiKhoans",
                c => new
                {
                    TenTK = c.String(nullable: false, maxLength: 128),
                    MatKhau = c.String(),
                    MaNV = c.Int(nullable: true),
                })
                .PrimaryKey(t => t.TenTK)
                .ForeignKey("dbo.eNhanViens", t => t.MaNV, cascadeDelete: true)
                .Index(t => t.MaNV);

        }

        public override void Down()
        {
            DropForeignKey("dbo.eTaiKhoans", "MaNV", "dbo.eNhanViens");
            DropForeignKey("dbo.eHopDongs", "MaPhieuKTra", "dbo.ePhieuYeuCauKiemTraPhongs");
            DropForeignKey("dbo.ePhieuYeuCauKiemTraPhongs", "MaPhong", "dbo.eVanPhongs");
            DropForeignKey("dbo.ePhieuYeuCauKiemTraPhongs", "MaNV", "dbo.eNhanViens");
            DropForeignKey("dbo.eHopDongs", "MaNV", "dbo.eNhanViens");
            DropForeignKey("dbo.eHoaDons", "MaNV", "dbo.eNhanViens");
            DropForeignKey("dbo.eHopDongs", "MaKH", "dbo.eKhachHangs");
            DropForeignKey("dbo.eHoaDons", "MaHopDong", "dbo.eHopDongs");
            DropForeignKey("dbo.eChiTietHoaDons", "MaHoaDon", "dbo.eHoaDons");
            DropIndex("dbo.eTaiKhoans", new[] { "MaNV" });
            DropIndex("dbo.ePhieuYeuCauKiemTraPhongs", new[] { "MaNV" });
            DropIndex("dbo.ePhieuYeuCauKiemTraPhongs", new[] { "MaPhong" });
            DropIndex("dbo.eHopDongs", new[] { "MaPhieuKTra" });
            DropIndex("dbo.eHopDongs", new[] { "MaKH" });
            DropIndex("dbo.eHopDongs", new[] { "MaNV" });
            DropIndex("dbo.eHoaDons", new[] { "MaHopDong" });
            DropIndex("dbo.eHoaDons", new[] { "MaNV" });
            DropIndex("dbo.eChiTietHoaDons", new[] { "MaHoaDon" });
            DropTable("dbo.eTaiKhoans");
            DropTable("dbo.eVanPhongs");
            DropTable("dbo.ePhieuYeuCauKiemTraPhongs");
            DropTable("dbo.eNhanViens");
            DropTable("dbo.eKhachHangs");
            DropTable("dbo.eHopDongs");
            DropTable("dbo.eHoaDons");
            DropTable("dbo.eChiTietHoaDons");
        }
    }
}
