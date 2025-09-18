using MediatR;

public record OrderCreatedEvent : INotification
{
    public int OrderId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public decimal TotalCost { get; set; }
};