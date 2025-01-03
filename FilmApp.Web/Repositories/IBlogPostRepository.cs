using FilmApp.Web.Models.Domain;

namespace FilmApp.Web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync(Guid id); // null olabileceği için task içine ?

        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

        Task<BlogPost> AddAsync(BlogPost blogPost);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost); // işlem yapılacak entity'nin bulunmama durumu da var

        Task<BlogPost?> DeleteAsync(Guid id);  // bu yüzden ? kullanılır
        
    }
}
