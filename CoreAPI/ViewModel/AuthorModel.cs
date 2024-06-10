namespace CoreAPI.ViewModel;

public class AuthorModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    private List<PostModel> Posts { get; set; }
}