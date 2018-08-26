using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessageDecoder
{
    //Controller for the Master view
    public partial class Site1 : System.Web.UI.MasterPage
    {
        //Initalize ClientScriptManager on page load
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ClientScriptManager cs = Page.ClientScript;   
        }


        //Redirect method 
        protected void Redirect(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int to =int.Parse(btn.CommandArgument);
            SITE_PAGES page = SITE_PAGES.NONE;
            if (to == 1)
                page = SITE_PAGES.MAIN;
            else if (to == 2)
                page = SITE_PAGES.ENCODE;
            else if (to == 3)
                page = SITE_PAGES.DECODE;

            if (page != SITE_PAGES.NONE)
            Response.Redirect(Helpers.SitePages[page], false);
        }
    }
}