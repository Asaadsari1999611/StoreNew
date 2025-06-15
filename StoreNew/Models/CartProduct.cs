using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class CartProduct
{
    public decimal Id { get; set; }

    public decimal? Productprice { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Cartid { get; set; }

    public decimal? Productid { get; set; }

    public virtual Shopingcart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
