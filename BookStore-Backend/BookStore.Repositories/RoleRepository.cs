using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class RoleRepository:BaseRepository
    {

        public ListResponse<Role> GetRoles(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Roles.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Role> roles = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Role>()
            {
                records = roles,
                totalRecords = totalRecords
            };

        }
        public Role GetRole(int id)
        {
            return _context.Roles.FirstOrDefault(c => c.Id == id);
        }

        public Role AddRole(Role role)
        {
            var entry = _context.Roles.Add(role);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Role UpdateRole(Role role)
        {
            RoleRepository _rolerepository = new RoleRepository();
            var update = _rolerepository.GetRole(role.Id);
            if (update == null)
            {
                return null;
            }
            var entry = _context.Roles.Update(role);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteRole(int id)
        {
            RoleRepository _rolerepository = new RoleRepository();
            var role = _rolerepository.GetRole(id);
            if (role == null)
                return false;
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
    }
}

