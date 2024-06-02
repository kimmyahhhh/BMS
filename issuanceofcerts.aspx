<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="issuanceofcerts.aspx.cs" Inherits="bms.issuanceofcerts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issuance of Certificates</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="issuanceofcert.css">
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
    <script type="text/javascript">
        function clearLabelAfterDelay(mbox, delay) {
            setTimeout(function() {
                document.getElementById(mbox).innerText = '';
            }, delay);
        }

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
                        <div  style="margin-right: 110px" ><i class="fas fa-sign-out-alt" style="margin-right: 23px"></i>Log Out</div>                        
                    </button>
                </li>
            </ul>
        </div>
        <div class="main">
            <div class="hcards">
                <div class="hcards-content">
                    <div class="add-res">
                        <h1 class="resinfo_lbl">Issuance of Certificates</h1>
                     </div>
                </div>
            </div>
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="certificateCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Issued Certificate</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-folders"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="pendingCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Pending</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-tasks"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="verifiedCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Verified</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-badge-check"></i>
                    </div>
                </div>
            </div>
            <div class="charts">
                <div class="chart">
                    <div class="tab" style="display: flex; justify-content: space-between; margin-left: 45px; margin-top:20px;">
                      <asp:Button ID="reqModalbtn" Class="tab_btn" runat="server" Text="Request" OnClick="reqModalbtn_Click" />
                      <asp:Button ID="hisModalbtn" Class="tab_btn" runat="server" Text="Issued" OnClick="hisModalbtn_Click" />
                    </div>
                         <div class="reqGrid" style="margin-top: 60px; margin-left:-240px">
                                <asp:GridView ID="RequestGV" runat="server" CssClass="gridview-style" DataKeyNames="res_id, cert_id" AutoGenerateColumns="False"
                                         AllowPaging="True" PageSize="10" OnPageIndexChanging="RequestGV_PageIndexChanging" OnRowCommand="RequestGV_RowCommand" OnRowDataBound="RequestGV_RowDataBound" style="width: 100%;" >
                                            <Columns>                               
                                                      <asp:BoundField DataField="purok" HeaderText="Purok" />
                                                      <asp:BoundField DataField="l_name" HeaderText="Last Name" />
                                                      <asp:BoundField DataField="f_name" HeaderText="First Name" />
                                                      <asp:BoundField DataField="m_name" HeaderText="Middle Name" />
                                                      <asp:BoundField DataField="contact_no" HeaderText="Contact No." />
                                                      <asp:BoundField DataField="certificate" HeaderText="Certificate" />
                                                      <asp:BoundField DataField="purpose" HeaderText="Purpose" />
                                                      <asp:TemplateField HeaderText="">
                                                          <ItemTemplate>
                                                              <asp:Button ID="confirm_btn" runat="server" Text="Confirm" CommandName="Confirm" style="border-color: white; border:none; background-color: limegreen; color: white; border-color: black; border-radius:5px; width: 70px; height: 20px" />
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                </asp:GridView>
                         </div>              
                         <div class="historyGrid" style="margin-top: 60px; margin-left:0px">
                             <asp:GridView ID="HistoryGV" runat="server" CssClass="gridview-style" DataKeyNames="res_id, cert_id" AutoGenerateColumns="False"
                                         AllowPaging="True" PageSize="10" OnPageIndexChanging="HistoryGV_PageIndexChanging" OnRowCommand="HistoryGV_RowCommand" style="width: 100%;" >
                                            <Columns>                               
                                                      <asp:BoundField DataField="purok" HeaderText="Purok" />
                                                      <asp:BoundField DataField="l_name" HeaderText="Last Name" />
                                                      <asp:BoundField DataField="f_name" HeaderText="First Name" />
                                                      <asp:BoundField DataField="m_name" HeaderText="Middle Name" />
                                                      <asp:BoundField DataField="contact_no" HeaderText="Contact No." />
                                                      <asp:BoundField DataField="certificate" HeaderText="Certificate" /> 
                                                      <asp:BoundField DataField="purpose" HeaderText="Purpose" />
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                              </asp:GridView>
                         </div>
                    </div>
                </div>
            <asp:Label ID="mbox" style="display: flex; justify-content: center;" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
