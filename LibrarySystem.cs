using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inlemningsuppgift03
{


    public class LibrarySystem
    {
        MinLillaDB minLillaDB = new MinLillaDB();

        public List<Book>books = new List<Book>();
        public List<Author> authors = new List<Author>();
        public string dataJsonFilePath = "LibraryData.json";

        public LibrarySystem() 
        {

            DataLoading(books,authors);
        
        }


        public void Addnewbook(List<Book>books, List<Author>authors) 
        {
            Console.WriteLine("------Please, enter all book's details------\n");
            Console.WriteLine("Enter book title:");
            string newBookTitle = Console.ReadLine()!;

            Console.WriteLine("Enter author name:");
            string newAuthorName = Console.ReadLine()!;

            Console.WriteLine("Enter genre:");
            string newGenre = Console.ReadLine()!;

            Console.WriteLine("Enter Published Year:");
            int newPublishedYear = Convert.ToInt32( Console.ReadLine())!;

            Console.WriteLine("Enter ISBN Code:");
            int newISBNCode = Convert.ToInt32(Console.ReadLine())!;



            var author = authors.FirstOrDefault(author => author.AuthorName.Equals(newAuthorName, StringComparison.OrdinalIgnoreCase));

            if (newAuthorName == null) 
            {

                Console.WriteLine("The author not found. Add the Author first ");
                return;
            
            }
            Book newBook = new(books.Count + 1, newBookTitle, newAuthorName, newGenre, newPublishedYear, newISBNCode, [4])
            {

                BookId = books.Count+1,
                BookTitle = newBookTitle,  
                AuthorName = newAuthorName,
                Genre = newGenre,
                PublishedYear = newPublishedYear,
                ISBNCode = newISBNCode

            };

            books.Add(newBook);
            // author.booksIsWritten.Add(newBook.BookId);
            MirrorChangesToProjectRoot("LibraryData.json");
            SaveAllDataAndExit();
            Console.WriteLine($"The book {newBookTitle} added successfully.");

        }

        public void AddnewAuthor(List<Author>authors) 
        
       {

            Console.WriteLine("------Please, enter all author's details------\n");

            Console.WriteLine("Enter Author's name:");
            string newAuthorName = Console.ReadLine()!;

            Console.WriteLine("Enter Author's nationality:");
            string newAuthorsCountry = Console.ReadLine()!;

            Author newAuthor = new (authors.Count + 1, newAuthorName, newAuthorsCountry)
            {
             AuthorId = authors.Count + 1,
             AuthorName = newAuthorName,
             AuthorsCountry = newAuthorsCountry
            };

            authors.Add(newAuthor);
            MirrorChangesToProjectRoot("LibraryData.json");
            SaveAllDataAndExit();
            Console.WriteLine("Auther added successfully");
        
    
        }

        public void UpdateBookDetails(List<Book> books, List<Author> authors)
        {
            int bookUpdateID = int .Parse(Prompt("Enter the book Id that you want to Update:"));

            var book = books.FirstOrDefault(book => book.BookId == bookUpdateID);

            if (book == null) 
            {
                Console.WriteLine("Book not found");
                return;
            }
              book.BookTitle = Prompt($"Enter new title of {book.BookTitle}:",book.BookTitle);
              book.Genre = Prompt($"Enter new genre of {book.Genre}:", book.Genre);
              book.PublishedYear = int.Parse(Prompt($"Enter new publication year of the current {book.PublishedYear}:", book.PublishedYear.ToString()));
            SaveAllDataAndExit();
             Console.WriteLine("Book details updated");
        }
        public void UpdateAuthorDetails(List<Author> authors) 
        {
            int updatedAuthorID = int.Parse(Prompt("Enter the author Id number that you want to update:"));
            var author = authors.FirstOrDefault(author => author.AuthorId == updatedAuthorID);
            if (author == null)
            {
                Console.WriteLine("Author not found");
                return;
            }
             author.AuthorName =Prompt($"Enter new Author corrent name is: {author.AuthorName}:",author.AuthorName);
             author.AuthorsCountry = Prompt($"Enter new country corrent country is {author.AuthorsCountry}", author.AuthorsCountry);
            MirrorChangesToProjectRoot("LibraryData.json");
            SaveAllDataAndExit();
             Console.WriteLine("Author has been updated.");
        
        }



        public void DeleteBook(List<Book>books) 
        {
            int bookIDtoDelete = int.Parse(Prompt("Enter the book ID that you want to delete:"));
            DataLoading(books, authors);

            var book = books.FirstOrDefault(book => book.BookId == bookIDtoDelete);
            if (book == null) 
            {
                Console.WriteLine("The book is not found.");
                return;
            }

            books.Remove(book);
            var author = authors.FirstOrDefault(author => author.AuthorName == book.AuthorName);
            if (author == null) 
            {
              
                author?.booksIsWritten.Remove(bookIDtoDelete);
                MirrorChangesToProjectRoot("LibraryData.json");
                SaveAllDataAndExit();
                Console.WriteLine("Book has been deleted.");
            
            }


        }
        public void DeleteAuthor(List<Author>authors) 
        {
            int authorIdToDelet = int.Parse(Prompt("Enter the author Id to delete:"));
            var author = authors.FirstOrDefault(author=>author.AuthorId == authorIdToDelet);
            if (author == null) 
            {

                Console.WriteLine("Author not found.");
                return;

            }
            authors.Remove(author);
            books.RemoveAll(book => book.AuthorName == author.AuthorName);
            MirrorChangesToProjectRoot("LibraryData.json");
            SaveAllDataAndExit();
            Console.WriteLine("Author and their books has been deleted.");

        }

        public void ListAll(List<Book>books,List<Author>authors)
        {
            Console.WriteLine("Books:");
            foreach (var book in books) 
            
                Console.WriteLine($"{book.BookId}:{book.BookTitle} by {book.AuthorName} - Rating {book.BooksAveragerating}");

                Console.WriteLine("\nAuthors:");
                foreach (var author in authors) 
                {
                    Console.WriteLine($"{author.AuthorId}:{author.AuthorName}from: {author.AuthorsCountry}");
                    foreach (var bookUpdateID in author.booksIsWritten) 
                    { 
                      var book= books.First(book => book.BookId == bookUpdateID);
                        Console.WriteLine($"{book.BookTitle} rating: {book.BooksAveragerating}");
                    
                    }
                
                }
        
        }

        public void SearchAndFillterBooks(List<Book>books,List<Author>authors)
        {
            Console.WriteLine("--------Search and Filter Option:--------");
            Console.WriteLine("1. Search by Genre ");
            Console.WriteLine("2. Search by Author");
            Console.WriteLine("3. serch by Publication year");
            Console.WriteLine("4. List all books with Average rating above threshold");
            Console.WriteLine("5. Sort book by Publication year");
            Console.WriteLine("6 Sort book by book Title");
            Console.WriteLine("7. Sort book by Author name");
            Console.WriteLine("Choose one Option");



            string alternatives = Console.ReadLine()!;

            IEnumerable<Book> booksfilter = books;
            IEnumerable<Book> sortedBooks = books;



            switch (alternatives)
            {
                case "1":
                    string genre = Prompt("Enter Genre: ");
                    booksfilter = books.Where(book => book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
                    DisplayFiltringBooks(booksfilter);
                    ;
                    break;
                case "2":

                    string author = Prompt("Enter Author Name:");
                    booksfilter = books.Where(book => book.AuthorName.Equals(author, StringComparison.OrdinalIgnoreCase));
                    DisplayFiltringBooks(booksfilter);
                    break;

                case "3":
                    int year = int.Parse(Prompt("Enter publiched Year "));
                    booksfilter = books.Where(book => book.PublishedYear == year);
                    DisplayFiltringBooks(booksfilter);

                    List<Book> AllBookWithPublishedYear = books.Where(book => book.PublishedYear == 1969).ToList();

                    AllBookWithPublishedYear.ForEach(book => Console.WriteLine(book.BookTitle));
                    break;

                case "4":
                    Console.WriteLine("Enter The rating threshold  1-5 : ");
                    double thresholdratingFromUser = Convert.ToDouble(Console.ReadLine());

                    if (thresholdratingFromUser >= 1 && thresholdratingFromUser <= 5)
                    {
                        List<Book> BooksAverageThreshold = books.Where(book => book.BooksAveragerating > thresholdratingFromUser).ToList();

                        if (BooksAverageThreshold.Any())
                        {
                            Console.WriteLine($"Book with average rating above {thresholdratingFromUser}:");
                            foreach (Book book in BooksAverageThreshold)
                            {
                                Console.WriteLine($"{book.BookTitle} by {book.AuthorName} with average rating:{book.BooksAveragerating}");

                            }

                        }
                        else
                        {

                            Console.WriteLine("No books found above the threshold");

                        }
                    }
                    else
                    {

                        Console.WriteLine("Invalid input, Please enter a number between 1 - 5 ");
                    }

                    break;

                case "5":
                    sortedBooks = books.OrderBy(book => book.PublishedYear).ToList();
                    DisplaySortingBook(sortedBooks);
                    break;

                case "6":
                    sortedBooks = books.OrderBy(book => book.BookTitle).ToList();
                    DisplaySortingBook(sortedBooks);
                    break;

                case "7":
                    sortedBooks = books.OrderBy(book => book.AuthorName).ToList();
                    DisplaySortingBook(sortedBooks);
                    break;


            }

        }

        private static void DisplaySortingBook(IEnumerable<Book> sortedBooks)
        {
            foreach (var book in sortedBooks)
                Console.WriteLine($"{book.BookId} : {book.BookTitle} by {book.AuthorName} , {book.Genre} published in  {book.PublishedYear}, ISBN: {book.ISBNCode}, Average Rating: {book.BooksAveragerating}");
        }

        private static void DisplayFiltringBooks(IEnumerable<Book> booksfilter)
        {
            Console.WriteLine("\nBooks: ");
            foreach (var book in booksfilter)
                Console.WriteLine($"{book.BookId} : {book.BookTitle} by {book.AuthorName} , {book.Genre} published in  {book.PublishedYear}, ISBN: {book.ISBNCode}, Average Rating: {book.BooksAveragerating}");
        }

        public void DataLoading(List<Book> books, List<Author> authors) 
        
        {
            if (File.Exists(dataJsonFilePath))
            
            {
                string dataJsonFilePath = "LibraryData.json";
                string alldatasomJSOType = File.ReadAllText(dataJsonFilePath);
                MinLillaDB minLillaDB = JsonSerializer.Deserialize<MinLillaDB>(alldatasomJSOType)!;
                List<Book> allbook = minLillaDB.AllbooksfromDB;
                List<Author> allauthors = minLillaDB.allaAuthorsDatafromDB;
                MirrorChangesToProjectRoot("LibraryData.json");

            }
        }


        public void SaveAllDataAndExit() 
        {

            string updatedlillaDB = JsonSerializer.Serialize(minLillaDB, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(dataJsonFilePath, updatedlillaDB);

            Console.WriteLine("the daata has been saved ");


        }



        //Lista alla böcker med ett genomsnittligt betyg över ett användarspecificerat tröskelvärde.

        public void ListAllBooksAboveRatingThreshold()
        {

            Console.WriteLine("Enter The rating threshold  1-5 : ");
            double thresholdratingFromUser = Convert.ToDouble(Console.ReadLine());

            if (thresholdratingFromUser >= 1 && thresholdratingFromUser <= 5)
            {
                List<Book> BooksAverageThreshold = books.Where(book => book.BooksAveragerating > thresholdratingFromUser).ToList();

                if (BooksAverageThreshold.Any())
                {
                    Console.WriteLine($"Book with average rating above {thresholdratingFromUser}:");
                    foreach (Book book in BooksAverageThreshold)
                    {
                        Console.WriteLine($"{book.BookTitle} by {book.AuthorName} with average rating:{book.BooksAveragerating}");

                    }

                }
                else
                {

                    Console.WriteLine("No books found above the threshold");

                }
            }
            else 
            {

                Console.WriteLine("Invalid input, Please enter a number between 1 - 5 ");
            }
        }

        

        public void SortAllBookByYearTitleOrAuthor(string OptingSotr)
        {
            IEnumerable<Book> sortedBooks;

            switch (OptingSotr.ToLower()) 
            {
                case "Year":
                    sortedBooks = books.OrderBy(book =>book.PublishedYear).ToList();
                    break;
                case "Title":
                    sortedBooks = books.OrderBy(book => book.BookTitle).ToList();
                    break;
                case "author":
                    sortedBooks = books.OrderBy(book => book.AuthorName).ToList();
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose the correct option (year,title or author)");
                    return;
            }

            Console.WriteLine(" all books are sorted by " + OptingSotr + ":");
            foreach (Book book in sortedBooks) 
            {
                Console.WriteLine($"{book.BookTitle} written by {book.AuthorName} published in {book.PublishedYear}");
            }
               
        }

        public void AddRatingToBook() 
        {
            Console.WriteLine("Enter the ID of the book that you want to rate: ");
            if (int.TryParse(Console.ReadLine(), out int addRatingToBook))
            {
                var book = books.FirstOrDefault(book => book.BookId == addRatingToBook);
                if (book != null)
                {
                    Console.WriteLine("enter rating between 1 - 5 :");
                    if (int.TryParse(Console.ReadLine(), out int rating))
                    {

                        book.AddRating(rating);

                    }
                    else
                    {
                        Console.WriteLine("The entered data mist be numeric value");

                    }


                }
                else
                {
                    Console.WriteLine("The book you want to rate  is not found. ");
                }

            }
            else 
            {
                Console.WriteLine("Invalid book ID ");
            }
        }
        

static void MirrorChangesToProjectRoot(string fileName)
        {
            // Get the path to the output directory
            string outputDir = AppDomain.CurrentDomain.BaseDirectory;

            // Get the path to the project root directory
            string projectRootDir = Path.Combine(outputDir, "../../../");

            // Define paths for the source (output directory) and destination (project root)
            string sourceFilePath = Path.Combine(outputDir, fileName);
            string destFilePath = Path.Combine(projectRootDir, fileName);

            // Copy the file if it exists
            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destFilePath, true); // true to overwrite
                Console.WriteLine($"{fileName} has been mirrored to the project root.");
            }
            else
            {
                Console.WriteLine($"Source file {fileName} not found.");
            }
        }


        public string Prompt(string message, string defaultValue = "")
        {
            Console.WriteLine(message);
            string input = Console.ReadLine()!;
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }



    }
}
