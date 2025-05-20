using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        public CatalogueRepository()
        {
        }

        public List<BookStock> GetCatalogue()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Loan)?
                    .ThenInclude(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.Loan)?
                    .ThenInclude(x => x.Fine)?
                    .ToList();
                return list;
            }
        }

        public List<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Loan)
                    .ThenInclude(x => x.Book)?
                    .ThenInclude(x => x.Author)
                    .AsQueryable();

                if (search != null && list.Any(x => x.Loan != null))
                {
                    if (!string.IsNullOrEmpty(search.Author)) {
                        list = list.Where(x => x.Loan.Book.Author.Name.Contains(search.Author));
                    }
                    if (!string.IsNullOrEmpty(search.BookName)) {
                        list = list.Where(x => x.Loan.Book.Name.Contains(search.BookName));
                    }
                }
                    
                return list.ToList();
            }
        }
    }
}
