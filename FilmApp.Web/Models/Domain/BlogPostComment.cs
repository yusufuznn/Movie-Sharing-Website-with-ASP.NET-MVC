namespace FilmApp.Web.Models.Domain
{
    public class BlogPostComment
    {
        public Guid Id { get; set; }
        public String Description { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
