using System;
using System.Collections.Generic;

namespace TestWeb.Models;

public partial class ThamGium
{
    public int MaSach { get; set; }

    public int MaTacGia { get; set; }

    public string? VaiTro { get; set; }

    public string? ViTri { get; set; }

    public virtual TacGium MaTacGiaNavigation { get; set; } = null!;
}
