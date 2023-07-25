using System.ComponentModel.DataAnnotations;

namespace CCM.TesteAcesso.Application.Input
{
    public record TransferInput
    {
        [Required]
        public string AccountOrigin { get; set; } = null!;
        [Required]
        public string AccountDestination { get; set; } = null!;
        public decimal Value { get; set; }
    }
}
