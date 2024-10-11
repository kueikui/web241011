<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="web_1.Web.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>監視器測試</title>
    <style>
    body {
        font-family: Arial, sans-serif;
    }

    .container {
        margin: 20px auto;
        padding: 20px;
        width: 80%;
        background-color: #f4f4f4;
        border-radius: 10px;
    }

    .header {
        text-align: center;
        padding: 10px;
        background-color: #f5e3c3;
        margin-bottom: 20px;
        font-size: 24px;
        font-weight: bold;
    }

    .monitor {
        text-align: center;
        margin-top: 20px;
    }

    iframe {
        width: 100%;
        height: 400px;
        border: 2px solid #ddd;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">

    <!--<iframe src="http://203.64.84.154:8088/video_feed" width="420" height="340"></iframe>-->

        <div class="container">
            <div class="header">全方位照護守護者</div>

            <div class="monitor">
                <asp:Label ID="LabelError" runat="server" Text="" ForeColor="Red" Font-Size="16"></asp:Label>

                <asp:Label ID="Label1" runat="server" Text="即時畫面" Font-Size="20" Font-Bold="True"></asp:Label>
                <br /><br />

                <!-- 使用 iframe 顯示監視器畫面 -->
                <iframe id="monitorFrame" src="http://203.64.84.154:8088/video_feed" runat="server"></iframe>
            </div>

            <div class="controls">
                <asp:Button ID="ButtonRefresh" runat="server" Text="重新加載畫面" OnClick="ButtonRefresh_Click" />
                <asp:Button ID="ButtonSwitch" runat="server" Text="切換畫面" OnClick="ButtonSwitch_Click" />
            </div>
        </div>
    </form>
</body>
</html>
