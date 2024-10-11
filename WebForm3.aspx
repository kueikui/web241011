<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="WebForm3.aspx.cs" Inherits="web_1.Web.WebForm3" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* 通用样式 */
body {
    font-family: 'Microsoft YaHei', Arial, sans-serif;
    font-size: 14px;
    color: #333;
}

h1, h2 {
    font-size: 24px;
    color: #333;
    margin-bottom: 20px;
}

/* 表格样式 */
.gridview {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

.gridview th, .gridview td {
    padding: 10px;
    border-bottom: 1px solid #ccc;
}

.gridview th {
    background-color: #f4f4f4;
    font-weight: bold;
    text-align: left;
}

.gridview tr:nth-child(even) {
    background-color: #f9f9f9;
}

.gridview tr:hover {
    background-color: #f1f1f1;
}

/* 按钮样式 */
.button {
    padding: 5px 10px;
    margin: 5px;
    border: none;
    color: white;
    cursor: pointer;
    border-radius: 3px;
}

.button-details { background-color: #337ab7; }
.button-edit { background-color: #f0ad4e; }
.button-delete { background-color: #d9534f; }
.button-add { background-color: #5cb85c; }
.button-search { background-color: #5bc0de; }
.button-showall { background-color: #5e5e5e; }
.button-back { background-color: #5e5e5e; }
.button-cancel { background-color: #d9534f; }
.button-save { background-color: #5cb85c; }

/* 文本框样式 */
.textbox {
    width: 200px;
    padding: 5px;
    margin-right: 10px;
}

/* 面板样式 */
.panel {
    background-color: #fff;
    padding: 15px;
    border-radius: 5px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    margin-bottom: 20px;
}

.data-table td {
    padding: 5px;
}
 .btn-twinkle{
     color: #fff;
     border:none;
     animation: twinkle 1s alternate infinite;
 }
 @keyframes twinkle{
     from{background: #16e2eb;}
     to{background: #3b6e99;}
 }

    </style>
    <main aria-labelledby="title">

  
    <button class="btn-twinkle">我是个会闪烁的按钮</button>
        <h1>長者資料</h1>
        <asp:Panel ID="Panel1" runat="server" CssClass="panel">
            新增一筆資料
           &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Search_Text" runat="server" CssClass="textbox"></asp:TextBox>
           
            <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AllowPaging="True" PageSize="5">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" CssClass="panel" BackColor="#FFFFCC" Visible="False">
            <h2>長者資料</h2>
            <table class="data-table">
                <tr>
                    <td>長者編號:<asp:Label ID="eId_Lebel" runat="server"></asp:Label>
                        <asp:TextBox ID="eId_Text" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>管理員編號:<asp:TextBox ID="cId_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>房號:<asp:TextBox ID="pId_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox>
                        <asp:DropDownList ID="pId_list" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>長者姓名:<asp:TextBox ID="eName_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                    <td>身分證字號:<asp:TextBox ID="eIdCard_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                    <td>性別:<asp:DropDownList ID="eGender_list" runat="server" Enabled="False">
                            <asp:ListItem>male</asp:ListItem>
                            <asp:ListItem>female</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>生日:<asp:TextBox ID="eBirth_Text" runat="server" Width="160px" TextMode="Date" ReadOnly="True"></asp:TextBox></td>
                    <td>電話:<asp:TextBox ID="ePhone_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                    <td>通訊地址:<asp:TextBox ID="eAddress_Text" runat="server" Width="200px" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>身高:<asp:TextBox ID="eHeight_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                    <td>體重:<asp:TextBox ID="eWeight_Text" runat="server" Width="160px" ReadOnly="True"></asp:TextBox></td>
                    <td>建檔日期:<asp:TextBox ID="eCreateFile_Text" runat="server" TextMode="Date" Width="160px" ReadOnly="True"></asp:TextBox></td>
                </tr>
            </table>
             
            <asp:Button ID="Add_check" runat="server" OnClick="Add_check_Click" Text="新增" CssClass="button button-add" Visible="False" />
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="Required_eId" runat="server" ControlToValidate="eId_Text" ErrorMessage="長者編號不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="Required_eName" runat="server" ControlToValidate="eName_Text" ErrorMessage="長者姓名不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="Required_eBirth" runat="server" ControlToValidate="eBirth_Text" ErrorMessage="長者生日不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="Required_eIdCard" runat="server" ControlToValidate="eIdCard_Text" ErrorMessage="長者身分證不得為空" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
        </asp:Panel>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel" BackColor="#CCFFCC" Visible="False">
            <h2>家屬資料</h2>
            <p>家屬姓名: <asp:TextBox ID="rName_Text" runat="server" ReadOnly="True"></asp:TextBox></p>
            <p>身分證字號: <asp:TextBox ID="rIdCard_Text" runat="server" ReadOnly="True"></asp:TextBox></p>
            <p>性別: <asp:DropDownList ID="rGender_list" runat="server" Enabled="False">
                <asp:ListItem>male</asp:ListItem>
                <asp:ListItem>female</asp:ListItem>
            </asp:DropDownList></p>
            <p>電話: <asp:TextBox ID="rPhone_Text" runat="server" ReadOnly="True"></asp:TextBox></p>
            <p>通訊地址: <asp:TextBox ID="rAddress_Text" runat="server" ReadOnly="True"></asp:TextBox></p>
            <p>工作: <asp:TextBox ID="rJob_Text" runat="server" ReadOnly="True"></asp:TextBox></p>
            <p>工作地點: <asp:TextBox ID="rWorkPlace_Text" runat="server" ReadOnly="True"></asp:TextBox></p>
            <asp:Button ID="back" runat="server" Text="返回" OnClick="back_Click" CssClass="button button-back" />
            <asp:Button ID="edit" runat="server" Text="編輯" OnClick="edit_Click" CssClass="button button-edit" />
            <asp:Button ID="save" runat="server" Text="儲存" OnClick="save_Click" CssClass="button button-save" Visible="False" />
            <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            <asp:RequiredFieldValidator ID="Required_rName" runat="server" ControlToValidate="rName_Text" Enabled="False" ErrorMessage="家屬姓名不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="Required_rPhone" runat="server" ControlToValidate="rPhone_Text" Enabled="False" ErrorMessage="家屬電話不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="Required_rPhone0" runat="server" ControlToValidate="rIdCard_Text" Enabled="False" ErrorMessage="家屬身分證不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="Required_rPhone1" runat="server" ControlToValidate="rAddress_Text" Enabled="False" ErrorMessage="家屬通訊地址不可為空" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        </asp:Panel>

        <asp:Panel ID="Panel4" runat="server" CssClass="panel" BackColor="#FFCCCC" Visible="False">
            <asp:GridView ID="GridView2" runat="server" CssClass="gridview">
                <Columns>
                    <asp:ButtonField ButtonType="Button" Text="編輯"  />
                </Columns>
            </asp:GridView>
            <asp:Button ID="FDetail_back" runat="server" Text="返回" OnClick="FDetail_back_Click" Class="button button-back" />
        </asp:Panel>

        <asp:Panel ID="Panel5" runat="server" CssClass="panel" BackColor="#CCCCFF" Visible="False">
            <h2>跌倒資料</h2>
            <p>跌倒編號: <asp:Label ID="fId_Label" runat="server" Text=""></asp:Label></p>
            <p>長者編號: <asp:Label ID="feId_Label" runat="server" Text=""></asp:Label> 長者姓名: <asp:TextBox ID="feName_Text" runat="server" Enabled="False"></asp:TextBox></p>
            <p>跌倒時間: <asp:TextBox ID="fTime_Text" runat="server" Enabled="False" TextMode="Date"></asp:TextBox></p>
            <p>跌倒地點: <asp:TextBox ID="fPlace_Text" runat="server" Enabled="False"></asp:TextBox></p>
            <p>送醫: <asp:TextBox ID="fHospital_Text" runat="server" Enabled="False"></asp:TextBox></p>
            <p>跌倒原因: <asp:TextBox ID="fWhy_Text" runat="server" TextMode="MultiLine" Enabled="False"></asp:TextBox></p>
            <asp:Button ID="Fback" runat="server" Text="返回" OnClick="Fback_Click" CssClass="button button-back" />
            <asp:Button ID="Fedit" runat="server" Text="編輯" OnClick="Fedit_Click" CssClass="button button-edit" />
            <asp:Button ID="Fsave" runat="server" Text="儲存" OnClick="Fsave_Click" CssClass="button button-save" Visible="False" />
        </asp:Panel>

        <div class="video-container"> 
            <asp:Label ID="Label3" runat="server" Text="123"></asp:Label>
            <asp:Button ID="ButtonViewFeed" runat="server" Text="顯示即時監控" OnClick="ButtonViewFeed_Click" CssClass="btn-view-feed" />

            <br />

            <br />
            <asp:PlaceHolder ID="videoPlaceHolder" runat="server">
                <video id="videoPlayer" runat="server" width="420" height="340" controls>
                    <source id="videoSource" runat="server" src="test.mp4" type="video/mp4">
                    Your browser does not support the video tag.
                </video>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="webViewPlaceHolder" runat="server" Visible="false">
                <iframe src="http://203.64.84.154:8088/video_feed" width="420" height="340"></iframe>
            </asp:PlaceHolder>
        </div>




    </main>

</asp:Content>
