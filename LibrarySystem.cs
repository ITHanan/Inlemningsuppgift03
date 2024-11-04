
using System.Text.Json;

namespace Inlemningsuppgift03
{


    public class LibrarySystem
    {

        public void Addnewbook(MinLillaDB minLillaDB)

        {

            Console.WriteLine("------Please, enter all book's details------\n");
            Console.WriteLine("Enter book title:");
            string newBookTitle = Console.ReadLine()!;

            Console.WriteLine("Enter author name:");
            string newAuthorName = Console.ReadLine()!;

            var author = minLillaDB.allaAuthorsDatafromDB.FirstOrDefault(author => author.AuthorName.Equals(newAuthorName, StringComparison.OrdinalIgnoreCase));

            if (author == null)
            {

                Console.WriteLine("The author not found. Add the Author first ");
                return;

            }


            Console.WriteLine("Enter genre:");
            string newGenre = Console.ReadLine()!;


            Console.WriteLine("Enter Published Year:");


            if (!int.TryParse(Console.ReadLine(), out int newPublishedYear))
            {
                Console.WriteLine("Invalid input for Published Year. Please enter a valid number.");
                return;
            }


            Console.WriteLine("Enter ISBN Code:");

            if (!int.TryParse(Console.ReadLine(), out int newISBNCode))
            {
                Console.WriteLine("Invalid input for ISBN Code. Please enter a valid number.");
                return;
            }




            Book newBook = new(minLillaDB.allaAuthorsDatafromDB.Count + 1, newBookTitle, newAuthorName, newGenre, newPublishedYear, newISBNCode, [])
            {

                BookId = minLillaDB.allaAuthorsDatafromDB.Count + 1,
                BookTitle = newBookTitle,
                AuthorName = newAuthorName,
                Genre = newGenre,
                PublishedYear = newPublishedYear,
                ISBNCode = newISBNCode,
                Ratings = []


            };

            minLillaDB.AllbooksfromDB.Add(newBook);
            author.booksIsWrittenIds.Add(newBook.BookId);
            SaveAllDataAndExit(minLillaDB);
            MirrorChangesToProjectRoot("LibraryData.json");

        }

        public void AddnewAuthor(MinLillaDB minLillaDB)

        {


            Console.WriteLine("------Please, enter all author's details------\n");

            Console.WriteLine("Enter Author's name:");
            string newAuthorName = Console.ReadLine()!;

            Console.WriteLine("Enter Author's nationality:");
            string newAuthorsCountry = Console.ReadLine()!;

            Author newAuthor = new(minLillaDB.allaAuthorsDatafromDB.Count + 1, newAuthorName, newAuthorsCountry)
            {
                AuthorId = minLillaDB.allaAuthorsDatafromDB.Count + 1,
                AuthorName = newAuthorName,
                AuthorsCountry = newAuthorsCountry
            };

            minLillaDB.allaAuthorsDatafromDB.Add(newAuthor);

            SaveAllDataAndExit(minLillaDB);
            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine("Auther added successfully");


        }

        public void UpdateBookDetails(MinLillaDB minLillaDB)
        {
            int bookUpdateID = int.Parse(Prompt("Enter the book Id that you want to Update:"));

            var book = minLillaDB.AllbooksfromDB.FirstOrDefault(book => book.BookId == bookUpdateID);

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }
            book.BookTitle = Prompt($"Enter new title of the current{book.BookTitle}:", book.BookTitle);

            book.Genre = Prompt($"Enter new genre of the current {book.Genre}:", book.Genre);

            book.PublishedYear = int.Parse(Prompt($"Enter new publication year of the current {book.PublishedYear}:", book.PublishedYear.ToString()));

            SaveAllDataAndExit(minLillaDB);

            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine("Book details updated");

        }



