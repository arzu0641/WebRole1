using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using TestWebMsgApp;

namespace WebRole1
{
    public partial class Kayit : System.Web.UI.Page
    {
        #region Degiskenler
        OleDbConnection baglantiYolu;
        MailMessage mail = new MailMessage();
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {

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

        #region Kayit

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            int kullaniciAdiSonuc = 0;
            kullaniciAdiSonuc = KullaniciAdiKontrol();

            if (kullaniciAdiSonuc == 1)
            {
                WebMsgBox.Show("Kullanici Adi Kullanimda, Lutfen Yeni Bir Kullanici Adi Secin..");
                return;
            }

            if (this.txtimgcode.Text != this.Session["CaptchaImageText"].ToString())
            {
                WebMsgBox.Show("Captcha Gecersiz");
                return;
            }

            if (txtPass.Text != txtPassConfirm.Text)
            {
                WebMsgBox.Show("Girilen Sifreler Eslesmiyor, Tekrar Giriniz...");
                txtPass.Text = "";
                txtPassConfirm.Text = "";
                return;
            }


            if (txtEmail.Text == null || txtUsername.Text == null || txtHatirlatmaSoru.Text == null || txtHatirlatmaCevap.Text == null || txtName.Text == null || txtPass.Text == null || txtPassConfirm.Text == null)
            {
                WebMsgBox.Show("Alanlar Bos Gecilemez...");
                return;
            }

            string buguntarih = DateTime.Now.ToShortDateString();
            string username = "", pass = "", isim = "", email = "", hatirlatmaSorusu = "", hatirlatmaCevap = "";

            username = txtUsername.Text;
            pass = txtPass.Text;
            isim = txtName.Text;
            email = txtEmail.Text;
            hatirlatmaSorusu = txtHatirlatmaSoru.Text;
            hatirlatmaCevap = txtHatirlatmaCevap.Text;

            string strBaglanti = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\\CloudDb.accdb;Persist Security Info=False;";

            OleDbConnection conBaglanti = new OleDbConnection(strBaglanti);
            OleDbCommand cmdKullaniEkle = new OleDbCommand();

            cmdKullaniEkle.CommandType = CommandType.StoredProcedure;
            cmdKullaniEkle.CommandText = "sp_KullaniciEkle";

            // Username
            OleDbParameter prmUsername = new OleDbParameter();
            prmUsername.ParameterName = "@Username";
            prmUsername.OleDbType = OleDbType.VarChar;
            prmUsername.Size = 255;
            prmUsername.Value = username;


            // Pass

            String md5Pass = "";

            OleDbParameter prmPass = new OleDbParameter();
            prmPass.ParameterName = "@Password";
            prmPass.OleDbType = OleDbType.VarChar;
            prmPass.Size = 255;

            md5Pass = MD5Hash(pass);

            prmPass.Value = md5Pass;

            // Email
            OleDbParameter prmEmail = new OleDbParameter();
            prmEmail.ParameterName = "@Email";
            prmEmail.OleDbType = OleDbType.VarChar;
            prmEmail.Size = 255;
            prmEmail.Value = email;

            // isim
            OleDbParameter prmIsim = new OleDbParameter();
            prmIsim.ParameterName = "@Isim";
            prmIsim.OleDbType = OleDbType.VarChar;
            prmIsim.Size = 255;
            prmIsim.Value = isim;


            // hatirlatmaSorusu
            OleDbParameter prmHatirlatmaSoru = new OleDbParameter();
            prmHatirlatmaSoru.ParameterName = "@HatirlatmaSorusu";
            prmHatirlatmaSoru.OleDbType = OleDbType.VarChar;
            prmHatirlatmaSoru.Size = 255;
            prmHatirlatmaSoru.Value = hatirlatmaSorusu;

            // hatirlatmaCevap
            OleDbParameter prmHatirlatmaCevap = new OleDbParameter();
            prmHatirlatmaCevap.ParameterName = "@HatirlatmaCevap";
            prmHatirlatmaCevap.OleDbType = OleDbType.VarChar;
            prmHatirlatmaCevap.Size = 255;
            prmHatirlatmaCevap.Value = hatirlatmaCevap;


            // kayitTarihi
            OleDbParameter prmKayitTarihi = new OleDbParameter();
            prmKayitTarihi.ParameterName = "@KayitTarihi";
            prmKayitTarihi.OleDbType = OleDbType.Date;
            prmKayitTarihi.Size = 255;
            prmKayitTarihi.Value = buguntarih;

            string kullaniciKod = "";

            kullaniciKod = System.Web.Security.Membership.GeneratePassword(25, 10);

            // kullaniciKod
            OleDbParameter prmKullaniciKod = new OleDbParameter();
            prmKullaniciKod.ParameterName = "@KullaniciKod";
            prmKullaniciKod.OleDbType = OleDbType.VarChar;
            prmKullaniciKod.Size = 255;
            prmKullaniciKod.Value = kullaniciKod;

            /***** Parametreleri komuta ekle ****/
            //VALUES ([@Username], [@Password], [@Isim], [@Email], [@HatirlatmaSorusu], [@HatirlatmaCevap],[@KayitTarihi]);
            cmdKullaniEkle.Parameters.Add(prmUsername);
            cmdKullaniEkle.Parameters.Add(prmPass);
            cmdKullaniEkle.Parameters.Add(prmIsim);
            cmdKullaniEkle.Parameters.Add(prmEmail);
            cmdKullaniEkle.Parameters.Add(prmHatirlatmaSoru);
            cmdKullaniEkle.Parameters.Add(prmHatirlatmaCevap);
            cmdKullaniEkle.Parameters.Add(prmKayitTarihi);
            cmdKullaniEkle.Parameters.Add(prmKullaniciKod);

            // Komuta bağlantı nesnemizi de ekleyelim:
            cmdKullaniEkle.Connection = conBaglanti;

            // Bağlantıyı açalım:
            conBaglanti.Open();

            // Komutu çağıralım ve sonucu ekrana yazdıralım:
            int sonuc = cmdKullaniEkle.ExecuteNonQuery();
            WebMsgBox.Show(isim + " kaydedildi...");

            string mailsonuc = "";

            mailsonuc = SendMail(txtEmail.Text, kullaniciKod);

            // Bağlantıyı kopart.
            conBaglanti.Close();
            Response.Redirect("Aktivasyon.aspx");
        }
        #endregion

