using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using SampleWebApi.Contracts;

namespace SampleWebApi.Controllers
{
    [System.Web.Http.Route("api/Customers")]
    public class CustomersController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IEnumerable<Customer> Get()
        {
            return new[] { new Customer
            {
                Surname = "Bill",
                Forename = "Gates"
            } };
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Add([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

    }
}