using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
namespace BookStore.Repositories
{
    public class UserRepository : BaseRepository
    {
        
        public ListResponse<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {
                keyword = keyword?.ToLower()?.Trim();
                var query = _context.Users.Where(s =>keyword==null || s.Firstname.ToLower().Contains(keyword) || s.Lastname.ToLower().Contains(keyword)).AsQueryable();
                int totalrecords = query.Count();
                List<User> userlist = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new ListResponse<User>()
                {
                    records = userlist,
                    totalRecords=totalrecords,
                };
        }

        public User Login(User model)
        {
            return _context.Users.FirstOrDefault(l => l.Email.Equals(model.Email.ToLower()) && l.Password.Equals(model.Password));

        }

        public User Register(User model)
        {

            var entry = _context.Users.Add(model);
            _context.SaveChanges();
            return entry.Entity;

        }

        public User GetUser(int id)
        {
              return _context.Users.FirstOrDefault((u => u.Id == id));
        }

        public User updateUser(User model)
        {
            UserRepository _repository=new UserRepository();
            var users = _repository.GetUser(model.Id);
            if (users == null)
            {
                return null;
            }
            var updatedUser=_context.Users.Update(model);
            _context.SaveChanges();
            return updatedUser.Entity;
        }

        public bool deleteUser(int id)
        {
            UserRepository _repository = new UserRepository();
            var model = _repository.GetUser(id);
            if (model == null)
            {
                return false;
            }
            _context.Users.Remove(model);
            _context.SaveChanges();
            return true;
        }
    }
}
