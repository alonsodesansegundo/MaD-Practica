using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web.Security;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Authentication : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLoginError.Visible = false;
            lblPasswordError.Visible = false;
      
        }

        public void BtnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblLoginError.Visible = false;
                lblPasswordError.Visible = false;

                try
                {
                    SessionManager.Login(Context, txtbLogin.Text,
                            txtPassword.Text, checkRememberPassword.Checked);

                    FormsAuthentication.
                        RedirectFromLoginPage(txtbLogin.Text,
                            checkRememberPassword.Checked);

                    if (Session["redirectPostLogin"] != null)
                    {
                        Session.Remove("redirectPostLogin");
                        Response.Redirect("~/Pages/Shopping/ShoppingCart.aspx");
                    }
                    else if (Session["toProductDetails"] != null) {
                        long productId = (long)(Session["productId"]);
                        Session.Remove("toProductDetails");
                        Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/" +
                            "ShowProductDetails.aspx"
                            + "?id=" + productId));
                    }

                }
                catch (InstanceNotFoundException)
                {
                    lblLoginError.Visible = true;
                }
                catch (IncorrectPasswordException)
                {
                    lblPasswordError.Visible = true;
                }
            }

        }
    }
}