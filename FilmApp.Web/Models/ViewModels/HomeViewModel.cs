using FilmApp.Web.Models.Domain;

namespace FilmApp.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public int TotalPages { get;  set; }
        public int CurrentPage { get;  set; }
    }
}
