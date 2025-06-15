using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class Payment
{
    public decimal Id { get; set; }

    public decimal? Totalamount { get; set; }

    public DateTime? Paymentdate { get; set; }

    public string? Paymenttype { get; set; }

    public decimal? Shopingcartid { get; set; }

    public virtual Shopingcart? Shopingcart { get; set; }
}
