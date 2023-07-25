using CCM.TesteAcesso.Domain.Entities;

namespace CCM.TesteAcesso.Domain.Interfaces.Repository
{
    public interface ITransferRepository
    {
        Task Update(Transfer domain);

        Task<Transfer?> CreateTransfer(string accountOrigin, string accountDestin, decimal value);
        Task<Transfer?> GetByTransaction(Guid transaction);
        Task<Transfer?> Get(long id);
    }
}
