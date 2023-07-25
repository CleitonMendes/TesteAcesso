using CCM.TesteAcesso.Application.Response;
using CCM.TesteAcesso.Application.Services.CreateTransfer;
using CCM.TesteAcesso.Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;

namespace CCM.TesteAcesso.Application.Services.GetTransfer
{
    public sealed class GetTransferService : IGetTransferService
    {
        private readonly ILogger<CreateTransferService> _logger;
        private readonly ITransferRepository _repository;

        public GetTransferService(ITransferRepository repository, ILogger<CreateTransferService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TransferResponse?> Execute(Guid transaction)
        {
            var domain = await _repository.GetByTransaction(transaction);
            if (domain == null)
                return null;

            return new TransferResponse(domain);
        }
    }
}
