using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class AddCreditCard : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorNum.Visible = false;
            lblErrorCode.Visible = false;
            lblErrorDate2.Visible = false;
            lblErrorDate.Visible = false;
            lblDuplicateCard.Visible = false;
            rfvNumCC.ErrorMessage = GetLocalResourceObject("txtErrorNum").ToString();
            rfvCodeCC.ErrorMessage = GetLocalResourceObject("txtErrorCode").ToString();
            rfvDateCC.ErrorMessage = GetLocalResourceObject("txtErrorDate").ToString();

            if (!IsPostBack)
            {
                /* Inicializamos dropdownlist type credit card */
                string cred = GetLocalResourceObject("txtCredit").ToString();
                string deb = GetLocalResourceObject("txtDebit").ToString();

                ddTypeCC.Items.Add(deb);
                ddTypeCC.Items.Add(cred);
                ddTypeCC.SelectedValue = deb;
            }
        }

        protected void btnAddCC_Click(object sender, EventArgs e)
        {
            lblErrorNum.Visible = false;
            lblErrorCode.Visible = false;
            lblErrorDate2.Visible = false;
            lblErrorDate.Visible = false;
            lblDuplicateCard.Visible = false;
            if (Page.IsValid)
            {
                bool numOk = comprobarNum();
                bool codeOk = comprobarCode();
                bool dateOk = comprobarDate();
                if (numOk && codeOk && dateOk)
                    addCreditCard();
            }
        }

        private void addCreditCard()
        {
            try
            {
                string typeCard = "Débito";
                if (ddTypeCC.SelectedItem.Text.Equals(GetLocalResourceObject("txtCredit").ToString()))
                    typeCard = "Crédito";


                CreditCard ucc = new CreditCard();
                ucc.userId = SessionManager.GetUserSession(Context).UserId;
                ucc.creditType = typeCard;
                ucc.number = (long)Convert.ToDouble(txtNumCC.Text);
                ucc.verificationCode = Convert.ToInt32(txtCodeCC.Text);
                string[] aux = txtDateCC.Text.Split('/');
                string mes = aux[0];
                string año = aux[1];
                string dateAux = "01/" + mes + "/20" + año;
                ucc.expirationDate = Convert.ToDateTime(dateAux);
                if (SessionManager.GetUserSession(Context).DefaultCreditCard == null)
                {
                    ucc.defaultCard = true;
                    SessionManager.GetUserSession(Context).DefaultCreditCard = ucc;
                }
                else
                    ucc.defaultCard = false;

                SessionManager.RegisterCreditCard(Context, ucc);

                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/User/UpdateUserProfile.aspx"));
            }
            catch (DuplicateInstanceException)
            {
                lblDuplicateCard.Visible = true;
            }
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        private bool comprobarNum()
        {

            bool flag = true;
            if (!IsDigitsOnly(txtNumCC.Text))
            {
                lblErrorNum.Visible = true;
                flag = false;
            }
            return flag;
        }
        private bool comprobarCode()
        {
            bool flag = true;
            if (!IsDigitsOnly(txtCodeCC.Text))
            {
                lblErrorCode.Visible = true;
                flag = false;
            }
            return flag;
        }
        private bool comprobarDate()
        {
            string[] partes = txtDateCC.Text.Split('/');

            if (partes.Length < 2)
            {
                lblErrorDate2.Visible = true;
                return false;
            }

            int mes;
            bool exitoMes = int.TryParse(partes[0], out mes);
            if (mes > 12 || mes < 1)
            {
                lblErrorDate2.Visible = true;
                return false;
            }


            int anio;
            bool exitoAnio = int.TryParse(partes[1], out anio);

            if (exitoMes && exitoAnio)
            {
                // Ambas conversiones fueron exitosas, puedes utilizar mes y anio aquí como enteros
                int mesActual = DateTime.Now.Month;
                int añoActual = int.Parse(DateTime.Now.ToString("yy"));
                if (añoActual > anio)
                {
                    lblErrorDate.Visible = true;
                    return false;
                }
                if (añoActual == anio && mes < mesActual)
                {
                    lblErrorDate.Visible = true;
                    return false;
                }
            }
            else
            {
                // Ocurrió un error durante la conversión, maneja el error aquí
                lblErrorDate2.Visible = true;
                return false;
            }
            return true;
        }
    }
}