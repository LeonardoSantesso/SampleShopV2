using Domain.Entities.Base;

namespace Domain.Entities;

public class OrderItem : BaseEntity
{
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public int OrderId { get; private set; }
    public Order Order { get; set; }

    protected OrderItem() { }

    public OrderItem(string name, string brand, decimal price, int quantity)
    {
        Name = name;
        Brand = brand;
        Price = price;
        Quantity = quantity;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        Quantity = newQuantity;
    }
}

