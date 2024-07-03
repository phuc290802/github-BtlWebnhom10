using TestWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using TestWeb.Models.Authentication;
using TestWeb.Areas.Admin.Models;
using System.Linq;

namespace AdminTest.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QuanLiBanSachContext db = new QuanLiBanSachContext();
        [Route("")]
        [Route("index")]
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
        // Hiện danh Sách sản phẩm ///////////////////////////////////////////////////////////
        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pagesize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Saches.AsNoTracking().OrderBy(x => x.TenSach);
            PagedList<Sach> lst = new PagedList<Sach>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaNxb=new SelectList(db.NhaXuatBans.ToList(),"MaNxb","TenNxb");
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList(), "MaChuDe", "TenChuDe");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(Sach sanPham)
        {
            if(ModelState.IsValid)
            {
                db.Saches.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int maSanPham)   
        {
            ViewBag.MaNxb = new SelectList(db.NhaXuatBans.ToList(), "MaNxb", "TenNxb");
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList(), "MaChuDe", "TenChuDe");
            var sanPham = db.Saches.Find(maSanPham);
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Sach sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham","HomeAdmin");
            }
            return View();
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.Saches.Where(x => x.MaSach == maSanPham).ToList();
            var anhSanPhams = db.Saches.Where(x => x.MaSach == maSanPham).ToList();
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.Saches.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Chủ đề đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
        // Hiện chủ đề  ///////////////////////////////////////////////////////////
        [Route("danhmucchude")]
        public IActionResult DanhMucChuDe(int? page)
        {
            int pagesize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.ChuDes.AsNoTracking().OrderBy(x => x.TenChuDe);
            PagedList<ChuDe> lst = new PagedList<ChuDe>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }
        [Route("ThemChuDeMoi")]
        [HttpGet]
        public IActionResult ThemChuDeMoi()
        {
            return View();
        }
        [Route("ThemChuDeMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemChuDeMoi(ChuDe sanPham)
        {
            if (ModelState.IsValid)
            {
                db.ChuDes.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("ThemChuDeMoi");
            }
            return View(sanPham);
        }
        [Route("XoaChuDe")]
        [HttpGet]
        public IActionResult XoaChuDe(int maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.ChuDes.Where(x => x.MaChuDe == maSanPham).ToList();
            var anhSanPhams = db.ChuDes.Where(x => x.MaChuDe == maSanPham).ToList();
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.ChuDes.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Chủ đề đã được xóa";
            return RedirectToAction("DanhMucChuDe", "HomeAdmin");
        }

        // Hiện Nhà xuất bản  ///////////////////////////////////////////////////////////

        [Route("danhmucNXB")]
        public IActionResult DanhMucNxb(int? page)
        {
            int pagesize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.NhaXuatBans.AsNoTracking().OrderBy(x => x.TenNxb);
            PagedList<NhaXuatBan> lst = new PagedList<NhaXuatBan>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }
        [Route("ThemNxb")]
        [HttpGet]
        public IActionResult ThemNxb()
        {
            return View();
        }
        [Route("ThemNxb")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNxb(NhaXuatBan sanPham)
        {
            if (ModelState.IsValid)
            {
                db.NhaXuatBans.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("ThemNxb");
            }
            return View(sanPham);
        }
        [Route("XoaNxb")]
        [HttpGet]
        public IActionResult XoaNxb(int maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.NhaXuatBans.Where(x => x.MaNxb == maSanPham).ToList();

            var anhSanPhams = db.NhaXuatBans.Where(x => x.MaNxb == maSanPham).ToList();
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.NhaXuatBans.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucNxb", "HomeAdmin");
        }
        // Hiện Danh sách nhân viên   ///////////////////////////////////////////////////////////
        [Route("DanhSachNhanVien")]
        public IActionResult DanhSachNhanVien(int? page)
        {
            int pagesize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.NhanViens.AsNoTracking().OrderBy(x => x.TenNhanVien);
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }
        
        [Route("ThemNhanVienMoi")]
        [HttpGet]
        public IActionResult ThemNhanVienMoi()
        {
            return View();
        }
        [Route("ThemNhanVienMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVienMoi(NhanVien sanPham)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("ThemNhanVienMoi");
            }
            return View(sanPham);
        }
        
        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(int maSanPham)
        {
            var sanPham = db.NhanViens.Find(maSanPham);
            return View(sanPham);
        }
        [Route("SuaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(NhanVien sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachNhanVien", "HomeAdmin");
            }
            return View();
        }

        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(int maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.NhanViens.Where(x => x.MaNhanVien == maSanPham).ToList();   
            var anhSanPhams = db.NhanViens.Where(x => x.MaNhanVien == maSanPham).ToList();
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.NhanViens.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhSachNhanVien", "HomeAdmin");
        }
        // Hiện Danh sách Chi tiết đơn Hàng  ///////////////////////////////////////////////////////////
        [Route("DanhSachChiTietDonHang")]
        public IActionResult DanhSachChiTietDonHang(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstsanpham = db.ChiTietDonHangs
                .Where(c => c.MaSach != null)
                .Join(
                    db.Saches,
                    chiTiet => chiTiet.MaSach,
                    sach => sach.MaSach,
                    (chiTiet, sach) => new ChiTietDonHangViewModel
                    {
                        MaDonHang1 = chiTiet.MaDonHang,
                        SoLuong1 = (int)chiTiet.SoLuong,
                        img1 = sach.AnhBia,
                        DonGia1 = chiTiet.DonGia,
                        MaSach1 = chiTiet.MaSach,
                        TenSach1 = sach.TenSach,
                        AnhBia1 = sach.AnhBia,
                        GiaBan1 = (decimal)sach.GiaBan,
                        // Include other properties from ChiTietDonHangViewModel if needed
                    }
                )
                .OrderBy(x => x.MaDonHang1) // Sort by MaDonHang1 in ascending order
                .ToPagedList(pageNumber, pageSize);

            return View(lstsanpham);
        }



        [Route("SuaDonHang")]
		[HttpGet]
		public IActionResult SuaDonHang(int maSanPham,int maSach)
		{
            var sanPham = db.ChiTietDonHangs.Find(maSanPham,maSach);
			return View(sanPham);
		}
		[Route("SuaDonHang")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SuaDonHang(ChiTietDonHang sanPham)
		{
			if (ModelState.IsValid)
			{
				db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
				return RedirectToAction("DanhSachChiTietDonHang", "HomeAdmin");
			}
			return View();
		}

        [Route("XoaDonHang")]
        [HttpGet]
        public IActionResult XoaDonHang(int maSanPham, int maSach)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.ChiTietDonHangs.Where(x => x.MaDonHang == maSanPham && x.MaSach == maSach).ToList();
            var anhSanPhams = db.ChiTietDonHangs.Where(x => x.MaDonHang == maSanPham && x.MaSach == maSach).ToList();
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.ChiTietDonHangs.Find(maSanPham,maSach));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhSachChiTietDonHang", "HomeAdmin");
        }
        //////////////////////////////////Doanh thu hàng tháng/////////////////////////////////
        ///
        [Route("DoanhTHuDonHang")]
        public void  DoanhTHuDonHang(int dt)
        {
            List <int> listHd = new List<int>();
            List<int> listCthdh = new List<int>();
            DonHang donhang = new DonHang();
            ChiTietDonHang cthdh = new ChiTietDonHang();
            if (dt == donhang.NgayDat.Value.Month)
            {
                listHd.Add(donhang.MaDonHang);
            }
			
        }
        
        //Danh sách khách hàng API///////////////////////////////

        [Route("DanhSachKhachHang")]
        [HttpGet]
        public IActionResult DanhSachKhachHang(int? page)
        {
            int pagesize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.KhachHangs.AsNoTracking().OrderBy(x => x.MaKh);
            PagedList<KhachHang> lst = new PagedList<KhachHang>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }
        //Danh sách Nhân viên API///////////////////////////////

        [Route("DanhSachNhanVienAPI")]
        [HttpGet]
        public IActionResult DanhSachNhanVienAPI(int? page)
        {
            int pagesize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.NhanViens.AsNoTracking().OrderBy(x => x.MaNhanVien);
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }
        /////////////////////////////////Doanh thu API///////////////////////////////
        [Route("Revenue")]
        public IActionResult Revenue()
        {   
            return View();
        }
        /////////////////////////////////Danh sách sản phẩm API///////////////////////////////
        [Route("DanhSachSanPhamAPI")]
        public IActionResult DanhSachSanPhamAPI(int? page)
        {
            int pagesize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Saches.AsNoTracking().OrderBy(x => x.TenSach);
            PagedList<Sach> lst = new PagedList<Sach>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }
        /////////////////////////////////Danh sách đơn hàng API///////////////////////////////
        [Route("DonHangAPI")]
        public IActionResult DonHangAPI(int? page)
        {
            int pagesize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.DonHangs.AsNoTracking().OrderBy(x => x.MaDonHang);
            PagedList<DonHang> lst = new PagedList<DonHang>(lstsanpham,
                pageNumber, pagesize);
            return View(lst);
        }

    }

}
