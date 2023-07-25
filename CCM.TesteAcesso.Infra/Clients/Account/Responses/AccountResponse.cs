namespace CCM.TesteAcesso.Infra.Clients.Account.Responses
{
    public record AccountResponse
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
