using Microsoft.AspNetCore.Mvc;
using TestWeb.Models;

namespace TestWeb.Controllers
{
    public class AccessController : Controller
    {
        QuanLiBanSachContext db = new QuanLiBanSachContext();
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName1") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("HomeAdmin", "Admin");
            }
        }
        [HttpPost]
        public IActionResult Login(NhanVien nhanVien)
        {
            if (HttpContext.Session.GetString("UserName1") == null)
            {
                var nv = db.NhanViens.Where(x => x.Username.Equals(nhanVien.Username) && x.SoDienThoai.Equals(nhanVien.SoDienThoai)).FirstOrDefault();
                if (nv != null)
                {
                    HttpContext.Session.SetString("UserName1", nv.Username.ToString());
                    return RedirectToAction("HomeAdmin", "Admin");
                }
            }
            return View();
        }
        public IActionResult Logout() { 
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName1");
            return RedirectToAction("Login","Access");
        }
    }
}
