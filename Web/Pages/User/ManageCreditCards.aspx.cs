using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ManageCreditCards : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNotDelete.Visible = false;
            lblEmpty.Visible = false;
            this.gvCreditCards.Columns[0].HeaderText = GetLocalResourceObject("txtTipo").ToString();
            this.gvCreditCards.Columns[1].HeaderText = GetLocalResourceObject("txtNum").ToString();
            this.gvCreditCards.Columns[2].HeaderText = GetLocalResourceObject("txtFech").ToString();
            this.gvCreditCards.Columns[3].HeaderText = GetLocalResourceObject("txtDef").ToString();

            if (!IsPostBack)
            {
                List<CreditCard> cards = SessionManager.FindCreditCards(Context);

                if (cards.Count!=0)
                {
                    this.gvCreditCards.DataSource = cards;
                    this.gvCreditCards.DataBind();
                }
                else
                    lblEmpty.Visible = true;

            }
        }

        protected void OnRowDeleting(object sender, GridViewCommandEventArgs e)
        {
            lblNotDelete.Visible = false;

            string stringCardId = e.CommandArgument.ToString();
            long cardId = long.Parse(stringCardId);

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            if (e.CommandName == "Delete")
            {
                if (SessionManager.GetUserSession(Context).DefaultCreditCard.creditCardId == cardId) {
                    lblNotDelete.Visible = true;
                    return;
                }
                CreditCard cc = SessionManager.GetUserSession(Context).DefaultCreditCard;
                userService.RemoveCard(cardId);
            }
            else if (e.CommandName == "SelectDefault")
            {
                CreditCard card = userService.FindUserCreditCard(cardId);
                userService.UpdateUserCreditCard(cardId, card);
                SessionManager.GetUserSession(Context).DefaultCreditCard = card;
            }

            List<CreditCard> cards = SessionManager.FindCreditCards(Context);
            if (cards.Count == 0) {
                SessionManager.GetUserSession(Context).DefaultCreditCard = null;
                lblEmpty.Visible = true;
            }

            this.gvCreditCards.DataSource = cards;
            this.gvCreditCards.DataBind();
        }

        protected void gvCreditCards_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}