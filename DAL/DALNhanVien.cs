using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNhanVien
    {
        VanPhongDbContext dt;
        public DALNhanVien()
        {
            dt = new VanPhongDbContext();
        }
    }
}
