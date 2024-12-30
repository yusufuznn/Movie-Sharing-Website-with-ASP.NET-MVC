using FilmApp.Web.Models.ViewModels;
using FilmApp.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FilmApp.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();



            if (blogPost != null)
            {
                var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Tags = blogPost.Tags,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    TotalLikes = totalLikes
                };
            }

            return View(blogDetailsViewModel);
        }
    }
}
