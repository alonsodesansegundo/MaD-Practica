﻿using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using System;
using System.Globalization;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Register : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            doInivisbleLabelsError();
            if (!IsPostBack)
            {
                /* Get current language and country from browser */
                String defaultLanguage =
                    GetLanguageFromBrowserPreferences();
                String defaultCountry =
                    GetCountryFromBrowserPreferences();

                /* Combo box initialization */
                UpdateComboLanguage(defaultLanguage);
                UpdateComboCountry(defaultLanguage, defaultCountry);

                /* Inicializamos combobox type user */
                string admin = GetLocalResourceObject("txtAdmin").ToString();
                string estandar = GetLocalResourceObject("txtEstandar").ToString();

                comboUser.Items.Add(estandar);
                comboUser.Items.Add(admin);
                comboUser.SelectedValue = estandar;
            }
        }

        private void doInivisbleLabelsError()
        {
            //hago invisibles las etiquetas de error
            lblErrorLogin.Visible = false;
            lblRepeatLogin.Visible = false;
        }

        private String GetLanguageFromBrowserPreferences()
        {
            String language;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
            language = cultureInfo.TwoLetterISOLanguageName;
            LogManager.RecordMessage("Preferred language of user" +
                                     " (based on browser preferences): " + language);
            return language;
        }

        private String GetCountryFromBrowserPreferences()
        {
            String country;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            if (cultureInfo.IsNeutralCulture)
            {
                country = "";
            }
            else
            {
                // cultureInfoName is something like en-US
                String cultureInfoName = cultureInfo.Name;
                // Gets the last two caracters of cultureInfoname
                country = cultureInfoName.Substring(cultureInfoName.Length - 2);

                LogManager.RecordMessage("Preferred region/country of user " +
                                         "(based on browser preferences): " + country);
            }

            return country;
        }

        /// <summary>
        /// Loads the languages in the comboBox in the *selectedLanguage*.
        /// Also, the selectedLanguage will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboLanguage(String selectedLanguage)
        {
            this.comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
            this.comboLanguage.DataTextField = "text";
            this.comboLanguage.DataValueField = "value";
            this.comboLanguage.DataBind();
            this.comboLanguage.SelectedValue = selectedLanguage;
        }

        /// <summary>
        /// Loads the countries in the comboBox in the *selectedLanguage*.
        /// Also, the *selectedCountry* will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboCountry(String selectedLanguage, String selectedCountry)
        {
            this.comboCountry.DataSource = Countries.GetCountries(selectedLanguage);
            this.comboCountry.DataTextField = "text";
            this.comboCountry.DataValueField = "value";
            this.comboCountry.DataBind();
            this.comboCountry.SelectedValue = selectedCountry;
        }

        /// <summary>
        /// Handles the Click event of the btnRegister control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance
        /// containing the event data.</param>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    bool typeUser = false;
                    if (comboUser.SelectedItem.Text.Equals(GetLocalResourceObject("txtAdmin").ToString()))
                        typeUser = true;

                    UserDetails userProfileDetailsVO = new UserDetails(txtNombre.Text, txtApellidos.Text,
                        txtEmail.Text, txtDir.Text, comboCountry.Text, comboLanguage.Text, typeUser);

                    SessionManager.RegisterUser(Context, txtLogin.Text,
                        txtPassword.Text, userProfileDetailsVO);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Index.aspx"));


                }
                catch (DuplicateInstanceException)
                {
                    lblRepeatLogin.Visible = true;
                }
            }
        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            /* After a language change, the countries are printed in the
             * correct language.
             */
            this.UpdateComboCountry(comboLanguage.SelectedValue,
                comboCountry.SelectedValue);
        }
    }
}