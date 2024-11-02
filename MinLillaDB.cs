using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
