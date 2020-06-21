using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entities;
using DAL;
namespace VanPhongServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChoThueVanPhongService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ChoThueVanPhongService.svc or ChoThueVanPhongService.svc.cs at the Solution Explorer and start debugging.
    public class ChoThueVanPhongService : IChoThueVanPhongService
    {
        VanPhongDbContext dt;
        public ChoThueVanPhongService()
        {
            dt = new VanPhongDbContext();
        }
        Boolean KiemTraPhongDaThueChua(string maPhong)
        {
            var pkt = (from p in dt.tblVanPhong
                       join pktra in dt.tblPhieuYeuCauKiemTraPhong
                       on p.MaPhong equals pktra.MaPhong
                       select pktra).LastOrDefault();
            if (pkt == null)
                return true;
            var hd = (from h in dt.tblHopDong
                      where h.MaPhieuKTra == pkt.MaPhieuKTra
                      select h).FirstOrDefault();
            if (hd == null)
                return true;
            if (hd.NgayTraThucTe != null)
                return true;
            return false;
        }

        public List<eVanPhong> LayDanhSachPhong()
        {
            var ds = (from n in dt.tblVanPhong
                      select n).ToList();
            return ds;
        }

        public List<eVanPhong> LayDSVanPhongTrong()
        {
            throw new NotImplementedException();
        }
        //public List<eVanPhong> LayDSVanPhongTrong()
        //{
        //    var dsphongtrong = ()
        //}
    }
}
