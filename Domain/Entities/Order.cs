using Domain.Entities.Base;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public string CustomerName { get; private set; }
    public string CustomerAddress { get; private set; }
    public DateTime CreateDate { get; private set; }
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    public Order(string customerName, string customerAddress)
    {
        CustomerName = customerName;
        CustomerAddress = customerAddress;
        CreateDate = DateTime.UtcNow;
    }

    public Order(string customerName, string customerAddress, DateTime createDate)
    {
        CustomerName = customerName;
        CustomerAddress = customerAddress;
        CreateDate = createDate;
    }

    public void AddOrderItem(string name, string brand, decimal price, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");

        var newItem = new OrderItem(name, brand, price, quantity);
        OrderItems.Add(newItem);
    }

    public void RemoveOrderItem(int itemId)
    {
        var item = OrderItems.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new ArgumentException("Item not found.");

        OrderItems.Remove(item);
    }

    public void UpdateOrderItem(int itemId, int newQuantity)
    {
        var item = OrderItems.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new ArgumentException("Item not found.");

        item.UpdateQuantity(newQuantity);
    }

    public decimal GetTotalPrice()
    {
        return OrderItems.Sum(item => item.Price * item.Quantity);
    }
}

