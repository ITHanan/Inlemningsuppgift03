
using System.Text.Json.Serialization;

namespace Inlemningsuppgift03
{
    public class MinLillaDB
    {
        [JsonPropertyName("Books")]
        public List<Book> AllbooksfromDB { get; set; } = new List<Book>();

        [JsonPropertyName("Authors")]
        public List<Author> allaAuthorsDatafromDB{ get; set; } = new List<Author>();
    }
}
