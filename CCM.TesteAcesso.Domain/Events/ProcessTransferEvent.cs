namespace CCM.TesteAcesso.Domain.Events
{
    public record ProcessTransferEvent
    {
        public ProcessTransferEvent(long transferId)
        {
            TransferId = transferId;
        }
        public long TransferId { get; set; }
    }
}
