using FilmApp.Web.Models.Domain;

namespace FilmApp.Web.Repositories
{
    public interface ITagRepository
    {
        /// veritabanına nasıl erişeceğiz, veritabanında tag tablosuna erişme

        Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery = null);

        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);

    }
}
