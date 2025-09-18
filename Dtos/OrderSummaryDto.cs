public record OrderSummaryDto
{
    public int OrderId { get; set; }
    
    public required string CustomerName { get; set; }

    public required string Status { get; set; }

    public Decimal TotalCost { get; set; } 
    
}