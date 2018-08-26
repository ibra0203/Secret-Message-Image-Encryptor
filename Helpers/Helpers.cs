using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Net;

namespace MessageDecoder
{
    //Class that contains helpers that are shared throughout the app 
    public class Helpers
    {
        const int MAXWIDTH = 800;

        //Dictionary to store page locations
        public static Dictionary<SITE_PAGES, string> SitePages = new Dictionary<SITE_PAGES, string>();
       
        //Images path 
        public static string FolderPath
        {
            get
            {
                string folderPath = HttpContext.Current.Server.MapPath("~/Files/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                return folderPath;
            }
        }

        //Init the app
        public static void init(Page page)
        {
            
            bool DoneInit = true;
            if(page.Session["DoneInit"] == null)
            {
                DoneInit = false;
                page.Session["DoneInit"] = true;
            }

            if (!DoneInit)
            {
                DeleteOldFiles();
                if (SitePages.Count == 0)
                {
                    SitePages.Add(SITE_PAGES.MAIN, "~/Views/Main.aspx");
                    SitePages.Add(SITE_PAGES.ENCODE, "~/Views/Encode.aspx");
                    SitePages.Add(SITE_PAGES.DECODE, "~/Views/Decode.aspx");
                    SitePages.Add(SITE_PAGES.DISPLAYIMG, "~/Views/DisplayImage.aspx");
                }
            }
        }

        //Captcha test
        public static bool IsHuman(ref string msg, HttpRequest Request)
        {
            string Response =  Request["g-recaptcha-response"];
            bool Valid = false;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            (" https://www.google.com/recaptcha/api/siteverify?secret=6LdtJlAUAAAAABREm11bUoCuhWQh8KRrLATfdvit&response=" + Response);
            try
            {
                using (StreamReader readStream = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json

                    Valid = Convert.ToBoolean(data.success);
                }
                if (!Valid)
                {
                    msg = "Error: Captcha test failed. Please try again.";
                    return false;
                }

            }

            catch (Exception exp)
            {
                msg = "Error: Can't perform captcha request";
            }
            msg = "Captcha confirmed";
            return true;
        }

        //Try uploading the image for decryption
        public static IMAGE_UPLOAD_RESULT UploadImage(System.Web.UI.WebControls.FileUpload fileUpload, ref string _name)
        {
            IMAGE_UPLOAD_RESULT result = IMAGE_UPLOAD_RESULT.SUCCESS;
            //Get the file name
            string filename = Path.GetFileName(fileUpload.FileName);
            //Generate a new random name (GUID) for the image
            filename = Guid.NewGuid().ToString() + "_" + filename;

            //Save the file in the proper path with the proper name
            fileUpload.SaveAs(FolderPath + filename);
           
            //Check is it's not an image
            if (!isImage(FolderPath + filename))
            {
                result = IMAGE_UPLOAD_RESULT.FAILED;
            }
            else
            {
                //If it's an image
                try
                {
                    //Read the image data to check if it's valid
                    using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fileUpload.PostedFile.InputStream))
                    {
                        //If it's not a square
                        if (myImage.Height != myImage.Width)
                        {
                            //Display error because image isn't square
                            result = IMAGE_UPLOAD_RESULT.DIMNOTEQUAL;
                        }
                        //Check if the image width is greater than the maximum width allowed (for memroy reasons)
                        if (myImage.Width > MAXWIDTH)
                        {
                            //Display error because image is too big
                            result = IMAGE_UPLOAD_RESULT.BIGFILE;
                        }
                    }  
                }
                catch (Exception exp)
                {
                    //Display error because uploading has failed
                    result = IMAGE_UPLOAD_RESULT.FAILED;
                }
            }
            //If uploading was a success, set _name to the filename 
            if (result == IMAGE_UPLOAD_RESULT.SUCCESS)
                _name = filename;
            else
                //If it wasn't successful, delete the uploaded file
                DeleteFile(FolderPath + filename);

           //Return the upload result
            return result;
        }

