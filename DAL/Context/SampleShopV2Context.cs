using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Context
{
    public class SampleShopV2Context : DbContext
    {
        public SampleShopV2Context(DbContextOptions<SampleShopV2Context> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(e => e.CreateDate).HasColumnName("create_date").IsRequired();
                entity.HasMany(e => e.OrderItems)
                    .WithOne(e => e.Order)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Brand).HasColumnName("brand").HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
                entity.Property(e => e.OrderId).HasColumnName("order_id").IsRequired();
            });
        }
    }

}
