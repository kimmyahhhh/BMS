<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_blotter.aspx.cs" Inherits="bms.user_blotter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Blotter</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="user_blotter1.css">
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
 </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
        <div class="topbar">
            <div class="logo">
                <a href="residentdashboard.aspx" style="text-decoration: none; cursor: pointer;">
                <h2>BMS</h2>
                <img src="images/bmslogo.jpg">
                    </a>
            </div>
            <div class="search">
                <asp:TextBox ID="search" runat="server" type="text" placeholder="Search"></asp:TextBox>
                <label for="search"><i class="fas fa-search"></i></label>
            </div>
            <i class="fas fa-bell"></i>
            <div class="user">
                <img src="images/n2logo.jpg" alt="">
            </div>
        </div>
        <div class="sidebar">
            <ul>
                <li>
                    <a href="residentdashboard.aspx">
                        <i class="fas fa-th-large"></i>
                        <div>Dashboard</div>
                    </a>
                </li>
                <li>
                    <a href="user_res_info.aspx">
                        <i class="fas fa-user-friends"></i>
                        <div>Personal Info</div>
                    </a>
                </li>                
                <li>
                    <a href="user_blotter.aspx">
                      <i class="fas fa-layer-group"></i>
                        <div>Blotters</div>
                    </a>
                </li>
                <li>
                    <a href="user_issuanceofcerts.aspx">
                        <i class="fas fa-folder"></i>
                        <div>Issuance of Certificates</div>
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
                         <asp:Button ID="complain_btn" Class="user_res_edit_btn" runat="server" Text="Complain" OnClick="complain_btn_Click"/>
                     </div>
                </div>
            </div>
            <div class="charts">
                <div class="chart">
                    <div class="tab" style="display: flex; justify-content: space-between; margin-left: 45px; margin-top:20px;">
                      <asp:Button ID="reqModalbtn" Class="tab_btn" runat="server" Text="Request" OnClick="reqModalbtn_Click" />
                      <asp:Button ID="hisModalbtn" Class="tab_btn" runat="server" Text="History" OnClick="hisModalbtn_Click" />
                    </div>
                    <div class="usercomplain" style="margin-top: 60px; margin-left:-240px">
                        <asp:GridView ID="usercomplainGV" runat="server" CssClass="gridview-style" DataKeyNames="res_id, blotter_id" AutoGenerateColumns="False" OnRowCommand="usercomplainGV_RowCommand"
                     AllowPaging="True" PageSize="10">
                        <Columns>
                                    <asp:BoundField DataField="blotter_id" HeaderText="ID" />
                                    <asp:BoundField DataField="compliance" HeaderText="Compliance" />
                                    <asp:BoundField DataField="incident_date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="incident_time" HeaderText="Time"/>
                                    <asp:BoundField DataField="suspect" HeaderText="Suspect" />                                    
                                    <asp:BoundField DataField="started" HeaderText="Scheduled" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Archive">
                                        <ItemTemplate>
                                            <asp:Button ID="ArchiveButton" runat="server" Text="Archive" CommandName="ArchiveRow" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                    </asp:GridView>
                    </div>
                    <div class="historycomplain" style="margin-top: 60px; margin-left: 0px">
                        <asp:GridView ID="historycomplainGV" runat="server" CssClass="gridview-style" DataKeyNames="res_id, blotter_id" AutoGenerateColumns="False" OnRowCommand="usercomplainGV_RowCommand"
                         AllowPaging="True" PageSize="10">
                            <Columns>
                                        <asp:BoundField DataField="blotter_id" HeaderText="ID" />
                                        <asp:BoundField DataField="compliance" HeaderText="Compliance" />
                                        <asp:BoundField DataField="incident_date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="incident_time" HeaderText="Time"/>
                                        <asp:BoundField DataField="suspect" HeaderText="Suspect" />
                                        <asp:BoundField DataField="started" HeaderText="Scheduled" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="ended" HeaderText="Settled" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="status" HeaderText="Status" />
                                        <asp:TemplateField HeaderText="Archive">
                                            <ItemTemplate>
                                                <asp:Button ID="ArchiveButton" runat="server" Text="Archive" CommandName="ArchiveRow" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <asp:Label ID="mbox" style="display: flex; justify-content: center;" runat="server"></asp:Label>
            </div>
          </div>
            <div id="complaining" class="modal">
                 <div class="modal-content">
                    <div class="modal-header">
                        <h2>Add Complain</h2>
                        <span class="close" onclick="closeModal()">&times;</span>
                    </div>
                    <div class="f_info">
                        <div class="res_data">
                            <label for="complainant_tb">Complainant: </label>
                            <asp:TextBox ID="complainant_tb" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                        </div>
                        <div class="res_data">
                            <label for="complain_tb">Complain: </label>
                            <asp:TextBox ID="complain_tb" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="res_data">
                            <label for="date_tb">Date: </label>
                            <asp:TextBox ID="date_tb" runat="server" type="date" placeholder="YYYY-MM-DD" CssClass="form-control" OnTextChanged="date_tb_TextChanged"></asp:TextBox>
                        </div>
                        <div class="res_data">
                            <label for="time_tb">Time: </label>
                            <asp:TextBox ID="time_tb"  type="time" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="res_data">
                            <label for="suspect_tb">Suspect: </label>
                            <asp:TextBox ID="suspect_tb" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                        <div class="res_data" style="display: flex; justify-content: center; gap: 10px;">
                            <asp:Button ID="save_edited_btn" CssClass="saveres_btn" runat="server" Text="Add" OnClick="save_edited_btn_Click"  />
                            <asp:Button ID="clear_edited_btn" CssClass="clear_btn" runat="server" Text="Clear" OnClick="clear_edited_btn_Click" />
                        </div>  
                    </div>                    
            </div>
         </div>
       </form>
    <script>
         function clearLabelAfterDelay(mbox, delay) {
            setTimeout(function() {
                document.getElementById(mbox).innerText = '';
            }, delay);
        }

        function openModal() {
            var modal = document.getElementById("complaining");
            modal.style.display = "block";
        }

        function closeModal() {
             var modal = document.getElementById("complaining");
             modal.style.display = "none";
        }
    </script>
</body>
</html>
