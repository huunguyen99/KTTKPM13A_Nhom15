using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BUS
{
    public class BUSVanPhong
    {
        DALVanPhong dt;
        public BUSVanPhong()
        {
            dt = new DALVanPhong();
        }
        

        public List<eVanPhong> LayDanhSachPhong()
        {
            return dt.LayDanhSachPhong();
        }
        public List<eVanPhong> LayDSVanPhongDangChoThue()
        {
            return dt.LayDSVanPhongDangChoThue();
        }
        public List<eVanPhong> LayDSVanPhongTrong()
        {
            return dt.LayDSVanPhongTrong();
        }
        public bool TraPhong(string maPhong)
        {
            return dt.TraPhong(maPhong);
        }
    }
}
