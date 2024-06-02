<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="residentlogin.aspx.cs" Inherits="bms.residentlogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log-in</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="styles1.css">
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    <script>
         function clearLabelAfterDelay(mbox, delay) {
            setTimeout(function() {
                document.getElementById(mbox).innerText = '';
            }, delay);
        }
    </script>
</head>
<body>
    <div class="container">
        <div class="sign-in">
            <form id="form1" runat="server" class="sign-in-form">
                <h2 class="title">Resident Sign in</h2>
                <div class="input-field">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="username" runat="server" type="text" placeholder="Username"></asp:TextBox>
                    </div>
                <div class="input-field">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="password" runat="server" type="password" placeholder="Password" ></asp:TextBox>
                </div>        
                <span></span>
                <br />
                <p class="account-text"> 
                    <asp:LinkButton ID="changeAccount" runat="server" PostBackUrl="~/reset_account.aspx" style="text-decoration: none; color:#ffbc04; cursor: pointer;">Change Username and Password?</asp:LinkButton>                  
                </p>            
                <span></span>
                <br />
                <div class="g-recaptcha" data-sitekey="6Ld04MQpAAAAAJhoPojzlZF5DipOm_k-sL1NgoKo"></div>            
                <span></span>
                <br />
                <div>
                    <asp:Button ID="reslog_btn" class="btn" runat="server" Text="Sign in" OnClick="reslog_btn_Click" />
                    <asp:Button ID="back_btn" class="btn" runat="server" Text="Back" OnClick="back_btn_Click" />
                </div>
                <br />
                <asp:Label ID="mbox" style="display: flex; justify-content: center;" runat="server"></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
