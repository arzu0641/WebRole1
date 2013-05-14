using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebMsgApp;
namespace WebRole1
{
    public partial class Aktivasyon : System.Web.UI.Page
    {
        OleDbConnection baglantiYolu;
        String kullaniciKod = "";

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

        #region aktivasyon
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtKullaniciAdi.Text.Equals("") || txtSifre.Text.Equals("") || txtKullaniciKod.Text.Equals(""))
            {
                WebMsgBox.Show("Alanlar Bos Gecilemez...");
                return;
            }

            int aktivasyonSonuc = 0;

            aktivasyonSonuc = AktivasyonKontrol();

            //Check weather session variable null or not
            if (aktivasyonSonuc == 1)
            {
                WebMsgBox.Show("Hesap Aktive Edilmis...");
                Response.Redirect("Giris.aspx");
                return;
            }

            if (txtKullaniciKod.Text != kullaniciKod)
            {
                WebMsgBox.Show("Kullanici Kod Eslesmiyor...");
                return;
            }

            string username = "", pass = "", kod = "";
            username = txtKullaniciAdi.Text;
            pass = txtSifre.Text;
            kod = txtKullaniciKod.Text;
            bool sonuc = false;
            bool guncellemeSonuc = false;

            if (txtKullaniciAdi.Text.Equals("") || txtSifre.Text.Equals("") || txtKullaniciKod.Text.Equals(""))
            {
                WebMsgBox.Show("Alanlar Bos Gecilemez...");
                return;
            }
            else
            {
                sonuc = CheckUser();

                if (sonuc == true)
                {

                    //guncellemeSonuc = Guncelle();
                    guncellemeSonuc = VeriDüzenle();

                    if (guncellemeSonuc == true)
                    {
                        Response.Redirect("AnaSayfa.aspx");
                    }
                    else
                    {
                        WebMsgBox.Show("Aktivasyon Basarisiz Oldu");
                    }
                }
                else
                {
                    WebMsgBox.Show("Gecersiz Giris");
                }
            }

        }
        #endregion

        #region Guncelle
        private bool VeriDüzenle()
        {
            Baglanti();
            OleDbCommand düzenleSrgusu = new OleDbCommand();
            string sorgu = "update Kullanici set Status=" + 1 + " where Username='" + txtKullaniciAdi.Text + "'";

            düzenleSrgusu.CommandText = sorgu;
            düzenleSrgusu.Connection = baglantiYolu;

            try
            {
                düzenleSrgusu.ExecuteNonQuery();
                baglantiYolu.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region AktivasyonKontrol
        private int AktivasyonKontrol()
        {
            OleDbCommand sorgu, sorgu2;
            OleDbDataReader veri, veri2;
            int sonuc = 0, bayrak = 0, bayrak2 = 0;
            try
            {
                Baglanti();// bağlantı açıldı
                sorgu = new OleDbCommand();//sorgu komutu oluşturuldu
                sorgu.CommandText = "select Status FROM Kullanici where Username = '" + txtKullaniciAdi.Text + "'";
                sorgu.Connection = baglantiYolu;//sorgu bağlantı yoluna gönderildi
                veri = sorgu.ExecuteReader();// sorgu sunuçları okundu veri yığınına atıldı
                while (veri.Read())
                {
                    bayrak2 = 1;
                    sonuc = (int)veri[0];   // veri yığınındaki bilgiler döngü bitene kadar listBoxa aktarılacak
                    if (sonuc == 1)
                    {
                        bayrak = 1;
                    }
                }

                sorgu.Dispose();

                if (bayrak2 == 1)
                {
                    sorgu2 = new OleDbCommand();//sorgu komutu oluşturuldu
                    sorgu2.CommandText = "select KullaniciKod FROM Kullanici where Username = '" + txtKullaniciAdi.Text + "'";
                    sorgu2.Connection = baglantiYolu;//sorgu bağlantı yoluna gönderildi
                    veri2 = sorgu2.ExecuteReader();// sorgu sunuçları okundu veri yığınına atıldı

                    while (veri2.Read())
                    {
                        kullaniciKod = (String)veri2[0];
                    }
                    sorgu2.Dispose();
                }


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

        #region UserKontrolu
        public bool CheckUser()
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\CloudDb.accdb";
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String md5Pass = "";
            md5Pass = MD5Hash(txtSifre.Text);
            command.CommandText = "select count(*) FROM Kullanici where Username = '" + txtKullaniciAdi.Text + "'" + " AND Password = '" + md5Pass + "' AND KullaniciKod = '" + txtKullaniciKod.Text + "'";
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

    }
}