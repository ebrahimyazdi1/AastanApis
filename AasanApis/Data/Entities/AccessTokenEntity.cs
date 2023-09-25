using System.Text.Json;

namespace AasanApis.Data.Entities
{
    public sealed class AccessTokenEntity
    {
        public string Id { get; set; }
        public DateTime TokenDateTime { get; set; }
        public string AccessToken { get; set; }
        public string TokenName { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