        //Check is specific file is an image
        public static bool isImage(string fileName)
        {
            try
            {
                System.Drawing.Image _img = System.Drawing.Image.FromFile(fileName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Delete file (usually due to expiration)
        public static void DeleteFile(string name)
        {
            try
            {//If the file exists, delete it
                if (System.IO.File.Exists(name))
                {
                    System.IO.File.Delete(name);
                }
            }
            catch (Exception exp)
            {
            }
        }

        //Deletes all image files
        public static void DeleteAllFiles()
        {
            //A list of file names
            string[] names = System.IO.Directory.GetFiles(FolderPath);
        
            //Delete all files in a loop
            foreach (string n in names)
            {
                if (System.IO.File.Exists(n))
                {
                    System.IO.File.Delete(n);
                }
            }
        }

        //Generates the image from a binary string where 0=white pixel, 1=black pixel, end of string=red
        public static string GenerateImageFromBinary(string _bin)
        {
            string filename = "";
            //Get the length of the binary string
            int count = _bin.Length;
            /*
             Get the square root of the binary character length. By doing that, we can 
             make a square that would fit every character in the binary string if we converted every
             0 to a white pixel and 1 to a black pixel. Where r*r=count.
            */
            int r =(int) Math.Ceiling(Math.Sqrt(count));
            //New bitmap with the dimensions r*r
            Bitmap bmp = new Bitmap(r, r);
            //GDI+ drawing surface from our image so we can draw on it
            Graphics grp = Graphics.FromImage(bmp);
            //This counter will count the number of characters in the binary string
            int i = 0;
            //When the binary string is over, set this to true to break the loop
            bool finishedMessage = false;

            //Loop through the x and y of the image through a nested for loop.
            for(int y=0; y<r; y++)
            {
                for(int x=0; x<r; x++)
                {
                    //If the binary string is over, set the current pixel to red to mark it and break
                    if(i>= count)
                    {
                        bmp.SetPixel(x, y, Color.Red);
                        finishedMessage = true;
                        //Break out of the loop
                        break;
                    }
                    //Get the current character in the binary string (at position i)
                    char c = _bin[i];

                    //If the current character = 1, set the current pixel to black
                    if (c == '1')
                        bmp.SetPixel(x, y, Color.Black);
                    //If the current character = 0, set the current pixel to white
                    else if (c == '0')
                        bmp.SetPixel(x, y, Color.White);
                    //Increase i
                    i++;
                }
                //If you just broke out of the inner loop using this var then this will break you out of the outer one
                if (finishedMessage) break;
                
            }

            //Generate random GUID name for this image
            string name = Guid.NewGuid().ToString()+".bmp";
            filename = FolderPath + name;
            //Save image at path
            bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
            //Dispose of the image and free memory
            bmp.Dispose();

            //Return the filename
            return filename;
        }

        //Reads image and converts it to a binary string
        public static string ReadBinaryFromImage(string filename)
        {
            string _binary = "";
            //Open bitmap from filename
            Bitmap bmp = new Bitmap(filename);
            //Get r, which represents both width and height of the image
            int r = bmp.Width;
            //Loop through the y and x coordinates of the image
            for(int y=0; y<r; y++)
            {
                for (int x = 0; x < r; x++)
                {
                    //Get current color 
                    Color col = bmp.GetPixel(x, y);
                    //If it's red, that means reading is done. Break.
                    if (col.ToArgb() == Color.Red.ToArgb())
                        break;
                    //If it's black, add 1 to the binary string
                    if (col.ToArgb()== Color.Black.ToArgb())
                        _binary += '1';
                    //If it's white, add 0 to the binary string
                    else if (col.ToArgb() == Color.White.ToArgb())
                        _binary += '0';
                }
            }

            return _binary;
        }
        //Downloads image into computer
        public static bool DownloadImage(string filename)
        {
            
            try
            {
                //Get the file name merged with the path
                string filePath = FolderPath + filename;
                //Get file info
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    //Add headers to download file and send it as response
                    HttpResponse Response = HttpContext.Current.Response;
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }

            }
            catch(Exception exp)
            {
                return false;
            }
            return true;
        }

        //Display warning to the user using a presented control
        public static void WarningMessage(System.Web.UI.Control control, string msg)
        {
            //check if previous warnings still exists, if so, remove it.
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {

                Control c = control.Controls[i];
                if (c is Label)
                {
                    Label l = (Label)c;
                    if (l.CssClass == "warning-text")
                        control.Controls.Remove(l);
                    l.Dispose();
                }
            }
            //Create new label
            Label label = new Label();
            //Set the new warning message
            label.Text = msg;
            //Set the warning message css class 
            label.CssClass = "warning-text";
            //Add it to the control
            control.Controls.Add(label);
        }

        //Deletes expired files
        public static void DeleteOldFiles()
        {
            //Loop through images and delete those who're older than a specific time period
            foreach(string file in Directory.GetFiles(FolderPath))
            {
                FileInfo info = new FileInfo(file);
                if(info.CreationTime < DateTime.Now.AddHours(-2))
                {
                    info.Delete();
                }
            }
        }
    }

   //An object that represents a JSON response
    public class MyObject
    {
        public string success { get; set; }
    }
}