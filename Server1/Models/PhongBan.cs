using System;
using System.Collections.Generic;

namespace Server1.Models
{
    public partial class PhongBan
    {
        public PhongBan()
        {
            ViTri = new HashSet<ViTri>();
        }

        public string Idpb { get; set; }
        public string Name { get; set; }

        public ICollection<ViTri> ViTri { get; set; }
    }
}
