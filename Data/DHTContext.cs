using DHTRespository.Models;
using Microsoft.EntityFrameworkCore;
namespace DHTRespository.Data;

public class DHTContext : DbContext
{
    public DHTContext(DbContextOptions<DHTContext> options) : base(options) { }

    public DbSet<InfoHash> InfoHashes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InfoHash>().ToTable("infohash");
    }
}
