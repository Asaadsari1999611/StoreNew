using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreNew.Models;

public partial class Product
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public decimal? Sale { get; set; }

    public decimal? Stochquntity { get; set; }

    public string? Status { get; set; }

    public string? Imagepath { get; set; }

    public decimal? Categoryid { get; set; }

    [NotMapped]
    public IFormFile Imagefile { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Color> Colors { get; set; } = new List<Color>();

    public virtual Detail? Detail { get; set; }

    public virtual ICollection<FeedbackProduct> FeedbackProducts { get; set; } = new List<FeedbackProduct>();

    public virtual ICollection<FeedbackStore> FeedbackStores { get; set; } = new List<FeedbackStore>();

    public virtual ICollection<StatisticsProduct> StatisticsProducts { get; set; } = new List<StatisticsProduct>();
}
