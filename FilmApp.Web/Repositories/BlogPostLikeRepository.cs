
using FilmApp.Web.Data;
using FilmApp.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FilmApp.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike?> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            // Daha önce aynı kullanıcı ve blog gönderisi için bir beğeni var mı?
            var existingLike = await bloggieDbContext.BlogPostLike
                .FirstOrDefaultAsync(x => x.BlogPostId == blogPostLike.BlogPostId && x.UserId == blogPostLike.UserId);

            if (existingLike != null)
            {
                // Kullanıcı zaten bu blogu beğenmiş
                return null;
            }

            // Yeni bir beğeni ekle
            await bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike
                .Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike
                .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
