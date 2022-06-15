using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public string? Name { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
