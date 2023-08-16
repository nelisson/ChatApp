using ChatApp.Server.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ChatApp.Server.Services
{
    public class BotService : IBotService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BotService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> ProcessStockCommand(string stockCode)
        {
            var stockData = await FetchStockDataAsync(stockCode);
            if (stockData == null)
            {
                return "Error fetching stock data.";
            }

            var latestRecord = stockData.First();
            var formattedMessage = FormatStockMessage(latestRecord);

            return formattedMessage;
        }

        private async Task<List<StockRecord>?> FetchStockDataAsync(string stockCode)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            List<StockRecord> stockRecords;

            try
            {
                stockRecords = await csv.GetRecordsAsync<StockRecord>().ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
            
            return stockRecords;
        }

        private static string FormatStockMessage(StockRecord stockRecord)
        {
            return $"{stockRecord.Symbol} quote is ${stockRecord.Close:0.00} per share.";
        }

        public void SendMessageToBroker(string stockMessage, int chatroomId)
        {
            string uri = "amqps://hvfpkgbx:FmqZDeK0YfU1vmIXEjy1aOgrgAM056Vb@jackal.rmq.cloudamqp.com/hvfpkgbx";
            string queue = "stockMessages";

            using IRabbitMqService rabbitMqService = new RabbitMqService(uri, queue);
            rabbitMqService.SendMessage($"{chatroomId}@@@{stockMessage}", queue);
        }
    }
}
