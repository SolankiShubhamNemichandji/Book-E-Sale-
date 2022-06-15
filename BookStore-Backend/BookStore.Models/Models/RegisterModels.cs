using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {

        }

        public RegisterModel(User model)
        {
            //Id = model.Id;
            Firstname = model.Firstname;
            Lastname = model.Lastname;  
            Email = model.Email;    
            Password = model.Password;
            Roleid = model.Roleid;
        }
        //public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Roleid { get; set; }
    }
}
