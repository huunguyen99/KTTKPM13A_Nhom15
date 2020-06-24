using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eVanPhong
    {
        public eVanPhong()
        {
            dsPhieu = new HashSet<ePhieuYeuCauKiemTraPhong>();
        }
        [Key]
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public decimal GiaThue { get; set; }
        public int TangLau { get; set; }
        public double DienTich { get; set; }
        public int SoBongDen { get; set; }
        public int SoMayLanh { get; set; }
        public Boolean TinhTrang { get; set; }
        public ICollection<ePhieuYeuCauKiemTraPhong> dsPhieu { get; set; }
    }
}
