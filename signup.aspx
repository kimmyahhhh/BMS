<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="bms.signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Sign-up</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="styles1.css">
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
        <div class="sign-up">
            <form id="form2" runat="server" class="sign-up-form">
                <asp:Label ID="otplbl" runat="server" Text="OTP Sent" style="display: none; color: #79b751;"></asp:Label>
                <h2 class="title">Sign up</h2>
                <div class="input-field">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="su_user" runat="server" type="text" placeholder="Username"></asp:TextBox>
                </div>
                <div class="input-field">
                    <i class="fab fa-google"></i>
                    <asp:TextBox ID="su_email" runat="server"  type="email" placeholder="Email"></asp:TextBox>
                </div>
                <div class="input-field">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="su_pass" runat="server" type="password" placeholder="Password"></asp:TextBox>
                </div>
                <div class="input-field">                   
                    <asp:Button ID="SendOTP" class="btn"  runat="server" Text="Send OTP" OnClick="SendOTP_Click"  />
                </div>
                <div id ="otpverify" class="input-field" runat="server">                    
                    <asp:TextBox ID="otp_inp" runat="server" placeholder="Enter OTP" style="text-align: center;"></asp:TextBox>
                    <asp:Button ID="VerifyOTP" class="btn"  runat="server" Text="Verify" OnClick="VerifyOTP_Click" />
                </div>               
                <div>
                    <p class="account-text">Already have an account? <asp:LinkButton ID="si_link" runat="server" PostBackUrl="~/login.aspx">Sign in</asp:LinkButton></p>
                    <asp:HiddenField ID="otphidden" runat="server" />
                </div>
                <div>
                     <asp:Button ID="su_btn" class="btn" runat="server" Text="Sign up" OnClick="su_btn_Click" Visible="false"/>
                     <asp:Button ID="back_btn" class="btn" runat="server" Text="Back" OnClick="back_btn_Click" />
                </div>
                <br />
                <asp:Label ID="mbox" style="display: flex; justify-content: center;" runat="server"></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
