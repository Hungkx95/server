using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class CaLamViec
    {
        public CaLamViec()
        {
            GioLamViec = new HashSet<GioLamViec>();
            NhanVien = new HashSet<NhanVien>();
        }

        public int MaCa { get; set; }
        public string Ten { get; set; }
        public TimeSpan? GiobatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }

        public ICollection<GioLamViec> GioLamViec { get; set; }
        public ICollection<NhanVien> NhanVien { get; set; }
    }
}
