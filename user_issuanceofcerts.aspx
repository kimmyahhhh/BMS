<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssuanceOfCertificates.aspx.cs" Inherits="bms.userIOC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issuance of Certificates</title>
    <script>
        function showMessage(event) {
            event.preventDefault();

            var purpose = document.getElementById("purpose").value.trim();
            if (purpose === "") {
                return;
            }

            var message = document.getElementById("message");
            message.innerHTML = "Request Submitted";
            message.style.display = "block";

            document.getElementById("purpose").value = "";
            setTimeout(function () {
                message.style.display = "none";
            }, 10000);
        }
    </script>

    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="userIOC.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="topbar">
                <div class="logo">
                    <h2>BMS</h2>
                </div>
                <div class="search">
                    <asp:TextBox ID="search" runat="server" type="text" placeholder="Search"></asp:TextBox>
                    <label for="search"><i class="fas fa-search"></i></label>
                </div>
                <i class="fas fa-bell"></i>
                <div class="user">
                    <img src="img/user.png" alt="">
                </div>
            </div>
            <div class="sidebar">
                <ul>
                    <li>
                        <a href="#">
                            <i class="fas fa-th-large"></i>
                            <div>Dashboard</div>
                        </a>
                    </li>
                    <li>
                        <a href="res_record.aspx">
                            <i class="fas fa-user-friends"></i>
                            <div>Resident Records</div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="fas fa-home"></i>
                            <div>Household Records</div>
                        </a>
                    </li>
                    <li>
                        <a href="chalve.html">
                            <i class="fas fa-layer-group"></i>
                            <div>Blotters</div>
                        </a>
                    </li>
                    <li>
                        <a href="try.html">
                            <i class="fa-regular fa-file"></i>
                            <div>Issuance of Certificates</div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="fas fa-chart-bar"></i>
                            <div>Analytics</div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="fas fa-hand-holding-usd"></i>
                            <div>Earnings</div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="fas fa-cog"></i>
                            <div>Settings</div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="fas fa-question"></i>
                            <div>Help</div>
                        </a>
                    </li>
                </ul>
            </div>

            <section class="certificate-generator">
                <h1>Issuance of Certificates</h1>

                <div class="form-group">
                    <label for="name">Purpose:</label>
                    <input type="text" id="purpose" name="purpose" required><br><br>
                </div>

                <div class="error-message" id="errorMessage"></div>

                <form>
                    <label for="type">Type:</label>
                    <select id="type" name="type" required>
                        <option value="Brgy. Clearance">Brgy. Clearance</option>
                        <option value="Certificate of Indigency">Certificate of Indigency</option>
                        <option value="Certificate of Residency">Certificate of Residency</option>
                    </select><br><br>
                    <button type="submit" onclick="showMessage()">Request</button>
                </form>
                <div id="message" style="display: none;"></div>

            </section>
        </div>
    </form>
</body>
</html>
