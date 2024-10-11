<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="WebForm1.aspx.cs" Inherits="web_1.WebForm1"Async="true"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body, html {
            overflow: hidden;
            align-content:center;
            
        }
                
        body::before {
            content: "";
            position: absolute;
            width: 100%;
            height: 100%;
            background-image: url('../Images/b-4.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            opacity: 1; /*透明度，0.5 是 50% 濃度*/
            z-index: -1; /*確保背景在內容後面*/
        }
        .wrapper {
             display: flex;
             width:2500px;
             height: 2500px;
             grid-template-rows: 2fr 5fr; 
             grid-template-columns: 1fr 5fr;
             position:relative;
        }
        .Button1{/*home玄關*/
            position: absolute; 
            top:220px; 
            left: 520px; 
        }
        .Button2{/*home房間*/
            position: absolute; 
            top:170px; 
            left: 40px; 
        }
        .Button7{/*home客廳*/
            position: absolute; 
            top:280px; 
            left: 120px; 
        }
        .Button8{/*home客廳*/
            position: absolute; 
            top:230px; 
            left: 200px; 
        }
        .Button9{/*home玄關*/
            position: absolute; 
            top:270px; 
            left: 420px; 
        }
        .Button10{/*home飯廳*/
            position: absolute; 
            top:25px; 
            left: 200px; 
        }

        .Button14 { /*system大廳*/
            position: absolute;
            top: 220px;
            left: 520px;
        }
        .Button15 { /*system交誼廳*/
            position: absolute;
            top: 280px;
            left: 120px;
        }
        .Button16 { /*system樓梯*/
            position: absolute;
            top: 70px;
            left: 300px;
        }
        .Button17 { /*systemA105*/
            position: absolute;
            top: 210px;
            left: 245px;
        }
        .Button18 { /*system交誼廳*/
            position: absolute;
            top: 250px;
            left: 380px;
        }
        .Button19 { /*systemA101*/
            position: absolute;
            top: 50px;
            left: 245px;
        }


        .Button11{/*警報1*/
            position: absolute; 
            top:400px; 
            left: 20px; 
        }
        .Button13{/*警報2*/
            position: absolute; 
            top:400px; 
            left: 80px; 
        }
        .image-container {
            position: absolute;
            width:600px;
            height:500px;
            overflow: hidden;
        }
        .image-container img {
            width: 600px;
            height: 350px;
        }
        .video-container {
            position: relative;
            width: 50%;
            overflow: hidden;
            padding: 10px;
            margin-left: 600px;
        }      
        .alert{
            position: absolute;
            top:380px;
            left:610px;
            width:50px;
            height:30px;
            background-color:aquamarine;
        }
       .btn-twinke{
            color: #fff;
            border:none;
            animation: twinkle 1s alternate infinite;
            /*動畫的名稱 1秒 兩種狀態交替 無限重複*/
       }
       @keyframes twinkle{
           from{background: #16e2eb;}
           to{background: #3b6e99;}
       }
        .auto-style1 {
/*home客廳*/
            position: absolute;
            top: 280px;
            left: 120px;
            width: 65px;
        }
    </style>
    <script type="text/javascript">
        function startTwinkling() {
            var button = document.getElementById('<%= Button1.ClientID %>');
            button.classList.add('btn-twinke');
        }
    </script>

    <main aria-labelledby="title"> 
        <div class="wrapper">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" />
            </div>
            <div class="video-container"> 
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <br />
                <iframe src="https://tcumicarewatchsafeguard.me/0"   style="width: 1400px; height: 1000px; transform: scale(0.4); transform-origin: 0 0;"></iframe>
            </div>
            <div class="button">
                <asp:Button ID="Button1" runat="server" Text="玄關" Width="50px" OnClick="Button1_Click" CssClass="Button1" />
                <asp:Button ID="Button7" runat="server" Text="客廳" Width="50px" OnClick="Button7_Click" CssClass="Button7" />        
                <asp:Button ID="Button2" runat="server" Text="房間" Width="50px" OnClick="Button2_Click" CssClass="Button2" Visible="False" />
                <asp:Button ID="Button8" runat="server" Text="客廳" Width="50px" OnClick="Button8_Click" CssClass="Button8" Visible="False"/>
                <asp:Button ID="Button9" runat="server" Text="玄關" Width="50px" OnClick="Button9_Click" CssClass="Button9" Visible="False"/>
                <asp:Button ID="Button10" runat="server" Text="飯廳" Width="50px" OnClick="Button10_Click" CssClass="Button10" Visible="False"/>

                <asp:Button ID="Button14" runat="server" Text="大廳" Width="50px" OnClick="Button14_Click" CssClass="Button14" />
                <asp:Button ID="Button15" runat="server" Text="交誼廳" OnClick="Button15_Click" CssClass="auto-style1" />
                <asp:Button ID="Button16" runat="server" Text="樓梯" Width="50px" OnClick="Button16_Click" CssClass="Button16" Visible="False"/>
                <asp:Button ID="Button17" runat="server" Text="A105" Width="50px" OnClick="Button17_Click" CssClass="Button17" Visible="False"/>
                <asp:Button ID="Button18" runat="server" Text="交誼廳" Width="65px" OnClick="Button18_Click" CssClass="Button18" Visible="False"/>
                <asp:Button ID="Button19" runat="server" Text="A101" Width="50px" OnClick="Button19_Click" CssClass="Button19" Visible="False"/>
            <br />
                <asp:Button ID="Button11" runat="server" Text="警報" OnClick="Button11_Click" CssClass="Button11" OnClientClick="startTwinkling();" />
                <asp:Button ID="Button13" runat="server" OnClick="Button13_Click" Text="警報2" CssClass="Button13"/>
            </div>
            <asp:Panel ID="Panel1" runat="server" class="alert" Visible="false" BackColor="#FF5050" ForeColor="White" Height="160px" Width="300px">
                在<asp:Label ID="PlaceText" runat="server" BackColor="#FF5050" BorderColor="#FF5050" BorderStyle="None" BorderWidth="50px" ForeColor="White" Width="59px"></asp:Label>&nbsp;&nbsp;&nbsp; 發生跌倒事件<br /> 請立即派人前往救助<br /> 請至通報系統填寫資料<br /> &nbsp;<asp:Button ID="Button12" runat="server" OnClick="Button12_Click" Text="事件完成" OnClientClick="return confirm('確認通報單填寫完畢，要關閉視窗嗎？');" />
                
            </asp:Panel>
        </div>
    </main>
</asp:Content>