using CloudComDevs.ShoppingCartDemo.Web.Models;
using CloudComDevs.ShoppingCartDemo.Web.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CloudComDevs.ShoppingCartDemo.Web.Controllers
{
    public class PaymentController : ApiController
    {

        PaymentRepository repository = new PaymentRepository();
        // GET api/default1
        //[RouteAttribute({"id"}]
        public IHttpActionResult Get(string Id)
        {
            Payment card = repository.GetPaymentForOrder(Id);
          if (card != null)
          {
              return Ok(card.TransactionId);
          }
          else
          {
              return BadRequest("Transaction Failed");
          }
        }

        // GET api/default1/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/default1

      

        public IHttpActionResult Post([FromBody]Payment value)
        {
            if (value != null)
            {
                repository.Add(value);
                repository.SaveChanges();
                return Ok(value.TransactionId);
            }
            else
            {
                return BadRequest("Item cannot be null.");
            }
        }

    }
}
