namespace CCM.TesteAcesso.Domain.Events
{
    public record ValidateTransferEvent
    {
        public ValidateTransferEvent(long transferId)
        {
            TransferId = transferId;
        }
        public long TransferId { get; set; }
    }
}
