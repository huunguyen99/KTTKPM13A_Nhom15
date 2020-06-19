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
    public class eChiTietHoaDon
    {
        [Key]
        [DataMember]
        public int MaCTHD { get; set; }
        [DataMember]
        public int MaHoaDon { get; set; }
        public decimal TienDien { get; set; }
        [DataMember]
        public decimal TienNuoc { get; set; }
        [DataMember]
        public decimal TienGuiXe { get; set; }
        [DataMember]
        public decimal PhiBaoTri { get; set; }
        [DataMember]
        public decimal PhiVeSinh { get; set; }
        [DataMember]
        public decimal PhiThangMay { get; set; }
        [DataMember]
        public decimal PhiBaoVe { get; set; }
        [DataMember]
        public decimal TienPhong { get; set; }

        public decimal TienRac { get; set; }
        [DataMember]
        public virtual eHoaDon EHoaDon { get; set; }
    }
}
