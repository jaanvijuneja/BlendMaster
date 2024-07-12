using Newtonsoft.Json;

namespace WebApplication2.Models
{
    public class RecipeModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ingredients")]
        public List<string> Ingredients { get; set; }

        [JsonProperty("instructions")]
        public List<string> Instructions { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}
