using Microsoft.AspNetCore.Mvc;
using TestWeb.Models;
using HienlthOnline.Helpers;
using System.Net.WebSockets;
using TestWeb.Models.Home;
using System.Text;
using Newtonsoft.Json;
using TestWeb.Models.AuthenticationClient;
using TestWeb.Models.GmailClient;
using MailKit.Net.Smtp;
using MimeKit;
namespace BtlWebNhom10.Controllers
{
	public class CartController : Controller
	{
		private readonly QuanLiBanSachContext _context;
		public CartController(QuanLiBanSachContext context)
		{
			_context = context;
		}
		public List<CartItem> Carts
		{
			get
			{
				var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
				if (data == null)
				{
					data = new List<CartItem>();
				}
				return data;
			}
		}
		public IActionResult Index()
		{
			return View(Carts);
		}
		public static int sosach=0;
		public int getSoSach()
		{
			return sosach;
		}
		public void setSoSach(int ss)
		{
			sosach = ss;
		}
		private GmailClient gm = new GmailClient();
		[HttpGet]
		public IActionResult AddToCart(int id)
		{
			
			var myCart = Carts;
			var item = myCart.SingleOrDefault(p => p.ProductId == id);
			var maxProductId = myCart.Any() ? myCart.Max(p => p.ProductId) : 0; // Kiểm tra danh sách giỏ hàng có rỗng hay không
			
			if (item == null)//chưa có trong giỏ hàng
			{
				var Product = _context.Saches.SingleOrDefault(p => p.MaSach == id);
				item = new CartItem
				{

					ProductId = id,
					ProductName = Product.TenSach,
					Price = Product.GiaBan.Value,
					Quantity = 1,
					Image = Product.AnhBia
				};
				setSoSach(getSoSach()+1);
				myCart.Add(item);
				gm.setSoSach(getSoSach());
			}
			else
			{
				item.Quantity += 1;
			}
			
			HttpContext.Session.Set("GioHang", myCart);
			return RedirectToAction("Index");
		}
		public IActionResult delete(int id)
		{
			var myCart = Carts;
			var item = myCart.SingleOrDefault(p => p.ProductId == id);
			myCart.Remove(item);
			gm.setSoSach(gm.getSoSach()-1);
			setSoSach(getSoSach() -1);
			HttpContext.Session.Set("GioHang", myCart);
			return RedirectToAction("Index");
		}
		public IActionResult AdjustQuantity(int id)
		{
			var quantity = int.Parse(Request.Form["quantity"]);
			var myCart = Carts;
			var item = myCart.SingleOrDefault(p => p.ProductId == id);
			if (item != null)//chưa có trong giỏ hàng
			{
				item.Quantity = quantity;
			}
			HttpContext.Session.Set("GioHang", myCart);
			return RedirectToAction("Index");
		}
		////////////////Thanh toan////////////////
		[Route("PayMent")]
		public IActionResult PayMent()
		{
			return View();
		}
		///Gửi gmail ////////////////
		 public void SendEmail()
		{
			GmailClient gm = new GmailClient();
			var myGmail = gm.getEmail();
			MimeMessage message = new MimeMessage();
			MailboxAddress from = new MailboxAddress("E-Book", "phucmk700@gmail.com");
			message.From.Add(from);
			MailboxAddress to = new MailboxAddress("ToDemo", myGmail);
			message.To.Add(to);
			message.Subject = "Đơn hàng đã được xác nhận";
			BodyBuilder bodybuilder = new BodyBuilder();
			bodybuilder.HtmlBody =
    "<section class=\"login-main-wrapper\">\r\n" +
    "   <div class=\"main-container\">\r\n" +
    "       <div class=\"login-process\">\r\n" +
    "           <div class=\"login-main-container\">\r\n" +
    "               <div class=\"thankyou-wrapper\" style=\" width: 100%;\r\n" +
    "                   height: auto;\r\n" +
    "                   margin: auto;\r\n" +
    "                   background: #ffffff;\r\n" +
    "                   padding: 10px 0px 50px;\">\r\n" +
    "                   <h1 style=\"font: 100px Arial, Helvetica, sans-serif;\r\n" +
    "                       text-align: center;\r\n" +
    "                       color: #333333;\r\n" +
    "                       padding: 0px 10px 10px;\">\r\n" +
    "                       <img src=\"http://montco.happeningmag.com/wp-content/uploads/2014/11/thankyou.png\" alt=\"thanks\" />\r\n" +
    "                   </h1>\r\n" +
    "                   <p style=\"font: 26px Arial, Helvetica, sans-serif;\r\n" +
    "                       text-align: center;\r\n" +
    "                       color: #333333;\r\n" +
    "                       padding: 5px 10px 10px;\">Cảm ơn quý khách đã đặt hàng của Shop</p>\r\n" +
	"                   <a href=\"https://localhost:7086/payment\" style=\"font: 26px Arial, Helvetica, sans-serif;\r\n" +
    "                       text-align: center;\r\n" +
    "                       color: #ffffff;\r\n" +
    "                       display: block;\r\n" +
    "                       text-decoration: none;\r\n" +
    "                       width: 250px;\r\n" +
    "                       background: #E47425;\r\n" +
    "                       margin: 10px auto 0px;\r\n" +
    "                       padding: 15px 20px 15px;\r\n" +
    "                       border-bottom: 5px solid #F96700;\">Thanh toán</a>\r\n" +
    "                   <div class=\"clr\"></div>\r\n" +
    "               </div>\r\n" +
    "               <div class=\"clr\"></div>\r\n" +
    "           </div>\r\n" +
    "       </div>\r\n" +
    "       <div class=\"clr\"></div>\r\n" +
    "   </div>\r\n" +
    "</section>";
           
			
	}
	
	[AuthenticationClient]
		///Thêm chi tiết hóa đơn ////////////////
		public IActionResult ThemDonHang()
		{
			GmailClient gm = new GmailClient();
			var myGmail = gm.getEmail();
			if (ModelState.IsValid)
			{
				DonHang sanpham = new DonHang() { DaThanhToan = 0, TinhTrangGiaoHang = 0, NgayDat = DateTime.Now, NgayGiao = DateTime.Now, MaKh = gm.getMaKhachHang(), MaNhanVien = 1 };
				_context.DonHangs.Add(sanpham);
				_context.SaveChanges();
			}
			var myCart = Carts;
			int maxMadonhang = _context.DonHangs.OrderByDescending(c => c.MaDonHang).FirstOrDefault().MaDonHang;
			if (maxMadonhang != null)
			{
				foreach (CartItem item in Carts)
				{
					ChiTietDonHang chiTietDonHang = new ChiTietDonHang() { MaDonHang = maxMadonhang, MaSach = item.ProductId, SoLuong = item.Quantity, DonGia = item.Total.ToString() };
					_context.ChiTietDonHangs.Add(chiTietDonHang);
					_context.SaveChanges();
				}
			}
			SendEmail();
			gm.setSoSach(0);
			HttpContext.Session.Remove("GioHang");
			return RedirectToAction("Index");

		}

	}

}
