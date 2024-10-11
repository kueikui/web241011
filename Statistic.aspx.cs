using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
namespace web_1
{
    public partial class Statistic : System.Web.UI.Page
    {
        static string connectionString = "server=203.64.84.154;database=care;uid=root;password=Topic@2024;port = 33061";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cAccount"] == null && Session["homeAccount"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (Session["LoginType"] == "System")
            { 
                ControlBarPanel.Visible = true;  // 當系統用戶登錄時顯示 ControlBar
                Panel4.Visible = true;
                //Panel2.Visible = true;  // 顯示主要圖表
                //Panel3.Visible = false;  // 隱藏 Chart5-7
                //Panel5.Visible = false;  // 隱藏其他圖表
                //Panel6.Visible = false;  // 隱藏其他圖表

                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand("SELECT COUNT(Eid) AS TotalCount, SUM(CASE WHEN eGender = 'male' THEN 1 ELSE 0 END) AS MaleCount, SUM(CASE WHEN eGender = 'female' THEN 1 ELSE 0 END) AS FemaleCount FROM Elder", connection);
                MySqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Label1.Text = "總人數 " + reader["TotalCount"].ToString();
                        Label2.Text = "  男   " + reader["MaleCount"].ToString();
                        Label3.Text = "女 " + reader["FemaleCount"].ToString();
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
            else if (Session["LoginType"] == "Home")
            {
                ControlBarPanel.Visible = false;  // 當家用用戶登錄時隱藏 ControlBar
                Panel4.Visible = false;
                //Panel2.Visible = false;  // 隱藏主要圖表
                //Panel3.Visible = false;  // 隱藏其他圖表
                //Panel5.Visible = false;  // 隱藏其他圖表
                //Panel6.Visible = true;   // 顯示 Chart11-13
            }
        }
        
        protected void Chart1_Load(object sender, EventArgs e)//跌倒次數
        {
            if (Session["LoginType"] == "System")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand("SELECT DATE_FORMAT(fTime, '%Y-%m') AS Month, COUNT(*) AS FallCount FROM Fall GROUP BY DATE_FORMAT(fTime, '%Y-%m')", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                Chart1.DataSource = dataset;
                Chart1.Series[0].XValueMember = "Month";
                Chart1.Series[0].YValueMembers = "FallCount";
                Chart1.DataBind();
            }
            else if (Session["LoginType"] == "Home")
            {

                string homeUserName = Session["userName"].ToString();
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand("SELECT DATE_FORMAT(hfTime, '%Y-%m') AS Month, COUNT(*) AS FallCount FROM HomeElderFall WHERE homeUserName = @homeUserName GROUP BY DATE_FORMAT(hfTime, '%Y-%m')", connection);


                command.Parameters.AddWithValue("@homeUserName", homeUserName);
                // 使用 DataAdapter 和 DataSet 填充圖表數據
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                Chart1.DataSource = dataset;
                Chart1.Series[0].XValueMember = "Month";
                Chart1.Series[0].YValueMembers = "FallCount";
                Chart1.DataBind();
            }
        }
        //Response.Write($"<script>alert('Error: {ex.Message}');</script>");

        protected void Chart2_Load(object sender, EventArgs e)//跌倒地點
        {
            if (Session["LoginType"] == "System")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT Place.pName, COUNT(Fall.fId) AS FallCount FROM Fall JOIN Place ON Fall.pId = Place.pId GROUP BY Place.pName ", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                Chart2.DataSource = dataset;
                Chart2.Series[0].XValueMember = "pName";
                Chart2.Series[0].YValueMembers = "FallCount";
                // Insert code for additional chart formatting here.
                Chart2.DataBind();
                connection.Close();
            }
            else if (Session["LoginType"] == "Home")
            {
                 string homeUserName = Session["userName"].ToString();
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT HomePlace.hPlace, COUNT(HomeElderFall.hfId) AS FallCount FROM HomeElderFall JOIN HomePlace ON HomePlace.hPlace = HomeElderFall.hPlace WHERE homeUserName = @homeUserName GROUP BY HomePlace.hPlace ", connection);
                command.Parameters.AddWithValue("@homeUserName", homeUserName);
                // 使用 DataAdapter 和 DataSet 填充圖表數據
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                Chart2.DataSource = dataset;
                Chart2.Series[0].XValueMember = "hPlace";
                Chart2.Series[0].YValueMembers = "FallCount";
                // Insert code for additional chart formatting here.
                Chart2.DataBind();
                connection.Close();
            }
        }

