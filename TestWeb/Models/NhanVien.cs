using System;
using System.Collections.Generic;

namespace TestWeb.Models;

public partial class NhanVien
{
    public int MaNhanVien { get; set; }

    public string? Username { get; set; }

    public string? TenNhanVien { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? ChucVu { get; set; }

    public string? AnhDaiDien { get; set; }
}
