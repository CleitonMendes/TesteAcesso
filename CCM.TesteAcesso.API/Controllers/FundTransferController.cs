using CCM.TesteAcesso.Application.Input;
using CCM.TesteAcesso.Application.Services.CreateTransfer;
using CCM.TesteAcesso.Application.Services.GetTransfer;
using Microsoft.AspNetCore.Mvc;

namespace CCM.TesteAcesso.API.Controllers
{
    [ApiController]
    [Route("api/fund-transfer")]
    public class FundTransferController : ControllerBase
    {

        private readonly ILogger<FundTransferController> _logger;
        private readonly ICreateTransferService _createTransferService;
        private readonly IGetTransferService _getTransferService;

        public FundTransferController(ILogger<FundTransferController> logger,
            ICreateTransferService createTransferService,
            IGetTransferService getTransferService)
        {
            _logger = logger;
            _createTransferService = createTransferService;
            _getTransferService = getTransferService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransferInput request)
        {
            var transactionGenerate = await _createTransferService.Execute(request);
            if (transactionGenerate != null)
            {
                return Accepted(new { transaction = Guid.NewGuid() });
            }

            return BadRequest();
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> Get([FromRoute] Guid transactionId)
        {
            var transaction = await _getTransferService.Execute(transactionId);
            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }
    }
}