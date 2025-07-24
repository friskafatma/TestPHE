using System;
using System.Collections.Generic;

namespace TestPHE.Models;

public partial class TblBarang
{
    public int Id { get; set; }

    public string? Nama { get; set; }

    public int? Stok { get; set; }

    public decimal? Harga { get; set; }
}
