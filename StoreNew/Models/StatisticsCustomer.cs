using System;
using System.Collections.Generic;

namespace StoreNew.Models;

public partial class StatisticsCustomer
{
    public decimal Id { get; set; }

    public decimal? Topnumorderforcustomer { get; set; }

    public decimal? Topbayforcustomer { get; set; }

    public decimal? Userid { get; set; }

    public virtual User? User { get; set; }
}
