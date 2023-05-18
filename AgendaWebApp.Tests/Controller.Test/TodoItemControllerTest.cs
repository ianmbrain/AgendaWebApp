using AgendaWebApp.Controllers;
using AgendaWebApp.Models;
using AgendaWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWebApp.Tests.Controller.Test
{
    public class TodoItemControllerTest
    {
        [Fact]
        public void Index_ReturnsAViewResult_ListOfTasks()
        {
            // Arrange
            var mockRepo = new Mock<ITodoItemModelRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(GetTodoItems());
            var controller = new TodoItemModelController(mockRepo.Object);

            // Act
            var result = controller.AllTasks();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TodoItemModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<TodoItemModel> GetTodoItems()
        {
            var sessions = new List<TodoItemModel>();
            sessions.Add(new TodoItemModel()
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                Importance = 0,
                CreationDate = new DateTime(2023, 5, 1),
                GroupModelId = 1
            });
            sessions.Add(new TodoItemModel()
            {
                Id = 2,
                Name = "Name2",
                Description = "Description2",
                Importance = 0,
                CreationDate = new DateTime(2023, 5, 1),
                GroupModelId = 1
            });
            return sessions;
        }
    }
}
