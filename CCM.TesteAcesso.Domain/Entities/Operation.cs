using CCM.TesteAcesso.Domain.Enums;

namespace CCM.TesteAcesso.Domain.Entities
{
    public class Operation : Entity
    {
        private Operation()
        {

        }
        /// <inheritdoc />
        private Operation(string accountDestination, decimal value, OperationTypeEnum operationTypeEnum)
        {
            AccountDestination = accountDestination;
            Value = value;
            OperationType = operationTypeEnum;
            Executed = false;
            DateExecuted = null;
        }

        public string AccountDestination { get; private set; }
        public OperationTypeEnum OperationType { get; private set; }
        public decimal Value { get; private set; }
        public bool Executed { get; private set; }
        public DateTime? DateExecuted { get; private set; }
        public long TransferId { get; private set; }
        public virtual Transfer Transfer { get; private set; }

        public void SetTranfer(Transfer transfer)
        {
            Transfer = transfer;
            TransferId = transfer.Id;
        }
        public void SetExecuted()
        {
            Executed = true;
            DateExecuted = DateTime.Now;
            SetUpdateDate();
        }
        public static Operation CreateDebit(string accountDestination, decimal value) =>
            new(accountDestination, value, OperationTypeEnum.Debit);
        public static Operation CreateCredit(string accountDestination, decimal value) =>
            new(accountDestination, value, OperationTypeEnum.Credit);
    }
}
