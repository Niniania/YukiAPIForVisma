using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.DAL;

public interface IPostRepository
{
    Post? GetById(int id, bool includeAuthor);
    void Add(Post entity);
}
public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;

    public PostRepository(BlogDbContext context)
    {
        _context = context;
    }

    public Post? GetById(int id, bool includeAuthor)
    {
        if (includeAuthor)
        {
            return _context.BlogPosts
                .Include(post => post.Author)
                .FirstOrDefault(post => post.Id == id);
        }
        return _context.BlogPosts
            .FirstOrDefault(post => post.Id == id);
    }
    
    public void Add(Post entity)
    {
        _context.BlogPosts.Add(entity);
        _context.SaveChanges();
    }
}