using OneBeyondApi.Model;
namespace OneBeyondApi.DataAccess
{
    public interface IFineRepository
    {
        public List<Fine>? GetAllFines();
        public Fine? GetFine(Guid Id);
        public List<Fine>? SearchFines(FineSearch search);
        public Fine PayFine(Guid Id);
    }
}