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
    public class ChiTietDonHangAPIController : ControllerBase
    {
        private readonly QuanLiBanSachContext _context;

        public ChiTietDonHangAPIController(QuanLiBanSachContext context)
        {
            _context = context;
        }

        // GET: api/ChiTietDonHangAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietDonHang>>> GetChiTietDonHangs()
        {
          if (_context.ChiTietDonHangs == null)
          {
              return NotFound();
          }
            return await _context.ChiTietDonHangs.ToListAsync();
        }

        // GET: api/ChiTietDonHangAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietDonHang>> GetChiTietDonHang(int id)
        {
          if (_context.ChiTietDonHangs == null)
          {
              return NotFound();
          }
            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);

            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return chiTietDonHang;
        }

        // PUT: api/ChiTietDonHangAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChiTietDonHang(int id, ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.MaDonHang)
            {
                return BadRequest();
            }

            _context.Entry(chiTietDonHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietDonHangExists(id))
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

        // POST: api/ChiTietDonHangAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChiTietDonHang>> PostChiTietDonHang(ChiTietDonHang chiTietDonHang)
        {
          if (_context.ChiTietDonHangs == null)
          {
              return Problem("Entity set 'QuanLiBanSachContext.ChiTietDonHangs'  is null.");
          }
            _context.ChiTietDonHangs.Add(chiTietDonHang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChiTietDonHangExists(chiTietDonHang.MaDonHang))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChiTietDonHang", new { id = chiTietDonHang.MaDonHang }, chiTietDonHang);
        }

        // DELETE: api/ChiTietDonHangAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChiTietDonHang(int id)
        {
            if (_context.ChiTietDonHangs == null)
            {
                return NotFound();
            }
            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            _context.ChiTietDonHangs.Remove(chiTietDonHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChiTietDonHangExists(int id)
        {
            return (_context.ChiTietDonHangs?.Any(e => e.MaDonHang == id)).GetValueOrDefault();
        }
    }
}
