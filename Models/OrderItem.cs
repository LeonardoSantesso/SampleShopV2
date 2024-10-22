using Models.Base;

namespace Models;

public class OrderItem : BaseEntity
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}

