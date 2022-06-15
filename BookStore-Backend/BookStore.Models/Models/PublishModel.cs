using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class PublishModel
    {
        public PublishModel()
        {

        }
        public PublishModel(Publisher p)
        {
            Id = p.Id;
            Name = p.Name;
            Address = p.Address;
            Contact = p.Contact;

        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Contact { get; set; } = null!;
    }
   
}
