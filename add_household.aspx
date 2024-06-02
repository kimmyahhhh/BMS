<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_household.aspx.cs" Inherits="bms.add_household" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adding of Household</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="add1_household1.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDsEVa_EMLnAKlC5CDB9-07TFS7ZIYm0Rw&callback=initMap" async defer></script>

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

        #map {
            height: 400px;
            width: 100%;
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

        function clearLabelAfterDelay(mbox, delay) {
            setTimeout(function() {
                document.getElementById(mbox).innerText = '';
            }, delay);
        }

        let map;
        let marker;

        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: 15.6895, lng: 120.7436 },
                zoom: 30
            });

            map.addListener('click', function (e) {
                placeMarkerAndPanTo(e.latLng, map);
            });
        }

        function placeMarkerAndPanTo(latLng, map) {
            if (marker) {
                marker.setPosition(latLng);
            } else {
                marker = new google.maps.Marker({
                    position: latLng,
                    map: map
                });
            }
            map.panTo(latLng);
            document.getElementById('<%= locationLat.ClientID %>').value = latLng.lat();
            document.getElementById('<%= locationLng.ClientID %>').value = latLng.lng();
        }         

        function openModal() {
            document.getElementById("householdModal").style.display = "block";
        }

        function viewLocation(latitude, longitude) {
            // Check if latitude and longitude are valid
            if (latitude !== "" && longitude !== "") {
                // Convert latitude and longitude strings to floats
                var lat = parseFloat(latitude);
                var lng = parseFloat(longitude);

                // Create a new Google Maps LatLng object
                var latLng = new google.maps.LatLng(lat, lng);

                // Create a new map centered at the specified location
                var map = new google.maps.Map(document.getElementById('map'), {
                    center: latLng,
                    zoom: 18
                });

                // Add a marker at the specified location
                var marker = new google.maps.Marker({
                    position: latLng,
                    map: map
                });
            } else {
                // Handle case when latitude or longitude is not available
                alert("Latitude or longitude is not available.");
            }
        }

        function showModal() {
            document.getElementById("membersModal").style.display = "block";
            
        }

        function closeModal() {
            document.getElementById("householdModal").style.display = "none";
            document.getElementById("membersModal").style.display = "none";
            document.getElementById("locationModal").style.display = "none";
        }

        $(document).ready(function () {
            var timeout;
            $('#<%= search.ClientID %>').on('input', function () {
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    __doPostBack('<%= search.UniqueID %>', '');
                }, 700);
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
                    <a href="#">
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
                        <h1 class="resinfo_lbl">Household Records</h1>
                         <asp:Button ID="add_HH_btn" Class="user_res_edit_btn" runat="server" Text="Add Household" OnClick="add_HH_btn_Click"/>
                     </div>
                </div>
            </div>
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="householdCount" class="number" runat="server"></asp:Label>
                        <div class="card-name">Household</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-home"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="p1Count" class="number" runat="server"></asp:Label>
                        <div class="card-name">Purok 1</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-street-view"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="p2Count" class="number" runat="server"></asp:Label>
                        <div class="card-name">Purok 2</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-street-view"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="p3Count" class="number" runat="server"></asp:Label>
                        <div class="card-name">Purok 3</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-street-view"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="p4Count" class="number" runat="server"></asp:Label>
                        <div class="card-name">Purok 4</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-street-view"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="p5Count" class="number" runat="server"></asp:Label>
                        <div class="card-name">Purok 5</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-street-view"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="p6Count" class="number" runat="server"></asp:Label>
                        <div class="card-name">Purok 6</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-street-view"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <asp:Label ID="p7Count" class="number" runat="server"></asp:Label>
                        <div class="card-name">Purok 7</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-street-view"></i>
                    </div>
                </div>
            </div>
            <div class="charts">
                <div class="chart">
                    <div class="householdtable" style="margin-top: 0px; margin-left:0px">
                        <asp:GridView ID="householdGV" runat="server" CssClass="gridview-style" DataKeyNames="household_id" AutoGenerateColumns="False"
                         AllowPaging="True" PageSize="10" OnRowCommand="householdGV_RowCommand" style="width:100%">
                            <Columns>    
                                        <asp:BoundField DataField="purok" HeaderText="Purok" />
                                        <asp:BoundField DataField="household_id" HeaderText="Household ID" />
                                        <asp:BoundField DataField="house_id" HeaderText="House ID" />
                                        <asp:BoundField DataField="household_name" HeaderText="Household" />
                                        <asp:BoundField DataField="lat" HeaderText="Latitude" />
                                        <asp:BoundField DataField="lng" HeaderText="Longitude" />
                                        <asp:TemplateField HeaderText="Members">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="MembersButton" runat="server" Text='<%# GetMembersCount(Eval("household_id")) %>' CommandArgument='<%# Eval("household_id") %>' OnClick="MembersButton_Click" style="text-decoration: none; color:#ffbc04; cursor: pointer;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="viewLocation_btn" runat="server" Text="View" CommandName="viewLoc" CommandArgument='<%# Eval("household_id") %>'  OnClick="viewLocation_btn_Click" style="text-decoration: none; color:white; cursor: pointer; background-color: limegreen; padding: 3px 10px; border-radius: 7px;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        </asp:GridView> 
                    </div>                                    
                </div>                
            </div>
            </div>
            
        </div>
        <div id="householdModal" class="modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Add Household</h2>
                    <span class="close" onclick="closeModal()">&times;</span>
                </div>
                <div class="f_info">
                    <div class="res_data">
                        <label for="household_tb">Household: </label>
                        <asp:TextBox ID="household_tb" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="houseID_tb">House ID: </label>
                        <asp:TextBox ID="houseID_tb" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="res_data">
                                <label>Purok:</label>
                           <div style="display: flex; align-items: center;">
                                <asp:DropDownList ID="PurokList" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">Purok</asp:ListItem>
                                    <asp:ListItem Text="1" />
                                    <asp:ListItem Text="2" />
                                    <asp:ListItem Text="3" />
                                    <asp:ListItem Text="4" />
                                    <asp:ListItem Text="5" />
                                    <asp:ListItem Text="6" />
                                    <asp:ListItem Text="7" />
                                </asp:DropDownList>
                               </div>
                       </div>
                        <div class="res_data">
                            <label>Location:</label>
                            <div id="map"></div>
                        </div>
                        <asp:HiddenField ID="locationLat" runat="server" />
                        <asp:HiddenField ID="locationLng" runat="server" />
                </div>
                    <div class="res_data" style="display: flex; justify-content: center; gap: 10px;">
                        <asp:Button ID="save_edited_btn" CssClass="saveres_btn" runat="server" Text="Add" OnClick="save_edited_btn_Click" />
                        <asp:Button ID="clear_edited_btn" CssClass="clear_btn" runat="server" Text="Clear" OnClick="clear_edited_btn_Click" />
                    </div>
                <br />
                    <asp:Label ID="mbox" runat="server"></asp:Label>
                </div>
            </div>        
            <div id="membersModal" class="modal">
                <div class="modal-content">
                    <div class="modal-header">
                        <h2>Household Members for <asp:Label ID="householdNameLabel" runat="server"></asp:Label></h2>
                        <span class="close" onclick="closeModal()">&times;</span>
                    </div>                        
                    <asp:GridView ID="membersGridView" CssClass="gridview-style" runat="server" AutoGenerateColumns="False" style="width:100%">
                        <Columns>
                            <asp:BoundField DataField="l_name" HeaderText="Last Name" />
                            <asp:BoundField DataField="f_name" HeaderText="First Name" />
                            <asp:BoundField DataField="m_name" HeaderText="Middle Name" />
                            <asp:BoundField DataField="contact_no" HeaderText="Contact No." />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>     
    </form> 
</body>
</html>
