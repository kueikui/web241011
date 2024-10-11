using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_1.Web
{
    public partial class WebForm4 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // 初始化監視器畫面
                    if (CheckUrl("http://203.64.84.154:8088/video_feed"))
                    {
                        monitorFrame.Attributes["src"] = "http://203.64.84.154:8088/video_feed";
                        LabelError.Text = "";  // 清除錯誤訊息
                    }
                    else
                    {
                        LabelError.Text = "無法加載即時畫面，請檢查網路連線或網址。";
                    }
                }
                catch (Exception ex)
                {
                    LabelError.Text = $"發生錯誤: {ex.Message}";
                }
            }
        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUrl("http://203.64.84.154:8088/video_feed"))
                {
                    monitorFrame.Attributes["src"] = "http://203.64.84.154:8088/video_feed";
                    LabelError.Text = "";  // 清除錯誤訊息
                }
                else
                {
                    LabelError.Text = "無法加載即時畫面，請檢查網路連線或網址。";
                }
            }
            catch (Exception ex)
            {
                LabelError.Text = $"發生錯誤: {ex.Message}";
            }
        }

        protected void ButtonSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUrl("http://203.64.84.154:8088/another_feed"))
                {
                    monitorFrame.Attributes["src"] = "http://203.64.84.154:8088/another_feed";
                    LabelError.Text = "";  // 清除錯誤訊息
                }
                else
                {
                    LabelError.Text = "無法加載另一個畫面，請檢查網路連線或網址。";
                }
            }
            catch (Exception ex)
            {
                LabelError.Text = $"發生錯誤: {ex.Message}";
            }
        }

        // 驗證URL是否有效
        private bool CheckUrl(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
