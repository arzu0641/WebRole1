using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebMsgApp;

namespace WebRole1
{
    public partial class Kontrol : System.Web.UI.Page
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            String username = (String)Session["username"];
            if (username == null)
            {
                WebMsgBox.Show("Kullanici Girisi Yapiniz");
                Response.Redirect("Kod.aspx");
                return;
            }
        }
        #endregion

        #region DosyaVarMi
        static bool FileExist(string p)
        {
            if (File.Exists(p))
            {
                return true;
            }
            WebMsgBox.Show("dosya bulunamadi");
            return false;
        }
        #endregion

        #region Md5Hash
        public string Compute_MD5Hash_FromFile(string FileNameAndPath)
        {
            using (FileStream fs = new FileStream(FileNameAndPath,
            FileMode.Open))
            {
                return Convert.ToBase64String(new
                MD5CryptoServiceProvider().ComputeHash(fs));
            }
        }

        #endregion

        #region Hesapla
        protected void btnHesapla_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text;
            bool dosyaVarMi = false;

            if (path != null)
            {
                dosyaVarMi = FileExist(path);

                if (dosyaVarMi == true)
                {
                    FileStream file = new FileStream(path, FileMode.Open);
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] retVal = md5.ComputeHash(file);
                    file.Close();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }

                    string sMD5hash = sb.ToString();

                    txtSonuc.Text = "";
                    txtSonuc.Text = "MD5 : " + sb.ToString();

                    FileStream file2 = new FileStream(path, FileMode.Open);
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] retVal2 = sha1.ComputeHash(file2);
                    file2.Close();

                    StringBuilder sb2 = new StringBuilder();
                    for (int i = 0; i < retVal2.Length; i++)
                    {
                        sb2.Append(retVal2[i].ToString("x2"));
                    }

                    string sSHA1hash = sb2.ToString();

                    txtSonuc.Text += "\n" + "SHA1 : " + sb2.ToString();

                }
            }
        }
        #endregion
    }
}