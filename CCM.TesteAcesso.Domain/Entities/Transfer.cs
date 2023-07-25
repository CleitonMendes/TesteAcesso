using CCM.TesteAcesso.Domain.Enums;

namespace CCM.TesteAcesso.Domain.Entities
{
    public class Transfer : Entity
    {
        /// <inheritdoc />
        public Transfer(string accountOrigin, string accountDestination, decimal value)
        {
            AccountOrigin = accountOrigin;
            AccountDestination = accountDestination;
            Value = value;
            Status = StatusEnum.Queued;
            TransactionUid = Guid.NewGuid();
            Operations = new List<Operation>
            {
                Operation.CreateCredit(accountDestination,value),
                Operation.CreateDebit(accountOrigin,value)
            };
        }

        public string AccountOrigin { get; private set; }
        public string AccountDestination { get; private set; }
        public decimal Value { get; private set; }
        public StatusEnum Status { get; private set; }
        public string? Message { get; private set; }
        public Guid TransactionUid { get; private set; }

        public Operation Debit => Operations.FirstOrDefault(x => x.OperationType == OperationTypeEnum.Debit)!;
        public Operation Credit => Operations.FirstOrDefault(x => x.OperationType == OperationTypeEnum.Credit)!;

        public List<Operation> Operations { get; }

        public void SetError(string message)
        {
            Status = StatusEnum.Error;
            Message = message;
        }
        public void SetStatus(StatusEnum status)
        {
            Status = status;
            SetUpdateDate();
        }

    }
}
