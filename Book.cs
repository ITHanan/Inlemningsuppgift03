using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlemningsuppgift03
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public Author Author { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public int ISBNCode { get; set; }
        //public int Ratings { get; set;  }

        public List<int> Ratings { get; set; } = new List<int>();

        public double BooksAveragerating 
        { 
            get 
            {
                if (Ratings.Count > 0) 
                { 
                    return Ratings.Average(); 
                } 
                else
                { 
                    return 0.0;
                } 
            } 
        }


        public void AddRating(int rating)
        {

            if (rating >= 1 && rating <= 5)
            {
                Ratings.Add(rating);
                Console.WriteLine($"Rating {rating} has been added to this book '{BookTitle}' ");
            }

            else
            { 
                Console.WriteLine("Your rating must be between 1-5");
            }

        }

        //  public List <Book> books = new List<Book>();    




        public Book(int bookId, string bookTitle, string authorName, string genre, int publishedYear, int iSBNCode, List<int> ratings)
        {
            BookId = bookId;
            BookTitle = bookTitle;
            AuthorName = authorName;
            Genre = genre;
            PublishedYear = publishedYear;
            ISBNCode = iSBNCode;
            Ratings = ratings;
        }
    }
}
