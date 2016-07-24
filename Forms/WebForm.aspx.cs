using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CloudComDevs.ShoppingCartDemo.Web;
using System.Threading.Tasks;
using System.Net.Http;
using CloudComDevs.ShoppingCartDemo.Web.Models;
namespace CloudComDevs.ShoppingCartDemo.Web.Forms
{
    public partial class WebForm : System.Web.UI.Page
    {
        string order = string.Empty;
        double ammount = 0.0d;
        string return_url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                
            }
        }



        protected void submitButton_Click(object sender, EventArgs e)
        {

            this.CallMethods();
        }


        private void CallMethods()
        {
            if (Request.QueryString.Count > 0)
            {
                order = Request.QueryString["order"];
                ammount = Convert.ToDouble(Request.QueryString["o"]);
                return_url = Request.QueryString["return_url"];


                string server = string.Format(Request.Url.Host + "{0}", !string.IsNullOrEmpty(Request.Url.Port.ToString()) ? ":" + Request.Url.Port.ToString() : "");


                HandlePay pay = new HandlePay();
                Payment card = new Payment()
                {
                    CardNumber = cardNumberTextBox.Text,
                    NameInCard = nameTextBox.Text,
                    CSVNumber = cvcTextBox.Text,
                    ExpiaryMonth = monthDropDown.SelectedValue,
                    ExpiaryYear = yearDropdown.SelectedValue,
                    ReferenceNumber = order,
                    Ammount = ammount
                };
                //   pay.Post(card, server);
                bool status = pay.ProcessPayment(card, server);
                if (status)
                {                    
                    Response.Redirect("/order/verification?id=" + order, true);
                }
                else
                {
                    Response.Redirect("/order/processfailed", true);
                }
            }

        }

    }
}