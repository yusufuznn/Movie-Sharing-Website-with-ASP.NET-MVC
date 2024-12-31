using Microsoft.AspNetCore.Identity;

namespace FilmApp.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
