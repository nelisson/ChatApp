using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Client.HubConnectionClient
{
    public class HubConnectionFactory : IHubConnectionFactory
    {
        public async Task<HubConnection> CreateConnectionAsync(string url, string accessToken)
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(accessToken);
                })
                .Build();

            await hubConnection.StartAsync();
            return hubConnection;
        }
    }

}
