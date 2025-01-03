using System.Diagnostics;
using FilmApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using FilmApp.Web.Models;
using FilmApp.Web.Repositories;
using FilmApp.Web.Models.ViewModels;

namespace FilmApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,
            IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(string tag = null, int page = 1)
        {
            const int pageSize = 6; // Her sayfada kaç paylaþým göstereceðimizi belirtiyoruz

            // Paylaþýmlarý alýyoruz (eðer tag varsa filtreleme yapýyoruz)
            var blogPosts = await blogPostRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(tag))
            {
                blogPosts = blogPosts.Where(bp => bp.Tags.Any(t => t.Name == tag)).ToList();
            }

            // Toplam paylaþým sayýsý
            var totalPosts = blogPosts.Count();

            // Sayfalama iþlemi
            var pagedPosts = blogPosts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Tag'leri alýyoruz
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = pagedPosts,
                Tags = tags,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        






    }
}
