using System;
using System.Drawing.Imaging;

namespace WebRole1
{
    public partial class CImage : System.Web.UI.Page
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["CaptchaImageText"] = GenerateRandomCode();
            RandomImage ci = new RandomImage(this.Session
                ["CaptchaImageText"].ToString(), 300, 75);
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            ci.Dispose();
        }

        #endregion
        #region GenerateRandomCode
        private string GenerateRandomCode()
        {
            Random r = new Random();
            string s = "";
            for (int j = 0; j < 5; j++)
            {
                int i = r.Next(3);
                int ch;
                switch (i)
                {
                    case 1:
                        ch = r.Next(0, 9);
                        s = s + ch.ToString();
                        break;
                    case 2:
                        ch = r.Next(65, 90);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    case 3:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    default:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                }
                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }
        #endregion
    }
}