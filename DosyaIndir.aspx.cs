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
    public partial class DosyaIndir : System.Web.UI.Page
    {
        #region Degiskenler
        private const string Salt = "d5fg4df5sg4ds5fg45sdfg4";

        #endregion

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

        #region SifreCoz
        protected void btnSifreCoz_Click(object sender, EventArgs e)
        {
            String tarih = "";

            tarih = (DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString());

            string fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = Server.MapPath("Data") + "\\" + fn;
            if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
            {

                try
                {
                    FileUpload1.PostedFile.SaveAs(SaveLocation);
                    Response.Write("Dosya Yuklendi");
                }
                catch (Exception ex)
                {
                    WebMsgBox.Show("Error: " + ex.Message);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Dosya Secin");
                return;
            }

            string path = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            string source = Server.MapPath("Data") + "\\" + fn;
            string destination = "";
            string sKey = txtPassword.Text;


            try
            {
                if (rblAlgoritma.SelectedItem.Value == "Des")
                {
                    destination = Server.MapPath("Data") + "\\" + path + "_DES_" + tarih + ".txt";
                    //DecryptFileDes(source, destination, sKey);
                    DecryptFile(source, destination, sKey);

                    WebMsgBox.Show("Dosya Indirildi");
                    System.IO.File.Delete(source);

                    Label3.Visible = false;
                    txtPassword.Visible = false;

                }
                if (rblAlgoritma.SelectedItem.Value == "Aes")
                {
                    destination = Server.MapPath("Data") + "\\" + path + "_AES_" + tarih + ".txt";
                    DecryptFileAes(source, destination, sKey);

                    WebMsgBox.Show("Dosya Indirildi");
                    System.IO.File.Delete(source);


                    Label3.Visible = true;
                    txtPassword.Visible = true;
                    txtPassword.Text = sKey;
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show("Error: " + ex.Message);

            }

        }
        #endregion

        #region Des
        static void DecryptFile(string sInputFilename,
                    string sOutputFilename,
                    string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
                                           FileMode.Open,
                                           FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                                                         desdecrypt,
                                                         CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }

        private void DecryptFileDes(string source, string destination, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(source,
                                           FileMode.Open,
                                           FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                                                         desdecrypt,
                                                         CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(destination);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }
        #endregion

        #region Aes
        internal static void DecryptFileAes(string inputPath, string outputPath, string password)
        {
            var input = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var output = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);

            // Essentially, if you want to use RijndaelManaged as AES you need to make sure that:
            // 1.The block size is set to 128 bits
            // 2.You are not using CFB mode, or if you are the feedback size is also 128 bits
            var algorithm = new RijndaelManaged { KeySize = 256, BlockSize = 128 };
            var key = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(Salt));

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            try
            {
                using (var decryptedStream = new CryptoStream(output, algorithm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    CopyStream(input, decryptedStream);
                }
            }
            catch (CryptographicException)
            {
                throw new InvalidDataException("Please supply a correct password");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private const int SizeOfBuffer = 1024 * 8;

        private static void CopyStream(Stream input, Stream output)
        {
            using (output)
            using (input)
            {
                byte[] buffer = new byte[SizeOfBuffer];
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, read);
                }
            }
        }

        #endregion

    }
}