<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reset_account.aspx.cs" Inherits="bms.reset_account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Account Details</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="reset_account.css">
    <style>
     .modal {
            display: block;
            position: fixed; /* Stay in place */
            z-index: 4; /* Sit on top */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        .modal-content {
            background-color: #fefefe;
            margin: 5% auto; /* 15% from the top and centered */
            padding: 0;
            border: 1px solid #888;
            width: 35%; /* Could be more or less, depending on screen size */
            border-radius: 8px; /* Added border radius for smooth corners */
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Added shadow for better visibility */
            overflow: hidden;
        }

        .modal-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px 20px;
            background-color: #ff951c;/* Background color for header */
            color: white; /* Text color for header */
            border-top-left-radius: 8px; /* Rounded corners for the header */
            border-top-right-radius: 8px; /* Rounded corners for the header */
        }

        .modal-header h2 {
            margin: 0; /* Remove margin to keep header title aligned */
        }

        .close {
            color: white; /* Changed color to white to match header */
            font-size: 28px;
            font-weight: bold;
            cursor: pointer;
        }

        .close:hover,
        .close:focus {
            color: black; /* Slightly lighter color on hover */
            text-decoration: none;
        }

        .modal-content .res_data .form-control {
            position: relative;
            height: 30px;
            width: 100%;
            outline: none;
            font-size: 1rem;
            color: #707070;
            margin-top: 8px;
            border: 1px solid #ccc; /* Added a default border color */
            border-radius: 6px;
            padding: 0 15px;
        }

        .res_data {
            margin: 15px 20px;
        }
 </style>
    <script>
        function clearLabelAfterDelay(mbox, delay) {
            setTimeout(function() {
                document.getElementById(mbox).innerText = '';
            }, delay);
        }

        function setModalState() {
            var modal = document.getElementById("loginchangeModal");
            var modalState = document.getElementById("modalState").value;
            if (modalState === "hidden") {
                modal.style.display = "none";
            } else {
                modal.style.display = "block";
            }
        }

        function closeModal() {
             var modal = document.getElementById("loginchangeModal");
             modal.style.display = "none";
        }

        window.onload = setModalState;
    </script>
</head>  
<body>
    <div class="container">
        <div class="sign-up">
            <form id="form1" runat="server" class="sign-up-form">
                <asp:Label ID="otplbl" runat="server" Text="OTP Sent" style="display: none; color: #79b751;"></asp:Label>
                <h2 class="title">Change Account Details</h2>
                <div class="input-field">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="res_user" runat="server" type="text" placeholder="Username"></asp:TextBox>
                </div>
                <div class="input-field">
                    <i class="fab fa-google"></i>
                    <asp:TextBox ID="res_email" runat="server"  type="email" placeholder="Email"></asp:TextBox>
                </div>
                <div class="input-field">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="res_pass" runat="server" type="password" placeholder="Password"></asp:TextBox>
                </div>
                <div class="input-field">                   
                    <asp:Button ID="SendOTP" class="btn"  runat="server" Text="Send OTP" OnClick="SendOTP_Click"  />
                </div>
                <div id ="otpverify" class="input-field" runat="server">                    
                    <asp:TextBox ID="otp_inp" runat="server" placeholder="Enter OTP" style="text-align: center;"></asp:TextBox>
                    <asp:Button ID="VerifyOTP" class="btn"  runat="server" Text="Verify" OnClick="VerifyOTP_Click" />
                </div>              
                <div>
                    <p class="account-text">Already have an account? <asp:LinkButton ID="si_link" runat="server" PostBackUrl="~/residentlogin.aspx">Sign in</asp:LinkButton></p>
                    <asp:HiddenField ID="otphidden" runat="server" />
                </div>
                <div style="display: flex; justify-content: center;">
                     <asp:Button ID="su_btn" class="btn" runat="server" Text="Confirm" OnClick="su_btn_Click" Visible="false"/>
                     <br />
                     <asp:Label ID="mbox" runat="server"></asp:Label>
                </div> 
                
                <div id="loginchangeModal" class="modal">
                     <div class="modal-content">
                        <div class="modal-header">
                            <h2>Change of Account Confirmation</h2>
                            
                        </div>
                        <div class="f_info">
                            <div class="res_data">
                                <label for="username_tb">Username: </label>
                                <asp:TextBox ID="username_tb" runat="server" CssClass="form-control"  ></asp:TextBox>
                            </div>
                            <div class="res_data">
                                <label for="password_tb">Password: </label>
                                <asp:TextBox ID="password_tb" runat="server" type="password" CssClass="form-control" ></asp:TextBox>
                            </div>     
                            <asp:HiddenField ID="modalState" runat="server" />
                        </div>
                            <div class="res_data" style="display: flex; justify-content: center; gap: 10px;">
                                <asp:Button ID="save_edited_btn" CssClass="saveres_btn" runat="server" Text="Confirm" OnClick="save_edited_btn_Click"  />
                                <asp:Button ID="clear_edited_btn" CssClass="clear_btn" runat="server" Text="Back" OnClick="clear_edited_btn_Click" />
                            </div>  
                        </div>                    
                </div>
            </form>
        </div>
    </div>
    
</body>
</html>
