using Application.Events;

namespace Application.Services;

public interface ITransactionService
{
    event EventHandler<TransactionProcessedEventArgs> OnTransactionProcessed;
    bool MakeDeposit(decimal amount);
    bool MakeWithdrawal(decimal amount);
}