        #region Md5
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

        #region KullaniciAdiKontrol
        private int KullaniciAdiKontrol()
        {
            OleDbCommand sorgu;
            OleDbDataReader veri;
            int sonuc = 0, bayrak = 0;
            try
            {
                Baglanti();// bağlantı açıldı
                sorgu = new OleDbCommand();//sorgu komutu oluşturuldu
                sorgu.CommandText = "select Status FROM Kullanici where Username = '" + txtUsername.Text + "'";
                sorgu.Connection = baglantiYolu;//sorgu bağlantı yoluna gönderildi
                veri = sorgu.ExecuteReader();// sorgu sunuçları okundu veri yığınına atıldı
                while (veri.Read())
                {
                    sonuc = (int)veri[0];   // veri yığınındaki bilgiler döngü bitene kadar listBoxa aktarılacak
                    bayrak = 1;
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

        #region MailGonder
        public string SendMail(string toList, string password)
        {


            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress("gazibulutbilisim@gmail.com");
                message.From = fromAddress;
                message.To.Add(toList);
                message.Subject = "Gazi Bulut Bilisim";
                message.IsBodyHtml = true;
                message.Body = password;
                // We use gmail as our smtp client
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(
                    "gazibulutbilisim@gmail.com", "11**22**33");

                smtpClient.Send(message);
                msg = "Successful<BR>";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion

    }
}