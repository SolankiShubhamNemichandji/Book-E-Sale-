
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        RoleRepository _rolerepository = new RoleRepository();

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<RoleModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult GetRoles(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                if (pageIndex > 0)
                {
                    var roles = _rolerepository.GetRoles(pageIndex, pageSize, keyword);
                    if (roles == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Roles not found!");
                    }
                    ListResponse<RoleModel> listResponse = new ListResponse<RoleModel>()
                    {
                        records = roles.records.Select(c => new RoleModel(c)),
                        totalRecords = roles.totalRecords,
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
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetRole(int id)
        {
            try
            {
                if (id > 0)
                {
                    var roles = _rolerepository.GetRole(id);
                    if (roles == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Role not found!");
                    }
                    RoleModel roleModel = new RoleModel(roles);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), roleModel);
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
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddRole(RoleModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Role category = new Role()
                {
                    Id = model.Id,
                    Name = model.Name,
                };
                var response = _rolerepository.AddRole(category);
                RoleModel roleModel = new RoleModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), roleModel);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateRole(RoleModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Role category = new Role()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                var response = _rolerepository.UpdateRole(category);
                if (response == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Role not found!");
                }
                RoleModel roleModel = new RoleModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), roleModel);
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
        public IActionResult DeleteRole(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = _rolerepository.DeleteRole(id);
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
