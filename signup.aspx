<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="bms.signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <div class="container">
        <div class="sign-up">
            <form id="form2" runat="server" class="sign-up-form">
                <h2 class="title">Sign up</h2>
                <div class="input-field">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="su_user" runat="server" type="text" placeholder="Username"></asp:TextBox>
                </div>
                <div class="input-field">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="su_pass" runat="server" type="password" placeholder="Password"></asp:TextBox>
                </div>
                <div class="input-field">
                    <i class="fas fa-check"></i>
                    <asp:TextBox ID="su_copass" runat="server" type="password" placeholder="Confirm Password"></asp:TextBox>
                </div>
                <div>
                    <p class="account-text">Already have an account? <asp:LinkButton ID="si_link" runat="server" PostBackUrl="~/login.aspx">Sign in</asp:LinkButton></p>
                </div>
                <div>
                     <asp:Button ID="su_btn" class="btn" runat="server" Text="Sign up" OnClick="su_btn_Click"/>
                     <asp:Label ID="mbox" runat="server"></asp:Label>
                </div>
                              
            </form>
        </div>
    </div>
    <script src="app.js"></script>
</body>
</html>
