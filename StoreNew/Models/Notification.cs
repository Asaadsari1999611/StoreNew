using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class Notification
{
    public decimal Id { get; set; }

    public string? Payment { get; set; }

    public string? Arrive { get; set; }

    public string? Viewmessage { get; set; }

    public string? Message { get; set; }

    public string? Feedbackshare { get; set; }

    public string? Cardbalance { get; set; }

    public string? Changeinfcard { get; set; }

    public DateTime? Datenoti { get; set; }

    public string? Orderstate { get; set; }


    public decimal? Userid { get; set; }

    public virtual User? User { get; set; }
}
