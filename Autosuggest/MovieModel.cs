using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autosuggest
{
    class MovieModel
    {
        [JsonProperty(PropertyName = "adult")]
        public bool Adault { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty(PropertyName = "popularity")]
        public decimal Popularity { get; set; }

        [JsonProperty(PropertyName = "video")]
        public bool Video { get; set; }
    }
}
