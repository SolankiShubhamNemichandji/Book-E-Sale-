using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _categoryrepository = new CategoryRepository();

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult GetCategories(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                if (pageIndex > 0)
                {
                    var categories = _categoryrepository.GetCategories(pageIndex, pageSize, keyword);
                    if (categories == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Category not found!");
                    }
                    ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
                    {
                        records = categories.records.Select(c => new CategoryModel(c)),
                        totalRecords = categories.totalRecords,
                    };
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetCategory(int id)
        {
            try
            {
                if (id > 0)
                {
                    var categories = _categoryrepository.GetCategory(id);
                    if (categories == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Category not found!");
                    }
                    CategoryModel categoryModel = new CategoryModel(categories);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), categoryModel);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Category category = new Category()
                {
                    Id = model.Id,
                    Name = model.Name,
                };
                var response = _categoryrepository.AddCategory(category);
                CategoryModel categoryModel = new CategoryModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), categoryModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Category category = new Category()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                var response = _categoryrepository.UpdateCategory(category);
                if (response == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Category not found!");
                }
                CategoryModel categoryModel = new CategoryModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), categoryModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = _categoryrepository.DeleteCategory(id);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), response);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert correct details!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

    }
    
}
