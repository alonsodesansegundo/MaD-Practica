using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class RegisterOrder : SpecificCulturePage
    {
        CreditCard creditCardOrder;
        List<CreditCard> cards;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //primera vez que se carga la pag
                lblErrorCode.Visible = false;
                rfvDirOrder.ErrorMessage = GetLocalResourceObject("txtObligatorio").ToString();
                rfvCodeCC.ErrorMessage = GetLocalResourceObject("txtObligatorio").ToString();
                rfvNomOrder.ErrorMessage = GetLocalResourceObject("txtObligatorio").ToString();

                creditCardOrder = SessionManager.GetUserSession(Context).DefaultCreditCard;
                if (creditCardOrder != null)
                {
                    //si tiene tarjeta por defecto es que tiene tarjetas
                    cards = SessionManager.FindCreditCards(Context);

                    ddNumberCC.DataSource = cards;
                    ddNumberCC.DataTextField = "number";
                    ddNumberCC.SelectedValue = creditCardOrder.number.ToString();
                    ddNumberCC.DataBind();
                    lblFechCC_2.Text = creditCardOrder.expirationDate.ToString("MM/yy");
                }
                txtDirOrder.Text = SessionManager.GetUserSession(Context).PostalAddress;
            }
            else {
                //se ha seleccionado algo en el drop down list
                ListItem selectedItem = ddNumberCC.SelectedItem;
                cards = SessionManager.FindCreditCards(Context);
                // Buscar la tarjeta de crédito que tenga el mismo identificador que el valor seleccionado
                CreditCard aux = cards.FirstOrDefault(c => c.number.ToString() == selectedItem.Value);
                // Comprobar si se ha encontrado la tarjeta de crédito
                if (aux != null)
                {
                    creditCardOrder = aux;
                    lblFechCC_2.Text = creditCardOrder.expirationDate.ToString("MM/yy");
                }
            }
        }

        protected void btnDoOrder_Click(object sender, EventArgs e)
        {
            lblErrorCode.Visible = false;
            if (Page.IsValid)
            {
                //compruebo que el codigo de verificación es ok
                if (txtCodeCC.Text == creditCardOrder.verificationCode.ToString())
                {
                    //hago el pedido
                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();
                    Model.ShoppingService.ShoppingCart shoppingCart = SessionManager.GetShoppingCart(Context);
                    long userId = SessionManager.GetUserSession(Context).UserId;
                    string postalAddress = txtDirOrder.Text;
                    string orderName = txtNameOrder.Text;
                    shoppingService.RegisterOrder(shoppingCart, userId, creditCardOrder.creditCardId, postalAddress, orderName);
                    Response.Redirect("~/Pages/Index.aspx");
                }
                else {
                    lblErrorCode.Visible = true;
                }
            }
        }
    }
}