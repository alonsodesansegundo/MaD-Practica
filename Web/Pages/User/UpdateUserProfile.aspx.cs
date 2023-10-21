using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using System;
using System.Collections.Generic;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class UpdateUserProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
            if (!IsPostBack)
            {
                UserDetails userDetails =
                    SessionManager.FindUserProfileDetails(Context);


                txtNombre.Text = userDetails.FirstName;
                txtApellidos.Text = userDetails.LastName;
                txtDir.Text = userDetails.PostalAddress;
                txtEmail.Text = userDetails.Email;

                /* Añadir lo de las tarjetas de credito*/

                /* Combo box initialization */
                UpdateComboLanguage(userDetails.Language);
                UpdateComboCountry(userDetails.Language,
                    userDetails.Country);
            }
        }
        private void UpdateComboLanguage(String selectedLanguage)
        {
            this.comboIdioma.DataSource = Languages.GetLanguages(selectedLanguage);
            this.comboIdioma.DataTextField = "text";
            this.comboIdioma.DataValueField = "value";
            this.comboIdioma.DataBind();
            this.comboIdioma.SelectedValue = selectedLanguage;
        }
        private void UpdateComboCountry(String selectedLanguage, String selectedCountry)
        {
            this.comboPais.DataSource = Countries.GetCountries(selectedLanguage);
            this.comboPais.DataTextField = "text";
            this.comboPais.DataValueField = "value";
            this.comboPais.DataBind();
            this.comboPais.SelectedValue = selectedCountry;
        }

        private void UpdateProfile(long userId, UserDetails userDetails)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            /* Get Account Data */
            try
            {
                userService.UpdateUserDetails(userId, userDetails);
                lblMensaje.Text = GetLocalResourceObject("txtCorrectUpdate").ToString();
            }
            catch (Exception e)
            {
                lblMensaje.Text = GetLocalResourceObject("txtErrorUpdate").ToString();
            }
            lblMensaje.Visible = true;
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
            if (Page.IsValid)
            {
                //puedo actualizar los datos de usuario
                UserDetails userDetails = new UserDetails(txtNombre.Text, txtApellidos.Text,
                        txtEmail.Text, txtDir.Text, comboPais.SelectedValue, comboIdioma.SelectedValue);

                SessionManager.UpdateUserProfileDetails(Context,
                   userDetails);

                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/Index.aspx"));
            }
        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboCountry(comboIdioma.SelectedValue,
                comboPais.SelectedValue);
        }


    }
}