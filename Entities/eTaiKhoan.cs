using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eTaiKhoan
    {
        [Key]
        public string TenTK { get; set; }
        public string MatKhau { get; set; }
        public int MaNV { get; set; }
        public virtual eNhanVien ENhanVien { get; set; }
    }
}
