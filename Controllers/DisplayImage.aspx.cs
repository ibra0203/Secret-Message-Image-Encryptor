using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessageDecoder
{
    //Controller for the view DisplayImage
    public partial class DisplayImage : System.Web.UI.Page
    {
        string webImgPath, ImageName;

        //When the page loads, get Image name and display it by setting it in the URL
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageName = (string)Session["ImageName"];
            webImgPath = "~/Files/" + ImageName;


            Image1.ImageUrl = webImgPath;

        }

        //Download image when the user clicks on the download button 
        protected void DownloadImgBtn_Click(object sender, EventArgs e)
        {
            if (ImageName != null)
                Helpers.DownloadImage(ImageName);
        }

       
    }
}