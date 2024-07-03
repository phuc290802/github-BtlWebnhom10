using System;
using System.Collections.Generic;

namespace TestWeb.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string? TaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
