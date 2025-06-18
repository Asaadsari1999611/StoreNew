using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreNew.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CartProduct> CartProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Contactreverse> Contactreverses { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<FeedbackProduct> FeedbackProducts { get; set; }

    public virtual DbSet<FeedbackStore> FeedbackStores { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shopingcart> Shopingcarts { get; set; }

    public virtual DbSet<StatisticsCustomer> StatisticsCustomers { get; set; }

    public virtual DbSet<StatisticsProduct> StatisticsProducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

 /*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=device_store;Password=321;");
 */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
           // .HasDefaultSchema("DEVICE_STORE")
           .HasDefaultSchema("ADMIN")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008459");

            entity.ToTable("CARD");

            entity.HasIndex(e => e.Userid, "SYS_C008460").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cardnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("CARDNUMBER");
            entity.Property(e => e.Cvv)
                .HasColumnType("NUMBER")
                .HasColumnName("CVV");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithOne(p => p.Card)
                .HasForeignKey<Card>(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008461");
        });

        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008488");

            entity.ToTable("CART_PRODUCT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Cartid)
                .HasColumnType("NUMBER")
                .HasColumnName("CARTID");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Productprice)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTPRICE");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.Cartid)
                .HasConstraintName("SYS_C008489");

            entity.HasOne(d => d.Product).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("SYS_C008490");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008447");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Gategoryname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GATEGORYNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008456");

            entity.ToTable("COLORS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Namecolors)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAMECOLORS");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");

            entity.HasOne(d => d.Product).WithMany(p => p.Colors)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("SYS_C008457");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008471");

            entity.ToTable("CONTACT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Senddate)
                .HasColumnType("DATE")
                .HasColumnName("SENDDATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Visiondate)
                .HasColumnType("DATE")
                .HasColumnName("VISIONDATE");

            entity.HasOne(d => d.User).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008472");
        });

        modelBuilder.Entity<Contactreverse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008474");

            entity.ToTable("CONTACTREVERSE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Senddate)
                .HasColumnType("DATE")
                .HasColumnName("SENDDATE");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Visiondate)
                .HasColumnType("DATE")
                .HasColumnName("VISIONDATE");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008452");

            entity.ToTable("DETAILS");

            entity.HasIndex(e => e.Productid, "SYS_C008453").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Battery)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BATTERY");
            entity.Property(e => e.Dimensions)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIMENSIONS");
            entity.Property(e => e.Launch)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LAUNCH");
            entity.Property(e => e.Maincamera)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MAINCAMERA");
            entity.Property(e => e.Memoryy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MEMORYY");
            entity.Property(e => e.Os)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OS");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Selfecamera)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SELFECAMERA");
            entity.Property(e => e.Sizee)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SIZEE");
            entity.Property(e => e.Typee)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TYPEE");
            entity.Property(e => e.Weight)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WEIGHT");

            entity.HasOne(d => d.Product).WithOne(p => p.Detail)
                .HasForeignKey<Detail>(d => d.Productid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008454");
        });

        modelBuilder.Entity<FeedbackProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008467");

            entity.ToTable("FEEDBACK_PRODUCT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Ratingdetails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("RATINGDETAILS");
            entity.Property(e => e.Ratingsatars)
                .HasColumnType("NUMBER")
                .HasColumnName("RATINGSATARS");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Product).WithMany(p => p.FeedbackProducts)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("SYS_C008468");

            entity.HasOne(d => d.User).WithMany(p => p.FeedbackProducts)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008469");
        });

        modelBuilder.Entity<FeedbackStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008463");

            entity.ToTable("FEEDBACK_STORE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Ratingdetails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("RATINGDETAILS");
            entity.Property(e => e.Ratingsatars)
                .HasColumnType("NUMBER")
                .HasColumnName("RATINGSATARS");
            entity.Property(e => e.Shared)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SHARED");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Product).WithMany(p => p.FeedbackStores)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("SYS_C008464");

            entity.HasOne(d => d.User).WithMany(p => p.FeedbackStores)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008465");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008476");

            entity.ToTable("NOTIFICATION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Arrive)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ARRIVE");
            entity.Property(e => e.Cardbalance)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CARDBALANCE");
            entity.Property(e => e.Changeinfcard)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CHANGEINFCARD");
            entity.Property(e => e.Datenoti)
                .HasColumnType("DATE")
                .HasColumnName("DATENOTI");
            entity.Property(e => e.Feedbackshare)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FEEDBACKSHARE");
            entity.Property(e => e.Message)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Payment)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAYMENT");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Viewmessage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VIEWMESSAGE");
            entity.Property(e => e.Orderstate)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("ORDERSTATE");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008477");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008492");

            entity.ToTable("PAYMENT");

            entity.HasIndex(e => e.Shopingcartid, "SYS_C008493").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Paymentdate)
                .HasColumnType("DATE")
                .HasColumnName("PAYMENTDATE");
            entity.Property(e => e.Paymenttype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAYMENTTYPE");
            entity.Property(e => e.Shopingcartid)
                .HasColumnType("NUMBER")
                .HasColumnName("SHOPINGCARTID");
            entity.Property(e => e.Totalamount)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALAMOUNT");

            entity.HasOne(d => d.Shopingcart).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.Shopingcartid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008494");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008449");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Categoryid)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORYID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER")
                .HasColumnName("PRICE");
            entity.Property(e => e.Sale)
                .HasColumnType("NUMBER")
                .HasColumnName("SALE");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.Stochquntity)
                .HasColumnType("NUMBER")
                .HasColumnName("STOCHQUNTITY");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("SYS_C008450");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008439");

            entity.ToTable("ROLES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Shopingcart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008485");

            entity.ToTable("SHOPINGCART");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Orderdate)
                .HasColumnType("DATE")
                .HasColumnName("ORDERDATE");
            entity.Property(e => e.Totalamount)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALAMOUNT");
            entity.Property(e => e.Datearrive)
               .HasColumnType("DATE")
               .HasColumnName("DATEARRIVE");
            entity.Property(e => e.Orderstate)
       .HasMaxLength(50)
         .IsUnicode(false)
       .HasColumnName("ORDERSTATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Shopingcarts)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008486");
        });

        modelBuilder.Entity<StatisticsCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008479");

            entity.ToTable("STATISTICS_CUSTOMER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Topbayforcustomer)
                .HasColumnType("NUMBER")
                .HasColumnName("TOPBAYFORCUSTOMER");
            entity.Property(e => e.Topnumorderforcustomer)
                .HasColumnType("NUMBER")
                .HasColumnName("TOPNUMORDERFORCUSTOMER");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.StatisticsCustomers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008480");
        });

        modelBuilder.Entity<StatisticsProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008482");

            entity.ToTable("STATISTICS_PRODUCT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Topevaluateforproduct)
                .HasColumnType("NUMBER")
                .HasColumnName("TOPEVALUATEFORPRODUCT");
            entity.Property(e => e.Topnumorderforproduct)
                .HasColumnType("NUMBER")
                .HasColumnName("TOPNUMORDERFORPRODUCT");

            entity.HasOne(d => d.Product).WithMany(p => p.StatisticsProducts)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("SYS_C008483");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008441");

            entity.ToTable("USERS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Adress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ADRESS");
            entity.Property(e => e.Birthdate)
                .HasColumnType("DATE")
                .HasColumnName("BIRTHDATE");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.Onlinee)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'of'\n")
                .HasColumnName("ONLINEE");
            entity.Property(e => e.Phonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("PHONENUMBER");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008443");

            entity.ToTable("USER_LOGIN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("DATE")
                .HasColumnName("LASTLOGIN");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("SYS_C008445");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008444");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
