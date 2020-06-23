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
    }
}
