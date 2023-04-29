using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MailApp
{
    public class CustomMailModel
    {
        [Required]
        [JsonPropertyName("tos")]
        public List<String> Tos { get; set; } = new List<String>();
        [Required]
        [JsonPropertyName("subject")]
        public String Subject { get; set; } = String.Empty;
        [Required]
        [JsonPropertyName("body")]
        public String Body { get; set; } = String.Empty;
        [JsonPropertyName("is_body_html")]
        public bool IsBodyHtml { get; set; }

        public static implicit operator MailModel(CustomMailModel customMailModel)
        {
            return new MailModel
            {
                Body = customMailModel.Body,
                Subject = customMailModel.Subject,
                IsBodyHtml = customMailModel.IsBodyHtml,
                Tos = customMailModel.Tos,
            };
        }
    }
}