using AgendaWebApp.Controllers;
using AgendaWebApp.Models;
using AgendaWebApp.Service;
using AgendaWebApp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace AgendaWebApp.Tests.Controller.Test
{
    public class GroupModelControllerTest
    {
        [Fact]
        public void Index_ReturnsAViewResult_ListOfGroups()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "test@test.com"),
            }, "mock"));

            var httpContext = new DefaultHttpContext();
            httpContext.User = user;

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var mockRepo = new Mock<IGroupModelRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(Groups());

            var controller = new GroupModelController(mockRepo.Object, httpContextAccessorMock.Object);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = httpContextAccessorMock.Object.HttpContext;

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GroupModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void CreatePost_RedirectToAction_CreateGroup()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "test@test.com"),
            }, "mock"));

            var httpContext = new DefaultHttpContext();
            httpContext.User = user;

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var groupVM = new CreateGroupViewModel
            {
                GroupId = 1,
                Name = "Name1",
                Description = "Description1",
                AppUserId = "123"
            };

            var mockRepo = new Mock<IGroupModelRepository>();
            mockRepo.Setup(repo => repo.Add(Groups().ElementAt(0)))
                .Returns(true);
            var controller = new GroupModelController(mockRepo.Object, httpContextAccessorMock.Object);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = httpContextAccessorMock.Object.HttpContext;

            // Act
            var result = controller.Create(groupVM);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        private List<GroupModel> Groups()
        {
            var groups = new List<GroupModel>();
            groups.Add(new GroupModel()
            {
                GroupId = 1,
                Name = "Name1",
                Description = "Description1"
            });
            groups.Add(new GroupModel()
            {
                GroupId = 2,
                Name = "Name2",
                Description = "Description2"
            });
            return groups;
        }
    }
}
