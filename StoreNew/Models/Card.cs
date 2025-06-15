using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class Card
{
    public decimal Id { get; set; }

    public decimal? Cardnumber { get; set; }

    public decimal? Balance { get; set; }

    public string? Status { get; set; }

    public decimal? Userid { get; set; }

    public decimal? Cvv { get; set; }

    public virtual User? User { get; set; }
}
