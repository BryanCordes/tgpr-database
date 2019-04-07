using Newtonsoft.Json;

namespace TGPR.Database.Common.Models.Authenitcation
{
    public class JwtResponseModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "auth_token")]
        public string AuthToken { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expiry")]
        public long Expiry { get; set; }
    }
}
