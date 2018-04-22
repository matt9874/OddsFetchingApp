using Newtonsoft.Json;

namespace OddsFetchingEntities
{
    public class Entity
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string IdStr { get; set; }

        [JsonProperty(PropertyName = "name")]
        public virtual string Name { get; set; }
    }

}