        public void UpdateAuthorDetails(MinLillaDB minLillaDB)
        {
            int updatedAuthorID = int.Parse(Prompt("Enter the author Id number that you want to update:"));
            var author = minLillaDB.allaAuthorsDatafromDB.FirstOrDefault(author => author.AuthorId == updatedAuthorID);
            if (author == null)
            {
                Console.WriteLine("Author not found");
                return;
            }
            author.AuthorName = Prompt($"Enter new Author the corrent name is: {author.AuthorName}:", author.AuthorName);
            author.AuthorsCountry = Prompt($"Enter new country the corrent country is {author.AuthorsCountry}", author.AuthorsCountry);
            SaveAllDataAndExit(minLillaDB);
            MirrorChangesToProjectRoot("LibraryData.json");

            Console.WriteLine("Author has been updated.");

        }



        public void DeleteBook(MinLillaDB minLillaDB)
        {
            int bookIDtoDelete = int.Parse(Prompt("Enter the book ID that you want to delete:"));

            var book = minLillaDB.AllbooksfromDB.FirstOrDefault(book => book.BookId == bookIDtoDelete);
            if (book == null)
            {
                Console.WriteLine("The book is not found.");
                return;
            }

            minLillaDB.AllbooksfromDB.Remove(book);
            var author = minLillaDB.allaAuthorsDatafromDB.FirstOrDefault(author => author.AuthorName == book.AuthorName);
            if (author != null)
            {

                author.booksIsWrittenIds.Remove(bookIDtoDelete);


                Console.WriteLine("Book has been deleted.");

            }
            SaveAllDataAndExit(minLillaDB);
            MirrorChangesToProjectRoot("LibraryData.json");



        }
        public void DeleteAuthor(MinLillaDB minLillaDB)
        {
            int authorIdToDelet = int.Parse(Prompt("Enter the author Id to delete:"));

            var author = minLillaDB.allaAuthorsDatafromDB.FirstOrDefault(author => author.AuthorId == authorIdToDelet);
            if (author == null)
            {

                Console.WriteLine("Author not found.");
                return;

            }
            minLillaDB.allaAuthorsDatafromDB.Remove(author);
            minLillaDB.AllbooksfromDB.RemoveAll(book => book.AuthorName == author.AuthorName);
            Console.WriteLine("Author and their books has been deleted.");

            SaveAllDataAndExit(minLillaDB);
            MirrorChangesToProjectRoot("LibraryData.json");
        }

