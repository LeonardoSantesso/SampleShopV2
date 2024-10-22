using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace SampleShopV2;

public class TransactionFunction
{
    private readonly ITransactionService _transactionService;

    public TransactionFunction(ITransactionService transactionService, IAuditService auditService)
    {
        _transactionService = transactionService;
        auditService.Subscribe(_transactionService);
    }

    [FunctionName("MakeDeposit")]
    public async Task<IActionResult> MakeDeposit(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "transaction/deposit")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var transactionRequest = JsonSerializer.Deserialize<TransactionRequest>(requestBody);

        if (transactionRequest == null || transactionRequest.Amount <= 0)
        {
            return new BadRequestObjectResult("Invalid amount.");
        }

        var result = _transactionService.MakeDeposit(transactionRequest.Amount);

        return result ? new OkResult() : new BadRequestObjectResult("Failed to make deposit.");
    }

    [FunctionName("MakeWithdrawal")]
    public async Task<IActionResult> MakeWithdrawal(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "transaction/withdrawal")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var transactionRequest = JsonSerializer.Deserialize<TransactionRequest>(requestBody);

        if (transactionRequest == null || transactionRequest.Amount <= 0)
        {
            return new BadRequestObjectResult("Invalid amount.");
        }

        var result = _transactionService.MakeWithdrawal(transactionRequest.Amount);

        return result ? new OkResult() : new BadRequestObjectResult("Failed to make withdrawal.");
    }
}

public class TransactionRequest
{
    public decimal Amount { get; set; }
}