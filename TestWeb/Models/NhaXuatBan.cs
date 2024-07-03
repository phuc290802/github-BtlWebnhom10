using System;
using System.Collections.Generic;

namespace TestWeb.Models;

public partial class NhaXuatBan
{
    public int MaNxb { get; set; }

    public string? TenNxb { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
}
