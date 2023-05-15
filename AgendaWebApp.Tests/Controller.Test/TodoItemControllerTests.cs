using AgendaWebApp.Controllers;
using AgendaWebApp.Models;
using AgendaWebApp.Service;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWebApp.Tests.Controller.Test
{
    public class TodoItemControllerTests
    {
        private TodoItemModelController _itemController;
        private ITodoItemModelRepository _context;

        public TodoItemControllerTests() {
            // Dependency so that the tests are run on mocked data rather than the production database
            _context = A.Fake<ITodoItemModelRepository>();

            // SUT
            _itemController = new TodoItemModelController(_context);
        }

        [Fact]
        public void TodoItemController_Index_ReturnsSuccess()
        {
            //Arrange - what to bring in
            var items = A.Fake<ICollection<TodoItemModel>>();
            A.CallTo(() => _context.GetAll()).Returns(items);

            //Act
            var result = _itemController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void TodoItemController_Details_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var item = A.Fake<TodoItemModel>();
            A.CallTo (() => _context.GetById(id)).Returns(item);

            //Act
            var result = _itemController.Details(id);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
