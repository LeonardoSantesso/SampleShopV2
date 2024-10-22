namespace Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public decimal TotalPrice { get; set; }
}
