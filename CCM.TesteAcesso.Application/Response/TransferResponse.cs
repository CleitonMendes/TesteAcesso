using CCM.TesteAcesso.Domain.Entities;
using CCM.TesteAcesso.Domain.Enums;

namespace CCM.TesteAcesso.Application.Response
{
    public sealed class TransferResponse
    {
        public TransferResponse(Transfer domain)
        {
            Status = domain.Status.ToString();
            Message = domain.Status == StatusEnum.Error ? domain.Message : null;
        }
        public string Status { get; }
        public string? Message { get; }
    }
}
