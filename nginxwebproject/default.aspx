<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="nginxwebproject._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录页面</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <label>用户名:</label></td>
                    <td>
                        <input type="text" id="txtUserName" value="redis" /></td>
                </tr>
                <tr>
                    <td>
                        <label>密码:</label></td>
                    <td>
                        <input type="text" id="txtPassword" value="redis" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <input type="button" value="登录" onclick="loginSystem();" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript" src="//192.168.164.130:8084\jquery1.9.1\jquery-1.9.1.js"></script>
    <script type="text/javascript">
        function loginSystem() {
            var userName = $("#txtUserName").val();
            var userPwd = $("#txtPassword").val();
            var params = { type: 0, userName: userName, userPwd: userPwd, rd: Math.random() };
            $.post("default.aspx",
                params,
                function (result) {
                    if (result == null) {
                        alert("连接网络异常");
                        return;
                    }
                    alert(result.message);
                    if (result.status == "true") {
                        window.location.href = "content.aspx";
                    }
                }, "json");
        }
    </script>
</body>
</html>
