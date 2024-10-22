using Models.Base;

namespace Models;

public class Order : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

