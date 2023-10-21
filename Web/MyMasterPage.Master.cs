using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class MyMasterPage : System.Web.UI.MasterPage
    {
        private static List<TagDetails> tags;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetTagByUses();

            if (!SessionManager.IsUserAuthenticated(Context))
            {
                if(lnkLogout!=null)
                    lnkLogout.Visible = false;
                if (lnkMyOrders != null)
                    lnkMyOrders.Visible = false;
                if (lnkMyProfile != null)
                    lnkMyProfile.Visible = false;
            }
            else if (lnkAuthenticate != null)
                lnkAuthenticate.Visible = false;
        }


        protected void GetTagByUses()
        {
            var listTag = CommentManager.GetTagsByUse();
            tags = listTag;

            RepeaterTags.DataSource = tags;
            RepeaterTags.DataBind();

        }

    }
}