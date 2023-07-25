using CCM.TesteAcesso.Domain.Entities;

namespace CCM.TesteAcesso.Domain.Interfaces.Repository
{
    public interface IOperationRepository
    {
        ValueTask<Operation?> Get(long id);
        Task<List<Operation>> GetByTransactionId(long transactionId);
        Task<Operation?> Update(Operation domain);
    }
}
