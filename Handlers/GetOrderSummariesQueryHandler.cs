
using Microsoft.EntityFrameworkCore;

public class GetOrderSummariesQueryHandler : IQueryHandler<GetOrderSummariesQuery, List<OrderSummaryDto>>
{
    public readonly ReadDbContext _context;

    public GetOrderSummariesQueryHandler(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderSummaryDto>?> HandleAsync(GetOrderSummariesQuery query)
    {
        return await _context.Orders
            .Select(o => new OrderSummaryDto{
                OrderId = o.Id,
                CustomerName = o.FirstName + " " + o.LastName,
                Status = o.Status,
                TotalCost = o.TotalCost
            }).ToListAsync();
    }
}