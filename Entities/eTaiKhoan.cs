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
    public class eTaiKhoan
    {
        [Key]
        [DataMember]
        public string TenTK { get; set; }
        [DataMember]
        public string MatKhau { get; set; }
        [DataMember]
        public int MaNV { get; set; }
        [DataMember]
        public virtual eNhanVien ENhanVien { get; set; }
    }
}
