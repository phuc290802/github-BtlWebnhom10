using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using TestWeb.Models;

namespace TestWeb.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachAPIController : ControllerBase
    {
        private readonly QuanLiBanSachContext _context;

        public SachAPIController(QuanLiBanSachContext context)
        {
            _context = context;
        }
        //// Danh Sách Sản Phẩm ///////
        // GET: api/SachAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sach>>> GetSaches()
        {
          if (_context.Saches == null)
          {
              return NotFound();
          }
            return await _context.Saches.ToListAsync();
        }

        //// Tìm sách theo ID ///////
        [HttpGet("{id}")]
        public async Task<ActionResult<Sach>> GetSach(int id)
        {
          if (_context.Saches == null)
          {
              return NotFound();
          }
            var sach = await _context.Saches.FindAsync(id);

            if (sach == null)
            {
                return NotFound();
            }

            return sach;
        }
        //// Tìm kiếm theo tên  ///////
        [HttpGet("Search/{tenSach}")]
        public async Task<ActionResult<List<Sach>>> Search(string tenSach)
        {
            var sachList = await _context.Saches
                .Where(s => s.TenSach.Contains(tenSach))
                .ToListAsync();

            if (sachList == null || sachList.Count == 0)
            {
                return NotFound();
            }

            return sachList;
        }
        //// Sắp xếp theo tên ///////
        [HttpGet("SapXepTheoTenSach")]
        public async Task<ActionResult<List<Sach>>> SapXepTheoTenSach()
        {
            var sachList = await _context.Saches.OrderBy(s => s.TenSach).ToListAsync();
            return sachList;
        }
        //// Tìm kiếm theo mã chủ đề ///////
        [HttpGet("SearchByMaChuDe/{maChuDe}")]
        public async Task<ActionResult<List<Sach>>> SearchByMaChuDe(int maChuDe)
        {
            var sachList = await _context.Saches
                .Where(s => s.MaChuDe == maChuDe)
                .ToListAsync();

            if (sachList == null || sachList.Count == 0)
            {
                return NotFound();
            }

            return sachList;
        }
        [HttpGet("DanhSachHangTon")]
        public async Task<ActionResult<List<Sach>>> DanhSachHangTon()
        {
            var sachList = await _context.Saches
                .Where(s => s.SoLuongTon >0)
                .ToListAsync();

            if (sachList == null || sachList.Count == 0)
            {
                return NotFound();
            }

            return sachList;
        }


        //// Sửa sách ///////
        // PUT: api/SachAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSach(int id, Sach sach)
        {
            if (id != sach.MaSach)
            {
                return BadRequest();
            }

            _context.Entry(sach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(id))
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

        //// Thêm sách ///////
        // POST: api/SachAPI
        [HttpPost]
        public async Task<ActionResult<Sach>> PostSach(Sach sach)
        {
          if (_context.Saches == null)
          {
              return Problem("Entity set 'QuanLiBanSachContext.Saches'  is null.");
          }
            _context.Saches.Add(sach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSach", new { id = sach.MaSach }, sach);
        }
        //// Xoa sách ///////
        // DELETE: api/SachAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSach(int id)
        {
            if (_context.Saches == null)
            {
                return NotFound();
            }
            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }

            _context.Saches.Remove(sach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SachExists(int id)
        {
            return (_context.Saches?.Any(e => e.MaSach == id)).GetValueOrDefault();
        }

        
    }
}
