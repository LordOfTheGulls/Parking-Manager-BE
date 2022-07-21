using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.User;
using PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries.User;

namespace PM_API.Controllers
{
    [Route("[controller]")]
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

        [HttpPost("update/{userId}")]
        public async Task<ActionResult> UpdateUser(Int64 userId, [FromBody] UserDTO user, CancellationToken token = default)
        {
            user.UserId = userId;

            await _commandDispatcher.DispatchAsync<UpdateUserCommand>(
                new UpdateUserCommand()
                {
                    User = user,
                }
            );
            return Ok();
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUser(CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<AddUserCommand>(
                new AddUserCommand(){}
            );
            return Ok();
        }

        [HttpPost("delete/{userId}")]
        public async Task<ActionResult> AddUser(Int64 userId, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<DeleteUserCommand>(
                new DeleteUserCommand() {
                    UserId = userId,
                }
            );
            return Ok();
        }

        [HttpPost("permission/update/{userId}")]
        public async Task<ActionResult> UpdatePermission(Int64 userId, [FromBody] UserRightDto userRight, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<UpdatePermissionCommand>(
                new UpdatePermissionCommand()
                {
                    UserId = userId,
                    UserRights = userRight,
                }
            );
            return Ok();
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<ActionResult> GetUser(Int64 userId,CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetUserQuery, UserDTO>(
                new GetUserQuery()
                {
                    UserId = userId
                }
            );
            return Ok(result);
        }

        [HttpPost("all")]
        [ProducesResponseType(typeof(PagingResult<UserDTO>), 200)]
        public async Task<ActionResult> GetAllUsers([FromBody] FilterDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetUsersQuery, PagingResult<UserDTO>>(
                new GetUsersQuery()
                {
                    Filter = filter
                }
            );
            return Ok(result);
        }

        [HttpPost("permissions/{userId}")]
        [ProducesResponseType(typeof(UserRightDto), 200)]
        public async Task<ActionResult> GetUserPermissions(Int64 userId, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetUserPermissionsQuery, UserRightDto>(
                new GetUserPermissionsQuery()
                {
                    UserId = userId
                }
            );
            return Ok(result);
        }
    }
}
