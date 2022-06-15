using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using System;
using System.Net;
using BookStore.Models.Models;
using bookstore;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/public")]
    public class BookStoreController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
        DemoAES obj = new DemoAES();
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult Login(LoginModel model)
        {
            try
            {
                if (model==null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                User userlogin = new User()
                {
                    Email = model.Email,
                    Password= obj.ComputeMD5Hash(model.Password),
                };
                var response= _repository.Login(userlogin);
                if (response == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");
                }
                UserModel user = new UserModel(response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Register(RegisterModel model)
        {
            try
            {


                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                User userRegister = new User()
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email,
                    Password = obj.ComputeMD5Hash(model.Password),
                    Roleid = model.Roleid,
                };
                var user = _repository.Register(userRegister);
                UserModel user1 = new UserModel(user);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), user1);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }

        }
    }
}


