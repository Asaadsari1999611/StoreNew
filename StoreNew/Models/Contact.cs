using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class Contact
{
    public decimal Id { get; set; }

    public string? Message { get; set; }

    public DateTime? Senddate { get; set; }

    public DateTime? Visiondate { get; set; }

    public decimal? Userid { get; set; }

    public virtual User? User { get; set; }
}
