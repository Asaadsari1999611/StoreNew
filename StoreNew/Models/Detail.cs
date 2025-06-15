using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class Detail
{
    public decimal Id { get; set; }

    public string? Launch { get; set; }

    public string? Dimensions { get; set; }

    public string? Weight { get; set; }

    public string? Typee { get; set; }

    public string? Sizee { get; set; }

    public string? Os { get; set; }

    public string? Memoryy { get; set; }

    public string? Maincamera { get; set; }

    public string? Selfecamera { get; set; }

    public string? Battery { get; set; }

    public decimal? Productid { get; set; }

    public virtual Product? Product { get; set; }
}
