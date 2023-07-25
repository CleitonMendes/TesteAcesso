namespace CCM.TesteAcesso.Application.Services.ValidateTransfer
{
    public interface IValidateTransferService
    {
        Task<bool> Execute(long transferId);
    }
}
