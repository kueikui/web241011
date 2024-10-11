<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Statistic.aspx.cs" Inherits="web_1.Statistic" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-image: url(../Images/b-4.jpg);
            background-size: cover;
        }
        .panel-container {
           display: flex;
           justify-content: center;
           overflow:hidden;
           height:500px;
        }
        #ControlBar {
            width:250px;
            padding: 10px;
            background-color: #c7d9ff;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .chart-container {
            display: flex;
            flex-direction: row;
            gap: 20px;
            margin-left: 50px;
            width: 640px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            background-color:cadetblue; 
            padding: 10px;
            flex-wrap: wrap;  /* 允許圖表在多行中排列 */
        }
        .chart{
            /*flex:1;*/
            border-radius: 10px; 
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); 
            overflow: hidden; 
            width:300px;
            background-color: powderblue; /* Example light background */
            margin:5px;
            float: left;
        }
        .chart-panel {
            margin-top: 20px;
            background-color:#ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        #OtherInfo {
            background-color:#c0f0f6;/* cadetblue;*/
            padding: 20px;
            border-radius: 10px;
         
            display: flex; /* 将其设置为 flex 容器 */
            flex-direction: column; /* 将子元素纵向排列 */
            align-items: center; /* 水平居中 */
        }
        .OtherInfoBlock {
            background-color:#609a9b;
            padding: 10px;
            border-radius: 10px;
            height: 60px;
            font-size: 30px;
            text-align: center; /* 文本内容居中 */
            width: 200px; /* 设置宽度为100% */
            max-width: 200px; /* 设置最大宽度，使得标签宽度与最大宽度一致 */
            margin-bottom: 10px; /* 在标签之间添加一些间距 */
        }
    </style>
    <h1 style="margin-left:80px">數據統計</h1>
    <main aria-labelledby="title" class="panel-container">
        <asp:Panel ID="ControlBarPanel" runat="server">
        <div id="ControlBar">
            <asp:Panel ID="Panel1" runat="server" >
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>請選擇</asp:ListItem>
                    <asp:ListItem>長者ID</asp:ListItem>
                    <asp:ListItem>地點</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" Visible="False" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="確定" />
                <br />
                <br />
                <asp:Label ID="Label4" runat="server" ForeColor="#FF3300" Text="Label" Visible="False"></asp:Label>
            </asp:Panel>
            <div id="OtherInfo">
                <asp:Panel ID="Panel4" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="Label" width="200px"  CssClass="OtherInfoBlock"></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Label" width="200px"  CssClass="OtherInfoBlock" ></asp:Label>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Label" width="200px"  CssClass="OtherInfoBlock"></asp:Label>
                </asp:Panel>
            </div>
        </div>
