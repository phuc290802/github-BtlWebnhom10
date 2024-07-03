using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWeb.Models;
using TestWeb.Models.ProductModels;

namespace TestWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QuanLiBanSachContext db = new QuanLiBanSachContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var sanPham=(from p in db.Saches select new Product
            {
                MaChuDe=p.MaChuDe,
                MaSach=p.MaSach,
                TenSach=p.TenSach,
                GiaBan=p.GiaBan,
                AnhBia=p.AnhBia,
                SoLuongTon=p.SoLuongTon,   
            }).ToList();
            return sanPham;
        }
        [HttpGet("{maloai}")]
        public IEnumerable<Product> GetAllProductsByCategory(int maLoai)
        {
            var sanPham = (from p in db.Saches
                           where p.MaChuDe==maLoai
                           select new Product
                           {
                               MaChuDe= p.MaChuDe,
                               MaSach = p.MaSach,
                               TenSach = p.TenSach,
                               GiaBan = p.GiaBan,
                               AnhBia = p.AnhBia,
                               SoLuongTon = p.SoLuongTon,
                           }).ToList();
            return sanPham;
        }
    }
}
