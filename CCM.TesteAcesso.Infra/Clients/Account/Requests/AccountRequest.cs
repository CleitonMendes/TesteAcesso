namespace CCM.TesteAcesso.Infra.Clients.Account.Requests
{
    public record AccountRequest
    {
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
    }
}
