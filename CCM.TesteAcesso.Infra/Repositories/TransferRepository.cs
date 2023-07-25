using CCM.TesteAcesso.Domain.Entities;
using CCM.TesteAcesso.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CCM.TesteAcesso.Infra.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly ILogger<TransferRepository> _logger;
        private readonly Context _context;

        public TransferRepository(ILogger<TransferRepository> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Transfer?> CreateTransfer(string accountOrigin, string accountDestin, decimal value)
        {
            try
            {
                var domain = new Transfer(accountOrigin, accountDestin, value);
                await _context.Transfers.AddAsync(domain);
                await _context.SaveChangesAsync();
                _logger.LogInformation("New transfer successfuly inserted - Id: {id}", domain.Id);
                return domain;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to insert transfer on database");
                return null;
            }
        }

        /// <inheritdoc />
        public Task<Transfer?> GetByTransaction(Guid transaction)
        {
            return _context.Transfers.Include(x => x.Operations).FirstOrDefaultAsync(x => x.TransactionUid == transaction);
        }

        /// <inheritdoc />
        public Task<Transfer?> Get(long id)
        {
            return _context.Transfers.Include(x => x.Operations).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public Task Update(Transfer domain)
        {
            try
            {
                domain.SetUpdateDate();
                _context.Transfers.Update(domain);
                _context.SaveChangesAsync();
                _logger.LogInformation("Transfer successfuly Updated - Id: {id}", domain.Id);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to update Transfer on database. - Id: {id}", domain?.Id);
                return Task.FromException(ex);
            }
        }
    }
}
