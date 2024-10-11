using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;

namespace web_1.Web
{
    public partial class Login : System.Web.UI.Page
    {
        static string connectionString = "server=203.64.84.154;database=care;uid=root;password=Topic@2024;port = 33061";
        protected void Page_Load(object sender, EventArgs e)
        {
            //https://ithelp.ithome.com.tw/articles/10265283?sc=rss.iron
            if (Session["gap"] == null)
            {
                Session["gap"] = "0";//gap=0為系統登入 gap=1為居家登入
            }
        }
        protected void Button1_Click(object sender, EventArgs e)//系統登入
        {
            Session["gap"] = "0";
            sign_up.Visible = false;
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Button1.BackColor = System.Drawing.Color.Aqua;
            Button2.BackColor = System.Drawing.Color.LightBlue;
            signup_name.Text = "";
            signup_account.Text = "";
            signup_password.Text = "";
            Label1.Visible = false;
            cAccountText.Text = "";
            cPasswordText.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)//居家登入
        {
            Session["gap"] = "1";
            sign_up.Visible = true;
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Button2.BackColor = System.Drawing.Color.Aqua;
            Button1.BackColor = System.Drawing.Color.LightBlue;
            signup_name.Text = "";
            signup_account.Text = "";
            signup_password.Text = "";
            Label1.Visible = false;
            cAccountText.Text = "";
            cPasswordText.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)//登入按鈕
        {
            string account = cAccountText.Text.Trim();
            string password = cPasswordText.Text.Trim();
            int gap = Convert.ToInt32(Session["gap"]); // 从会话变量中获取 gap 的值
            if (gap == 0)//(Session["gap"] == "0")
            {//系統登入
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT CarerLogin.cId,CarerLogin.cAccount, Carer.cName FROM CarerLogin JOIN Carer ON CarerLogin.cId = Carer.cId WHERE CarerLogin.cAccount = @Account AND CarerLogin.cPassword = @Password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Account", account);
                command.Parameters.AddWithValue("@Password", password);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Session["cAccount"] = reader["cAccount"].ToString();
                    Session["userName"] = reader["cName"].ToString();
                    Session["cId"] = reader["cId"].ToString();
                    Session["LoginType"] = "System";
                    Response.Redirect("WebForm1.aspx");
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "帳號或密碼不正確";
                }
                reader.Close();
                connection.Close();
            }
            else if (gap == 1)
            {//居家登入
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT * FROM HomeLogin WHERE homeEmail = @homeEmail AND homePassword = @homePassword";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homeEmail", account);
                command.Parameters.AddWithValue("@homePassword", password);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Session["homeAccount"] = reader["homeEmail"].ToString();
                    Session["userName"] = reader["homeUserName"].ToString();
                    Session["LoginType"] = "Home";
                    Response.Redirect("WebForm1.aspx");
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "帳號或密碼不正確";
                }
                reader.Close();
                connection.Close();
            }
        }

        protected void next_Click(object sender, EventArgs e)//註冊帳號下一步
        {
            Panel2.Visible = false;
            Panel5.Visible = true;

            Random random = new Random();// 生成隨機碼
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomCode = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

            Session["VerificationCode"] = randomCode;
            string email = signup_account.Text;// 發送電子郵件
            SendVerificationCodeEmail(email, randomCode);
        }
        private void SendVerificationCodeEmail(string email, string code)
        {
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpUser);
            mail.To.Add(email);
            mail.Subject = "全方位守護者-註冊帳號驗證碼";
            mail.Body = $"您好!\n\n您的驗證碼是：{code}\n請使用此驗證碼完成註冊。";
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
            smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                Label2.Text = "驗證碼已發送到您的電子郵件。";
                Label2.Visible = true;
            }
            catch (Exception ex)
            {
                Label2.Text = "發送驗證碼失敗，請稍後再試。";
                Label2.Visible = true;
                Console.WriteLine("Exception: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
        }
        protected void btnSign_Click(object sender, EventArgs e)//註冊驗證按鈕
        {
            string homeName = signup_name.Text.Trim();
            string homeEmail = signup_account.Text.Trim();
            string homePassword = signup_password.Text.Trim();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM HomeLogin WHERE homeEmail = @Email";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", homeEmail);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())//已註冊
            {
                Label2.Visible = true;
                Label2.Text = "帳號已註冊";
                reader.Close();
                connection.Close();
            }
            else//先判對有沒有在系統登入的帳密組中
            {
                connection.Close();
                connection.Open();
                query = "SELECT * FROM CarerLogin WHERE cAccount = @cAccount AND cPassword = @cPassword";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@cAccount", homeEmail);
                command.Parameters.AddWithValue("@cPassword", homePassword);
                reader = command.ExecuteReader();

                if (reader.Read())//已註冊
                {
                    Label2.Visible = true;
                    Label2.Text = "帳號已註冊";
                    reader.Close();
                    connection.Close();
                }
                else//尚未註冊
                {
                    string code = Session["VerificationCode"].ToString();
                    if (verification_Text.Text == code)
                    {
                        connection.Close();
                        connection.Open();
                        query = "INSERT INTO HomeLogin (homeUserName, homeEmail, homePassword) VALUES (@homeUserName, @homeEmail, @homePassword)";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@homeUserName", signup_name.Text);
                        command.Parameters.AddWithValue("@homeEmail", signup_account.Text);
                        command.Parameters.AddWithValue("@homePassword", signup_password.Text);
                        command.ExecuteNonQuery();
                        connection.Close();

                        signup_account.Text = "";
                        signup_name.Text = "";
                        signup_password.Text = "";
                        string script = "alert('註冊成功'); setTimeout(function(){ window.location='Login.aspx'; }, 500);"; // 延遲0.5秒
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                        Label2.Visible = false;
                    }
                    else
                    {
                        Label2.Visible = true;
                        Label2.Text = "驗證碼錯誤";
                    }
                }
            }
        }

        protected void sign_up_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
            Panel4.Visible = false;
        }

        protected void forget_Click(object sender, EventArgs e)
        {
            Panel4.Visible = true;
            Panel1.Visible = false;
        }
        

        protected void btnForgetPass_Click(object sender, EventArgs e)//忘記密碼
        {
            if (forgetPass_Text.Text == "")
            {
                Label4.Visible = true;
            }
            else
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";//string digits = "0123456789";
                string randomPassword = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());//digits

                Session["randomPassword"] = randomPassword;

                string email = forgetPass_Text.Text;
                SendRandomPassword(email, randomPassword);
            }
        }
        private void SendRandomPassword(string email, string code)
        {
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpUser);
            mail.To.Add(email);
            mail.Subject = "全方位守護者-忘記密碼";
            mail.Body = $"您好!\n\n您的臨時密碼是：{code}\n請使用此密碼進行登入。\n提醒您，登入後請記得修改密碼。";
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
            smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
            smtp.EnableSsl = true;

            try
            { 
                if (Session["gap"] == "0") {
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query = "SELECT cEmail FROM CarerLogin WHERE cEmail = @cEmail";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cEmail", forgetPass_Text.Text.Trim()); // 确保去除多余空格
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Label3.Visible = false;
                        //while (reader.Read())
                        //{
                            smtp.Send(mail);// 發送電子郵件
                            string script = "alert('密碼已發送到您的電子郵件，請至信箱查看'); setTimeout(function(){ window.location='Login.aspx'; }, 500);"; // 延遲0.5秒
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                           
                            reader.Close();
                            string query1 = "UPDATE CarerLogin SET cPassword=@cPassword WHERE cEmail=@cEmail";

                            MySqlCommand command1 = new MySqlCommand(query1, connection);
                            command1.Parameters.AddWithValue("@cEmail", forgetPass_Text.Text);
                            command1.Parameters.AddWithValue("@cPassword", code);
                            command1.ExecuteNonQuery();
                            connection.Close();
                        //}
                    }
                    else
                    {
                        Label3.Text = "發送密碼失敗，請確認信箱是否正確。";
                        Label3.Visible = true;
                    }
                }
                else if (Session["gap"] == "1")
                {
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query = "SELECT homeEmail FROM HomeLogin WHERE homeEmail = @homeEmail";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@homeEmail", forgetPass_Text.Text.Trim()); // 确保去除多余空格
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Label3.Visible = false;
                        //while (reader.Read())
                        //{
                            smtp.Send(mail);// 發送電子郵件
                            string script = "alert('密碼已發送到您的電子郵件，請至信箱查看'); setTimeout(function(){ window.location='Login.aspx'; }, 500);"; // 延遲0.5秒
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);

                            reader.Close();
                            string query1 = "UPDATE HomeLogin SET homePassword=@homePassword WHERE homeEmail=@homeEmail";

                            MySqlCommand command1 = new MySqlCommand(query1, connection);
                            command1.Parameters.AddWithValue("@homeEmail", forgetPass_Text.Text);
                            command1.Parameters.AddWithValue("@homePassword", code);
                            command1.ExecuteNonQuery();
                            connection.Close();
                        //}
                    }
                    else
                    {
                        Label3.Text = "發送密碼失敗，請確認信箱是否正確。";
                        Label3.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Label3.Text = "發送密碼失敗，請稍後再試。";
                Label3.Visible = true;
                Console.WriteLine("Exception: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }     
        }
    }
}
/*protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
        {
            // 自定義電子郵件內容
            e.Message.Body = "Dear User,\n\nPlease use the following link to reset your password:\n" + e.Message.Body;
        }
        protected void PasswordRecovery1_VerifyingUser(object sender, LoginCancelEventArgs e)
        {
            PasswordRecovery recovery = (PasswordRecovery)sender;
            string email = recovery.UserName;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM HomeLogin WHERE homeEmail = @homeEmail";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homeEmail", email);
                MySqlDataReader reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    // 如果電子郵件地址不存在，取消恢復密碼過程
                    e.Cancel = true;
                    Label3.Text = "電子郵件地址不存在。";
                    Label3.Visible = true;
                }
                else
                {
                    SendPasswordRecoveryEmail(email);
                }
                reader.Close();
            }
        }
        private void SendPasswordRecoveryEmail(string email)//https://www.gmass.co/blog/gmail-smtp/
        {
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];

            // 生成验证令牌
            string token = HttpUtility.UrlEncode(email); //string token = email;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpUser);
            mail.To.Add(email);
            mail.Subject = "全方位守護者 重新設定密碼";
            mail.Body = "您好!\n\n請點擊下方連結重新設定密碼:\n" +
                        "https://localhost:44313/Web/ForgetPass.aspx?token=" + token;//33061
            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
            smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                Session["Email"] = "Email";
                Label3.Text = "An email with instructions to reset your password has been sent to you.";
                Label3.Visible = true;
            }
            catch (Exception ex)
            {
                Label3.Text = "Failed to send email. Please try again later.";
                Label3.Visible = true;
                Console.WriteLine("Exception: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
        }*/