<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="bms.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Log-in</title>
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
                <h2 class="title">Sign in</h2>
                <div class="input-field">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="username" runat="server" type="text" placeholder="Username"></asp:TextBox>
                    </div>
                <div class="input-field">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="password" runat="server" type="password" placeholder="Password" ></asp:TextBox>
                </div>
                <br />
                <div class="g-recaptcha" data-sitekey="6Ld04MQpAAAAAJhoPojzlZF5DipOm_k-sL1NgoKo"></div>
                <br />
                <p class="account-text">Don't have an account? 
                    <asp:LinkButton ID="su_link" runat="server" OnClick="su_link_Click" PostBackUrl="~/signup.aspx">Sign up</asp:LinkButton>                  
                </p>            
                <span></span>                
                <p style="margin-top: 5px;" class="account-text">Sign in with Google?
                    <asp:LinkButton ID="gsignin" CssClass="google-icon" runat="server" OnClick="gsignin_Click">
                        <i style="color: #ffaa0d; " class="fab fa-google"></i>
                    </asp:LinkButton>
                </p>             
                <span></span>
                <br />
                <div>
                    <asp:Button ID="log_btn" class="btn" runat="server" Text="Sign in" OnClick="log_btn_Click" />
                    <asp:Button ID="back_btn" class="btn" runat="server" Text="Back" OnClick="back_btn_Click" />
                </div>                
                <br />
                <asp:Label ID="mbox" style="display: flex; justify-content: center;" runat="server"></asp:Label>
            </form>
        </div>
    </div>
    <script src="app.js"></script>
</body>
</html>
