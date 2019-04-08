using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using Testing.Model;


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
            string folderPath = Server.MapPath("~/Files/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            //Display the Picture in Image control.
            Image1.ImageUrl = "~/Files/" + Path.GetFileName(FileUpload1.FileName);


        }

      

      

        protected void btnEncode_Click(object sender, EventArgs e)
        {
             String filepath = "//Mac/Home/Pictures/Camera Roll/beach.jpg";
            
            String message = txtMsg.Text;
            Bitmap img = new Bitmap(filepath);

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);

                    if (i < 1 && j < message.Length)
                    {
                        Console.WriteLine("R = [" + i + "][" + j + "] = " + pixel.R);
                        Console.WriteLine("G = [" + i + "][" + j + "] = " + pixel.G);
                        Console.WriteLine("G = [" + i + "][" + j + "] = " + pixel.B);

                        char letter = Convert.ToChar(message.Substring(j, 1));
                        int value = Convert.ToInt32(letter);
                        Console.WriteLine("letter : " + letter + " value : " + value);

                        img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                    }

                    if (i == img.Width - 1 && j == img.Height - 1)
                    {
                        img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, message.Length));
                    }

                }
            }


            FileStream sourceFile = new FileStream(Server.MapPath(Image1.ImageUrl.ToString()), FileMode.Open);
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
            Response.End();

            //  SaveFileDialog saveFile = new SaveFileDialog();
            /*  saveFile.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
              saveFile.InitialDirectory = @"C:\Users\metech\Desktop";

              if (saveFile.ShowDialog() == DialogResult.OK)
              {
                  textBoxFilePath.Text = saveFile.FileName.ToString();
                  pictureBox1.ImageLocation = textBoxFilePath.Text;

                  img.Save(textBoxFilePath.Text);
              }*/
        }


        private void buttonDecode_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap("//Mac/Home/Pictures/Camera Roll/hi.png");
            string message = "";

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

             txtDecodedMsg.Text = message;
        }

    }
}