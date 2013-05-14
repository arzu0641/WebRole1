using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data.OleDb;
using TestWebMsgApp;
namespace WebRole1
{
    public partial class Giris : System.Web.UI.Page
    {
        OleDbConnection baglantiYolu;

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Md5Hash
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        #endregion

        #region Baglanti
        public void Baglanti()
        {
            try
            {
                baglantiYolu = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:/CloudDb.accdb");
                baglantiYolu.Open();
            }
            catch (OleDbException ex)
            {
                WebMsgBox.Show(ex.Message);
            }
        }
        public void BaglantiKapat()
        {
            try
            {
                baglantiYolu.Close();
                baglantiYolu.Dispose();
            }
            catch (OleDbException ex)
            {
                WebMsgBox.Show(ex.Message);
            }
        }
        #endregion

        #region Login
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtKullaniciAdi.Text.Equals("") || txtSifre.Text.Equals("") || txtKullaniciKod.Text.Equals(""))
            {
                WebMsgBox.Show("Alanlar Bos Gecilemez...");
            }

            int aktSonuc = 0;

            aktSonuc = AktivasyonKontrol();

            if (aktSonuc == 0)
            {
                WebMsgBox.Show("Aktivasyon Sayfasina Yonlendiriliyorsunuz");
                Response.Redirect("Aktivasyon.aspx");
                return;
            }

            String sessionKod = (String)Session["kod"];

            //Check weather session variable null or not
            if (sessionKod != txtKullaniciKod.Text)
            {
                WebMsgBox.Show("Kullanici Kodu Sayfasindan Kod Olusturunuz...");
                return;
            }

            string username = "", pass = "", kod = "";
            username = txtKullaniciAdi.Text;
            pass = txtSifre.Text;
            kod = txtKullaniciKod.Text;
            bool sonuc = false;

            if (txtKullaniciAdi.Text.Equals("") || txtSifre.Text.Equals("") || txtKullaniciKod.Text.Equals(""))
            {
                WebMsgBox.Show("Alanlar Bos Gecilemez...");
            }
            else
            {
                sonuc = CheckUser();

                if (sonuc == true)
                {
                    Session["username"] = txtKullaniciAdi.Text;
                    //         Session["yetki"] = "user";
                    Response.Redirect("AnaSayfa.aspx");
                }
                else
                {
                    WebMsgBox.Show("Gecersiz Giris");
                }
            }

        }
        #endregion

        #region CheckUserStatus
        public bool CheckUserStatus()
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\CloudDb.accdb";
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String md5Pass = "";
            md5Pass = MD5Hash(txtSifre.Text);

            command.CommandText = "select Status FROM Kullanici where Username = '" + txtKullaniciAdi.Text + "'";

            try
            {
                connection.Open();
                int status = (int)(command.ExecuteScalar());
                if (status != 0)
                {
                    return true;
                }
            }

            catch (Exception err)
            {

                WebMsgBox.Show(err.Message);
                return false;
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

        #region CheckUser
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

        #region AktivasyonKontrol
        private int AktivasyonKontrol()
        {
            OleDbCommand sorgu;
            OleDbDataReader veri;
            int sonuc = 0, bayrak = 0;
            try
            {
                Baglanti();// bağlantı açıldı
                sorgu = new OleDbCommand();//sorgu komutu oluşturuldu
                sorgu.CommandText = "select Status FROM Kullanici where Username = '" + txtKullaniciAdi.Text + "'";
                sorgu.Connection = baglantiYolu;//sorgu bağlantı yoluna gönderildi
                veri = sorgu.ExecuteReader();// sorgu sunuçları okundu veri yığınına atıldı
                while (veri.Read())
                {
                    sonuc = (int)veri[0];   // veri yığınındaki bilgiler döngü bitene kadar listBoxa aktarılacak

                    if (sonuc == 1)
                    {
                        bayrak = 1;
                    }
                }
                sorgu.Dispose();
            }
            catch (OleDbException ex)
            {
                bayrak = 0;
                WebMsgBox.Show(ex.Message);
            }
            finally
            {
                if (bayrak == 1)
                {
                    sonuc = 1;
                }
                else
                {
                    sonuc = 0;
                }

                BaglantiKapat();

            }
            return sonuc;

        }

        #endregion
    }
}