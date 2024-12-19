using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmApp.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }


        // Etiketleri göster
        public IEnumerable<SelectListItem> Tags { get; set; } // liste olduğu için IEnumerable
        // Etiketleri getir
        public string[] SelectedTags { get; set; } = Array.Empty<string>(); // birden fazla seçim yapmak için array


    }
}
