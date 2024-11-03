
using System.Text.Json.Serialization;

namespace Inlemningsuppgift03
{
    public class MinLillaDB
    {
        [JsonPropertyName("Books")]
        public List<Book> AllbooksfromDB { get; set; }

        [JsonPropertyName("Authors")]
        public List<Author> allaAuthorsDatafromDB{ get; set; }
    }
}
