using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestWeb.Models;
using TestWeb.Models.GmailClient;

namespace TestWeb.Controllers
{
    
    public class LoginClientController : Controller
    {
        QuanLiBanSachContext db = new QuanLiBanSachContext();
        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName")==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            
        }
        [HttpPost]
        public IActionResult Login(KhachHang khachhang)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var kh = db.KhachHangs.Where(x => x.TaiKhoan.Equals(khachhang.TaiKhoan) && x.MatKhau.Equals(khachhang.MatKhau)).FirstOrDefault();
                if (kh != null)
                {
                  
                    HttpContext.Session.SetString("UserName", kh.TaiKhoan .ToString());
                    GmailClient gm = new GmailClient();
                    gm.setEmail(kh.Email.ToString());
                    gm.setName(kh.TaiKhoan.ToString());
                    gm.setMaKhachHang(kh.MaKh);
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View();
        }
		
		[HttpGet]
		public IActionResult Register()
        {
            return View();
        }
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(KhachHang khachang)
		{
			if (ModelState.IsValid)
			{
				db.KhachHangs.Add(khachang);
				db.SaveChanges();
				return RedirectToAction("Login", "LoginClient");
			}
			return View(khachang);
		}
		public IActionResult Logout()
        {
			GmailClient gm = new GmailClient();
			gm.setEmail(null);
			gm.setName(null);
			HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
    }
}
