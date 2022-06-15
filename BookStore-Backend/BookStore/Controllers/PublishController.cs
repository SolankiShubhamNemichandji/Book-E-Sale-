using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/publish")]
    public class PublishController : ControllerBase
    {
        PublishRepository _publishrepository = new PublishRepository();

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<PublishModel>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetPublishers(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                if (pageIndex > 0)
                {

                    var publish = _publishrepository.GetPublishers(pageIndex, pageSize, keyword);
                    if (publish == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Publishers not found!");
                    }
                    ListResponse<PublishModel> publishlist = new ListResponse<PublishModel>()
                    {
                        records = publish.records.Select(c => new PublishModel(c)),
                        totalRecords = publish.totalRecords,
                    };
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), publishlist);
                }
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(PublishModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetPublisherById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var publisher = _publishrepository.GetPublisherById(id);
                    if (publisher == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Publisher not found!");
                    }
                    PublishModel publisherbyid = new PublishModel(publisher);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), publisherbyid);
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
        [ProducesResponseType(typeof(PublishModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddPublisher(PublishModel model)
        {
            try
            {


                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Publisher publisher = new Publisher()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                    Contact = model.Contact,
                };
                var addedpublisher = _publishrepository.addPublisher(publisher);
                PublishModel publisheradd = new PublishModel(addedpublisher);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), publisheradd);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(PublishModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdatePublisher(PublishModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Publisher publisher1 = new Publisher()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                    Contact = model.Contact,
                };

                var updatedPublisher = _publishrepository.updatePublisher(publisher1);
                if (updatedPublisher == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Plublisher not found!");
                }
                PublishModel publish = new PublishModel(updatedPublisher);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), publish);
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
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = _publishrepository.deletePublisher(id);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), result);
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
