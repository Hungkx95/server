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
    public class ViTriController : ControllerBase
    {
        private readonly QLNVContext _context;
        // GET: api/ViTri
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public ViTriController(QLNVContext context)
        {
            _context = context;
        }


        // GET: api/ViTri/5

        [HttpGet("{id}")]
        public async Task<IActionResult> GetViTri(string  id)
        {
            var vitri = _context.ViTri.Where(p => p.IdNv == id);
            if (vitri == null)
            {
                return NotFound();
            }
            return Ok(vitri);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetViTriBYId(string id)
        {
            var vitri = _context.ViTri.Where(p => p.IdNv == id);
            var pb = _context.PhongBan.ToList();
            var tencv = _context.LoaiNhanVien.ToList();
            var chucvu = (
                from x in vitri
                join y in pb on x.IdPb equals y.Idpb
                join z in tencv on x.MaLoai equals z.MaLoai
                select new ChucVu()
                {
                    IdPb = x.IdPb,
                    TenPB = y.Name,
                    TenCV = z.TenLoai
                }
                ).ToList();

           
            return Ok(chucvu);
        }

        // thay đổi chức vụ trong phòng ban
        // POST: api/ViTri
        [HttpPost]
        public async Task<ActionResult> PostThayDoiViTri(ViTriMoi vtMoi)
        {
           ViTri a = _context.ViTri.Where(s => s.IdNv == vtMoi.IdNv).Where(s => s.IdPb == vtMoi.IdPb).Single();
            if (a == null) return NotFound();

            a.IdPb = vtMoi.IdPb;
            a.MaLoai = vtMoi.MaLoai;
            await _context.SaveChangesAsync();
            
            return Ok("Chage success");
        }
      // them mot hcu vu cho nhan vien khac
         [HttpPost]
        public async Task<ActionResult> PostThemViTri(ViTri vtMoi)
        {
            if (vtMoi == null) return BadRequest("Nhân Viên Đã Thuộc Phòng Ban Rồi");
            await _context.ViTri.AddAsync(vtMoi);
            _context.SaveChanges();
         //   return Ok("Added");
            return CreatedAtAction(nameof(GetViTriBYId), new { id = vtMoi.IdNv },vtMoi);
        }


        //xoa vitri lam viec 
        [HttpDelete]
        public async Task<ActionResult> deleteVitri(ViTri vitri)
        {

            if (vitri == null) return BadRequest();
            _context.ViTri.Remove(vitri);
            await _context.SaveChangesAsync();
            

            return Ok("deleted" );
        }
        // lay cac ten chcu vu\\\\
        [HttpGet]
        public async Task<IActionResult> GetTenChucVu()
        {
            var result = _context.LoaiNhanVien.ToList();
            return Ok(result);
        }

        // get chuc vu by id
        [HttpGet]
        public async Task<IActionResult> getTenChucVuById(ViTri vt)
        {
            var result = _context.LoaiNhanVien.Single(p => p.MaLoai == vt.MaLoai);
          
            return Ok(result    );
        }
        // PUT: api/ViTri/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
