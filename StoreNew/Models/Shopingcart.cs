using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class Shopingcart
{
    public decimal Id { get; set; }

    public DateTime? Orderdate { get; set; }

    public decimal? Totalamount { get; set; }

    public DateTime? Datearrive { get; set; }
    public string? Orderstate { get; set; }

    public decimal? Userid { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual Payment? Payment { get; set; }

    public virtual User? User { get; set; }
}
