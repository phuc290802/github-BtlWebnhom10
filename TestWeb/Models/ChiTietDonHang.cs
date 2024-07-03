using System;
using System.Collections.Generic;

namespace TestWeb.Models;

public partial class ChiTietDonHang
{
    public int MaDonHang { get; set; }

    public int MaSach { get; set; }

    public int? SoLuong { get; set; }

    public string? DonGia { get; set; }

}
