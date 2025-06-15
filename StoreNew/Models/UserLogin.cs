using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class UserLogin
{
    public decimal Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public DateTime? Lastlogin { get; set; }

    public decimal? Userid { get; set; }

    public decimal? Roleid { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
}
