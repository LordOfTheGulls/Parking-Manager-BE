using Microsoft.AspNetCore.Mvc;
using PM_CQRS.Dispatcher;

namespace PM_API.Controllers
{
    public class UserController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
    }
}
