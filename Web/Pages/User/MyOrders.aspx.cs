using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class MyOrders : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }
            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.ShowOrdersResult;
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            long userId = SessionManager.GetUserSession(Context).UserId;

            OrderBlock ordersBlock = null;

            ordersBlock = shoppingService.FindOrdersByUserId(userId, startIndex, count);

            if (ordersBlock.Orders.Count == 0)
            {
                //no hay pedidos
                lblNoOrders.Visible = true;
                return;
            }
            lblNoOrders.Visible = false;
            if (ordersBlock != null)
            {
                this.gvOrders.DataSource = ordersBlock.Orders;


                this.gvOrders.Columns[0].HeaderText = GetLocalResourceObject("lblOrderName").ToString();
                this.gvOrders.Columns[1].HeaderText = GetLocalResourceObject("lblCreationDate").ToString();
                this.gvOrders.Columns[2].HeaderText = GetLocalResourceObject("lblTotalPrice").ToString();

                this.gvOrders.DataBind();

                /* "Previous" link */
                if ((startIndex - count) >= 0)
                {
                    String url = "/Pages/User/MyOrders.aspx" +
                        "?startIndex=" + (startIndex - count) + "&count=" + count;

                    this.lnkPrevious.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    this.lnkPrevious.Visible = true;
                }

                /* "Next" link */
                if (ordersBlock.ExistMoreItems)
                {
                    String url = "/Pages/User/MyOrders.aspx" +
                        "?startIndex=" + (startIndex + count) + "&count=" + count;

                    this.lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                    this.lnkNext.Visible = true;
                }
            }

        }
    }
}