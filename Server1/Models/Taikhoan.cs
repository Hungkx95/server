using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class Taikhoan
    {
        public string Username { get; set; }
        public string Passwork { get; set; }
        public string IdNv { get; set; }
        public string LoaiTk { get; set; }

        public NhanVien IdNvNavigation { get; set; }
        public LoaiTaiKhoan LoaiTkNavigation { get; set; }
    }
}
