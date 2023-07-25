using CCM.TesteAcesso.Application.Services.ValidateTransfer;
using CCM.TesteAcesso.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CCM.TesteAcesso.Application.Consumers.ValidateTransfer
{
    public class ValidateTransferConsumer : IConsumer<ValidateTransferEvent>
    {
        private readonly IValidateTransferService _validateTransferService;
        private readonly ILogger<ValidateTransferConsumer> _logger;

        public ValidateTransferConsumer(IValidateTransferService validateTransferService, ILogger<ValidateTransferConsumer> logger)
        {
            _validateTransferService = validateTransferService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ValidateTransferEvent> context)
        {
            try
            {
                _logger.LogInformation("{@ConsumerName} Read message: {@TransactionId}", nameof(ValidateTransferConsumer), context.Message.TransferId);

                var result = await _validateTransferService.Execute(context.Message.TransferId);

                _logger.LogInformation("{@ConsumerName} - TransferId: {@transferId} - {@UseCaseName} RESULT : {@seedReport}", nameof(ValidateTransferConsumer), context.Message.TransferId, nameof(IValidateTransferService), result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{@ConsumerName}: Unexpected Exception", nameof(ValidateTransferConsumer));
                await context.Redeliver(TimeSpan.FromSeconds(value: 10));
            }
        }
    }
}