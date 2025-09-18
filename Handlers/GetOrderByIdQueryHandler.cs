using Microsoft.EntityFrameworkCore;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly ReadDbContext _context;

    public GetOrderByIdQueryHandler(ReadDbContext context)
    {
        _context = context;
    }
    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == query.OrderId);

        if (order == null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            FirstName = order.FirstName,
            LastName = order.LastName,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            TotalCost = order.TotalCost
        };
    }

    // public static async Task<Order?> Handle(GetOrderByIdQuery query, AppDbContext context)
    // {
    //     return await context.Orders.FirstOrDefaultAsync(o => o.Id == query.OrderId);
    // }

}