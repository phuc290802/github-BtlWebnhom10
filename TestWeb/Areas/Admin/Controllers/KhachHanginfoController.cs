using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWeb.Models;

namespace TestWeb.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [Route("admin/Homeadmin/DanhSachKhachHang/api/[controller]")]
    [ApiController]
    public class KhachHanginfoController : ControllerBase
    {
        private readonly QuanLiBanSachContext _context;

        public KhachHanginfoController(QuanLiBanSachContext context)
        {
            _context = context;
        }

        // GET: api/KhachHanginfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHangs()
        {
          if (_context.KhachHangs == null)
          {
              return NotFound();
          }
            return await _context.KhachHangs.ToListAsync();
        }

        // GET: api/KhachHanginfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(int id)
        {
          if (_context.KhachHangs == null)
          {
              return NotFound();
          }
            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // PUT: api/KhachHanginfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (id != khachHang.MaKh)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KhachHanginfo
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
          if (_context.KhachHangs == null)
          {
              return Problem("Entity set 'QuanLiBanSachContext.KhachHangs'  is null.");
          }
            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKhachHang", new { id = khachHang.MaKh }, khachHang);
        }

        // DELETE: api/KhachHanginfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(int id)
        {
            return (_context.KhachHangs?.Any(e => e.MaKh == id)).GetValueOrDefault();
        }
    }
}
