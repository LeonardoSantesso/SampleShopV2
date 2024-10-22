namespace Application.Services;

    public class AuditService : IAuditService
    {
        private readonly ILogger _logger;

        public AuditService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Subscribes to TransactionService's OnTransactionProcessed and writes to log.
        /// </summary>
        public void Subscribe(ITransactionService transactionService)
        {
            transactionService.OnTransactionProcessed += (sender, args) =>
            {
                _logger.WriteToLog($"AUDIT LOG: {args.TransactionType} for ${args.Amount} processed");
            };
        }
    }

