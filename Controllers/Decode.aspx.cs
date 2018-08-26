using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MessageDecoder
{
    //Controller for the Decode view, where you can decrypt an already encrypted image
    public partial class Decode : System.Web.UI.Page
    {

        //When Upload is pressed
        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            string resp = "";
            //Check captcha
            bool isHuman = Helpers.IsHuman(ref resp, Request);
            if (isHuman)
            {
                //Validate image if Captcha was correct
                ValidateImage();
            }
            else
                Helpers.WarningMessage(Panel2, resp);
        }
        protected void ValidateImage()
        {
            string fileName = "";
            //Upload the image
            IMAGE_UPLOAD_RESULT result = Helpers.UploadImage(FileUpload1, ref fileName);

            //Handle upload errors by displaying a warning message
            if (result == IMAGE_UPLOAD_RESULT.FAILED)
            {
                Helpers.WarningMessage(Panel2, "Error: The selected file is not a proper image file");
            }
            else if (result == IMAGE_UPLOAD_RESULT.BIGFILE)
            {
                Helpers.WarningMessage(Panel2, "Error: File size is too big.");
            }
            else if (result == IMAGE_UPLOAD_RESULT.DIMNOTEQUAL)
            {
                Helpers.WarningMessage(Panel2, "Error: Image dimensions aren't equal.");
            }
            else if (result == IMAGE_UPLOAD_RESULT.NOTBMP)
            {
                Helpers.WarningMessage(Panel2, "Error: Only BMP files are allowed.");
            }
            //If Upload was successful
            else
            {
                //Try decrypting the message
                string key = KeyBox.Text;
                string path = Helpers.FolderPath + fileName;
                string bin = Helpers.ReadBinaryFromImage(path);
                string msg = Encryptor.FinalDecrypt(bin, key);

                //Decryption failed
                if (msg == null)
                {
                    Helpers.WarningMessage(Panel2, "Error: Invalid file or key");
                }

                //Decryption succeeded
                else
                {
                    //Replace panel controls with the result
                    for (int i = Panel2.Controls.Count - 1; i >= 0; i--)
                    {
                        Control c = Panel2.Controls[i];

                        if (c != TextArea1)
                        {
                            Panel2.Controls.Remove(c);
                            c.Dispose();
                        }
                    }
                    TextArea1.Attributes.Remove("hidden");
                    TextArea1.InnerText = msg;
                    TextArea1.Style.Clear();
                    TextArea1.Style.Add(HtmlTextWriterStyle.Width, "100%");

                    TextArea1.Style.Add(HtmlTextWriterStyle.Height, "100%");

                    TextArea1.Style.Add(HtmlTextWriterStyle.Display, "Block");
                }
            }
        }


    }
}