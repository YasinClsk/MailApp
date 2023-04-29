using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MailApp
{
    public class MailModel
    { 
        public List<String> Tos { get; set; } = new List<String>();

        public String Subject { get; set; } = String.Empty;

        public String Body { get; set; } = String.Empty;

        public bool IsBodyHtml { get; set; }
    }
}
