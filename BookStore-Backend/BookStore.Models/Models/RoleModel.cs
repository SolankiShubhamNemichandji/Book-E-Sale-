using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class RoleModel
    {
        public RoleModel()
        {

        }
        public RoleModel(Role c)
        {
            Id = c.Id;

            Name = c.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}
