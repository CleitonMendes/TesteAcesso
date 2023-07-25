using CCM.TesteAcesso.Application.Services.ProcessTransfer;
using CCM.TesteAcesso.Application.Services.ValidateTransfer;
using CCM.TesteAcesso.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CCM.TesteAcesso.Application.Consumers.ProcessTransfer
{
    public class ProcessTransferConsumer : IConsumer<ProcessTransferEvent>
    {
        private readonly IProcessTransferService _processTransferService;
        private readonly ILogger<ProcessTransferConsumer> _logger;

        public ProcessTransferConsumer(IProcessTransferService processTransferService, ILogger<ProcessTransferConsumer> logger)
        {
            _processTransferService = processTransferService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProcessTransferEvent> context)
        {
            try
            {
                _logger.LogInformation("{@ConsumerName} Read message: {@TransactionId}", nameof(ProcessTransferConsumer), context.Message.TransferId);

                var result = await _processTransferService.Execute(context.Message.TransferId);
                _logger.LogInformation("{@ConsumerName} - TransferId: {@transferId} - {@UseCaseName} RESULT : {@seedReport}", nameof(ProcessTransferConsumer), context.Message.TransferId, nameof(IValidateTransferService), result);

                if (!result)
                    await context.Redeliver(TimeSpan.FromSeconds(value: 10));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{@ConsumerName}: Unexpected Exception", nameof(ProcessTransferConsumer));
                await context.Redeliver(TimeSpan.FromSeconds(value: 10));
            }
        }
    }
}