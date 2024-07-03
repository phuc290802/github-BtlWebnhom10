using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestWeb.Models;
using X.PagedList;
using MailKit.Net.Smtp;
using MimeKit;
using TestWeb.Models.GmailClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		QuanLiBanSachContext db = new QuanLiBanSachContext();
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		

		public IActionResult Index(int? page)
		{

			int pagesize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstsanpham = db.Saches.AsNoTracking().OrderBy(x => x.TenSach);
			PagedList<Sach> lst = new PagedList<Sach>(lstsanpham,
				pageNumber, pagesize);
			return View(lst);
		}
		public IActionResult SanPhamtheoLoai(int maloai, int? page)
		{
			int pagesize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstsanpham = db.Saches.AsNoTracking().Where(x => x.MaChuDe == maloai).OrderBy(x => x.TenSach);
			PagedList<Sach> lst = new PagedList<Sach>(lstsanpham,
				pageNumber, pagesize);
			ViewBag.maloai = maloai;
			return View(lst);
		}
        ///////////////  Chi tiết sản phẩm ///////////////
        public IActionResult ChiTietSanPham(int MaSach)
        {
            Sach sp = db.Saches.SingleOrDefault(x => x.MaSach == MaSach);
            ViewBag.LstImages = db.Saches.Where(x => x.MaSach == MaSach).ToList();
            return View(sp);
        }

        ///////////////  Tìm kiếm sản phẩm ///////////////
        public IActionResult Search(string searchString)
        {
            var products = db.Saches.Where(p => p.TenSach.Contains(searchString));
            return View(products);
        }

        ///////////////  Đăng nhập đăng ký ///////////////
        public IActionResult Privacy(string SearchString = "")
		{
			if (SearchString != "")
			{
				var sach = db.Saches.Include(s => s.MaSach).Where(x => x.TenSach.ToUpper().Contains(SearchString.ToUpper()));
				return View(sach.ToList());
			}
			return View();
		}

        [Route("DanhMucKhachHang")]
        public IActionResult DanhMucKhachHang()
        {
			GmailClient gm = new GmailClient();
			KhachHang kh = db.KhachHangs.SingleOrDefault(x => x.MaKh == gm.getMaKhachHang());
			return View(kh);
        }
        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(int maSanPham)
        {
            var KhachHangSave = db.KhachHangs.Find(maSanPham);
            return View(KhachHangSave);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		

}
}