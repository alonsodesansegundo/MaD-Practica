using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ChangePassword : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOldPasswordError.Visible = false;
            rfvPasswordActual.ErrorMessage = GetLocalResourceObject("txtError").ToString();
            rfvConfirmacionPassword.ErrorMessage = GetLocalResourceObject("txtError").ToString();
            rfvNuevaPassword.ErrorMessage = GetLocalResourceObject("txtError").ToString();
            cvConfirmacionPassword.ErrorMessage = GetLocalResourceObject("txtDiferentes").ToString();
            cvPasswordsMatch.ErrorMessage = GetLocalResourceObject("txtNoIguales").ToString();
        }

        protected void btnUpdatePw_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SessionManager.ChangePassword(Context, txtOldPw.Text,
                        txtNewPw.Text);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/UpdateUserProfile.aspx"));

                }
                catch (IncorrectPasswordException)
                {
                    lblOldPasswordError.Visible = true;
                }
            }
        }
    }
}