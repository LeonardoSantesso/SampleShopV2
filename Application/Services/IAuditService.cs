namespace Application.Services;

public interface IAuditService
{
    void Subscribe(ITransactionService transactionService);
}
