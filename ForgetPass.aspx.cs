using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
namespace web_1.Web
{
    public partial class ForgetPass1 : System.Web.UI.Page
    {
        static string connectionString = "server=203.64.84.154;database=care;uid=root;password=Topic@2024;port = 33061";

        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["cAccount"] == null && Session["homeAccount"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}


            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                {
                    Response.Redirect("Login.aspx");
                }

                string email = HttpUtility.UrlDecode(token);
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try {
                        connection.Open();
                        string query = "SELECT homeEmail FROM HomeLogin WHERE homeEmail = @Email";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Email", email);
                        MySqlDataReader reader = command.ExecuteReader();

                        if (!reader.Read())
                        {
                            Response.Redirect("Login.aspx");
                            return;
                        }

                        // 将邮箱地址存储到会话中
                        Session["ResetEmail"] = email;
                    }
                    catch (Exception ex)
                    {
                        // 記錄錯誤
                        Console.WriteLine("Exception: " + ex.Message);
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                        }
                        // 顯示錯誤信息到頁面
                        LabelMessage.Visible = true;
                        LabelMessage.Text = "無法連接到數據庫，請稍後再試。";
                    }
                }

                //if (string.IsNullOrEmpty(token))
                //{
                //    Console.WriteLine("Token is missing in the query string.");
                //    Response.Redirect("Login.aspx");
                //    return;
                //}

                //if (Session[token] == null)//卡在這裡
                //{
                //    Console.WriteLine("Token is not found in the session.");
                //    Response.Redirect("Login.aspx");
                //    return;
                //}

                //Console.WriteLine("Token found: " + token);
            }
        }

        protected void ButtonResetPassword_Click(object sender, EventArgs e)
        {
            //string token = Request.QueryString["token"];
            string email = Session["ResetEmail"] as string;
            if (string.IsNullOrEmpty(email))
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "不可為空";
                return;
            }

            string newPassword = TextBoxNewPassword.Text.Trim();
            string confirmPassword = TextBoxConfirmPassword.Text.Trim();

            if (newPassword != confirmPassword)
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "密碼不正確";
                return;
            }
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE HomeLogin SET homePassword = @homePassword WHERE homeEmail = @homeEmail";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homePassword", newPassword);
                command.Parameters.AddWithValue("@homeEmail", email);
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    //LabelMessage.Visible = true;
                    //LabelMessage.Text = "密碼重置成功";
                    string script = "alert('新增成功'); setTimeout(function(){ window.location='Login.aspx'; }, 500);"; // 延遲0.5秒
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);

                }
                else
                {
                    LabelMessage.Visible = true;
                    LabelMessage.Text = "重置密碼時發生錯誤。請再試一次";
                }
            }
        }
    }
}