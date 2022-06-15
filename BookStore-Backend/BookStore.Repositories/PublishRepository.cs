using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;


using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class PublishRepository:BaseRepository
    {

        public ListResponse<Publisher> GetPublishers(int pagendex,int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Publishers.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalrecords=query.Count();
            List<Publisher> publishers=query.Skip((pagendex-1)*pageSize).Take(pageSize).ToList();
            return new ListResponse<Publisher>()
            {
                records = publishers,
                totalRecords = totalrecords
            };
        }

        public Publisher GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(c => c.Id == id);
        }

        public Publisher addPublisher(Publisher model)
        {
            var addedPublisher = _context.Publishers.Add(model);
            _context.SaveChanges();
            return addedPublisher.Entity;
        }

        public Publisher updatePublisher(Publisher model)
        {
            PublishRepository _publishrepository = new PublishRepository();
            var update = _publishrepository.GetPublisherById(model.Id);
            if(update == null)
            {
                return null;
            }
            var updatedPublisher = _context.Publishers.Update(model);
            _context.SaveChanges();
            return updatedPublisher.Entity;

        }

        public bool deletePublisher(int id)
        {
            PublishRepository _publishrepository = new PublishRepository();
            var publisher = _publishrepository.GetPublisherById(id);
            if (publisher == null) 
            {
               return false; 
            }
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return true;
        }
    }
}
