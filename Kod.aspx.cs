using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using TestWebMsgApp;
using System.Data.OleDb;

namespace WebRole1
{
    public partial class Kod : System.Web.UI.Page
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region SaltOlustur
        public static string GenerateHashWithSalt(string password, string salt)
        {
            string sHashWithSalt = password + salt;
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            return Convert.ToBase64String(hash);
        }
        public string CreateSalt(string UserName)
        {
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(UserName,
                System.Text.Encoding.Default.GetBytes("thisisasalt"), 10000);
            return Convert.ToBase64String(hasher.GetBytes(25));
        }

        #endregion

        #region KodOlustur
        protected void btnKod_Click(object sender, EventArgs e)
        {
            bool userSonuc = false;

            userSonuc = CheckUser();

            if (userSonuc == false)
            {
                WebMsgBox.Show("Bilgiler Hatali");
                return;
            }

            String salt = "", salt1 = "", salt2 = "";
            String mail = "";
            String kod = "";

            String tarih = "";

            tarih = (DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString());

            mail = txtMail.Text;

            salt1 = CreateSalt(txtKullaniciAdi.Text);
            salt2 = CreateSalt(tarih);

            salt = salt2 + salt1;

            string password = txtSifre.Text;  // original password

            byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + password);

            string passwordHashMD5 = "";
            string passwordHashSha1 = "";
            string passwordHashSha256 = "";
            string passwordHashSha384 = "";
            string passwordHashSha512 = "";

            if (RadioButtonList1.SelectedItem.Value == "SHA1")
            {
                passwordHashSha1 =
                   KodHash.ComputeHash(password, "SHA1", null);
                txtKod.Text = passwordHashSha1;
                kod = passwordHashSha1;
            }
            if (RadioButtonList1.SelectedItem.Value == "SHA256")
            {
                passwordHashSha256 =
                   KodHash.ComputeHash(password, "SHA256", null);
                txtKod.Text = passwordHashSha256;
                kod = passwordHashSha256;
            }
            if (RadioButtonList1.SelectedItem.Value == "SHA384")
            {
                passwordHashSha384 =
                   KodHash.ComputeHash(password, "SHA384", null);
                txtKod.Text = passwordHashSha384;
                kod = passwordHashSha384;
            }
            if (RadioButtonList1.SelectedItem.Value == "SHA512")
            {
                passwordHashSha512 =
                   KodHash.ComputeHash(password, "SHA512", null);
                txtKod.Text = passwordHashSha512;
                kod = passwordHashSha512;
            }
            if (RadioButtonList1.SelectedItem.Value == "MD5")
            {
                passwordHashMD5 =
                  KodHash.ComputeHash(mail, "MD5", data);

                txtKod.Text = passwordHashMD5;
                kod = passwordHashMD5;
            }

            Session["kod"] = kod;

        }
        #endregion

        #region HashOlustur
        private static string GenerateHash(string value, string salt)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + value);
            data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }

        #endregion

        #region KullaniciKontrol
        public bool CheckUser()
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\CloudDb.accdb";
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String md5Pass = "";
            md5Pass = MD5Hash(txtSifre.Text);
            command.CommandText = "select count(*) FROM Kullanici where Username = '" + txtKullaniciAdi.Text + "'" + " AND Password = '" + md5Pass + "' AND Status = " + 1 + "";
            try
            {
                connection.Open();
                int count = (int)(command.ExecuteScalar());

                if (count != 0)
                {
                    return true;
                }
            }

            catch (Exception err)
            {
                WebMsgBox.Show(err.Message);
            }

            finally
            {
                connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return false;
        }
        #endregion

        #region Md5Hash
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        #endregion
    }
}