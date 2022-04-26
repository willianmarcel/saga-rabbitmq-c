using Catalog.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Context;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {

    }

    public DbSet<CatalogItem>? CatalogItems { get; set; }
}
