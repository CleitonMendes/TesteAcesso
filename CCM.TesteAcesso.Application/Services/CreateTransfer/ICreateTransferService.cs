using CCM.TesteAcesso.Application.Input;

namespace CCM.TesteAcesso.Application.Services.CreateTransfer
{
    public interface ICreateTransferService
    {
        Task<Guid?> Execute(TransferInput request);
    }
}
