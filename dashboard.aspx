<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="bms.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="dashboard1.css">
    <style>
        .notification-dropdown {
            position: absolute;
            background-color: white;
            box-shadow: 0 8px 16px rgba(0,0,0,0.2);
            left: 77%;
            width: 200px;
            height: 75px;
            z-index: 1;
        }

        .notification-item {
            padding: 10px;
            border-bottom: 1px solid #ddd;
        }

        .notification-item:hover {
            background-color: #f1f1f1;
        }
    </style>
    <script>
        function toggleNotifications() {
            var dropdown = document.getElementById("notificationDropdown");
            if (dropdown.style.display === "none" || dropdown.style.display === "") {
                dropdown.style.display = "block";
            } else {
                dropdown.style.display = "none";
            }
        }

        // Close the dropdown if clicked outside
        window.onclick = function(event) {
            if (!event.target.matches('.notification, .notification *')) {
                var dropdowns = document.getElementsByClassName("notification-dropdown");
                for (var i = 0; i < dropdowns.length; i++) {
                    var openDropdown = dropdowns[i];
                    if (openDropdown.style.display === "block") {
                        openDropdown.style.display = "none";
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
        <div class="topbar">
            <div class="logo">
                <a href="dashboard.aspx" style="text-decoration: none; cursor: pointer;">
                    <h2>BMS</h2>
                    <img src="images/bmslogo.jpg">
                </a>                 
            </div>
            <div class="search">
                <asp:TextBox ID="search" runat="server" type="text" placeholder="Search"></asp:TextBox>
                <label for="search"><i class="fas fa-search"></i></label>
            </div>
            <div class="notification" onclick="toggleNotifications()">
                <i class="fas fa-bell"></i><span id="notificationCount" runat="server"></span>
                <div class="notification-dropdown" id="notificationDropdown" style="display: none;">
                    <asp:Repeater ID="NotificationList" runat="server">
                        <ItemTemplate>
                            <div class="notification-item">
                                <%# Eval("message") %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="user">
                <img src="images/n2logo.jpg" alt="">
            </div>
        </div>
        <div class="sidebar">
            <ul>
                <li>
                    <a href="dashboard.aspx">
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
                    <a href="add_household.aspx">
                        <i class="fas fa-home"></i>
                        <div>Household Records</div>
                    </a>
                </li>
                <li>
                    <a href="blotter.aspx">
                      <i class="fas fa-layer-group"></i>
                        <div>Blotters</div>
                    </a>
                </li>
                <li>
                    <a href="issuanceofcerts.aspx">
                        <i class="fas fa-folder"></i>
                        <div>Issuance of Certificates</div>
                    </a>
                </li>
                <li>
                    <a href="adminconfirmation.aspx">
                        <i class="fas fa-folder"></i>
                        <div>Admin Confirmation</div>
                    </a>
                </li>
                <li>
                    <button type="button" id="logoutButton" runat="server" onserverclick="Logout_Click" style="width: 100%; height: 50px; border:none; background-color:#ff951c; color: #fff; font-size: 18.5px; cursor: pointer;">
                        <div  style="margin-left: 20px" ><i class="fas fa-sign-out-alt" style="margin-right: 23px"></i>Log Out</div>                        
                    </button>
                </li>
            </ul>
        </div>
        <div class="main">
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="totalresCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Resident</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-users"></i>
                    </div>
                    
                </div>
                <div class="card">                    
                    <div class="card-content">
                        <asp:Label ID="totalblotterCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Blotter</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-layer-group"></i>
                    </div>
                </div>
                <div class="card">                    
                    <div class="card-content">
                        <asp:Label ID="totalhouseholdCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Household</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-home"></i>
                    </div>
                </div>
                <div class="card">                    
                    <div class="card-content">
                        <asp:Label ID="totalcertificateCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Issued Certificate</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-folder"></i>
                    </div>
                </div>
            </div>
            <div class="charts">
                <div class="chart">
                    <h2>Barangay Narvacan II</h2>
                    <div class="iframe-container">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15364.51865662329!2d120.73502323966451!3d15.691267917118338!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x33912da60deb4d09%3A0xe50d0693998a2108!2sNarvacan%20II%2C%20Guimba%2C%20Nueva%20Ecija!5e0!3m2!1sen!2sph!4v1713905119016!5m2!1sen!2sph"  width="800" height="330" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                </div>
                <div class="chart1">
                    <h2>Calendar</h2>
                    <div class="calendar-container">
                        <asp:Calendar ID="Calendar1" class="custom-calendar" runat="server"></asp:Calendar>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
