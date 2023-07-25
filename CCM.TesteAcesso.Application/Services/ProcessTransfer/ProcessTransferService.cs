using CCM.TesteAcesso.Domain.Entities;
using CCM.TesteAcesso.Domain.Enums;
using CCM.TesteAcesso.Domain.Extensions;
using CCM.TesteAcesso.Domain.Interfaces.Repository;
using CCM.TesteAcesso.Infra.Clients.Account;
using CCM.TesteAcesso.Infra.Clients.Account.Requests;
using MassTransit;
using Microsoft.Extensions.Logging;
using MiraeDigital.InvestmentFunds.Infrastructure.Http;

namespace CCM.TesteAcesso.Application.Services.ProcessTransfer
{
    public sealed class ProcessTransferService : IProcessTransferService
    {
        private readonly ILogger<ProcessTransferService> _logger;
        private readonly ITransferRepository _tranferRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly IAccountClient _accountClient;
        private readonly IRefitHttpWrapper _refitWrapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private Transfer? _transfer;
        public ProcessTransferService(
            ITransferRepository tranferRepository,
            IOperationRepository operationRepository,
            IAccountClient accountClient,
            IRefitHttpWrapper refitWrapper,
            IPublishEndpoint publishEndpoint,
            ILogger<ProcessTransferService> logger)
        {
            _tranferRepository = tranferRepository;
            _operationRepository = operationRepository;
            _accountClient = accountClient;
            _refitWrapper = refitWrapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _refitWrapper.UseRetryPolicy(Constants.CountRetryPolicy);
        }

        /// <inheritdoc />
        public async Task<bool> Execute(long transferId)
        {
            _transfer = await _tranferRepository.Get(transferId);
            if (_transfer == null)
            {
                return true;
            }

            var debitOperation = await ExecuteOperation(_transfer.Debit);
            if (!debitOperation) return false;

            var creditOperation = await ExecuteOperation(_transfer.Credit);
            if (!creditOperation) return false;

            _transfer.SetStatus(StatusEnum.Confirmed);
            return true;
        }

        private async Task<bool> ExecuteOperation(Operation operation)
        {
            if (operation.Executed)
                return true;

            AccountRequest request = new()
            {
                AccountNumber = operation.AccountDestination,
                Type = operation.OperationType.ToString(),
                Value = operation.Value
            };

            var operationResult = await _refitWrapper.Execute("IAccountClient.Post",
                () => _accountClient.Post(request));

            if (operationResult.HasError())
                return false;

            operation.SetExecuted();
            await _operationRepository.Update(operation);

            return !operationResult.HasError();
        }
    }
}