</asp:Panel>
        <div class="chart-container ">            
            <asp:Panel ID="Panel2" runat="server">
                <div class="chart">
                    <asp:Chart ID="Chart1" runat="server" OnLoad="Chart1_Load" AlternateText="載入失敗" BackColor="powderblue" BorderlineWidth="0" Height="230px">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="人數"></asp:Legend></Legends><Titles><asp:Title Text="跌倒次數總紀錄" /></Titles><Series><asp:Series Name="Series1" Legend="Legend1"></asp:Series></Series><ChartAreas><asp:ChartArea Name="ChartArea1">   <AxisX Title="時間（月）" /><AxisY Title="跌倒次數" /></asp:ChartArea></ChartAreas><Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation></Annotations>
                    </asp:Chart>
               </div>
               <div class="chart">
                   <asp:Chart ID="Chart2" runat="server" OnLoad="Chart2_Load" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="人數"></asp:Legend></Legends><Titles><asp:Title Text="跌倒地點紀錄" /></Titles><Series><asp:Series Name="Series1" Legend="Legend1"></asp:Series></Series><ChartAreas><asp:ChartArea Name="ChartArea1">  <AxisX Title="地點" /><AxisY Title="跌倒次數" /></asp:ChartArea></ChartAreas><Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation></Annotations>
                    </asp:Chart>
                </div>
                <br />
                <div class="chart">
                    <asp:Chart ID="Chart4" runat="server" OnLoad="Chart4_Load" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="原因"></asp:Legend></Legends><Titles><asp:Title Text="跌倒原因紀錄" /></Titles><Series><asp:Series Name="Series1" Legend="Legend1" ChartType="Pie"></asp:Series></Series><ChartAreas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></ChartAreas><Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation></Annotations>
                    </asp:Chart>
                </div>
                <div class="chart">
                    <asp:Chart ID="Chart3" runat="server" OnLoad="Chart3_Load" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="性別"></asp:Legend></Legends><Titles><asp:Title Text="跌倒男女比" /></Titles><Series><asp:Series Name="Series1" Legend="Legend1" ChartType="Pie"></asp:Series></Series><ChartAreas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></ChartAreas><Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation></Annotations>
                    </asp:Chart>
                </div>
                
           </asp:Panel>


            <asp:Panel ID="Panel3" runat="server" Visible="False">
                <div class="chart">
                    <asp:Chart ID="Chart5" runat="server" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="人數"></asp:Legend></Legends><Titles><asp:Title Text="跌倒次數總紀錄" /></Titles><Series><asp:Series Name="Series1"></asp:Series></Series><ChartAreas><asp:ChartArea Name="ChartArea1">  <AxisX Title="時間（月）" /><AxisY Title="跌倒次數" /></asp:ChartArea></ChartAreas>
                    </asp:Chart>
                </div>
                <div class="chart">
                    <asp:Chart ID="Chart6" runat="server" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="人數"></asp:Legend></Legends>
                        <Titles><asp:Title Text="跌倒地點" /></Titles>
                        <Series><asp:Series Name="Series1"></asp:Series></Series>
                        <ChartAreas><asp:ChartArea Name="ChartArea1"><AxisX Title="地點" /><AxisY Title="跌倒次數" /></asp:ChartArea></ChartAreas>
                          
                    </asp:Chart>
                </div>
                <br />
                <div class="chart">
                    <asp:Chart ID="Chart7" runat="server" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="原因"></asp:Legend></Legends>
                        <Titles><asp:Title Text="跌倒原因紀錄" /></Titles>
                        <Series><asp:Series ChartType="Pie" Legend="Legend1" Name="Series1"></asp:Series></Series>
                        <ChartAreas><asp:ChartArea Name="ChartArea1"><AxisX Title="時間（月）" /><AxisY Title="跌倒次數" /></asp:ChartArea></ChartAreas>
                          
                        <Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation></Annotations>
                    </asp:Chart>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel5" runat="server" Visible="False">
                <div class="chart">
                    <asp:Chart ID="Chart8" runat="server" AlternateText="載入失敗" BorderlineWidth="0" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="人數"></asp:Legend></Legends>
                        <Titles><asp:Title Text="跌倒次數總紀錄" /></Titles>
                        <Series><asp:Series Name="Series1" Legend="Legend1"></asp:Series></Series>
                        <ChartAreas><asp:ChartArea Name="ChartArea1">  <AxisX Title="時間（月）" /><AxisY Title="跌倒次數" /></asp:ChartArea></ChartAreas>
                        <Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation></Annotations>
                    </asp:Chart>
                </div>
                <div class="chart">
                    <asp:Chart ID="Chart9" runat="server" Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="性別"></asp:Legend></Legends>
                        <Titles><asp:Title Text="跌倒原因紀錄" /></Titles>
                        <Series><asp:Series Name="Series1" Legend="Legend1" ChartType="Pie"></asp:Series></Series>
                        <ChartAreas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></ChartAreas>
                        <Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation>
                        </Annotations>
                    </asp:Chart>
                </div>            
                <br />
                <div class="chart">
                    <asp:Chart ID="Chart10" runat="server"  Height="230px" BackColor="powderblue">
                        <Legends><asp:Legend AutoFitMinFontSize="5" Name="Legend1" Title="原因"></asp:Legend></Legends>
                        <Titles><asp:Title Text="跌倒男女比" /></Titles>
                        <Series><asp:Series Name="Series1" Legend="Legend1" ChartType="Pie"></asp:Series></Series>
                        <ChartAreas><asp:ChartArea Name="ChartArea1"></asp:ChartArea></ChartAreas>
                        <Annotations><asp:LineAnnotation LineColor="Brown" Name="LineAnnotation2"></asp:LineAnnotation></Annotations>
                    </asp:Chart>
                </div>
            </asp:Panel>
        </div>
    </main>
</asp:Content>
