using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {

        }
        public CategoryModel(Category c)
        {
            Id = c.Id;

            Name = c.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}
