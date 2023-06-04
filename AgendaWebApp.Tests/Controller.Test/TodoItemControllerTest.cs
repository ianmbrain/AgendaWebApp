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
    public class TodoItemControllerTest
    {

        [Fact]
        public void GetAll_ReturnsAViewResult_ListOfTasksNoUserLoggedIn()
        {
            //Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(GetTodoItems());

            var controller = new TodoItemModelController(mockRepo.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = httpContextAccessorMock.Object.HttpContext;

            // Act
            var result = controller.AllTasks();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Views/GroupModel/NoLoginView.cshtml", viewResult.ViewName);
        }

        [Fact]
        public void GetAll_ReturnsAViewResult_ListOfTasks()
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

            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                // Tasks are ordered by importance as that is how the repository orders them.
                .Returns(GetTodoItems().OrderByDescending(model => model.Importance).ToList());

            var controller = new TodoItemModelController(mockRepo.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = httpContextAccessorMock.Object.HttpContext;

            // Act
            var result = controller.AllTasks();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TodoItemModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Details_ReturnsAViewResult_OneTask()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetById(1))
                .Returns(GetTodoItems().ElementAt(0));
            var controller = new TodoItemModelController(mockRepo.Object);
            // Act
            var result = controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TodoItemModel>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal("Name1", model.Name);
            Assert.Equal("Description1", model.Description);
        }

        [Fact]
        public void CreatePost_RedirectToAction_CreateTask()
        {
            // Arrange
            var todoItem = new TodoItemModel
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                Importance = 0,
                GroupModelId = 1
            };

            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.Add(todoItem))
                .Returns(true);
            var controller = new TodoItemModelController(mockRepo.Object);

            // Act
            var result = controller.Create(CreateViewModel());

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetTasksByGroupId", redirectToActionResult.ActionName);
        }

        [Fact]
        public void CreateNoGroupPost_RedirectToAction_CreateTask()
        {
            // Arrange
            var todoItem = new TodoItemModel
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                Importance = 0,
                GroupModelId = 1
            };

            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.Add(todoItem))
                .Returns(true);
            var controller = new TodoItemModelController(mockRepo.Object);

            // Act
            var result = controller.Create(CreateViewModel());

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetTasksByGroupId", redirectToActionResult.ActionName);
        }

        [Fact]
        public void EditGet_ReturnsAEditViewModel()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetById(1))
                .Returns(GetTodoItems().ElementAt(0));
            var controller = new TodoItemModelController(mockRepo.Object);

            // Act
            var result = controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditItemViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal("Name1", model.Name);
            Assert.Equal("Description1", model.Description);
        }

        [Fact]
        public void EditPost_RedirectToAction_UpdatedTask()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetByIdNoTracking(1))
                .Returns(GetTodoItems().ElementAt(0));
            mockRepo.Setup(repo => repo.Update(GetTodoItems().ElementAt(0)))
                .Returns(true);
            var controller = new TodoItemModelController(mockRepo.Object);

            var editViewModel = new EditItemViewModel
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                Importance = 0,
                GroupModelId = 1
            };

            // Act
            var result = controller.Edit(1, editViewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetTasksByGroupId", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_RedirectToAction_DeleteTask()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetById(1))
                .Returns(GetTodoItems().ElementAt(0));
            mockRepo.Setup(repo => repo.Delete(GetTodoItems().ElementAt(0)))
                .Returns(true);
            var controller = new TodoItemModelController(mockRepo.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Finish_RedirectToAction_FinishTask()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetById(1))
                .Returns(GetTodoItems().ElementAt(0));
            mockRepo.Setup(repo => repo.Update(GetTodoItems().ElementAt(0)))
                .Returns(true);
            var controller = new TodoItemModelController(mockRepo.Object);

            // Act
            var result = controller.Finish(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        private List<TodoItemModel> GetTodoItems()
        {
            var tasks = new List<TodoItemModel>();
            tasks.Add(new TodoItemModel()
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                Importance = (Data.Enum.ImportanceEnum)0,
                CreationDate = new DateTime(2023, 5, 1),
                GroupModelId = 1
            });
            tasks.Add(new TodoItemModel()
            {
                Id = 2,
                Name = "Name2",
                Description = "Description2",
                Importance = (Data.Enum.ImportanceEnum)1,
                CreationDate = new DateTime(2023, 5, 1),
                GroupModelId = 1
            });
            return tasks;
        }

        private CreateTodoItemViewModel CreateViewModel()
        {
            return new CreateTodoItemViewModel
            {
                Name = "Name1",
                Description = "Description1",
                Importance = 0,
                GroupModelId = 1
            };
        }
    }
}
