using Microsoft.AspNetCore.Mvc;
using FilmApp.Web.Models.ViewModels;
using FilmApp.Web.Data;
using FilmApp.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using FilmApp.Web.Repositories;
///  BU SAYFADA YER ALAN BLOGGIE DB CONTEXT YAPILARI TagRepository.cs aktardık..
namespace FilmApp.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();  // başlangıç html dosyasını gösterdiğinden asenkron yapıya gerek yok
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {

            //  Domain modelini etiketlemek için addTagRequest'i yapılandırıyoruzz
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            // database e bağlanıyor asenkron yaparım
            //await bloggieDbContext.Tags.AddAsync(tag); // await ekleme sebebimiz asenkron yapıya uygun hale getirmek
            //await bloggieDbContext.SaveChangesAsync(); bu iki satır TagRepository'de AddAsync metoduna taşındı
            // bu komut yazılmazsa değişiklikler kaydedilmiyor çok önemli
            // await ile işlemin gerçekleşmesini beklemiyoruz

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List"); // işlemi string olarak alıyor
        }



        /// etiketleri liste halinde ekranda göstereceğiz
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            // etiketleri okumak için dbContext kullanacağız
            var tags = await tagRepository.GetAllAsync();


            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if(updatedTag != null)
            {
                //  başarılı geri bildirim
            }
            else
            {
                // başarısız geri bildirim
            }

            //çalışmadığına dair bildirim gönder
            return RedirectToAction("Edit", new { id = editTagRequest.Id });// edit metodu parametre aldığı için id objesiyle gönderiyoruz


        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if(deletedTag != null)
            {
                // başarılı geri bildirim
                RedirectToAction("List");
            }

            //  hata bildirimi göster
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }




    }
}
