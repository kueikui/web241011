using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace web_1
{
    public partial class NotificationInfo : System.Web.UI.Page
    {
        static string connectionString = "server=203.64.84.154;database=care;uid=root;password=Topic@2024;port = 33061";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // 確保只在第一次加載頁面時運行
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT eId FROM Elder";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // 從資料讀取器中取得 pid，並將其加入下拉選單
                        ListItem item = new ListItem(reader["eId"].ToString(), reader["eId"].ToString());
                        Noti_eId_list.Items.Add(item);
                    }
                }
                connection.Close();

                if (!string.IsNullOrEmpty(Noti_eId_list.SelectedValue))
                {

                    connection.Open();

                    // 查詢與選擇的 pId 對應的長者名稱
                    string query1 = "SELECT eName FROM Elder WHERE eId = @eId";
                    MySqlCommand command1 = new MySqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@eId", Noti_eId_list.SelectedValue);  // 使用選擇的 pId

                    MySqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.HasRows && reader1.Read())
                    {
                        // 將查詢到的 eName 顯示在 TextBox 中
                        Noti_eName.Text = reader1["eName"].ToString();
                    }
                    else
                    {
                        Noti_eName.Text = "無對應長者";
                    }

                    reader1.Close();
                }

                if (Session["ShowPanel"] != null && (bool)Session["ShowPanel"] == true)
                {
                    Panel1.Visible = true;
                    // 清除會話狀態，以便下次進入該頁面時不再顯示Panel1
                    //Session["ShowPanel"] = false;
                    string pName = Session["AlertLocation"] != null ? Session["AlertLocation"].ToString() : "未知地點";
                    DateTime fTime = Session["AlertTime"] != null ? (DateTime)Session["AlertTime"] : DateTime.Now;

                    Noti_pName.Text = pName;
                    Noti_fTime.Text = fTime.ToString();
                }
                else
                {
                    Panel1.Visible = false;
                }
            }
            if (Session["cAccount"] == null && Session["homeAccount"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            BindGridView();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Session["ShowPanel"] = false;
            if (Session["LoginType"] == "System")
            {
                string hId = null;
                if (RadioButtonList1.SelectedValue == "是")
                {
                    hId = Noti_hId.Text;
                }
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string getPIdQuery = "SELECT pId FROM Place WHERE pName = @pName";
                MySqlCommand getPIdCommand = new MySqlCommand(getPIdQuery, connection);
                getPIdCommand.Parameters.AddWithValue("@pName", Noti_pName.Text);
                object pIdResult = getPIdCommand.ExecuteScalar();
                string pId = pIdResult != null ? pIdResult.ToString() : null;

                if (pId == null)
                {
                    // 錯誤處理，當地點名稱無法找到對應的 pId 時
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('無法找到對應的地點ID');", true);
                    connection.Close();
                    return;
                }



                // 檢查是否有 Session 資料

                string query = "INSERT INTO Fall (eId,pId,fTime,fWhy,hId) VALUES (@eId,@pId,@fTime,@fWhy,@hId)";
                connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                command.Parameters.AddWithValue("@eId", Noti_eId_list.Text);
                command.Parameters.AddWithValue("@pId", pId);
                command.Parameters.AddWithValue("@fTime", DateTime.Now);
                command.Parameters.AddWithValue("@fWhy", Noti_fwhy.Text);
                //command.Parameters.AddWithValue("@hId", Noti_hId.Text);
                if (string.IsNullOrEmpty(hId))
                {
                    command.Parameters.AddWithValue("@hId", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@hId", hId);
                }
                command.ExecuteNonQuery();
                connection.Close();


                BindGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('新增成功');", true);
            }
            else if (Session["LoginType"] == "Home")
            {

            }
        }

        protected void BindGridView()
        {
            if (Session["LoginType"] == "System")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT Fall.fId as 跌倒編號,Fall.eId as 長者編號,Elder.eName as 長者姓名 ,Place.pName as 跌倒地點,Fall.fTime as 跌倒時間, Fall.fWhy as 跌倒原因 ,Fall.hId as 送醫 FROM Fall " +
                           "INNER JOIN Elder ON Fall.eId = Elder.eId INNER JOIN Place ON Fall.pId = Place.pId ORDER BY Fall.fId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;

                GridView1.DataBind();
                GridView1.Visible = true;
                connection.Close();
            }
            else if (Session["LoginType"] == "Home")
            {
                string homeUserName = Session["userName"].ToString();
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT HomeElderFall.hfId as 跌倒編號,HomeElder.heName as 長者姓名 ,HomeElderFall.hPlace as 跌倒地點,HomeElderFall.hfTime as 跌倒時間, HomeElderFall.hfWhy as 跌倒原因 ,HomeElderFall.hId as 送醫 FROM HomeElderFall " +
                           "INNER JOIN HomeElder ON HomeElderFall.heName = HomeElder.heName WHERE  HomeElder.homeUserName = @homeUserName";
                // 創建 MySqlCommand 並設置參數
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@homeUserName", homeUserName);

                // 使用 DataAdapter 和 DataSet 填充資料
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                // 將資料綁定到 GridView
                GridView1.DataSource = dataset.Tables[0];

                GridView1.DataBind();
                GridView1.Visible = true;
                connection.Close();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // 設置新的頁索引
            GridView1.PageIndex = e.NewPageIndex;

            // 重新綁定數據
            BindGridView();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "是")
            {
                Panel3.Visible = true;
            }
            else
            {
                Panel3.Visible = false;
            }
        }

        protected void Noti_eId_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Noti_eId_list.SelectedValue))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // 查詢與選擇的 pId 對應的長者名稱
                    string query = "SELECT eName FROM Elder WHERE eId = @eId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@eId", Noti_eId_list.SelectedValue);  // 使用選擇的 pId

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        // 將查詢到的 eName 顯示在 TextBox 中
                        Noti_eName.Text = reader["eName"].ToString();
                    }
                    else
                    {
                        Noti_eName.Text = "無對應長者";
                    }

                    reader.Close();
                }
            }
        }
    }
}