<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="bms.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Log-in</title>
     <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="styles.css">
</head>
<body>
        <div class="container">
        <div class="sign-in">
            <form id="form1" runat="server" class="sign-in-form">
                <h2 class="title">Sign in</h2>
                <div class="input-field">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="username" runat="server" type="text" placeholder="Username"></asp:TextBox>
                    </div>
                <div class="input-field">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="password" runat="server" type="password" placeholder="Password" ></asp:TextBox>
                </div>
                <p class="account-text">Don't have an account? <asp:LinkButton ID="su_link" runat="server" OnClick="su_link_Click" PostBackUrl="~/signup.aspx">Sign up</asp:LinkButton></p>            
                <span></span>
                <asp:Button ID="log_btn" class="btn" runat="server" Text="Sign in" OnClick="log_btn_Click" />
                <asp:Label ID="mbox" runat="server"></asp:Label>
            </form>
        </div>
    </div>
    <script src="app.js"></script>
</body>
</html>
