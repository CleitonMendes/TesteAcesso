using MassTransit;

namespace CCM.TesteAcesso.Application.Consumers.ValidateTransfer
{
    public class ValidateTransferConsumerDefinition : ConsumerDefinition<ValidateTransferConsumer>
    {
        public ValidateTransferConsumerDefinition()
        {
            Endpoint(x =>
                  x.ConcurrentMessageLimit = 1);

        }

    }
}