using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class LoaiNhanVien
    {
        public LoaiNhanVien()
        {
            ViTri = new HashSet<ViTri>();
        }

        public string MaLoai { get; set; }
        public string TenLoai { get; set; }

        public ICollection<ViTri> ViTri { get; set; }
    }
}
