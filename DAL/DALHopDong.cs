using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALHopDong
    {
        VanPhongDbContext dt;
        public DALHopDong()
        {
            dt = new VanPhongDbContext();
        }

        public void TaoHopDong(eHopDong hd)
        {
            try
            {
                dt.tblHopDong.Add(hd);
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<eHopDong> LayDSHopDongConHan(string maPhong)
        {
            var ds = (from n in dt.tblHopDong
                      where n.TinhTrangHD == true && n.EPhieu.MaPhong == maPhong
                      select n).ToList();
            return ds;
        }

        public void SuaHopDong(eHopDong hdSua, DateTime ngaythue, DateTime ngaytra)
        {
            var hd = (from n in dt.tblHopDong
                      where n.MaHopDong.Equals(hdSua.MaHopDong)
                      select n).FirstOrDefault();
            try
            {
                hd.NgayThue = ngaythue;
                hd.NgayTra = ngaytra;
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
