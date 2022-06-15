using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class BookRepository:BaseRepository
    {
        public ListResponse<Book> GetBook(int pageIndex,int pageSize,string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Books.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)
            || c.Description.ToLower().Contains(keyword)).AsQueryable();
            int totalrecords=query.Count();
            List<Book> booklist=query.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return new ListResponse<Book>()
            {
                records = booklist,
                totalRecords = totalrecords
            };
        }

        public Book GetBookId(int id)
        {
            return _context.Books.FirstOrDefault(c => c.Id == id);
        }

        public Book addBook(Book model)
        {
            var book=_context.Books.Add(model);
            _context.SaveChanges();
            return book.Entity;
        }

        public Book updateBook(Book model)
        {
            BookRepository bookRepository = new BookRepository();
            var bookFound= bookRepository.GetBookId(model.Id);
            if (bookFound == null)
            {
                return null;
            }
            var book = _context.Books.Update(model);
            _context.SaveChanges();
            return book.Entity;
        }

        public bool deleteBook(int id)
        {
            BookRepository _bookRepository = new BookRepository();
            var delbook = _bookRepository.GetBookId(id);
            if (delbook == null)
            {
                return false;
            }
            _context.Books.Remove(delbook);
            _context.SaveChanges();
            return true;
        }
    }


    
}
