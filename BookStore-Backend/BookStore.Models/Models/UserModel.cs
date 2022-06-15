using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(User model)
        {
            id = model.Id;
            firstName = model.Firstname;
            lastName = model.Lastname;
            email = model.Email;
            roleId = model.Roleid;
        }
        public int id { get; set; }
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public int roleId { get; set; }
    }
}
