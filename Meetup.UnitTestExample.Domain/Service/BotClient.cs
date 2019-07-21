using System;
using System.Net.Http;

namespace Meetup.UnitTestExample.Domain.Service
{
    public class BotClient
    {
        private const string BaseUrl = "https://api.telegram.org/bot";
        private readonly HttpClient _httpClient;
        private readonly string _token;
        private readonly string _baseRequestUrl;
        public int BotId { get; }

        public BotClient(string token, HttpClient httpClient = null)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            string[] parts = _token.Split(':');
            if (parts.Length > 1 && int.TryParse(parts[0], out int id))
            {
                BotId = id;
            }
            else
            {
                throw new ArgumentException(
                    "Invalid format. A valid token looks like \"1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy\".",
                    nameof(token)
                );
            }

            _baseRequestUrl = $"{BaseUrl}{_token}/";
            _httpClient = httpClient ?? new HttpClient();
        }
    }
}
