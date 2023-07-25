using CCM.TesteAcesso.Application.Services.CreateTransfer;
using CCM.TesteAcesso.Domain.Entities;
using CCM.TesteAcesso.Domain.Enums;
using CCM.TesteAcesso.Domain.Events;
using CCM.TesteAcesso.Domain.Extensions;
using CCM.TesteAcesso.Domain.Interfaces.Repository;
using CCM.TesteAcesso.Infra.Clients.Account;
using CCM.TesteAcesso.Infra.Clients.Account.Responses;
using MassTransit;
using Microsoft.Extensions.Logging;
using MiraeDigital.InvestmentFunds.Infrastructure.Http;
using System.Net;

namespace CCM.TesteAcesso.Application.Services.ValidateTransfer
{
    public sealed class ValidateTransferService : IValidateTransferService
    {
        private readonly ILogger<CreateTransferService> _logger;
        private readonly ITransferRepository _repository;
        private readonly IAccountClient _accountClient;
        private readonly IRefitHttpWrapper _refitWrapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private Transfer? _transfer;
        public ValidateTransferService(ITransferRepository repository,
            IAccountClient accountClient,
            IRefitHttpWrapper refitWrapper,
            IPublishEndpoint publishEndpoint,
            ILogger<CreateTransferService> logger)
        {
            _repository = repository;
            _accountClient = accountClient;
            _refitWrapper = refitWrapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _refitWrapper.UseRetryPolicy(Constants.CountRetryPolicy);
        }

        /// <inheritdoc />
        public async Task<bool> Execute(long transferId)
        {
            _transfer = await _repository.Get(transferId);
            if (_transfer == null)
            {
                return true;
            }

            _transfer.SetStatus(StatusEnum.Processing);
            await _repository.Update(_transfer);

            var debitValidate = await ValidateAccount(_transfer.Debit.AccountDestination);
            if (debitValidate == null) return true;

            var creditValidate = await ValidateAccount(_transfer.Credit.AccountDestination);
            if (creditValidate == null) return true;

            await _publishEndpoint.Publish(new ProcessTransferEvent(_transfer.Id));

            return true;
        }

        private async Task<AccountResponse?> ValidateAccount(string account)
        {
            var accountDetails = await _refitWrapper.Execute("IAccountClient.GetByAccount",
                () => _accountClient.GetByAccount(account));

            if (!accountDetails.HasError()) return accountDetails.ResultData;

            _transfer?.SetError(accountDetails.ErrorCode == HttpStatusCode.BadRequest
                ? accountDetails.Error
                : Messages.AccountInvalid);

            await _repository.Update(_transfer!);

            return null;
        }
    }
}
