using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [DataContract]
    public class eNhanVien
    {
        public eNhanVien()
        {
            dsphieuyeucau = new HashSet<ePhieuYeuCauKiemTraPhong>();
            dsHopDong = new HashSet<eHopDong>();
            dsHoaDon = new HashSet<eHoaDon>();
        }
        [Key]
        [DataMember]
        public int MaNV { get; set; }
        [DataMember]
        public string TenNV { get; set; }
        [DataMember]
        public DateTime NgaySinh { get; set; }
        [DataMember]
        public string SoCMND { get; set; }
        [DataMember]
        public string SDT { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string DiaChi { get; set; }
        [DataMember]
        public string QueQuan { get; set; }
        [DataMember]
        public Boolean GioiTinh { get; set; }
        [DataMember]
        public int ChucVu { get; set; }
        [DataMember]
        public Boolean Active { get; set; }

        public ICollection<ePhieuYeuCauKiemTraPhong> dsphieuyeucau { get; set; }
        public ICollection<eHopDong> dsHopDong { get; set; }
        public ICollection<eHoaDon> dsHoaDon { get; set; }
    }
}
