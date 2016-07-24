using CloudComDevs.ShoppingCartDemo.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudComDevs.ShoppingCartDemo.Web.Forms
{
    class HandlePay
    {


        public bool ProcessPayment(Payment payment, string host)
        {
            if (this.ValidateCard(payment))
            {
                Post(payment, host);
               // await UpdatePayment(payment, host).ConfigureAwait(false); 
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public async Task<HttpResponseMessage> Post(Payment payment, string host)
        {
            string uri = "http://" + host + "/api/payment/";
            HttpClient httpClient = new HttpClient();

            //var postData = new List<KeyValuePair<string, string>>();
            //postData.Add(new KeyValuePair<string, string>("CardNumber", payment.CardNumber));
            //postData.Add(new KeyValuePair<string, string>("NameInCard", payment.NameInCard));
            //postData.Add(new KeyValuePair<string, string>("CSVNumber", payment.CSVNumber));
            //postData.Add(new KeyValuePair<string, string>("ExpiaryMonth", payment.ExpiaryMonth));
            //postData.Add(new KeyValuePair<string, string>("ExpiaryYear", payment.ExpiaryYear));
            //postData.Add(new KeyValuePair<string, string>("ReferenceNumber", payment.ReferenceNumber));
            //postData.Add(new KeyValuePair<string, string>("Ammount", payment.Ammount.ToString()));

            //HttpContent content = new FormUrlEncodedContent(postData);

            var v = await httpClient.PostAsJsonAsync(uri, payment).ConfigureAwait(false); ;
            return v;
        }

        private string FormatCardNumber(string o)
        {
            string result = string.Empty;

            result = Regex.Replace(o, "[^0-9]", string.Empty);

            return result;
        }

        private string FormatName(string o)
        {
            string result = string.Empty;

            if (o.Contains("/"))
            {
                string[] NameSplit = o.Split('/');

                result = NameSplit[1] + " " + NameSplit[0];
            }
            else
            {
                result = o;
            }

            return result;
        }


        private bool ValidateCard(Payment card)
        {
            //todo validate the card and send status
            return true;
        }
    }
}
