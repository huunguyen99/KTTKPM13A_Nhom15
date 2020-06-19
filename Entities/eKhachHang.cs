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
    public class eKhachHang
    {
        public eKhachHang()
        {
            dsHopDong = new HashSet<eHopDong>();
        }
        [Key]
        [DataMember]
        public int MaKH { get; set; }
        [DataMember]
        public string TenKH { get; set; }
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
        public Boolean GioiTinh { get; set; }
        [DataMember]
        public Boolean Active { get; set; }

        public ICollection<eHopDong> dsHopDong { get; set; }
    }
}
