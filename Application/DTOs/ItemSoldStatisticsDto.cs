namespace Application.DTOs;

public class ItemSoldStatisticsDto
{
    public int OrderId { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }
}

