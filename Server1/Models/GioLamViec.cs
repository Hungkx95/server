using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class GioLamViec
    {
        public string Idnv { get; set; }
        public int MaCa { get; set; }
        public DateTime Ngay { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }

        public NhanVien IdnvNavigation { get; set; }
        public CaLamViec MaCaNavigation { get; set; }
    }
}
