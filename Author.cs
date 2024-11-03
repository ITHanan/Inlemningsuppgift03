

namespace Inlemningsuppgift03
{
    public class Author
    {
        public int AuthorId { get; set; } 
        public string AuthorName { get; set; }
        public string AuthorsCountry { get; set; }

       public List<int> booksIsWritten { get; set; } = new List<int>();

        public Author(int authorId, string authorName, string authorsCountry) 
        { 
         AuthorId = authorId;
         AuthorName = authorName;
         AuthorsCountry = authorsCountry;
         
        }

    }
}
