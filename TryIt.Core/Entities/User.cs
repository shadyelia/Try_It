using System.Text.Json.Serialization;

namespace TryIt.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
