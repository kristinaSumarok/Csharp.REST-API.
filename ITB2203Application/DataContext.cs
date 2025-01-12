using ITB2203Application.Models;
using Microsoft.EntityFrameworkCore;

namespace ITB22003Application;

public class DataContext(DbContextOptions options) : DbContext(options) {
    
    public DbSet<Courier> Couriers { get; set; } = default!;
    public DbSet<Restaurant> Restaurants { get; set; }= default!;
     public DbSet<Order> Orders { get; set; }= default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }
}
