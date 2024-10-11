using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_1.Web
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ButtonViewFeed_Click(object sender, EventArgs e)
        {
            // 隱藏影片播放器，顯示 WebView
            videoPlaceHolder.Visible = false;
            webViewPlaceHolder.Visible = true;
            Label3.Text = "即時監控";
        }

        protected void Add_check_Click(object sender, EventArgs e)
        {

        }

        protected void back_Click(object sender, EventArgs e)
        {

        }

        protected void save_Click(object sender, EventArgs e)
        {

        }

        protected void FDetail_back_Click(object sender, EventArgs e)
        {

        }

        protected void Fback_Click(object sender, EventArgs e)
        {

        }

        protected void Fedit_Click(object sender, EventArgs e)
        {

        }

        protected void Fsave_Click(object sender, EventArgs e)
        {

        }

        protected void edit_Click(object sender, EventArgs e)
        {

        }
    }
}