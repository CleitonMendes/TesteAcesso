using CCM.TesteAcesso.Application.Response;

namespace CCM.TesteAcesso.Application.Services.GetTransfer
{
    public interface IGetTransferService
    {
        Task<TransferResponse?> Execute(Guid transaction);
    }
}
