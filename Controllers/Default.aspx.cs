using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessageDecoder
{
    //Controller of the view Default 
    //This view is called first. When that happens, it initializes the app and redirects to Main.aspx
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Helpers.init(Page);
            Response.Redirect("~/Views/Main.aspx");
        }
    }
}