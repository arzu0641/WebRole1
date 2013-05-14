using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebMsgApp;
namespace WebRole1
{
    public partial class SpkSorgu : System.Web.UI.Page
    {
        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            String username = (String)Session["username"];
            if (username == null)
            {
                Response.Write("Kullanici Girisi Yapiniz");
                WebMsgBox.Show("Kullanici Girisi Yapiniz");


                Response.Redirect("Kod.aspx");
                return;
            }


        }
        #endregion

        #region Sorgula
        protected void btnSorgula_Click(object sender, EventArgs e)
        {
            Spk.MFundsServiceSoapClient srv = new Spk.MFundsServiceSoapClient();
            string[] sonuc;

            sonuc = srv.GetFundInfos(txtFonKod.Text);

            txtSonuc.Text = "";
            for (int i = 0; i < sonuc.Length; i++)
            {
                txtSonuc.Text += sonuc[i] + "\n";
            }
        }
        #endregion
    }
}