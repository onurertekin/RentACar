using System.ComponentModel.DataAnnotations;

namespace Contract.Request.Users
{
    public class CreateUserRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
