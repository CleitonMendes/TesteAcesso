using CCM.TesteAcesso.Infra.Clients.Account.Requests;
using CCM.TesteAcesso.Infra.Clients.Account.Responses;
using Refit;

namespace CCM.TesteAcesso.Infra.Clients.Account
{
    public interface IAccountClient
    {
        [Post(@"​/Account")]
        Task<AccountPostResponse> Post([Body] AccountRequest request);

        [Get(@"/Account/{accountNumber}")]
        Task<AccountResponse> GetByAccount(string accountNumber);

        [Get(@"​​/Account")]
        Task<List<AccountResponse>> Get();
    }
}
