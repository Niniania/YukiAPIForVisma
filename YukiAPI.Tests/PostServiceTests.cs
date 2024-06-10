using NUnit.Framework;
using Moq;
using AutoMapper;
using Core.Entities;
using Infrastructure.DAL;
using Infrastructure.Services;
using NUnit.Framework.Legacy;
using YukiAPI.Dtos;

namespace YukiAPI.Tests
{
    [TestFixture]
    public class BlogServiceTests
    {
        private Mock<IPostRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private BlogService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPostRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new BlogService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public void GetBlogPostById_ReturnsBlogPost_WhenBlogPostExists()
        {
            // Arrange
            var blogPost = new Post { Id = 1, Title = "Test", Content = "Test Content", AuthorId = 1, Author = new Author { Id = 1, Name = "Author" } };
            var blogPostDto = new PostDto { Id = 1, Title = "Test", Summary = "Test Content", AuthorName = "Author", AuthorId = 1 };

            _repositoryMock.Setup(repo => repo.GetById(1, false)).Returns(blogPost);
            _mapperMock.Setup(m => m.Map<PostDto>(blogPost)).Returns(blogPostDto);

            // Act
            var result = _service.GetBlogPostById(1, false);

            // Assert
            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(1, result.Id);
            ClassicAssert.AreEqual("Test", result.Title);
            ClassicAssert.AreEqual("Test Content", result.Summary);
            ClassicAssert.AreEqual("Author", result.AuthorName);
            ClassicAssert.AreEqual(1, result.AuthorId);
        }

        [Test]
        public void GetBlogPostById_ReturnsNull_WhenBlogPostDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetById(1, false)).Returns((Post)null);

            // Act
            var result = _service.GetBlogPostById(1, false);

            // Assert
            ClassicAssert.Null(result);
        }

        [Test]
        public void GetPostById_MapsAndReturnsPostDto_WhenBlogPostExists()
        {
            // Arrange
            var blogPost = new Post { Id = 1, Title = "Test", Content = "Test Content", AuthorId = 1, Author = new Author { Id = 1, Name = "Author" } };
            var blogPostDto = new PostDto { Id = 1, Title = "Test", Summary = "Test Content", AuthorName = "Author", AuthorId = 1 };

            _repositoryMock.Setup(repo => repo.GetById(1, false)).Returns(blogPost);
            _mapperMock.Setup(m => m.Map<PostDto>(blogPost)).Returns(blogPostDto);

            // Act
            var result = _service.GetPostById(1, false);

            // Assert
            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(1, result.Id);
            ClassicAssert.AreEqual("Test", result.Title);
            ClassicAssert.AreEqual("Test Content", result.Summary);
            ClassicAssert.AreEqual("Author", result.AuthorName);
            ClassicAssert.AreEqual(1, result.AuthorId);
        }

        [Test]
        public void CreateBlogPost_AddsAndReturnsPostDto()
        {
            // Arrange
            var postDto = new PostDto { Id = 1, Title = "Test",  Description = "Test Content", Summary = "Test Content", AuthorId = 1 };
            var post = new Post { Id = 1, Title = "Test", Content = "Test Content", AuthorId = 1 };

            _mapperMock.Setup(m => m.Map<Post>(postDto)).Returns(post);
            _mapperMock.Setup(m => m.Map<PostDto>(post)).Returns(postDto);
            _repositoryMock.Setup(repo => repo.Add(It.IsAny<Post>())).Callback<Post>(p => p.Id = 1);

            // Act
            var result = _service.CreateBlogPost(postDto);

            // Assert
            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(1, result.Id);
            ClassicAssert.AreEqual("Test", result.Title);
            ClassicAssert.AreEqual("Test Content", result.Description);
            ClassicAssert.AreEqual("Test Content", result.Summary);
            ClassicAssert.AreEqual(1, result.AuthorId);
        }
        
    }
}
