using CCM.TesteAcesso.Application.Consumers.ProcessTransfer;
using MassTransit;

namespace CCM.TesteAcesso.Application.Consumers.ProcessOperation
{
    public class ProcessTransferConsumerDefinition : ConsumerDefinition<ProcessTransferConsumer>
    {
        public ProcessTransferConsumerDefinition()
        {
            Endpoint(x =>
                  x.ConcurrentMessageLimit = 1);

        }

    }
}