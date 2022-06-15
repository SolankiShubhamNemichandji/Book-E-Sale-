using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class LoginModel
    {
        public LoginModel()
        {

        }
        public LoginModel(User model)
        {
            Email = model.Email;
            Password = model.Password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
