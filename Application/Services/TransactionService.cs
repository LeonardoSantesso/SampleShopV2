using Application.Events;

namespace Application.Services;

public class TransactionService: ITransactionService
{
    public event EventHandler<TransactionProcessedEventArgs> OnTransactionProcessed;

    public bool MakeDeposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be larger than 0.");

        ProcessDeposit(amount);

        OnTransactionProcessed?.Invoke(this,
            new TransactionProcessedEventArgs(amount, TransactionType.Deposit)
        );

        return true;
    }

    public bool MakeWithdrawal(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be larger than 0.");

        ProcessWithdrawal(amount);

        OnTransactionProcessed?.Invoke(this,
            new TransactionProcessedEventArgs(amount, TransactionType.Withdrawal)
        );

        return true;
    }

    private void ProcessDeposit(decimal amount)
    {
        // Processing logic not necessary for exam
    }

    private void ProcessWithdrawal(decimal amount)
    {
        // Processing logic not necessary for exam
    }
}
