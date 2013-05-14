using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebMsgApp;
namespace WebRole1
{
    public partial class GonderiSorgu : System.Web.UI.Page
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            Ptt.Sorgu srv = new Ptt.Sorgu();
            Ptt.Input input = new Ptt.Input();
            Ptt.OutputTum sonucTum = new Ptt.OutputTum();

            srv.Url = "https://pttws.ptt.gov.tr/GonderiTakipV2/services/Sorgu?wsdl";


            input.kullanici = "PttWs";
            input.sifre = "YazDes*1840";
            input.barkod = txtBarkod.Text.Trim();
            sonucTum = srv.gonderiSorgu(input);
            txtSonuc.Text += "";
            txtSonuc.Text += "Sonuc Aciklama : " + sonucTum.sonucAciklama + "\n";
            txtSonuc.Text += "ALICI : " + sonucTum.ALICI + "\n";
            txtSonuc.Text += "GONDERICI : " + sonucTum.GONDEREN + "\n";
            txtSonuc.Text += "UCRET : " + sonucTum.GONUCR + "\n";

            for (int i = 0; i < sonucTum.dongu.Length; i++)
            {
                txtSonuc.Text += sonucTum.dongu[i].siraNo + " " + sonucTum.dongu[i].ITARIH + " " + sonucTum.dongu[i].IMERK + " " + sonucTum.dongu[i].ISLEM + "\n"; ;
            }


        }
        #endregion
    }
}