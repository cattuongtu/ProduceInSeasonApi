using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ProduceInSeasonApi.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Produce { get; set; } = null!;
    }
}