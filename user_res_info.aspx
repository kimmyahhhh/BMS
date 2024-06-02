<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_res_info.aspx.cs" Inherits="bms.user_res_info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Information</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="user_res_info2.css">
    <style>
        .modal {
            display: none; /* Hidden by default */
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
        function openModal() {
            document.getElementById("infoModal").style.display = "block";
        }

        function closeModal() {
            document.getElementById("infoModal").style.display = "none";
        } 
    </script>
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
        <div class="main" style="">
            <div class="hcards">
                <div class="hcards-content">
                    <div class="add-res">
                        <h1 class="resinfo_lbl">Your Information</h1>
                         <asp:Button ID="user_res_edit_btn" Class="user_res_edit_btn" runat="server" Text="Edit" OnClick="user_res_edit_btn_Click"/>
                     </div>
                </div>
            </div>
            <br /><br /><br />
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <div class="f_info">
                            <div class="res_data">
                                <label>First Name: </label>
                                <asp:Label ID="fname_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Middle Name: </label>
                                <asp:Label ID="mname_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Last Name: </label>
                                <asp:Label ID="lname_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Birthday: </label>
                                <asp:Label ID="dob_lbl" CssClass="form-control" runat="server" Text='<%# Eval("dob", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Age: </label>
                                <asp:Label ID="age_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Sex: </label>
                                <asp:Label ID="sex_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Contact No.:</label>
                                <asp:Label ID="contact_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Household:</label>
                                <asp:Label ID="HH_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>                    
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="f_info1">
                            <div class="res_data">
                                <label>Place of Birth:</label>
                                <asp:Label ID="pob_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Religion:</label>
                                <asp:Label ID="religion_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Civil Status:</label>
                                <asp:Label ID="CS_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Educational Attainment:</label>
                                <asp:Label ID="EA_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Employment:</label>
                                <asp:Label ID="employment_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Employment Status:</label>
                                <asp:Label ID="Emp_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Occupation:</label>
                                <asp:Label ID="occupation_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                            <div class="res_data">
                                <label>Income Level:</label>
                                <asp:Label ID="income_lbl" CssClass="form-control" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>                
            </div>
         </div>
      </div>
        <br />
        <asp:Label ID="mbox" runat="server"></asp:Label>
        <div id="infoModal" class="modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Edit Information</h2>
                    <span class="close" onclick="closeModal()">&times;</span>
                </div>               
                <div class="f_info">
                    <div class="res_data">
                        <label for="fname_tb">First Name: </label>
                        <asp:TextBox ID="fname_tb" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="mname_tb">Middle Name: </label>
                        <asp:TextBox ID="mname_tb" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="lname_tb">Last Name: </label>
                        <asp:TextBox ID="lname_tb" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="dob_tb">Birthday: </label>
                        <asp:TextBox ID="dob_tb" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" type="Date" placeholder="YYYY-MM-DD" OnTextChanged="dob_tb_TextChanged"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="age_tb">Age: </label>
                        <asp:TextBox ID="age_tb" runat="server" CssClass="form-control" Enabled="false" placeholder="Age"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="sexlist">Sex</label>
                        <asp:DropDownList ID="sexlist" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Sex" Value="" />
                            <asp:ListItem Text="M" Value="M" />
                            <asp:ListItem Text="F" Value="F" />
                        </asp:DropDownList>
                    </div>
                    <div class="res_data">
                        <label for="contact_tb">Contact No.:</label>
                        <asp:TextBox ID="contact_tb" runat="server" CssClass="form-control" placeholder="Contact No." OnTextChanged="contact_tb_TextChanged"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="pob_db">Place of Birth:</label>
                        <asp:TextBox ID="pob_db" runat="server" CssClass="form-control" placeholder="Place of Birth"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="religion_tb">Religion:</label>
                        <asp:TextBox ID="religion_tb" runat="server" CssClass="form-control" placeholder="Religion"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="CSList">Civil Status:</label>
                        <asp:DropDownList ID="CSList" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Status" Value="" />
                            <asp:ListItem Text="Single" Value="Single" />
                            <asp:ListItem Text="Married" Value="Married" />
                            <asp:ListItem Text="Separated" Value="Separated" />
                            <asp:ListItem Text="Annuled" Value="Annuled" />
                            <asp:ListItem Text="Widow/er" Value="Widow/er" />
                        </asp:DropDownList>
                    </div>
                    <div class="res_data">
                        <label for="EAList">Educational Attainment:</label>
                        <asp:DropDownList ID="EAList" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Attainment" Value="" />
                            <asp:ListItem Text="Elementary School" Value="Elementary School" />
                            <asp:ListItem Text="High School (Junior)" Value="High School (Junior)" />
                            <asp:ListItem Text="High School (Senior)" Value="High School (Senior)" />
                            <asp:ListItem Text="College" Value="College" />
                            <asp:ListItem Text="College Graduate" Value="College Graduate" />
                            <asp:ListItem Text="Illiterate" Value="Illiterate" />
                        </asp:DropDownList>
                    </div>
                    <div class="res_data">
                        <label for="employment">Employment:</label>
                        <asp:DropDownList ID="employment" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="employment_SelectedIndexChanged">
                            <asp:ListItem Text="Select Employment" Value="" />
                            <asp:ListItem Text="Employed" Value="Employed" />
                            <asp:ListItem Text="Self-Employed" Value="Self-Employed" />
                            <asp:ListItem Text="Underemployed" Value="Underemployed" />
                            <asp:ListItem Text="Unemployed" Value="Unemployed" />
                        </asp:DropDownList>
                    </div>
                    <div class="res_data">
                        <label for="EmpList">Employment Status:</label>
                        <asp:DropDownList ID="EmpList" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Status" Value="" />
                            <asp:ListItem Text="Regular" Value="Regular" />
                            <asp:ListItem Text="Part-Time" Value="Part-Time" />
                            <asp:ListItem Text="Casual" Value="Casual" />
                            <asp:ListItem Text="Unemployed" Value="Unemployed" />
                        </asp:DropDownList>
                    </div>
                    <div class="res_data">
                        <label for="occupation_tb">Occupation:</label>
                        <asp:TextBox ID="occupation_tb" runat="server" CssClass="form-control" placeholder="Occupation"></asp:TextBox>
                    </div>
                    <div class="res_data">
                        <label for="income_tb">Income Level:</label>
                        <asp:TextBox ID="income_tb" runat="server" CssClass="form-control" placeholder="Income" OnTextChanged="income_tb_TextChanged"></asp:TextBox>
                    </div>                    
                    <div class="res_data" style="display: flex; justify-content: center; gap: 10px;">
                        <asp:Button ID="save_edited_btn" CssClass="saveres_btn" runat="server" Text="Save" OnClick="save_edited_btn_Click" />
                        <asp:Button ID="clear_edited_btn" CssClass="clear_btn" runat="server" Text="Clear" OnClick="clear_edited_btn_Click" />
                    </div>  
                </div>                
            </div>
        </div>
    </form>
</body>
</html>
