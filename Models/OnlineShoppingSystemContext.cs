using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class OnlineShoppingSystemContext : DbContext
    {
        public OnlineShoppingSystemContext()
        {
        }

        public OnlineShoppingSystemContext(DbContextOptions<OnlineShoppingSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLogin> AdminLogins { get; set; }
        public virtual DbSet<BankDetail> BankDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<ProductOrderDetail> ProductOrderDetails { get; set; }
        public virtual DbSet<Retailer> Retailers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data source=STS583L-BALAMUR\\SQLEXPRESS;user id=sa;password=spanindia; Database=OnlineShoppingSystem;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminLogin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__AdminLog__719FE4E87916355D");

                entity.ToTable("AdminLogin");

                entity.Property(e => e.AdminId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AdminID");

                entity.Property(e => e.AdminPassword)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BankDetail>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PK__BankDeta__6D903F03D1B8421B");

                entity.Property(e => e.BillId).HasColumnName("billId");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("balance");

                entity.Property(e => e.CardHoldername)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cardtye)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cardtye");

                entity.Property(e => e.Cvv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cvv");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Features)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductImage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RetailerId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Product__Categor__6FE99F9F");

                entity.HasOne(d => d.Retailer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.RetailerId)
                    .HasConstraintName("FK__Product__Retaile__70DDC3D8");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__ProductO__C3905BCF16BD0B34");

                entity.Property(e => e.OrderedDate).HasColumnType("date");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Productname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__ProductOr__Custo__3D2915A8");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductOr__Produ__2BFE89A6");
            });

            modelBuilder.Entity<ProductOrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Address)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Customername)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderedDate).HasColumnType("date");

                entity.Property(e => e.Productname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bill)
                    .WithMany()
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("FK__ProductOr__BillI__31B762FC");

                entity.HasOne(d => d.Customer)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__ProductOr__Custo__30C33EC3");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__ProductOr__Order__2FCF1A8A");
            });

            modelBuilder.Entity<Retailer>(entity =>
            {
                entity.ToTable("Retailer");

                entity.Property(e => e.RetailerId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RetailerName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