        public void ListAll(List<Book> books, List<Author> authors)
        {
            Console.WriteLine("Books:");
            foreach (var book in books)

                Console.WriteLine($"{book.BookId}:{book.BookTitle} by {book.AuthorName} - Rating {book.BooksAveragerating}");

            Console.WriteLine("\nAuthors:");
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.AuthorId}:{author.AuthorName} from: {author.AuthorsCountry}");
                foreach (var bookUpdateID in author.booksIsWrittenIds)
                {
                    var book = books.First(book => book.BookId == bookUpdateID);
                    Console.WriteLine($"{book.BookTitle} rating: {book.BooksAveragerating}");

                }

            }

        }

        public void SearchAndFillterBooks(MinLillaDB minLillaDB)
        {
            Console.WriteLine("--------Search and Filter Option:--------");
            Console.WriteLine("1. Search by Genre ");
            Console.WriteLine("2. Search by Author");
            Console.WriteLine("3. serch by Publication year");
            Console.WriteLine("4. List all books with Average rating above threshold");
            Console.WriteLine("5. Sort book by Publication year");
            Console.WriteLine("6 Sort book by book Title");
            Console.WriteLine("7. Sort book by Author name");
            Console.WriteLine("8. Add Rating to a Book ");
            Console.WriteLine("Choose one Option");



            string alternatives = Console.ReadLine()!;

            IEnumerable<Book> booksfilter = minLillaDB.AllbooksfromDB;
            IEnumerable<Book> sortedBooks = minLillaDB.AllbooksfromDB;



            switch (alternatives)
            {
                case "1":
                    string genre = Prompt("Enter Genre: ");
                    booksfilter = minLillaDB.AllbooksfromDB.Where(book => book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
                    DisplayFiltringBooks(booksfilter);
                    ;
                    break;
                case "2":

                    string author = Prompt("Enter Author Name:");
                    booksfilter = minLillaDB.AllbooksfromDB.Where(book => book.AuthorName.Equals(author, StringComparison.OrdinalIgnoreCase));
                    DisplayFiltringBooks(booksfilter);
                    break;

                case "3":
                    int year = int.Parse(Prompt("Enter publiched Year "));
                    booksfilter = minLillaDB.AllbooksfromDB.Where(book => book.PublishedYear == year);
                    DisplayFiltringBooks(booksfilter);

                    break;

                case "4":
                    Console.WriteLine("Enter The rating threshold  1-5 : ");
                    double thresholdratingFromUser = Convert.ToDouble(Console.ReadLine());

                    if (thresholdratingFromUser >= 1 && thresholdratingFromUser <= 5)
                    {
                        List<Book> BooksAverageThreshold = minLillaDB.AllbooksfromDB.Where(book => book.BooksAveragerating > thresholdratingFromUser).ToList();

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
                    sortedBooks = minLillaDB.AllbooksfromDB.OrderBy(book => book.PublishedYear).ToList();
                    DisplaySortingBook(sortedBooks);
                    break;

                case "6":
                    sortedBooks = minLillaDB.AllbooksfromDB.OrderBy(book => book.BookTitle).ToList();
                    DisplaySortingBook(sortedBooks);
                    break;

                case "7":
                    sortedBooks = minLillaDB.AllbooksfromDB.OrderBy(book => book.AuthorName).ToList();
                    DisplaySortingBook(sortedBooks);
                    break;

                case "8":
                    AddRatingToBook(minLillaDB.AllbooksfromDB);
                    DisplaySortingBook(sortedBooks);
                    SaveAllDataAndExit(minLillaDB);
                    MirrorChangesToProjectRoot("LibraryData.json");
                    break;


            }

        }

        private static void DisplaySortingBook(IEnumerable<Book> sortedBooks)
        {
            foreach (var book in sortedBooks)
                Console.WriteLine($"{book.BookId} : {book.BookTitle} by {book.AuthorName} , {book.Genre} published in  {book.PublishedYear}, ISBN: {book.ISBNCode}, Average Rating: {book.BooksAveragerating}\n");
        }

        private static void DisplayFiltringBooks(IEnumerable<Book> booksfilter)
        {
            Console.WriteLine("\nBooks: ");
            foreach (var book in booksfilter)
                Console.WriteLine($"{book.BookId} : {book.BookTitle} by {book.AuthorName} , {book.Genre} published in  {book.PublishedYear}, ISBN: {book.ISBNCode}, Average Rating: {book.BooksAveragerating}\n");
        }

        public void DataLoading(List<Book> books, List<Author> authors)

        {
            string dataJsonFilePath = "LibraryData.json";


            if (File.Exists(dataJsonFilePath))

            {
                string alldatasomJSOType = File.ReadAllText(dataJsonFilePath);

                MinLillaDB minLillaDB = JsonSerializer.Deserialize<MinLillaDB>(alldatasomJSOType)!;

                books = minLillaDB.AllbooksfromDB ?? new List<Book>();
                authors = minLillaDB.allaAuthorsDatafromDB ?? new List<Author>();


                MirrorChangesToProjectRoot("LibraryData.json");

            }
            else
            {
                Console.WriteLine("Data file not found. Initializing with an empty database.");
                books = new List<Book>();
                authors = new List<Author>();

            }
        }


        public void SaveAllDataAndExit(MinLillaDB minLillaDB)
        {

            string dataJsonFilePath = "LibraryData.json";

            string updatedlillaDB = JsonSerializer.Serialize(minLillaDB, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(dataJsonFilePath, updatedlillaDB);

            Console.WriteLine("The data has been saved ");

        }


        public void ListAllBooksAboveRatingThreshold(List<Book> books)
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


        public void AddRatingToBook(List<Book> books)
        {
            Console.WriteLine("Enter the ID of the book that you want to rate: ");
            if
               (int.TryParse(Console.ReadLine(), out int addRatingToBook))
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
