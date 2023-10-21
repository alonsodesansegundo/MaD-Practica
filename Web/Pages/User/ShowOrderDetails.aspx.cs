using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ShowOrderDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Request.Params.Get("orderId").ToString();
            long orderId = long.Parse(id);

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();
            List<OrderLine> orderLines = shoppingService.FindOrderLinesByOrderId(orderId);
            List<ProductDetailsOrderLine> productDetails = new List<ProductDetailsOrderLine>();
            ProductDetailsOrderLine aux = null;
            for (int i = 0; i < orderLines.Count; i++)
            {
                aux = shoppingService.FindProductDetailsOrderLineByOrderLineId(orderLines[i].orderLineId);
                productDetails.Add(aux);

            }

            if (productDetails != null)
            {
                this.gvOrderDetails.DataSource = productDetails;

                this.gvOrderDetails.Columns[0].HeaderText = GetLocalResourceObject("nameProduct").ToString();
                this.gvOrderDetails.Columns[1].HeaderText = GetLocalResourceObject("cantidad").ToString();
                this.gvOrderDetails.Columns[2].HeaderText = GetLocalResourceObject("unitPrice").ToString();

                this.gvOrderDetails.DataBind();
            }

        }
    }
}