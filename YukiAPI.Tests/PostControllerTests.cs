using NUnit.Framework;
using Moq;
using AutoMapper;
using Core.Entities;
using CoreAPI.ViewModel;
using Infrastructure.DAL;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Legacy;
using YukiAPI.Controllers;
using YukiAPI.Dtos;

namespace YukiAPI.Tests
{
  [TestFixture]
      public class BlogPostControllerTests
      {
          private Mock<IBlogService> _blogServiceMock;
          private Mock<IMapper> _mapperMock;
          private BlogPostController _controller;
  
          [SetUp]
          public void Setup()
          {
              _blogServiceMock = new Mock<IBlogService>();
              _mapperMock = new Mock<IMapper>();
              _controller = new BlogPostController(_blogServiceMock.Object, _mapperMock.Object);
          }
          
           [Test]
                  public void GetBlogPost_ReturnsOk_WhenPostExists()
                  {
                      // Arrange
                      var postDto = new PostDto { Id = 1, Title = "Test", Summary = "Test Content", AuthorName = "Author" };
                      _blogServiceMock.Setup(s => s.GetPostById(1, false)).Returns(postDto);
          
                      // Act
                      var result = _controller.GetBlogPost(1, false);
          
                      // Assert
                      ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
                      var okResult = result.Result as OkObjectResult;
                      var responseModel = okResult.Value as PostResponseModel;
                      ClassicAssert.AreEqual(1, responseModel.Id);
                      ClassicAssert.AreEqual("Test", responseModel.Title);
                      ClassicAssert.AreEqual("Test Content", responseModel.Content);
                      ClassicAssert.AreEqual("Author", responseModel.AuthorName);
                  }
            [Test]
                  public void GetBlogPost_ReturnsNotFound_WhenPostDoesNotExist()
                  {
                      // Arrange
                      _blogServiceMock.Setup(s => s.GetPostById(1, false)).Returns((PostDto)null);
          
                      // Act
                      var result = _controller.GetBlogPost(1, false);
          
                      // Assert
                      ClassicAssert.IsInstanceOf<NotFoundResult>(result.Result);
                  }
          
                  [Test]
                  public void CreateBlogPost_ReturnsCreatedAtAction_WhenPostIsCreated()
                  {
                      // Arrange
                      var postModel = new PostModel { Title = "Test", Content = "Test Content", AuthorId = 1 };
                      var postDto = new PostDto { Id = 1, Title = "Test",  Summary = "Test Content", AuthorId = 1 };
                      var createdPostDto = new PostDto { Id = 1, Title = "Test", Summary = "Test Content", AuthorName = "Author" };
          
                      _mapperMock.Setup(m => m.Map<PostDto>(postModel)).Returns(postDto);
                      _blogServiceMock.Setup(s => s.CreateBlogPost(postDto)).Returns(createdPostDto);
                      _mapperMock.Setup(m => m.Map<PostResponseModel>(createdPostDto)).Returns(new PostResponseModel
                      {
                          Id = 1,
                          Title = "Test",
                          Content = "Test Content",
                          AuthorName = "Author"
                      });
          
                      // Act
                      var result = _controller.CreateBlogPost(postModel);
          
                      // Assert
                      ClassicAssert.IsInstanceOf<CreatedAtActionResult>(result.Result);
                      var createdAtActionResult = result.Result as CreatedAtActionResult;
                      var responseModel = createdAtActionResult.Value as PostResponseModel;
                      ClassicAssert.AreEqual(1, responseModel.Id);
                      ClassicAssert.AreEqual("Test", responseModel.Title);
                      ClassicAssert.AreEqual("Test Content", responseModel.Content);
                      ClassicAssert.AreEqual("Author", responseModel.AuthorName);
                  }

}
}
