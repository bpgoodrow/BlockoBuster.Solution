using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlockBuster.Models
{
  public class BlockBusterContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<GenreMovie> GenreMovie { get; set; }
    public DbSet<Copy> Copies { get; set; }
    public DbSet<CheckOut> CheckOuts { get; set; }
    public DbSet<Patron> Patrons { get; set; }

    public BlockBusterContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}