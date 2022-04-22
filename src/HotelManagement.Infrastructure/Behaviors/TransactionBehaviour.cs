using HotelManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Infrastructure.Behaviors;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
    private readonly HotelManagementContext _dbContext;

    public TransactionBehaviour(
        HotelManagementContext dbContext,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger
        )
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(HotelManagementContext));
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        TResponse response = default;

        try
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                await _dbContext.BeginTransactionAsync();

                response = await next();

                await _dbContext.CommitTransactionAsync();

                _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
            });

            return response;
        }
        catch (Exception)
        {
            _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

            _dbContext.RollbackTransaction();
            throw;
        }
    }
}
