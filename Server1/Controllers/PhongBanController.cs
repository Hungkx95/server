using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server1.Models;
namespace Server1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhongBanController : ControllerBase
    {
        private readonly QLNVContext _context;

        public PhongBanController(QLNVContext context)
        {
            _context = context;
        }
        // GET: api/PhongBan
        [HttpGet]
        public IEnumerable<PhongBan> GetPhongBans()
        {
            return _context.PhongBan;
        }

        // GET: api/PhongBan/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhongBanById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Phong = await _context.PhongBan.FindAsync(id);
            if (Phong == null)
            {
                return NotFound();
            }
            return Ok(Phong);
        }
        // get so nha vien của pb
        [HttpGet("{id}")]
        public string GetSoNhanVien1PB(string id)
        {
            var SoLuong = _context.ViTri.Where(p => p.IdPb == id);
            return SoLuong.Count().ToString();
        }


        // POST: api/PhongBan
        [HttpPost]
        public async Task<ActionResult> PostThemPhongBan(PhongBan pb)
        {
            if (pb == null) return BadRequest();
       
            _context.PhongBan.Add(pb);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhongBanById), new { id = pb.Idpb }, pb);
        }

        //chinh suwa nhanv ien
        [HttpPost]
        public async Task<ActionResult> postSuaTTPhongBan(PhongBan phongban)
        {
            var phong = await _context.PhongBan.FindAsync(phongban.Idpb);
            if (phong == null) return NotFound();
            PhongBan pbToUpdate = _context.PhongBan.Single(pb => pb.Idpb == phongban.Idpb);
            pbToUpdate.Name = phongban.Name;
           
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPhongBanById), new { id = phongban.Idpb }, phongban);
        }



        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> deletePhongban(string id)
        {
            var phong = await _context.PhongBan.FindAsync(id);
            if (phong == null) return NotFound();
            ViTri vitri = new ViTri();
            do
            {
                vitri = _context.ViTri.FirstOrDefault(vt => vt.IdPb == id);
                if (vitri != null)
                {
                    _context.ViTri.Remove(vitri);
                    await _context.SaveChangesAsync();
                }
             
            } while (vitri != null);


            _context.PhongBan.Remove(phong);

            await _context.SaveChangesAsync();

            return Ok("deleted room "+id);
        }


        // xáo nhan vien khỏi phong ban
        // DELETE: api/ApiWithActions/5
        [HttpPost]
        public async Task<ActionResult> deleteNhanVienOfPhongban(ViTri vt)
        {
            _context.ViTri.Remove(vt);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
