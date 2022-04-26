using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Api.Model;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
