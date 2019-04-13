using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class ViTri
    {
        public string MaLoai { get; set; }
        public string IdPb { get; set; }
        public string IdNv { get; set; }

        public NhanVien IdNvNavigation { get; set; }
        public PhongBan IdPbNavigation { get; set; }
        public LoaiNhanVien MaLoaiNavigation { get; set; }
    }
}
