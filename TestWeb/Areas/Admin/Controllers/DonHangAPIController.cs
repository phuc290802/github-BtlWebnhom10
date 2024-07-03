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
    [ApiController]
    public class DonHangAPIController : ControllerBase
    {
        private readonly QuanLiBanSachContext _context;

        public DonHangAPIController(QuanLiBanSachContext context)
        {
            _context = context;
        }

        // GET: api/DonHangAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangs()
        {
          if (_context.DonHangs == null)
          {
              return NotFound();
          }
            return await _context.DonHangs.ToListAsync();
        }

        // GET: api/DonHangAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHang(int id)
        {
          if (_context.DonHangs == null)
          {
              return NotFound();
          }
            var donHang = await _context.DonHangs.FindAsync(id);

            if (donHang == null)
            {
                return NotFound();
            }

            return donHang;
        }

        // PUT: api/DonHangAPI/5
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonHang(int id, DonHang donHang)
        {
            if (id != donHang.MaDonHang)
            {
                return BadRequest();
            }

            _context.Entry(donHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(id))
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

        // POST: api/DonHangAPI
        
        [HttpPost]
        public async Task<ActionResult<DonHang>> PostDonHang(DonHang donHang)
        {
          if (_context.DonHangs == null)
          {
              return Problem("Entity set 'QuanLiBanSachContext.DonHangs'  is null.");
          }
            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonHang", new { id = donHang.MaDonHang }, donHang);
        }

        // DELETE: api/DonHangAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonHang(int id)
        {
            if (_context.DonHangs == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            _context.DonHangs.Remove(donHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet]
        [Route("GetNhanVienMaxDonHang")]
        public async Task<ActionResult<NhanVien>> GetNhanVienMaxDonHang()
        {
            var nhanVienMaxDonHang = await _context.DonHangs
                .GroupBy(d => d.MaNhanVien)
                .Select(g => new { MaNhanVien = g.Key, SoDonHang = g.Count() })
                .OrderByDescending(x => x.SoDonHang)
                .FirstOrDefaultAsync();

            if (nhanVienMaxDonHang == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(nhanVienMaxDonHang.MaNhanVien);

            if (nhanVien == null)
            {
                return NotFound();
            }

            return nhanVien;
        }


        private bool DonHangExists(int id)
        {
            return (_context.DonHangs?.Any(e => e.MaDonHang == id)).GetValueOrDefault();
        }
    }
}
