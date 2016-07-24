using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
    public class EmailModel : Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        
    }
}