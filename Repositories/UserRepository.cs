using MJIoT_DBModel;
using System.Linq;

namespace MJIoT.Storage.Models
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string login, string password);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MJIoTDBContext context)
            : base(context)
        {
        }

        public User Get(string login, string password)
        {
            var user = Context.Users
                .Where(n => n.Login == login && n.Password == password)
                .FirstOrDefault();

            //if (userCheck == null)
            //{
            //    return null;
            //}

            //return userCheck.Id;

            return user;
        }

        public MJIoTDBContext Context
        {
            get { return _context as MJIoTDBContext; }
        }
    }
}
