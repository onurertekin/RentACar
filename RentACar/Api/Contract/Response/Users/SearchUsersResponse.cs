namespace Contract.Response.Users
{
    public class SearchUsersResponse
    {
        public class User
        {
            public string userName { get; set; }
        }
        public SearchUsersResponse()
        {
            users = new List<User>();
        }
        public List<User> users { get; set; }
    }
}
