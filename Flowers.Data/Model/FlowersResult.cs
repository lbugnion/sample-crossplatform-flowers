using System.Collections.Generic;
using Newtonsoft.Json;

namespace Flowers.Model
{
    public class FlowersResult
    {
        [JsonProperty("data")]
        public IList<Flower> Data
        {
            get;
            set;
        }
    }
}