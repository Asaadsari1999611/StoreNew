using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class Contactreverse
{
    public decimal Id { get; set; }

    public string? Message { get; set; }

    public DateTime? Senddate { get; set; }

    public DateTime? Visiondate { get; set; }

    public string? Username { get; set; }
}
