using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShopAPI.Models;

public partial class ShopContext : DbContext
{
    string ConnectionString;
    public ShopContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<HistoricalInventory> HistoricalInventories { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderList> OrderLists { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Recovery> Recoveries { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F8ON76N;User ID=dbuser;Password=dbpass;Database=shop;Trusted_Connection=False;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brands__3214EC27BB406908");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BrandStatus).HasColumnName("Brand_Status");
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC27618ABF40");

            entity.ToTable("Cart");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasColumnName("Date_Created");
            entity.Property(e => e.InventoryId).HasColumnName("Inventory_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Carts)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_Historical_Cart_Inventory_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Historical_Cart_User_ID");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC2792A0AEF8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryStatus).HasColumnName("Category_Status");
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<HistoricalInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historic__3214EC27D98756DB");

            entity.ToTable("Historical_Inventory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AllTimeStock).HasColumnName("All_Time_Stock");
            entity.Property(e => e.InventoryId).HasColumnName("Inventory_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Inventory).WithMany(p => p.HistoricalInventories)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_Historical_Inventory_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.HistoricalInventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Historical_Inventory_Product_ID");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC27CDF19613");

            entity.ToTable("Inventory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CurrentStock).HasColumnName("Current_Stock");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Inventory_Product_ID");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC27D9236D12");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Historical_Notifications_User_ID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC2750D6A0F9");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AmountPurchased).HasColumnName("Amount_Purchased");
            entity.Property(e => e.DeliveryAddress).HasColumnName("Delivery_Address");
            entity.Property(e => e.OrderStatus).HasColumnName("Order_Status");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.RefCode)
                .HasMaxLength(100)
                .HasColumnName("Ref_Code");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Historical_Orders_User_ID");
        });

        modelBuilder.Entity<OrderList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_Li__3214EC272017EA59");

            entity.ToTable("Order_List");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InventoryId).HasColumnName("Inventory_ID");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Inventory).WithMany(p => p.OrderLists)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_Historical_Order_List_Inventory_ID");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC270017D8C5");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Origin).HasMaxLength(50);
            entity.Property(e => e.ProductStatus).HasColumnName("Product_Status");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Products_Brand_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Category_Id");
        });

        modelBuilder.Entity<Recovery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recovery__3214EC278AA28BF7");

            entity.ToTable("Recovery");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

           // entity.HasOne(d => d.User).WithMany(p => p.Recoveries)
          //      .HasForeignKey(d => d.UserId)
            //    .HasConstraintName("FK_Recovery_User_ID");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sales__3214EC27F552CA04");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AmountPurchased).HasColumnName("Amount_Purchased");
            entity.Property(e => e.DateCreated).HasColumnName("Date_Created");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.Sales)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Historical_Sales_Order_ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC274212C4D1");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E0099468").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Pass).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
