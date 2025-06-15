using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreNew.Models;

public partial class Color
{
    public decimal Id { get; set; }

    public string? Namecolors { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile Imagefile { get; set; }

    public decimal? Productid { get; set; }

    public virtual Product? Product { get; set; }
}
