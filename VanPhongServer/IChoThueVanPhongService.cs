using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;

namespace VanPhongServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChoThueVanPhongService" in both code and config file together.
    [ServiceContract]
    public interface IChoThueVanPhongService
    {
        [OperationContract]
        //public List<eVanPhong> LayDSVanPhongTrong();
        List<eVanPhong> LayDanhSachPhong();
        [OperationContract]
        List<eVanPhong> LayDSVanPhongTrong();
        [OperationContract]
        List<eVanPhong> LayDSVanPhongDangChoThue();
        [OperationContract]
        Boolean DangNhap(string taiKhoan, string matKhau);
        [OperationContract]
        eNhanVien LayNhanVienDangNhap(string taiKhoan, string matKhau);
        [OperationContract]
        List<eKhachHang> LayDSKhachHangDangThue(string maPhong);
        [OperationContract]
        void TaoPhieuKiemTra(ePhieuYeuCauKiemTraPhong p);
        [OperationContract]
        Boolean CapNhatPhieuKiemTra(ePhieuYeuCauKiemTraPhong p, bool tinhTrangPhong, int maNVKyThuat, string ghiChu);
        [OperationContract]
        List<ePhieuYeuCauKiemTraPhong> LayDSPhieuDaDuyet(string maPhong);
        [OperationContract]
        List<ePhieuYeuCauKiemTraPhong> LayDSPhieuChuaDuyet();
    }
}
