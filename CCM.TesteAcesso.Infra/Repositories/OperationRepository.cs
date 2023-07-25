using CCM.TesteAcesso.Domain.Entities;
using CCM.TesteAcesso.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CCM.TesteAcesso.Infra.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ILogger<OperationRepository> _logger;
        private readonly Context _context;

        public OperationRepository(ILogger<OperationRepository> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public ValueTask<Operation?> Get(long id)
        {
            return _context.Operations.FindAsync(id);
        }

        /// <inheritdoc />
        public Task<List<Operation>> GetByTransactionId(long transactionId)
        {
            return _context.Operations.Where(x => x.TransferId == transactionId).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Operation?> Update(Operation domain)
        {
            try
            {
                domain.SetUpdateDate();
                _context.Operations.Update(domain);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Operation successfuly Updated - Id: {id}", domain.Id);
                return domain;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to update Operation on database. - Id: {id}", domain?.Id);
                return null;
            }
        }
    }
}
