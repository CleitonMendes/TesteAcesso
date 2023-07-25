using CCM.TesteAcesso.Application.Input;
using CCM.TesteAcesso.Domain.Events;
using CCM.TesteAcesso.Domain.Interfaces.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CCM.TesteAcesso.Application.Services.CreateTransfer
{
    public sealed class CreateTransferService : ICreateTransferService
    {
        private readonly ILogger<CreateTransferService> _logger;
        private readonly ITransferRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateTransferService(ITransferRepository repository, IPublishEndpoint publishEndpoint, ILogger<CreateTransferService> logger)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Guid?> Execute(TransferInput request)
        {
            var domain = await _repository.CreateTransfer(request.AccountOrigin, request.AccountDestination, request.Value);
            if (domain == null)
            {
                return null;
            }
            await _publishEndpoint.Publish(new ValidateTransferEvent(domain.Id));
            return domain.TransactionUid;
        }
    }
}
