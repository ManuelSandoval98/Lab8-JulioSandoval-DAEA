
using Microsoft.EntityFrameworkCore;

namespace Lab8_JulioSandoval.Models;

public partial class LinqExampleContext : DbContext
{
    public LinqExampleContext()
    {
    }

    public LinqExampleContext(DbContextOptions<LinqExampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<client> clients { get; set; }

    public virtual DbSet<order> orders { get; set; }

    public virtual DbSet<product> products { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=linqexample;Username=postgres;Password=manuel1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<client>(entity =>
        {
            entity.HasKey(e => e.clientid).HasName("clients_pkey");

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.name).HasMaxLength(100);
        });

        modelBuilder.Entity<order>(entity =>
        {
            entity.HasKey(e => e.orderid).HasName("orders_pkey");

            entity.Property(e => e.orderdate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.client).WithMany(p => p.orders)
                .HasForeignKey(d => d.clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_clientid_fkey");

            entity.HasOne(d => d.product).WithMany(p => p.orders)
                .HasForeignKey(d => d.productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_productid_fkey");
        });

        modelBuilder.Entity<product>(entity =>
        {
            entity.HasKey(e => e.productid).HasName("products_pkey");

            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.price).HasPrecision(10, 2);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
