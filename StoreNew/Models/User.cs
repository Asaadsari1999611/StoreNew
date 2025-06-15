using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreNew.Models;

public partial class User
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? Adress { get; set; }

    public decimal? Phonenumber { get; set; }

    public string? Email { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile Imagefile { get; set; }
    public string? Onlinee { get; set; }

    public virtual Card? Card { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<FeedbackProduct> FeedbackProducts { get; set; } = new List<FeedbackProduct>();

    public virtual ICollection<FeedbackStore> FeedbackStores { get; set; } = new List<FeedbackStore>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Shopingcart> Shopingcarts { get; set; } = new List<Shopingcart>();

    public virtual ICollection<StatisticsCustomer> StatisticsCustomers { get; set; } = new List<StatisticsCustomer>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
