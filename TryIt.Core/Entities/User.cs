using System.Text.Json.Serialization;
using TryIt.SharedKernel.Interfaces;

namespace TryIt.Core.Entities
{
    public class User : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
