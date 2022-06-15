using Microsoft.AspNetCore.Mvc;
using BookStore.Repositories;
using BookStore.Models;
using BookStore.Models.ViewModels;
using System;
using System.Net;
using BookStore.Models.Models;
using bookstore;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
        DemoAES obj = new DemoAES();
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<UserModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                if (pageIndex > 0)
                {
                    var users = _repository.GetUsers(pageIndex, pageSize, keyword);
                    if (users == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Users not found");
                    }
                    ListResponse<UserModel> userlist = new ListResponse<UserModel>()
                    {
                        records = users.records.Select(c => new UserModel(c)),
                        totalRecords = users.totalRecords,
                    };
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), userlist);
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
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var users = _repository.GetUser(id);
                    if (users == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");
                    }
                    UserModel userbyid = new UserModel(users);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), userbyid);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Update(UserModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                User upuser = new User()
                {
                    Id = model.id,
                    Firstname = model.firstName,
                    Lastname = model.lastName,
                    Email = model.email,
                    Password = obj.ComputeMD5Hash(model.password),
                    Roleid = model.roleId,
                };
                var isSaved = _repository.updateUser(upuser);
                if (isSaved==null)
                {
                     return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");
                }
                UserModel updatedUser = new UserModel(isSaved);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), updatedUser);
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
        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var isDeleted = _repository.deleteUser(id);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), isDeleted);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}

