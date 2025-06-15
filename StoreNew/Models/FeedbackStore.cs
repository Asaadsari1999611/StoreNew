using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class FeedbackStore
{
    public decimal Id { get; set; }

    public decimal? Ratingsatars { get; set; }

    public string? Ratingdetails { get; set; }

    public decimal? Productid { get; set; }

    public decimal? Userid { get; set; }

    public string? Shared { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
