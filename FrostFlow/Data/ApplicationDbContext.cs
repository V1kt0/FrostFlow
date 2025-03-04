using FrostFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrostFlow.Data
{
  public class ApplicationDbContext : IdentityDbContext<IdentityUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        // Replace this with your actual database connection string
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
            b => b.MigrationsAssembly("FrostFlow"));
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure the Customer and Order relationship
      modelBuilder.Entity<Order>()
          .HasOne(o => o.Customer) // An Order belongs to a Customer
          .WithMany() // A Customer can have many Orders
          .HasForeignKey(o => o.CustomerId) // Foreign key on Order
          .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the Customer is deleted

      // Configure the Order and AirConditioner relationship
      modelBuilder.Entity<Order>()
          .HasOne(o => o.AirConditioner) // An Order belongs to an AirConditioner
          .WithMany() // An AirConditioner can have many Orders
          .HasForeignKey(o => o.AirConditionerId) // Foreign key on Order
          .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the AirConditioner is deleted

      // Configure the Payment and Order relationship
      modelBuilder.Entity<Payment>()
          .HasOne(p => p.Order) // A Payment belongs to an Order
          .WithMany() // An Order can have many Payments (if you want to track multiple payments for an order)
          .HasForeignKey(p => p.OrderId) // Foreign key on Payment
          .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the Order is deleted

      // Set up unique constraints, defaults, etc.
      modelBuilder.Entity<Customer>()
          .HasIndex(c => c.Email)
          .IsUnique(); // Ensure emails are unique in the Customer table

      modelBuilder.Entity<AirConditioner>()
          .HasIndex(ac => ac.ModelName)
          .IsUnique(); // Ensure air conditioner models are unique

      modelBuilder.Entity<AirConditioner>()
          .Property(ac => ac.Type)
          .IsRequired(); // Make the 'Type' property required

      modelBuilder.Entity<Order>()
          .HasIndex(o => o.OrderDate); // Optional: Index on OrderDate for better performance
    }

    // Add DbSet properties for each entity
    public DbSet<AirConditioner> AirConditioners { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Room> Rooms { get; set; }  // Add this line for the Room model
    public DbSet<User> Users { get; set; }

  }
}
