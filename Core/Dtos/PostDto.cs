namespace YukiAPI.Dtos;

public class PostDto
{
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string AuthorName { get; set; }
}