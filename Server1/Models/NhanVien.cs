using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            GioLamViec = new HashSet<GioLamViec>();
            Nghi = new HashSet<Nghi>();
            Taikhoan = new HashSet<Taikhoan>();
            ViTri = new HashSet<ViTri>();
        }

        public string Idnv { get; set; }
        public string Name { get; set; }
        public DateTime? Brithday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? CaLam { get; set; }
        public string Cmnd { get; set; }
        public double? BacLuong { get; set; }
        public string MaThue { get; set; }
        public double? Thuong { get; set; }
        public double? PhuCap { get; set; }

        public CaLamViec CaLamNavigation { get; set; }
        public ICollection<GioLamViec> GioLamViec { get; set; }
        public ICollection<Nghi> Nghi { get; set; }
        public ICollection<Taikhoan> Taikhoan { get; set; }
        public ICollection<ViTri> ViTri { get; set; }
    }
}
