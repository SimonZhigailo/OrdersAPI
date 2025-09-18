using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly WriteDbContext _context;
    private readonly IValidator<CreateOrderCommand> _validator;
    private readonly IMediator _mediator;

    public CreateOrderCommandHandler(WriteDbContext context,
        IValidator<CreateOrderCommand> validator,
        IMediator mediator)
    {
        _context = context;
        _validator = validator;
        _mediator = mediator;
    }


    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = new Order
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Status = request.Status,
            CreatedAt = DateTime.Now,
            TotalCost = request.TotalCost
        };

        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            FirstName = order.FirstName,
            LastName = order.LastName,
            TotalCost = order.TotalCost
        };

        await _mediator.Publish(orderCreatedEvent);

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

}