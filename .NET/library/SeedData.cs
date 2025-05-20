using Microsoft.CodeAnalysis.CSharp.Syntax;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi
{
    public class SeedData
    {
        public static void SetInitialData()
        {
            var ernestMonkjack = new Author
            {
                Name = "Ernest Monkjack"
            };
            var sarahKennedy = new Author
            {
                Name = "Sarah Kennedy"
            };
            var margaretJones = new Author
            {
                Name = "Margaret Jones"
            };

            var masashiKishimoto = new Author
            {
                Name = "Masashi Kishimoto"
            };

            var clayBook = new Book
            {
                Name = "The Importance of Clay",
                Format = BookFormat.Paperback,
                Author = ernestMonkjack,
                ISBN = "1305718181"
            };

            var agileBook = new Book
            {
                Name = "Agile Project Management - A Primer",
                Format = BookFormat.Hardback,
                Author = sarahKennedy,
                ISBN = "1293910102"
            };

            var rustBook = new Book
            {
                Name = "Rust Development Cookbook",
                Format = BookFormat.Paperback,
                Author = margaretJones,
                ISBN = "3134324111"
            };

            var landOfHiddenLeaf = new Book
            {
                Name = "Land Of The Hidden Leaf",
                Format = BookFormat.Paperback,
                Author = masashiKishimoto,
                ISBN = "3134324111"
            };

            var theFourthGreatNinjaWar = new Book
            {
                Name = "The Fourth Great Ninja War",
                Format = BookFormat.Paperback,
                Author = masashiKishimoto,
                ISBN = "3134324111"
            };


            var Fine1 = new Fine
            {
                Id = new Guid(),
                Price = (float)6.50,
                FineDate = DateTime.Now,
                Outstanding = true,
                FineRevoked = false
            };

            var Fine2 = new Fine
            {
                Id = new Guid(),
                Price = (float)3.50,
                FineDate = DateTime.Now,
                Outstanding = false,
                FineRevoked = false
            };
            var Fine3 = new Fine
            {
                Id = new Guid(),
                Price = (float)2.00,
                FineDate = DateTime.Now,
                Outstanding = false,
                FineRevoked = false
            };

            var Fine4 = new Fine
            {
                Id = new Guid(),
                Price = (float)5.00,
                FineDate = DateTime.Now,
                Outstanding = false,
                FineRevoked = true
            };

            Fine? Fine5 = null;

            var Fine6 = new Fine
            {
                Id = new Guid(),
                Price = (float)3.50,
                FineDate = DateTime.Now,
                Outstanding = false,
                FineRevoked = false
            };
            var Fine7 = new Fine
            {
                Id = new Guid(),
                Price = (float)2.00,
                FineDate = DateTime.Now,
                Outstanding = false,
                FineRevoked = false
            };

            Fine? Fine8 = null;

            var daveSmith = new Borrower
            {
                Id = new Guid(),
                Name = "Dave Smith",
                EmailAddress = "dave@smithy.com",
                Loans = new List<Loan> { new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine1, Book = landOfHiddenLeaf },
                                        new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine2, Book = theFourthGreatNinjaWar }
                                        }
            
            };

            var lianaJames = new Borrower
            {
                Id = new Guid(),
                Name = "Liana James",
                EmailAddress = "liana@gmail.com",
                Loans = new List<Loan> { new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine3, Book = landOfHiddenLeaf },
                                        new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine4, Book = theFourthGreatNinjaWar }
                                        }
            };

            var leBronJames = new Borrower
            {
                Id = new Guid(),
                Name = "LeBron James",
                EmailAddress = "LB@gmail.com",
                Loans = new List<Loan> { new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine5, Book = landOfHiddenLeaf },
                                        new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine6, Book = theFourthGreatNinjaWar }
                                        }
            };

            var narutoUzimaki = new Borrower
            {
                Id = new Guid(),
                Name = "Naruto Uzimaki",
                EmailAddress = "NU@gmail.ninj",
                Loans = new List<Loan> { new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine7, Book = landOfHiddenLeaf },
                                        new Loan { Id = new Guid(), Active = true, StartDate = DateTime.Now, EndDate = DateTime.Now, ExtentionDate = DateTime.Now, Fine = Fine8, Book = theFourthGreatNinjaWar }
                                        }
            };
            
            var loan1 = new Loan
            {
                Id = new Guid(),
                Active = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExtentionDate = DateTime.Now.Add(new TimeSpan(1, 2, 30, 45)),// 1 day, 2 hours, 30 minutes, 45 seconds.
                Fine = new Fine {  Id = new Guid(),  FineDate = DateTime.Now, FineRevoked = false, Outstanding = false, Price = 7.50f},
                Book = clayBook
            };

            var loan2 = new Loan
            {
                Id = new Guid(),
                Active = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExtentionDate = DateTime.Now.Add(new TimeSpan(4, 6, 10, 9)),// 1 day, 2 hours, 30 minutes, 45 seconds.,
                Fine = Fine1,
                Book = landOfHiddenLeaf
            };

            var loan3 = new Loan
            {
                Id = new Guid(),
                Active = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExtentionDate = DateTime.Now.Add(new TimeSpan(2, 2, 56, 15)),// 1 day, 2 hours, 30 minutes, 45 seconds.,
                Fine = null,
                Book = agileBook
            };

            var loan4 = new Loan
            {
                Id = new Guid(),
                Active = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ExtentionDate = DateTime.Now.Add(new TimeSpan(3, 10, 10, 25)),// 1 day, 2 hours, 30 minutes, 45 seconds.,
                Fine = null,
                Book = theFourthGreatNinjaWar
            };


            var bookOnLoanUntilToday = new BookStock {
                OnLoanTo = daveSmith,
                Loan = loan1
            };

            var bookNotOnLoan = new BookStock
            {
                OnLoanTo = null,
                Loan = loan2
            };

            var bookOnLoanUntilNextWeek = new BookStock
            {
                OnLoanTo = lianaJames,
                Loan = loan3
            };

            var rustBookStock = new BookStock
            {
                OnLoanTo = null,
                Loan = loan4
            };

            

            using (var context = new LibraryContext())
            {
                context.Authors.Add(ernestMonkjack);
                context.Authors.Add(sarahKennedy);
                context.Authors.Add(margaretJones);

                context.Books.Add(clayBook);
                context.Books.Add(agileBook);
                context.Books.Add(rustBook);

                context.Borrowers.Add(daveSmith);
                context.Borrowers.Add(lianaJames);
                context.Borrowers.Add(leBronJames);
                context.Borrowers.Add(narutoUzimaki);

                context.Catalogue.Add(bookOnLoanUntilToday);
                context.Catalogue.Add(bookNotOnLoan);
                context.Catalogue.Add(bookOnLoanUntilNextWeek);
                context.Catalogue.Add(rustBookStock);

                context.Loans.Add(loan1);
                context.Loans.Add(loan2);
                context.Loans.Add(loan3);
                context.Loans.Add(loan4);

                context.Fines.Add(Fine1);
                context.Fines.Add(Fine2);
                context.Fines.Add(Fine3);
                context.Fines.Add(Fine4);

                context.SaveChanges();

            }
        }
    }
}
