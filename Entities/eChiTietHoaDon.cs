using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eChiTietHoaDon
    {
        [Key]
        public int MaCTHD { get; set; }
        public int MaHoaDon { get; set; }
        public decimal TienDien { get; set; }
        public decimal TienNuoc { get; set; }
        public decimal TienGuiXe { get; set; }
        public decimal PhiBaoTri { get; set; }
        public decimal PhiVeSinh { get; set; }
        public decimal PhiThangMay { get; set; }
        public decimal PhiBaoVe { get; set; }
        public decimal TienPhong { get; set; }
        public virtual eHoaDon EHoaDon { get; set; }
    }
}
