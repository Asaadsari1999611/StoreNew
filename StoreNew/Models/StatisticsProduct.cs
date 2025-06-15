using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class StatisticsProduct
{
    public decimal Id { get; set; }

    public decimal? Topnumorderforproduct { get; set; }

    public decimal? Topevaluateforproduct { get; set; }

    public decimal? Productid { get; set; }

    public virtual Product? Product { get; set; }
}
