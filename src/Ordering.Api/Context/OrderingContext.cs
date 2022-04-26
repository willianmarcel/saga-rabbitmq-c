using Microsoft.EntityFrameworkCore;
using Ordering.Api.Model;

namespace Ordering.Api.Context;

public class OrderingContext : DbContext
{
    public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
    {

    }

    public DbSet<OrderItem>? OrderItems { get; set; }
}
