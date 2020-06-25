using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eKhachHang
    {
        public eKhachHang()
        {
            dsHopDong = new HashSet<eHopDong>();
        }
        [Key]
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMND { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public Boolean GioiTinh { get; set; }
        public Boolean Active { get; set; }

        public ICollection<eHopDong> dsHopDong { get; set; }
    }
}
