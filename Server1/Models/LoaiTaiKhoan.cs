using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class LoaiTaiKhoan
    {
        public LoaiTaiKhoan()
        {
            Taikhoan = new HashSet<Taikhoan>();
        }

        public string Id { get; set; }
        public string Tenloai { get; set; }

        public ICollection<Taikhoan> Taikhoan { get; set; }
    }
}
