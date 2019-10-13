using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreApp.DataAccess.Entities
{
    public partial class DoapSoapContext : DbContext
    {
        public DoapSoapContext()
        {
        }

        public DoapSoapContext(DbContextOptions<DoapSoapContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<InventoryItems> InventoryItems { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasKey(e => e.ColorId)
                    .HasName("PK__Colors__8DA7676D9F78C7A4");

                entity.HasIndex(e => e.Color)
                    .HasName("UQ__Colors__E11D38451B80265C")
                    .IsUnique();

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64B8CEBF65D2");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<InventoryItems>(entity =>
            {
                entity.HasKey(e => e.InventoryItemId)
                    .HasName("PK__Inventor__3BB2ACA044471F4D");

                entity.ToTable("Inventory_Items");

                entity.Property(e => e.InventoryItemId).HasColumnName("InventoryItemID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Inventory__Locat__7755B73D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Inventory__Produ__7849DB76");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Location__E7FEA47783E7B006");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => e.OrderItem)
                    .HasName("PK__Order_It__1A66061E97FFFE15");

                entity.ToTable("Order_Items");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Order_Ite__Order__73852659");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Order_Ite__Produ__74794A92");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BAF0EC858EF");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.TimeConfirmed)
                    .HasColumnName("Time_Confirmed")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__Customer__6FB49575");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Orders__Location__70A8B9AE");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6ED45015F20");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Products__ColorI__6CD828CA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
