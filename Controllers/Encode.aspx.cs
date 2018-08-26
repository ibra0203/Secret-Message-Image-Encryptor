using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;

namespace MessageDecoder
{
    //Controller for the Encode view
    public partial class Encode : System.Web.UI.Page
    {
       //Event triggered when user clicks submit button after entering their message and key
        protected void SubmitEncBtn_Click(object sender, EventArgs e)
        {
            string resp = "";
            //Check captcha
            bool isHuman = Helpers.IsHuman(ref resp, Request);
            //Captcha worked
            if (isHuman)
            {
                //Make sure key and message are safe for HTML
                string key = System.Text.RegularExpressions.Regex.Replace(TextBox1.Text, "<[^>]*>", string.Empty);
                string message = System.Text.RegularExpressions.Regex.Replace(TextBox2.Text, "<[^>]*>", string.Empty);

                //To test the strings properly, test them against versions with no whitespaces 
                string keyEmpty = System.Text.RegularExpressions.Regex.Replace(key, @"\s+", "");
                string msgEmpty = System.Text.RegularExpressions.Regex.Replace(message, @"\s+", "");

                if (keyEmpty.Length < 1 || msgEmpty.Length < 2)
                {
                    //Error message when there aren't enough characters
                    resp = "Error: Your message or key don't contain enough characters";

                    //Error message when one of the fields is empty
                    if (keyEmpty.Length ==0 || msgEmpty.Length ==0)
                    {
                        resp = "Error: Please don't leave any blank fields.";
                    }

                }
                else
                {

                    string encryptedMsg = Encryptor.FinalEncrypt(message, key);
                    string imgPath = Helpers.GenerateImageFromBinary(encryptedMsg);

                    Session["ImageName"] = Path.GetFileName(imgPath);

                    Response.Redirect(Helpers.SitePages[SITE_PAGES.DISPLAYIMG], false);
                }
                
            }
            //Display the warning message on Panel2
            Helpers.WarningMessage(Panel2, resp);
        }

        
    }
}