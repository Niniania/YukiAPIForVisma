using AutoMapper;
using CoreAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using YukiAPI.Dtos;

namespace YukiAPI.Controllers;
[Produces("application/json")]
public class BlogPostController : Controller
{
    private readonly IBlogService _blogPostService;
    private readonly IMapper _mapper;
    
    public BlogPostController(IBlogService blogService, IMapper mapper)
    {
        _blogPostService = blogService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public ActionResult<PostModel> GetBlogPost(int id, [FromQuery] bool includeAuthor = false)
    {
        var blogPostDto = _blogPostService.GetPostById(id, includeAuthor);
        if (blogPostDto == null)
            return NotFound();

        var blogPostResponseModel = new PostResponseModel
        {
            Id = blogPostDto.Id,
            Title = blogPostDto.Title,
            Content = blogPostDto.Summary,
            AuthorName = blogPostDto.AuthorName,
        };

        return Ok(blogPostResponseModel);
    }
    
    [HttpPost]
    public ActionResult<PostModel> CreateBlogPost([FromBody] PostModel blogPostModel)
    {
        var blogPostDto = _mapper.Map<PostDto>(blogPostModel);
        var createdBlogPost = _blogPostService.CreateBlogPost(blogPostDto);
        var blogPostResponseModel = _mapper.Map<PostResponseModel>(createdBlogPost);
        return CreatedAtAction(nameof(GetBlogPost), new { id = blogPostResponseModel.Id }, blogPostResponseModel);
    }
}