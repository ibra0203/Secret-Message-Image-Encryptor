using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessageDecoder
{
    //Controller for the Main view

    public partial class Main : System.Web.UI.Page
    {
        //Encode menu button
        protected void EncMenuBtn_Click1(object sender, EventArgs e)
        {
            Response.Redirect(Helpers.SitePages[SITE_PAGES.ENCODE], false);
        }
        //Decode menu button
        protected void DecMenuBtn_Click1(object sender, EventArgs e)
        {
            Response.Redirect(Helpers.SitePages[SITE_PAGES.DECODE], false);
        }
    }
}