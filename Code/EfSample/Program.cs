using EfSample.Context;
using EfSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using EfSample.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EfSample
{
    internal static class Program
    {
        private static void Main()
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    RunSelection();
                    if (int.TryParse(Console.ReadLine(), out var selectedOption))
                    {
                        switch (selectedOption)
                        {
                            case (int)AllowedOperations.ExitFromProgram:
                                Console.WriteLine("Closing application...");
                                exit = true;
                                break;
                            case (int)AllowedOperations.BasicOperations:
                                BasicOperations();
                                break;
                            case (int)AllowedOperations.WorkWithRelationships:
                                WorkWithRelationships();
                                break;
                            case (int)AllowedOperations.EagerLoadExample:
                                EagerLoadExample();
                                break;
                            case (int)AllowedOperations.LazyLoadExample:
                                LazyLoadExample();
                                break;
                            default:
                                Console.WriteLine("Operation not recognized");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Operation not recognized");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.GetaAllMessages());
                    exit = true;
                }
            }
        }

        private static void EagerLoadExample()
        {
            using var db = new LibraryContext();
            Console.WriteLine("Quering all books with authors...");
            WriteLineBooks(db.Books.Include(b => b.Author));
        }

        private static void LazyLoadExample()
        {
            using var db = new LibraryContext();
            Console.WriteLine("Quering all books with authors...");
            WriteLineBooks(db.Books);
        }

        private static void BasicOperations()
        {
            using var db = new LibraryContext();
            // Create author
            Console.WriteLine("Creating author...");
            db.Authors.Add(new Author
            {
                Name = "John",
                LastName = "Doe"
            });
            db.SaveChanges();

            // Query all authors
            Console.WriteLine("Quering all authors...");
            WriteLineAuthors(db.Authors);

            //// Update an author
            //Console.WriteLine("Update author...");
            //var author = db.Authors.FirstOrDefault(a => a.Name == "John" && a.LastName == "Doe");
            //author.Name = "Peter";
            //db.Authors.Update(author);
            //db.SaveChanges();

            //Console.WriteLine("Quering all authors...");
            //WriteLineAuthors(db.Authors);

            //// Delete author
            //Console.WriteLine("Delete author...");
            //db.Authors.Remove(author);
            //db.SaveChanges();

            //Console.WriteLine("Quering all authors...");
            //WriteLineAuthors(db.Authors);
        }

        private static void WorkWithRelationships()
        {
            using var db = new LibraryContext();
            // Create author
            Console.WriteLine("Creating author...");
            db.Authors.Add(new Author
            {
                Name = "John",
                LastName = "Doe"
            });
            db.SaveChanges();

            // Query all authors
            Console.WriteLine("Quering all authors...");
            WriteLineAuthors(db.Authors);

            // Adding a book to an author
            Console.WriteLine("Adding a book to an author...");
            var author = db.Authors.FirstOrDefault(a => a.Name == "John" && a.LastName == "Doe");
            author.Books.Add(new Book
            {
                Title = "Matter and time",
                Isbn = "3548-SD425",
                Bookcase = "E32B",
            });
            db.SaveChanges();
            WriteLineBooks(db.Books);

            // Delete an author with his books
            Console.WriteLine("Delete author...");
            db.Authors.Remove(author);
            db.SaveChanges();
            Console.WriteLine("Queryng all books...");
            WriteLineBooks(db.Books);
        }

        private static void WriteLineAuthors(IEnumerable<Author> authors)
        {
            Console.WriteLine(authors.ToStringTable(new string[] { "Id", "Author Name", "Author Last Name" },
                    a => a.Id,
                    a => a.Name,
                    a => a.LastName));
        }

        private static void WriteLineBooks(IEnumerable<Book> books)
        {
            Console.WriteLine(books.ToStringTable(new string[] { "Id", "Title", "ISBN", "Bookcase", "Author Name", "Author Last Name" },
                    b => b.Id,
                    b => b.Title,
                    b => b.Isbn,
                    b => b.Bookcase,
                    b => b.Author?.Name,
                    b => b.Author?.LastName));
        }

        private static void RunSelection()
        {
            Console.WriteLine("EF Core sample project");
            Console.WriteLine("====================================================");
            Console.WriteLine("Allowed operations:");
            Console.WriteLine("     - (0) Close application");
            Console.WriteLine("     - (1) Basic operations");
            Console.WriteLine("     - (2) Basic operations with relationship");
            Console.WriteLine("     - (3) Eager loading example (Remember comment LazyLoad config on LibraryContext)");
            Console.WriteLine("     - (4) Lazy loading example (Remember uncomment LazyLoad config on LibraryContext)");
            Console.WriteLine("Please, select an option");
        }
    }
}
