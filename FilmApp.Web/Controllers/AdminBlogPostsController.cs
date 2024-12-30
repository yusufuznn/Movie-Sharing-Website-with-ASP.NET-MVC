using FilmApp.Web.Models.Domain;
using FilmApp.Web.Models.ViewModels;
using FilmApp.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }


        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // etiketleri repositoriden al
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            // view modeli domain model ile eşliyoruz
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };


            // tagleri seçili tag ile eşliyoruz:,
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            // tagleri tekrar domain model ile eşliyoruz
            blogPost.Tags = selectedTags; 


            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]   
        public async Task<IActionResult> List()
        {
            // data getirmek için repository çağrılır. DB'den data getirmek repository'nin görevidir
            var blogPosts = await blogPostRepository.GetAllAsync();


            return View(blogPosts);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // sonucu repositoryden alalım
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if(blogPost != null)
            {
                // domain modeli view modele çekelim
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    PublishedDate = blogPost.PublishedDate,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                
                return View(model);
            }
            // datayı görüntülemek için iletelim
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            // domain modeli view modele eşleyelim
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                ShortDescription = editBlogPostRequest.ShortDescription,
                Visible = editBlogPostRequest.Visible
            };

            // etiketleri domain modelle eşleyelim
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTagId, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);
                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;


            // repositorye güncelleme bilgisini gönderelim
            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updatedBlog != null)
            {
                // başarılı geri bildirim
                return RedirectToAction("Edit");
            }
            // hata bildirimi
            return RedirectToAction("Edit");

            // get metoduna yönlendirelim

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            // silme işlemi için repositorye yönlendirme yapalım
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

            if (deletedBlogPost != null)
            {
                // başarılı geri bildirim
                return RedirectToAction("List");
            }
            // hata bildirimi
            return RedirectToAction("Edit", new {id = editBlogPostRequest.Id});

            // sonucu ekranda gösterelim

        }
    }
}
