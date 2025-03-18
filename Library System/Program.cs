

using System.Reflection;
using System.Threading.Channels;

namespace Library_System
{
    class Book
    {
        public string Titel { get; set; }
        public string Author { get; set; }
        public double Iben { get; set; }
        public bool Availability { get; set; }


        public Book(string tital, string author, double iben)
        {
            Titel = tital;
            Author = author;
            Iben = iben;
            Availability = true;
        }
    }
    class Library
    {
        public List<Book> books = new List<Book>();
        public void PrintList()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("there's no Books in the Library");
                return;
            }
            for (int i = 0; i < books.Count; i++)
            {
                string status = books[i].Availability ? "Avalibel" : "Borrowed";
                Console.WriteLine($"[ {books[i].Titel} ] [ {books[i].Author} ] [ {books[i].Iben} ] {status}");
            }
        }

        public string AddBook(Book book)
        {
            if (!books.Contains(book))
            {
                books.Add(book);
                return $"{book.Titel} Book has been Add";
            }
            return $"this book is alredy in the library";
        }

        public string BorrowBook(string title)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Titel.ToUpper().Contains(title.ToUpper()))
                {
                    if (books[i].Availability)
                    {
                        books[i].Availability = false;
                        return $"[ {books[i].Titel} - {books[i].Author} - {books[i].Iben} ]\nBook has been borrowed";
                    }
                    else if (!books[i].Availability)
                    {
                        books[i].Availability = true;
                        return $"[ {books[i].Titel} - {books[i].Author} - {books[i].Iben} ]\nbook is alredy borrowed";
                    }
                }
            }
            return $"This book is not in the library";
        }

        public string ReturnBook(string title)
        {
            for (int i = 0; i < books.Count; i++)
                if (books[i].Titel.ToUpper().Contains(title.ToUpper()))
                {
                    if (!books[i].Availability)
                    {
                        books[i].Availability = true;
                        return $"[ {books[i].Titel} - {books[i].Author} - {books[i].Iben} ]\nBook has been Returned";
                    }
                    else if (books[i].Availability)
                    {
                        books[i].Availability = true;
                        return $"[ {books[i].Titel} - {books[i].Author} - {books[i].Iben} ]\nbook still avalibale and not borrowed";
                    }
                }
            return $"This book not in the library you can add it"; 
        }

        public string SearchBook(string Key)
        {
            bool found = false;
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Titel.ToUpper().Contains(Key.ToUpper()))
                {
                    string status = books[i].Availability ? "Avalibel" : "Borrowed";
                    found = true;
                    return $"Book has been found " + $"[ " + $"{books[i].Titel} : {books[i].Author} : {books[i].Iben}\" ] {status}";
                }

            }
            return "the book is not in the library";
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            // Add Books
            Console.WriteLine("the Library Books");
            library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", 9780743273565));
            library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", 9780061120084));
            library.AddBook(new Book("1984", "George Orwell", 9780451524935));


            library.BorrowBook("Gatsby");
            library.BorrowBook("1984");
            library.BorrowBook("Harry Potter"); // This book is not in the library

            
            library.ReturnBook("Gatsby");
            library.ReturnBook("Harry Potter"); // This book is not borrowed

            library.PrintList();
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<");
            char chose = ' ';

            do
            {
                Console.WriteLine("A - Add Book");
                Console.WriteLine("P - print the library books");
                Console.WriteLine("S - Search For Book");
                Console.WriteLine("B - Borrow Book");
                Console.WriteLine("R - Return Book");
                Console.WriteLine("Q - Quit");
                chose = char.ToUpper(char.Parse(Console.ReadLine()));
                switch (chose)
                {
                    case 'P': // print library list book
                        library.PrintList();
                        break;
                    case 'A': // Adding books to the library
                        Console.WriteLine("pleas enter Book title and author and iben ");
                        string titel = Console.ReadLine().ToLower();
                        string author = Console.ReadLine().ToLower();
                        double iben = double.Parse(Console.ReadLine().ToLower());
                        string addBook = library.AddBook(new Book(titel, author, iben));
                        Console.WriteLine(addBook);
                        break;
                    case 'S': // search for books in the library
                        Console.WriteLine("search for Book title key ");
                        string search = Console.ReadLine();
                        string result = library.SearchBook(search);
                        Console.WriteLine(result);
                        break;
                    case 'B': // borrowing book
                        Console.WriteLine("what book do you need to borrow");
                        string keyWord = Console.ReadLine();
                        string borrow = library.BorrowBook(keyWord);
                        Console.WriteLine(borrow);
                        break;
                    case 'R':
                        Console.WriteLine("what book do you need to borrow");
                        string bookName = Console.ReadLine();
                        string Return = library.ReturnBook(bookName);
                        Console.WriteLine(Return);
                        break;
                    default:
                        Console.WriteLine("Unknown selection, please try again");
                        break;
                }
            } while (chose != 'Q');
            Console.WriteLine("Bye :)");


            Console.ReadLine();

        }
    }

}
