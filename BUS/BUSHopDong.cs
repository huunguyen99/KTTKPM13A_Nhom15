using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BUS
{
    public class BUSHopDong
    {
        DALHopDong dt;
        public BUSHopDong()
        {
            dt = new DALHopDong();
        }
        public void TaoHopDong(eHopDong hd)
        {
            dt.TaoHopDong(hd);
        }

        public List<eHopDong> LayDSHopDongConHan(string maPhong)
        {
            return dt.LayDSHopDongConHan(maPhong);
        }
        public void SuaHopDong(eHopDong hdSua, DateTime ngaythue, DateTime ngaytra)
        {
            dt.SuaHopDong(hdSua, ngaythue, ngaytra);
        }
    }
}
