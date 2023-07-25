namespace CCM.TesteAcesso.Application.Services.ProcessTransfer
{
    public interface IProcessTransferService
    {
        Task<bool> Execute(long transferId);
    }
}
