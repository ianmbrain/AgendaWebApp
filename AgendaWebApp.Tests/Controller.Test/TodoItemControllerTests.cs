using AgendaWebApp.Service;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWebApp.Tests.Controller.Test
{
    public class TodoItemControllerTests
    {
        private ITodoItemModelRepository _context;

        public TodoItemControllerTests() {
            // Dependency so that the tests are run on mocked data rather than the production database
            _context = A.Fake<ITodoItemModelRepository>();
        }
    }
}
