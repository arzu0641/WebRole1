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
    public partial class DosyaYukle : System.Web.UI.Page
    {
        #region Degiskenler
        private const string Salt = "d5fg4df5sg4ds5fg45sdfg4";
        private const int SizeOfBuffer = 1024 * 8;

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

            Label2.Visible = false;
            txtPassword.Visible = false;

        }

        #endregion

        #region Sifrele
        protected void btnSifrele_Click(object sender, EventArgs e)
        {
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
            string sKey = GenerateKey();

            string kullaniciKod = "";

            kullaniciKod = System.Web.Security.Membership.GeneratePassword(8, 8);

            sKey = kullaniciKod;
            try
            {
                if (rblAlgoritma.SelectedItem.Value == "Des")
                {

                    destination = Server.MapPath("Data") + "\\" + path + "_Des.txt";
                    EncryptFileDes(source, destination, sKey);

                    WebMsgBox.Show("Dosya Sifrelendi");
                    System.IO.File.Delete(source);

                    Label2.Visible = true;
                    txtPassword.Visible = true;
                    txtPassword.Text = sKey;
                }
                if (rblAlgoritma.SelectedItem.Value == "Aes")
                {
                    destination = Server.MapPath("Data") + "\\" + path + "_Aes.txt";
                    EncryptFileAes(source, destination, sKey);

                    WebMsgBox.Show("Dosya Sifrelendi");


                    Label2.Visible = true;
                    txtPassword.Visible = true;
                    txtPassword.Text = sKey;
                    System.IO.File.Delete(source);
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show("Error: " + ex.Message);

            }

        }


        #endregion

        #region Des
        private void EncryptFileDes(string source, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(source,
                FileMode.Open,
                FileAccess.Read);

            FileStream fsEncrypted = new FileStream(destination,
                            FileMode.Create,
                            FileAccess.Write);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
                                desencrypt,
                                CryptoStreamMode.Write);
            byte[] bytearrayinput = new byte[fsInput.Length - 1];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();

        }


        private string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);

        }
        #endregion

        #region Aes
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
        internal static void EncryptFileAes(string inputPath, string
    outputPath, string password)
        {
            var input = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var output = new FileStream(outputPath, FileMode.OpenOrCreate,
    FileAccess.Write);


            var algorithm = new RijndaelManaged { KeySize = 256, BlockSize = 128 };
            var key = new Rfc2898DeriveBytes(password,
    Encoding.ASCII.GetBytes(Salt));

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            using (var encryptedStream = new CryptoStream(output,
    algorithm.CreateEncryptor(), CryptoStreamMode.Write))
            {
                CopyStream(input, encryptedStream);
            }
        }
        public static string GenerateHashWithSalt(string password, string salt)
        {
            // merge password and salt together
            string sHashWithSalt = password + salt;
            // convert this merged value to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // return the has as a base 64 encoded string
            return Convert.ToBase64String(hash);
        }
        #endregion
    }
}