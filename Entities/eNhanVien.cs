using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eNhanVien
    {
        public eNhanVien()
        {
            dsphieuyeucau = new HashSet<ePhieuYeuCauKiemTraPhong>();
            dsHopDong = new HashSet<eHopDong>();
            dsHoaDon = new HashSet<eHoaDon>();
        }
        [Key]
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMND { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string QueQuan { get; set; }
        public Boolean GioiTinh { get; set; }
        public int ChucVu { get; set; }
        public Boolean Active { get; set; }

        public ICollection<ePhieuYeuCauKiemTraPhong> dsphieuyeucau { get; set; }
        public ICollection<eHopDong> dsHopDong { get; set; }
        public ICollection<eHoaDon> dsHoaDon { get; set; }
    }
}
