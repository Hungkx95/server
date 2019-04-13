using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class Nghi
    {
        public string Id { get; set; }
        public string IdNv { get; set; }
        public DateTime? NgayNghi { get; set; }
        public string Lydo { get; set; }

        public NhanVien IdNvNavigation { get; set; }
    }
}