        protected void Chart3_Load(object sender, EventArgs e)//男女
        {
            if (Session["LoginType"] == "System")
            {
                Chart3.Visible = true;
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT Elder.eGender, COUNT(Fall.fId) AS FallCount FROM Fall JOIN Elder ON Fall.eId = Elder.eId GROUP BY Elder.eGender ", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                Chart3.DataSource = dataset;
                Chart3.Series[0].XValueMember = "eGender";
                Chart3.Series[0].YValueMembers = "FallCount";
                // Insert code for additional chart formatting here.
                Chart3.DataBind();
                connection.Close();
            }
            else if (Session["LoginType"] == "Home")
            {
                Chart3.Visible = false;
            }
        }

        protected void Chart4_Load(object sender, EventArgs e)//原因
        {
            if (Session["LoginType"] == "System")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT fWhy , COUNT(*) AS FallCount FROM Fall GROUP BY fWhy", connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                Chart4.DataSource = dataset;
                Chart4.Series[0].XValueMember = "fWhy";
                Chart4.Series[0].YValueMembers = "FallCount";
                // Insert code for additional chart formatting here.
                Chart4.DataBind();
                connection.Close();
            }
            else if (Session["LoginType"] == "Home")
            {
                string homeUserName = Session["userName"].ToString();
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT hfWhy , COUNT(*) AS FallCount FROM HomeElderFall WHERE homeUserName = @homeUserName GROUP BY hfWhy", connection);
                command.Parameters.AddWithValue("@homeUserName", homeUserName);
                // 使用 DataAdapter 和 DataSet 填充圖表數據
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                Chart4.DataSource = dataset;
                Chart4.Series[0].XValueMember = "hfWhy";
                Chart4.Series[0].YValueMembers = "FallCount";
                // Insert code for additional chart formatting here.
                Chart4.DataBind();
                connection.Close();
            }
        }
     
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                return;
            }
            if (DropDownList1.Text.ToString() == "長者ID")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                // 檢查該 Elder 是否存在
                MySqlCommand checkElderCommand = new MySqlCommand("SELECT COUNT(*) FROM Elder WHERE eId = @eId", connection);
                checkElderCommand.Parameters.AddWithValue("@eId", TextBox1.Text);
                int elderCount = Convert.ToInt32(checkElderCommand.ExecuteScalar());

                if (elderCount == 0)
                {
                    // 長者不存在，顯示查詢失敗
                    Label4.Visible = true;
                    Label4.Text = "查詢失敗";
                    connection.Close();
                    return;
                }

                // 如果 Elder 存在，檢查是否有跌倒記錄
                MySqlCommand fallCommand = new MySqlCommand("SELECT COUNT(*) FROM Fall WHERE eId = @eId", connection);
                fallCommand.Parameters.AddWithValue("@eId", TextBox1.Text);
                int fallCount = Convert.ToInt32(fallCommand.ExecuteScalar());

                if (fallCount == 0)
                {
                    // 長者存在，但沒有跌倒紀錄，顯示沒有跌倒資料
                    Label4.Visible = true;
                    Label4.Text = "沒有跌倒資料";
                    connection.Close();
                    return;
                }

                // 如果有跌倒紀錄，顯示跌倒數據
                if (Session["LoginType"] == "System")
                {
                    // 獲取跌倒次數數據
                    MySqlCommand command2 = new MySqlCommand("SELECT Elder.*, DATE_FORMAT(fTime, '%Y-%m') AS Month, COUNT(*) AS FallCount FROM Fall INNER JOIN Elder ON Fall.eId = Elder.eId WHERE Fall.eId = @eId GROUP BY DATE_FORMAT(fTime, '%Y-%m')", connection);
                    command2.Parameters.AddWithValue("@eId", TextBox1.Text);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command2);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Chart5.DataSource = dt;
                    Chart5.Series[0].XValueMember = "Month";
                    Chart5.Series[0].YValueMembers = "FallCount";
                    Chart5.DataBind();

                    // 獲取跌倒地點數據
                    MySqlCommand command3 = new MySqlCommand("SELECT Place.pName, COUNT(Fall.fId) AS FallCount FROM Fall JOIN Place ON Fall.pId = Place.pId WHERE Fall.eId = @eId GROUP BY Place.pName", connection);
                    command3.Parameters.AddWithValue("@eId", TextBox1.Text);
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(command3);
                    DataTable dt2 = new DataTable();
                    adapter2.Fill(dt2);
                    Chart6.DataSource = dt2;
                    Chart6.Series[0].XValueMember = "pName";
                    Chart6.Series[0].YValueMembers = "FallCount";
                    Chart6.DataBind();

                    // 獲取跌倒原因數據
                    MySqlCommand command4 = new MySqlCommand("SELECT fWhy, COUNT(*) AS FallCount FROM Fall WHERE eId = @eId GROUP BY fWhy", connection);
                    command4.Parameters.AddWithValue("@eId", TextBox1.Text);
                    MySqlDataAdapter adapter3 = new MySqlDataAdapter(command4);
                    DataTable dt3 = new DataTable();
                    adapter3.Fill(dt3);
                    Chart7.DataSource = dt3;
                    Chart7.Series[0].XValueMember = "fWhy";
                    Chart7.Series[0].YValueMembers = "FallCount";
                    Chart7.DataBind();

                    // 顯示相應的面板
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    Panel5.Visible = false;
                    Label4.Visible = false;
                }

                connection.Close();
            }
        
            else if (DropDownList1.Text.ToString() == "地點")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT Place.pId, COUNT(Fall.fId) AS FallCount FROM Fall JOIN Place ON Fall.pId = Place.pId GROUP BY Place.pId ", connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (TextBox1.Text == reader.GetString("pId"))//如果沒有資料要顯示label
                        {
                            if (Session["LoginType"] == "System")
                            {
                                reader.Close();
                                MySqlCommand command2 = new MySqlCommand("SELECT Place.*, DATE_FORMAT(fTime, '%Y-%m') AS Month, COUNT(*) AS FallCount FROM Fall INNER JOIN Place ON Fall.pId = Place.pId WHERE Fall.pId = @pId GROUP BY DATE_FORMAT(fTime, '%Y-%m')", connection);
                                command2.Parameters.AddWithValue("@pId", TextBox1.Text);
                                MySqlDataAdapter adapter = new MySqlDataAdapter(command2);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                Chart8.DataSource = dt;
                                Chart8.Series[0].XValueMember = "Month";
                                Chart8.Series[0].YValueMembers = "FallCount";
                                Chart8.DataBind();


                                MySqlCommand command4 = new MySqlCommand("SELECT fWhy , COUNT(*) AS FallCount FROM Fall JOIN Place ON Fall.pId = Place.pId WHERE Place.pId = @pId GROUP BY fWhy", connection);
                                command4.Parameters.AddWithValue("@pId", TextBox1.Text);
                                MySqlDataAdapter adapter3 = new MySqlDataAdapter(command4);
                                DataTable dt3 = new DataTable();
                                adapter3.Fill(dt3);

                                Chart9.DataSource = dt3;
                                Chart9.Series[0].XValueMember = "fWhy";
                                Chart9.Series[0].YValueMembers = "FallCount";
                                Chart9.DataBind();

                                MySqlCommand command1 = new MySqlCommand("SELECT Place.*, Elder.eGender, COUNT(Fall.fId) AS FallCount FROM Fall JOIN Place ON Fall.pId = Place.pId JOIN Elder ON Fall.eId = Elder.eId WHERE Place.pId = @pId GROUP BY Elder.eGender", connection);
                                command1.Parameters.AddWithValue("@pId", TextBox1.Text);
                                MySqlDataAdapter da1 = new MySqlDataAdapter(command1);
                                DataSet dataset1 = new DataSet();
                                da1.Fill(dataset1);

                                Chart10.DataSource = dataset1;
                                Chart10.Series[0].XValueMember = "eGender";
                                Chart10.Series[0].YValueMembers = "FallCount";
                                // Insert code for additional chart formatting here.
                                Chart10.DataBind();
                                connection.Close();
                                connection.Close();

                                Panel2.Visible = false;
                                Panel3.Visible = false;
                                Panel5.Visible = true;
                                Label4.Visible = false;
                                break;
                            }
                            else if (Session["LoginType"] == "Home")
                            {

                            }
                        }
                        else
                        {
                            Label4.Visible = true;
                            Label4.Text = "查詢失敗";
                        }
                    }
                }
                connection.Close();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DropDownList1.SelectedValue;

            DropDownList2.Visible = false;
            TextBox1.Visible = false;
            TextBox1.Text = "";
            if (selectedValue == "長者ID"|| selectedValue == "地點") // 長者ID
            {
                TextBox1.Visible = true;
                
                //DropDownList2.Visible = false;
            }
            else
            {
                Chart1_Load(null, EventArgs.Empty);
                Chart2_Load(null, EventArgs.Empty);
                Chart3_Load(null, EventArgs.Empty);
                Chart4_Load(null, EventArgs.Empty);

                // 顯示相關的面板和隱藏不必要的元素
                Panel2.Visible = true;
                Panel3.Visible = false;
                Panel5.Visible = false;
                Label4.Visible = false;

                // 重設 DropDownList 到初始狀態
                DropDownList1.SelectedIndex = 0; // 假設 "請選擇" 是第 0 項
            }
        }

        protected void Chart11_Load(object sender, EventArgs e)
        {

        }

        protected void Chart12_Load(object sender, EventArgs e)
        {

        }

        protected void Chart13_Load(object sender, EventArgs e)
        {

        }
    }
}