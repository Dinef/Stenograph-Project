using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using Testing.Model;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Testing
{
    public partial class _Default : Page
    {
        Stenography stenography = new Stenography();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }



        protected void UploadFile(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Pictures/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            //Display the Picture in Image control.
            Image1.ImageUrl = "~/Pictures/" + Path.GetFileName(FileUpload1.FileName);

            //  Label1.Text = folderPath + Path.GetFileName(FileUpload1.FileName);

            String filepath = Server.MapPath(Image1.ImageUrl); ;
            filepath = filepath.Replace(@"\", "/");
            Label1.Text = filepath;

            String message = txtMsg.Text;
              Bitmap img = new Bitmap(filepath,true);

          
            img = Stenography.embedText(message, img);


            Response.ContentType = "image/jpeg";
            Response.AppendHeader("Content-Disposition", "attachment; filename=encoded.jpg");

            img.Save(Response.OutputStream, ImageFormat.Jpeg);

        }

      

      

        protected void btnEncode_Click(object sender, EventArgs e)
        {

            String filepath = @"C:\Users\shandinefacey\source\repos\Testing\Pictures\beach.jpg"; 
             //filepath = filepath.Replace(@"\", "/");
             Label1.Text = filepath;

           String message = txtMsg.Text;
          //  System.Drawing.Image Dummy = System.Drawing.Image.FromFile(filepath,true);
           // Dummy.Save("image.bmp", ImageFormat.Bmp);
             Bitmap img = new Bitmap(filepath);

         

            // Bitmap img = new Bitmap("C:/Users/shandinefacey/source/repos/Testing/Pictures/bye.png");

            // img = Stenography.embedText(message, img);
            //   byte[] fileData = null;



            /* using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
             {
                 fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
             }
             ImageConverter imageConverter = new System.Drawing.ImageConverter();
             System.Drawing.Bitmap img = imageConverter.ConvertFrom(fileData) as System.Drawing.Bitmap;
             img.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg); */

             for (int i = 0; i < img.Width; i++)
               {
                   for (int j = 0; j < img.Height; j++)
                   {
                       Color pixel = img.GetPixel(i, j);

                       if (i < 1 && j < txtMsg.Text.Length)
                       {
                           Console.WriteLine("R = [" + i + "][" + j + "] = " + pixel.R);
                           Console.WriteLine("G = [" + i + "][" + j + "] = " + pixel.G);
                           Console.WriteLine("B = [" + i + "][" + j + "] = " + pixel.B);

                           char letter = Convert.ToChar(txtMsg.Text.Substring(j, 1));
                           int value = Convert.ToInt32(letter);
                           Console.WriteLine("letter : " + letter + " value : " + value);

                           img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                       }

                       if (i == img.Width - 1 && j == img.Height - 1)
                       {
                           img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, txtMsg.Text.Length ));
                       }

                   }

               }
            Bitmap image = new Bitmap(img);

             Response.ContentType = "image/jpeg";
             Response.AppendHeader("Content-Disposition", "attachment; filename=rice.jpg");

              image.Save(Response.OutputStream, ImageFormat.Jpeg);




            /*  FileStream sourceFile = new FileStream(filepath, FileMode.Open);
              float FileSize;
              FileSize = sourceFile.Length;
              byte[] fileContent = new byte[(int)FileSize];
              sourceFile.Read(fileContent, 0, (int)sourceFile.Length);
              sourceFile.Close();
              Response.ClearContent();
              Response.ClearHeaders();
              Response.Buffer = true;
              Response.ContentType = "application/octet-stream";
              Response.AddHeader("Content-Length", fileContent.Length.ToString());
              Response.AddHeader("Content-Disposition", "attachment; filename=" + "encoded.jpg");
              Response.BinaryWrite(fileContent);
              Response.Flush();
              Response.End();*/







            Label1.Text = filepath;

            
        }

        protected void btnDecode_Click(object sender, EventArgs e)
        {
           // string folderPath = Server.MapPath("~/Pictures/");
        //   String filepath = folderPath + Path.GetFileName(FileUpload1.FileName);

            string message = "";
            Bitmap img = new Bitmap(@"C:\Users\shandinefacey\source\repos\Testing\Pictures\rice.jpg");

          /*  byte[] fileData = null;
            using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
            {
                fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
            }
            ImageConverter imageConverter = new System.Drawing.ImageConverter();
            System.Drawing.Bitmap image = imageConverter.ConvertFrom(fileData) as System.Drawing.Bitmap;
            image.Save(Image1.ImageUrl, System.Drawing.Imaging.ImageFormat.Jpeg); */

         //   message = Stenography.extractText(img);

            

             Color lastpixel = img.GetPixel(img.Width - 1, img.Height - 1);
             int msgLength = lastpixel.B;

             for (int i = 0; i < img.Width; i++)
             {
                 for (int j = 0; j < img.Height; j++)
                 {
                     Color pixel = img.GetPixel(i, j);

                     if (i < 1 && j < msgLength)
                     {
                         int value = pixel.B;
                         char c = Convert.ToChar(value);
                         string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });

                         message = message + letter;
                     }
                 }
             }

            lblDecodedMsg.Text = message; 
        }

       
       
    }
}