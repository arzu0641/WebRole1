using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebMsgApp;
namespace WebRole1
{
    public partial class KimlikSorgu : System.Web.UI.Page
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

        #region Sorgu
        protected void btnDogrula_Click(object sender, EventArgs e)
        {
            long tckimlik = long.Parse(txtTcKimlik.Text);
            int dogumYili = Int32.Parse(txtDogumYili.Text);
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;

            bool? durum;

            try
            {

                using (Kimlik.KPSPublicSoapClient servis = new Kimlik.KPSPublicSoapClient())
                {
                    durum = servis.TCKimlikNoDogrula(tckimlik, ad.ToUpper(), soyad.ToUpper(), dogumYili);
                }
            }
            catch
            {
                durum = null;
            }

            WebMsgBox.Show(durum.ToString());

        }
        #endregion
    }
}