using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.HubConnectionClient
{
    public interface IHubConnectionFactory
    {
        Task<HubConnection> CreateConnectionAsync(string url, string accessToken);
    }

}
