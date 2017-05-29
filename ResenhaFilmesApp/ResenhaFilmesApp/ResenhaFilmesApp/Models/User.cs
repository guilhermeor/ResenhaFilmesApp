using Newtonsoft.Json;

namespace ResenhaFilmesApp.Models
{
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
