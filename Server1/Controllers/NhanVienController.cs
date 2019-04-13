using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server1.Models;

namespace Server1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly QLNVContext _context;
        public NhanVienController(QLNVContext context)
        {
            _context = context;
        }


        // lấy thông tin toàn bộ nhân viên
        [HttpGet]
        public IEnumerable<NhanVien> getAllNhanVien()
        {
            return _context.NhanVien;
        }

        // lấy thông tin nhân viên theo id
        [HttpGet("{id}")]
        public async Task<IActionResult> getNhanVienById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nhanvien = await _context.NhanVien.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return Ok(nhanvien);
        }

        // lấy thông tin nhân viên theo id phong ba
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNhanVienByIdPB([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nv = _context.NhanVien.ToList();
            var pb = _context.PhongBan.Where(p=>p.Idpb==id).ToList();
            var vt = _context.ViTri.ToList();
            var cv = _context.LoaiNhanVien.ToList();

            var result = (from x in pb
                          join y in vt on x.Idpb equals y.IdPb
                          join z in nv on y.IdNv equals z.Idnv
                          join w in cv on y.MaLoai equals w.MaLoai
                          select new NhanVienOfPhongBan()
                          {
                              Idnv = z.Idnv,
                              Name = z.Name,
                              Brithday = z.Brithday,
                              Address = z.Address,
                              Phone = z.Phone,
                              CaLam = z.CaLam,
                              Email=z.Email,
                              TenChucVu=w.TenLoai,
                              Idpb=x.Idpb,
                              Maloai=w.MaLoai,
                              TenPb=x.Name
                             
                              
                          }

                ).ToList();
            return Ok(result);
        }

        // 
        [HttpGet("{id}")]
        public async Task<IActionResult> getChucVuById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vt = _context.ViTri.Where(p => p.IdNv == id).ToList();
            var cv = _context.LoaiNhanVien.ToList();
            var pb = _context.PhongBan.ToList();
            var result = (from x in vt join y in cv on x.MaLoai equals y.MaLoai
                          join z in pb on x.IdPb equals z.Idpb
                          select new ChucVu() {
                              IdPb=x.IdPb,
                              TenPB=z.Name,
                              TenCV=y.TenLoai
            }
                          ).ToList();

            return Ok(result);
        }

        // l
        //them nhanv ien
        [HttpPost]
        public async Task<ActionResult> postThemNV(NhanVien nhav)
        {
            _context.NhanVien.Add(nhav);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getNhanVienById), new { id = nhav.Idnv }, nhav);
        }
        //chinh suwa nhanv ien
        [HttpPost]
        public async Task<ActionResult> postSuaNV(NhanVien nhanv)
        {
            var nhanvien = await _context.NhanVien.FindAsync(nhanv.Idnv);
            if (nhanvien == null) return NotFound();
            NhanVien nvToUpdate = _context.NhanVien.Single(nv => nv.Idnv == nhanv.Idnv);
            nvToUpdate.Name = nhanv.Name;
            nvToUpdate.Brithday = nhanv.Brithday;
            nvToUpdate.Address = nhanv.Address;
            nvToUpdate.Phone = nhanv.Phone;
            nvToUpdate.Email = nhanv.Email;
            nvToUpdate.CaLam = nhanv.CaLam;
            nvToUpdate.BacLuong = nhanv.BacLuong;
            nvToUpdate.Cmnd = nhanv.Cmnd;
            nvToUpdate.MaThue = nhanv.MaThue;
            nvToUpdate.Thuong = nhanv.Thuong;
         
            nvToUpdate.PhuCap = nhanv.PhuCap;
            try { _context.SaveChanges(); }catch(Exception ex) { Console.WriteLine(ex.Message); }
          
            return CreatedAtAction(nameof(getNhanVienById), new { id = nvToUpdate.Idnv }, nvToUpdate);
        }





        //xóa nhân viên
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteNhanVien([FromRoute] string id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nhanvien = await _context.NhanVien.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            ViTri vitri = new ViTri();
            do
            {
                vitri = _context.ViTri.FirstOrDefault(vt => vt.IdNv == id);
                if (vitri != null)
                {
                    _context.ViTri.Remove(vitri);
                    await _context.SaveChangesAsync();
                }

            } while (vitri != null);
            _context.NhanVien.Remove(nhanvien);
            await _context.SaveChangesAsync();

            return Ok("deleted "+id);
        }
    }
}