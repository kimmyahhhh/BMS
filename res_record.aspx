<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="res_record.aspx.cs" Inherits="bms.res_record" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>Residents Records</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="res2_record1.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
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

       $(document).ready(function () {
            var timeout;
            $('#<%= search.ClientID %>').on('input', function () {
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    __doPostBack('<%= search.UniqueID %>', '');
                }, 500); // Adjust the delay as needed
            });
        });
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
                <asp:TextBox ID="search" runat="server" placeholder="Search" AutoPostBack="true" OnTextChanged="search_TextChanged" ></asp:TextBox>
                <asp:LinkButton ID="search_btn" runat="server" OnClick="search_btn_Click">
                    <i class="fas fa-search" style="color: black;"></i>
                </asp:LinkButton>
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
                <img src="images/n2logo.jpg" alt=""/>
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
                        <h1 class="resinfo_lbl">Resident's Information</h1>
                         <asp:Button ID="addres_btn" Class="addres_btn" runat="server" Text="Add Resident" OnClick="addres_btn_Click"/>
                     </div>
                </div>
            </div>
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="femaleCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Female</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-female"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="maleCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Male</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-male"></i>
                    </div>
                </div>                               
            </div>
            <div class="charts">
                <div class="chart">
                    <div class="csvexport" style="display: flex; justify-content: space-between; margin-left: 40px; margin-top:20px;">
                        <asp:Button ID="reqModalbtn" Class="tab_btn" runat="server" Text="Residents" OnClick="reqModalbtn_Click" />
                        <asp:Button ID="hisModalbtn" Class="tab_btn" runat="server" Text="Archived" OnClick="hisModalbtn_Click" />
                    </div>                    
                    <div class="resDiv" style="margin-top: 60px; margin-left:-240px">
                        <asp:GridView ID="GridView1" runat="server" CssClass="gridview-style" DataKeyNames="res_id, emp_id" AutoGenerateColumns="False"
                         AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" style="width: 100%;" >
                            <Columns>                               
                                        <asp:BoundField DataField="purok" HeaderText="Purok" />
                                        <asp:BoundField DataField="l_name" HeaderText="Last Name" />
                                        <asp:BoundField DataField="f_name" HeaderText="First Name" />
                                        <asp:BoundField DataField="m_name" HeaderText="Middle Name" />
                                        <asp:BoundField DataField="age" HeaderText="Age" />
                                        <asp:BoundField DataField="dob" HeaderText="Birthday" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="contact_no" HeaderText="Contact No." />
                                        <asp:BoundField DataField="job" HeaderText="Job" />
                                        <asp:TemplateField HeaderText="Archive">
                                            <ItemTemplate>
                                                <asp:Button ID="ArchiveButton" runat="server" Text="Archive" CommandName="ArchiveRow" style="text-decoration: none; color:white; cursor: pointer; background-color: red; padding: 3px 10px; border-radius: 7px; border-color: white;"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        </asp:GridView> 
                    </div>
                    <div class="archiveDiv" style="margin-top: 60px; margin-left: 0px">
                        <asp:GridView ID="GridView2" runat="server" CssClass="gridview-style" DataKeyNames="res_id, emp_id" AutoGenerateColumns="False"
                         AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView2_PageIndexChanging" style="width: 100%;" >
                            <Columns>                               
                                        <asp:BoundField DataField="purok" HeaderText="Purok" />
                                        <asp:BoundField DataField="l_name" HeaderText="Last Name" />
                                        <asp:BoundField DataField="f_name" HeaderText="First Name" />
                                        <asp:BoundField DataField="m_name" HeaderText="Middle Name" />
                                        <asp:BoundField DataField="age" HeaderText="Age" />
                                        <asp:BoundField DataField="dob" HeaderText="Birthday" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="contact_no" HeaderText="Contact No." />
                                        <asp:BoundField DataField="job" HeaderText="Job" />
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        </asp:GridView> 
                    </div>
                </div>
                <asp:Label ID="mbox" style="display: flex; justify-content: center;" runat="server"></asp:Label>
            </div>
          </div>
       </div>       
     </form>
   <script src="res_record.js"></script>
</body>
</html>
