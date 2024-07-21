using Contract.Request.Users;
using Contract.Response.Users;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rent-a-car/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserOperations userOperations;
        public UsersController(UserOperations userOperations)
        {
            this.userOperations = userOperations;
        }

        [HttpGet]
        public ActionResult<SearchUsersResponse> Search([FromQuery] SearchUsersRequest request)
        {
            var users = userOperations.Search(request.userName, request.password);

            var response = new SearchUsersResponse();
            foreach (var user in users)
            {
                response.users.Add(new SearchUsersResponse.User()
                {
                    userName = user.UserName,
                    password = user.Password,
                });
            }
            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        public ActionResult<GetSingleUserResponse> GetSingle(int id)
        {
            var user = userOperations.GetSingle(id);

            var response = new GetSingleUserResponse();
            response.id = id;
            response.userName = user.UserName;
            response.password = user.Password;

            return response;
        }

        [HttpPost]
        public void Create([FromBody] CreateUserRequest request)
        {
            userOperations.Create(request.userName, request.password);
        }

        [HttpPut("{id}")]
        public void Update([FromBody] UpdateUserRequest request, int id)
        {
            userOperations.Update(id, request.userName, request.password);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userOperations.Delete(id);
        }
    }
}
