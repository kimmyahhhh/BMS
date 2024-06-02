<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="blotter.aspx.cs" Inherits="blotter.blotter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Blotter</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="blotters.css"/>
    <style>
        .modal {
            display: none;
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
        function openModal() {
            document.getElementById("viewdetailsModal").style.display = "block";
        }
        function closeModal() {
            document.getElementById("viewdetailsModal").style.display = "none";
        }
       <%-- $(document).ready(function () {
            var timeout;
            $('#<%= search.ClientID %>').on('input', function () {
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    __doPostBack('<%= search.UniqueID %>', '');
                }, 500);
            });
        });    --%>
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
                <asp:TextBox ID="search" runat="server" type="text" placeholder="Search" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
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
                        <h1 class="resinfo_lbl1">Blotter</h1>
                     </div>
                </div>
            </div>
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="scheduledCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Scheduled</div>
                    </div>
                    <div class="icon-box">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkPending_Click">
                        <i class="fas fa-clock"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="activeCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Active</div>
                    </div>
                    <div class="icon-box">                       
                        <asp:LinkButton ID="lnkActive" runat="server" OnClick="lnkActive_Click">
                        <i class="fas fa-tasks"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="settledCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Settled</div>
                    </div>
                    <div class="icon-box">                       
                        <asp:LinkButton ID="lnkEnded" runat="server" OnClick="lnkEnded_Click">
                        <i class="fas fa-badge-check"></i>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="charts">
                <div class="chart">
                    <div class="tab" style="display: flex; justify-content: space-between; margin-left: 45px; margin-top:20px;">
                      <asp:Button ID="reqModalbtn" Class="tab_btn" runat="server" Text="Complain" OnClick="reqModalbtn_Click" />
                      <asp:Button ID="hisModalbtn" Class="tab_btn" runat="server" Text="Settled" OnClick="hisModalbtn_Click" />
                    </div>
                  <div class="adminblotter" style="margin-top: 60px; margin-left:-240px">
                        <asp:GridView ID="adminblotterGV" runat="server" CssClass="gridview-style" DataKeyNames="res_id, blotter_id" AutoGenerateColumns="False" OnRowCommand="adminblotterGV_RowCommand" OnRowDataBound="adminblotterGV_RowDataBound"
                                     AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging" PageSize="1">
                            <Columns>
                                <asp:BoundField DataField="blotter_id" HeaderText="ID" />
                                <asp:BoundField DataField="complainant" HeaderText="Complainant" />
                                <asp:BoundField DataField="compliance" HeaderText="Compliance" />
                                <asp:BoundField DataField="incident_date" HeaderText="Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="incident_time" HeaderText="Time"/>
                                <asp:BoundField DataField="suspect" HeaderText="Suspect" />
                                <asp:BoundField DataField="started" HeaderText="Scheduled" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="status" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Settled">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEnded" runat="server" Text='<%# Eval("ended", "{0:d}") %>' Visible='<%# Eval("status").ToString() == "Settled" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="viewdetails_btn" runat="server" Text='View' CommandArgument='<%# Eval("blotter_id") %>' CommandName="ViewDetails"><i class="fas fa-user-tie" style="color: #ffaa0d;"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       </div>
                    <div class="historyblotter" style="margin-top: 60px; margin-left: 0px;">
                        <asp:GridView ID="historyadminblotterGV" runat="server" CssClass="gridview-style" DataKeyNames="res_id, blotter_id" AutoGenerateColumns="False" 
                         AllowPaging="True" PageSize="10" OnRowDataBound="historyadminblotterGV_RowDataBound" OnRowCommand="historyadminblotterGV_RowCommand">
                            <Columns> 
                                        <asp:BoundField DataField="blotter_id" HeaderText="ID" />
                                        <asp:BoundField DataField="complainant" HeaderText="Complainant" />
                                        <asp:BoundField DataField="compliance" HeaderText="Compliance" />
                                        <asp:BoundField DataField="incident_date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="incident_time" HeaderText="Time"/>
                                        <asp:BoundField DataField="suspect" HeaderText="Suspect" />
                                        <asp:BoundField DataField="started" HeaderText="Scheduled" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="ended" HeaderText="Settled" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="status" HeaderText="Status" />                          
                            </Columns>
                            
                        </asp:GridView>
                    </div>
                </div>
                <asp:Label ID="mbox" style="display: flex; justify-content: center;" runat="server"></asp:Label>
            </div>
          </div>
        </div>
        <div id="viewdetailsModal" class="modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Blotter Details</h2>
                    <span class="close" onclick="closeModal()">&times;</span>
                </div>
                <div class="f_info">
                    <div class="res_data">
                        <label for="compliant_tb">Complainant: </label>
                        <asp:TextBox ID="compliant_tb" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="compliance_tb">Compliance: </label>
                        <asp:TextBox ID="compliance_tb" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="date_tb">Date: </label>
                        <asp:TextBox ID="date_tb" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="time_tb">Time: </label>
                        <asp:TextBox ID="time_tb" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="started_tb">Started: </label>
                        <asp:TextBox ID="started_tb" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="suspect_tb">Suspect: </label>
                        <asp:TextBox ID="suspect_tb" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="res_data">
                              <label>Status:</label>
                                <div style="display: flex; align-items: center;">
                                    <asp:DropDownList ID="blotterstatusList" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="">Status</asp:ListItem>
                                        <asp:ListItem Text="Scheduled" />
                                        <asp:ListItem Text="Active" />
                                        <asp:ListItem Text="Settled" />
                                    </asp:DropDownList>
                                </div>
                       </div>                        
                </div>
                    <div class="res_data" style="display: flex; justify-content: center; gap: 10px;">
                        <asp:Button ID="save_edited_btn" CssClass="saveres_btn" runat="server" Text="Save" OnClick="save_edited_btn_Click" />
                    </div>  
                </div>
            </div>
       </form>
</body>
</html>
