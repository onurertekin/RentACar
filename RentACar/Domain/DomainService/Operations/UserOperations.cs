using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Base;
using DomainService.Exceptions;

namespace DomainService.Operations
{
    public class UserOperations : DbContextHelper
    {
        private readonly MainDbContext mainDbContext;
        public UserOperations(MainDbContext mainDbContext) : base(mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IList<User> Search(string? userName, string? password)
        {
            var query = mainDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(userName))
                query = mainDbContext.Users.Where(x => x.UserName == userName);

            if (!string.IsNullOrEmpty(password))
                query = mainDbContext.Users.Where(x => x.Password == password);

            return query.ToList();
        }

        public User GetSingle(int id)
        {
            var user = mainDbContext.Users.Where(x => x.Id == id).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı bulunamadı.");

            return user;
        }

        public void Create(string userName, string password)
        {
            var user = mainDbContext.Users.Where(x => x.UserName == userName).SingleOrDefault();
            if (user != null)
                throw new BusinessException(409, "Kullanıcı adı mevcut");

            var _user = new User();
            _user.UserName = userName;
            _user.Password = password;

            SaveEntity(_user);
        }

        public void Update(int id, string userName, string password)
        {
            var user = mainDbContext.Users.Where(x => x.Id == id).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı bulunamadı.");

            user.UserName = userName;
            user.Password = password;

            UpdateEntity(user);
        }

        public void Delete(int id)
        {
            var user = mainDbContext.Users.Where(x => x.Id == id).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı bulunamadı.");

            DeleteEntity(user);
        }
    }
}
