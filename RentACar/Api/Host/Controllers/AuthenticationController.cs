using Contract.Request.Authentication;
using Contract.Response.Authentication;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rent-a-car")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationOperations authenticationOperations;
        public AuthenticationController(AuthenticationOperations authenticationOperations)
        {
            this.authenticationOperations = authenticationOperations;
        }

        [HttpPost("authentication")]
        public ActionResult<AuthenticationResponse> Authentication([FromBody] AuthenticationRequest request)
        {
            var token = authenticationOperations.Authentication(request.userName, request.password);

            var response = new AuthenticationResponse();
            response.token = token;
            return new JsonResult(response);
        }
    }
}
