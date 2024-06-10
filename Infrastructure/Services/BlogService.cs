using AutoMapper;
using Core.Entities;
using Infrastructure.DAL;
using YukiAPI.Dtos;

namespace Infrastructure.Services;

public interface IBlogService
{
    PostDto GetPostById(int id, bool includeAuthor);
    PostDto CreateBlogPost(PostDto postDto);
}

public class BlogService : IBlogService
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public BlogService(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public PostDto GetBlogPostById(int id, bool includeAuthor)
    {
        var entity = _repository.GetById(id, includeAuthor);
        if (entity == null)
        {
            return null;
        }

        return new PostDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Summary = entity.Content.Length > 100 ? entity.Content.Substring(0, 100) : entity.Content, 
            AuthorName = entity.Author?.Name,
            AuthorId = entity.Author.Id
        };
    }
    
    public PostDto GetPostById(int id, bool includeAuthor)
    {
        var entity = _repository.GetById(id, includeAuthor);
        if (entity == null)
        {
            return null;
        }

        return _mapper.Map<PostDto>(entity);
    }

    public PostDto CreateBlogPost(PostDto postDto)
    {
        var entity = _mapper.Map<Post>(postDto);
        _repository.Add(entity);
        return _mapper.Map<PostDto>(entity);
    }
}