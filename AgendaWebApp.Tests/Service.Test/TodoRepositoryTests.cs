using AgendaWebApp.Models;
using AgendaWebApp.Service;
using Autofac.Extras.Moq;
using Elfie.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgendaWebApp.Tests.Service.Test
{
    public class TodoRepositoryTests
    {
        [Fact]
        public Task TodoItemModelRepository_GetAll()
        {
            // GetStrict - method can be called but no other methods can be called
            // GetLoose - just tests if the one method was called
            using (var mock = AutoMock.GetLoose())
            {
                // Mock mocks the _context field in the repository class
                mock.Mock<ITodoItemModelRepository>()
                    .Setup(x => x.GetAll())
                    // Task. enables this to work for return types of Task
                    .Returns(Task.FromResult<IEnumerable<TodoItemModel>>(GetAllSample()));

                var repo = mock.Create<TodoItemModelRepository>();
                var expected = GetAllSample();

                // This should return the same values that are in the GetAllSample
                var actual = repo.GetAll();

                // Assert that the GetAll() method does not fail
                Assert.True(actual != null);
                for(int i = 0; i < expected.ToList().Count; i++)
                {
                    Assert.Equal(expected.ToList()[i].Name, actual.ToList()[i].Name);
                    Assert.Equal(expected.ToList()[i].Description, actual.ToList()[i].Description);
                }
            }
        }

        private IEnumerable<TodoItemModel> GetAllSample()
        {
            IEnumerable<TodoItemModel> output = new List<TodoItemModel>
            {
                new TodoItemModel
                {
                    Id = 1,
                    Name = "Do this 1",
                    Description = "Do this thing 1",
                    Importance = 0,
                    CreationDate = DateTime.Now,
                    FinishedDate = null,
                    Finished = false,
                    GroupModelId = 1
                },
                new TodoItemModel
                {
                    Id = 2,
                    Name = "Do this 2",
                    Description = "Do this thing 2",
                    Importance = 0,
                    CreationDate = DateTime.Now,
                    FinishedDate = null,
                    Finished = false,
                    GroupModelId = 1
                }
            };

            return output;
        }
    }
}
