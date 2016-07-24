using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public string CardNumber { get; set; }
        public string NameInCard { get; set; }
        public string CSVNumber { get; set; }
        public string ExpiaryMonth { get; set; }
        public string ExpiaryYear { get; set; }
        public double Ammount { get; set; }
        public string ReferenceNumber { get; set; }
    }
}