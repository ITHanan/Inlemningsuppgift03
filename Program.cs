
using System.Text.Json;

namespace Inlemningsuppgift03
{
    public class Program
    {
        static void Main(string[] args)
        {

         string dataJsonFilePath = "LibraryData.json";
         string alldatasomJSOType = File.ReadAllText(dataJsonFilePath);

           MinLillaDB minLillaDB = JsonSerializer.Deserialize<MinLillaDB>(alldatasomJSOType)!;
           LibrarySystem librarySystem = new LibrarySystem();
           List<Book> books = minLillaDB.AllbooksfromDB;
           List<Author> authors = minLillaDB.allaAuthorsDatafromDB;

            

            bool running = true;

            while (running)
            {
                DisplayMenu();
                string userInputsomString = Console.ReadLine()!;
                int userInputInt = Convert.ToInt32(userInputsomString);


                switch (userInputInt)
                {
                 
                    case 1:

                        librarySystem.Addnewbook(minLillaDB.AllbooksfromDB,minLillaDB.allaAuthorsDatafromDB);
                        break;
                    case 2:
                        librarySystem.AddnewAuthor(minLillaDB.AllbooksfromDB, minLillaDB.allaAuthorsDatafromDB);

                        break;
                    case 3:

                        librarySystem.UpdateBookDetails(minLillaDB.AllbooksfromDB,minLillaDB.allaAuthorsDatafromDB);
                        break;
                    case 4:

                        librarySystem.UpdateAuthorDetails(minLillaDB.AllbooksfromDB, minLillaDB.allaAuthorsDatafromDB);
                        break;
                    case 5:

                        librarySystem.DeleteBook(minLillaDB.AllbooksfromDB, minLillaDB.allaAuthorsDatafromDB);
                        break;
                    case 6:

                        librarySystem.DeleteAuthor(minLillaDB.AllbooksfromDB,minLillaDB.allaAuthorsDatafromDB);
                        break;
                    case 7:

                        librarySystem.ListAll(minLillaDB.AllbooksfromDB,minLillaDB.allaAuthorsDatafromDB);
                        break;
                    case 8:
                        librarySystem.SearchAndFillterBooks(minLillaDB.AllbooksfromDB, minLillaDB.allaAuthorsDatafromDB); 
                        break;
                    case 9:
                        librarySystem.SaveAllDataAndExit(minLillaDB.AllbooksfromDB, minLillaDB.allaAuthorsDatafromDB);

                        break;
  
                    case 0:
                        Console.WriteLine("Exit...");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again!");
                        break;


                }
                
                Console.WriteLine("Do you want to complete? (J/N)");
                string continueChoice = Console.ReadLine()!;
                if (continueChoice.ToUpper() == "N")
                {
                    running = false;
                }


            }


        }

        private static void DisplayMenu()
        {
            Console.WriteLine("--------welcome to Hanans Librar--------");
            Console.WriteLine("\n------ Library Menu ------");
            Console.WriteLine("Please Choose one option: ");
            Console.WriteLine("1. Add New Book");
            Console.WriteLine("2. Add New Author");
            Console.WriteLine("3. Update Book details");
            Console.WriteLine("4. Update Author details");
            Console.WriteLine("5. Delete Book");
            Console.WriteLine("6. Delete Author");
            Console.WriteLine("7. List All Books and Author");
            Console.WriteLine("8. Search and Filter Books");
            Console.WriteLine("9. Save All Data And Exit");
            Console.WriteLine("0. Exit");

        }
    }
}
