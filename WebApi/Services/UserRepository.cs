using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    internal class UserRepository : IRepository<User>
    {
        private readonly UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> ReadAll()
        {
            return context.Users.Include(x => x.UserNumbers).OrderBy(x => x.IdNumber);
        }

        public User? Read(string id)
        {
            return context.Users
                .Include(x => x.UserNumbers)
                .SingleOrDefault(x => x.IdNumber == id);
        }

        public void Add(User user)
        {
            context.Add(user);
            context.SaveChanges();
        }

        public void Edit(string id, User user)
        {
            User editedUser = context.Users.Single(x => x.IdNumber == id);
            editedUser.Name = user.Name;
            editedUser.Email = user.Email;
            editedUser.Position = user.Position;
            editedUser.UserNumbers = user.UserNumbers;
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            User? user = context.Users.SingleOrDefault(x => x.IdNumber == id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
