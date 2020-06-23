using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALHoaDon
    {
        VanPhongDbContext dt;
        public DALHoaDon()
        {
            dt = new VanPhongDbContext();
        }
    }
}
