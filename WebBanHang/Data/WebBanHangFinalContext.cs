using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using WebBanHang.Models.EF;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebBanHang.Data
{
    public partial class WebBanHangFinalContext : DbContext
    {
        public WebBanHangFinalContext()
        {
        }

        public WebBanHangFinalContext(DbContextOptions<WebBanHangFinalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbContact> TbContact { get; set; }
        public virtual DbSet<TbMenu> TbMenu { get; set; }
        public virtual DbSet<TbNews> TbNews { get; set; }
        public virtual DbSet<TbOrder> TbOrder { get; set; }
        public virtual DbSet<TbOrderDetail> TbOrderDetail { get; set; }
        public virtual DbSet<TbProductCategory> TbProductCategory { get; set; }
        public virtual DbSet<TbProductImage> TbProductImage { get; set; }
        public virtual DbSet<TbProducts> TbProducts { get; set; }
        public virtual DbSet<TbStatistic> TbStatistic { get; set; }
        public virtual DbSet<TbSubscibe> TbSubscibe { get; set; }
        public virtual DbSet<TbSystemSetting> TbSystemSetting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data source=.\\SQLExpress;initial catalog=WebBanHangFinal;Trusted_Connection=yes;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbContact>(entity =>
            {
                entity.ToTable("tb_Contact");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Message).HasMaxLength(2000);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TbMenu>(entity =>
            {
                entity.ToTable("tb_Menu");

                entity.Property(e => e.Alias).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TbNews>(entity =>
            {
                entity.ToTable("tb_News");

                entity.Property(e => e.Alias).HasMaxLength(200);

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.ToTable("tb_Order");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ward)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TbOrderDetail>(entity =>
            {
                entity.ToTable("tb_OrderDetail");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TbOrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_OrderD__Order__4316F928");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbOrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_OrderD__Produ__440B1D61");
            });

            modelBuilder.Entity<TbProductCategory>(entity =>
            {
                entity.ToTable("tb_ProductCategory");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TbProductImage>(entity =>
            {
                entity.ToTable("tb_ProductImage");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.IsDefault).HasColumnName("isDefault");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbProductImage)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__tb_Produc__Produ__02FC7413");
            });

            modelBuilder.Entity<TbProducts>(entity =>
            {
                entity.ToTable("tb_Products");

                entity.Property(e => e.Alias).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductCode).HasMaxLength(10);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Produc__Produ__403A8C7D");
            });

            modelBuilder.Entity<TbStatistic>(entity =>
            {
                entity.ToTable("tb_Statistic");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TbSubscibe>(entity =>
            {
                entity.ToTable("tb_Subscibe");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TbSystemSetting>(entity =>
            {
                entity.HasKey(e => e.SettingKey)
                    .HasName("PK__tb_Syste__01E719AC552AFB59");

                entity.ToTable("tb_SystemSetting");

                entity.Property(e => e.SettingKey).HasMaxLength(50);

                entity.Property(e => e.SettingDescription).HasMaxLength(4000);

                entity.Property(e => e.SettingValue).HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
