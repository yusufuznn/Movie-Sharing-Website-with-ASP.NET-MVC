using FilmApp.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FilmApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository ımageRepository;

        public ImagesController(IImageRepository ımageRepository)
        {
            this.ımageRepository = ımageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file) 
        {
            // repository çağrısı
            var imageURL = await ımageRepository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Bir şeyler yanlış gitti!",null,(int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new {link = imageURL });
        }
    }
}
