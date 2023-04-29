using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MailApp
{
    public class DiscountMailModel
    {
        [Required]
        [JsonPropertyName("tos")]
        public List<String> Tos { get; set; } = new List<String>();

    }
}
