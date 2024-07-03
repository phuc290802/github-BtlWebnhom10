using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWeb.Models;
using TestWeb.Models.OrderViewModel;
using TestWeb.Models.ProductModels;

namespace TestWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        QuanLiBanSachContext db = new QuanLiBanSachContext();
        [HttpGet]
        public IEnumerable<OrderViewModel> GetOrders()
        {
            //var hdb = db.THoaDonBans.Include(o => o.TChiTietHdbs).ToList();
            var hdb = from p in db.ChiTietDonHangs
                      join c in db.DonHangs on p.MaDonHang equals c.MaDonHang
                      select new OrderViewModel
                      {
                          Ngay = c.NgayDat,
                          DonGia = p.DonGia
                      };
            return hdb.ToList();

        }
        
        [HttpGet("GetRevenueByMonth/{month}")]
        public IEnumerable<OrderViewModel> GetRevenueByMonth(int month)
        {

            var hdb = from p in db.ChiTietDonHangs
                      join c in db.DonHangs on p.MaDonHang equals c.MaDonHang
                      where c.NgayDat.Value.Month == month
                      select new OrderViewModel
                      {
                          Ngay = c.NgayDat,
                          DonGia = p.DonGia
                      };
            var tongDoanhThu = hdb.Sum(x => Convert.ToInt32(x.DonGia));
            return new List<OrderViewModel> { new OrderViewModel { Ngay = null, DonGia = tongDoanhThu.ToString() } };
        }
        [Route("GetGiaTriMaSanPham")]
        [HttpGet("{year}/{maLoai}")]
        public IEnumerable<OrderViewModel> GetGiaTriMaSanPham(int year,int maLoai)
        {

            var hdb = from p in db.ChiTietDonHangs
                      join c in db.DonHangs on p.MaDonHang equals c.MaDonHang
                      where c.NgayDat.Value.Year == year && p.MaSach==maLoai
                      select new OrderViewModel
                      {
                          Ngay = c.NgayDat,
                          DonGia = p.DonGia
                      };
            var tongDoanhThu = hdb.Sum(x => Convert.ToInt32(x.DonGia));
            return new List<OrderViewModel> { new OrderViewModel { Ngay = null, DonGia = tongDoanhThu.ToString() } };
        }
        [Route("GetNhanVienMaxDonHang")]
		public async Task<ActionResult<NhanVien>> GetNhanVienMaxDonHang()
		{
            var nhanVienMaxDonHang = await db.DonHangs 
                .Where(d=>d.DaThanhToan==1)
                .GroupBy(d => d.MaNhanVien)
				.Select(g => new { MaNhanVien = g.Key, SoDonHang = g.Count()})
				.OrderByDescending(x => x.SoDonHang)
				.FirstOrDefaultAsync();

			if (nhanVienMaxDonHang == null)
			{
				return NotFound();
			}

			var nhanVien = await db.NhanViens.FindAsync(nhanVienMaxDonHang.MaNhanVien);

			if (nhanVien == null)
			{
				return NotFound();
			}
			return nhanVien;
		}



    }

}