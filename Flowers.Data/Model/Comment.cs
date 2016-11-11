using System;
using Newtonsoft.Json;

namespace Flowers.Model
{
    public class Comment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("inputdate")]
        public DateTime InputDate { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}