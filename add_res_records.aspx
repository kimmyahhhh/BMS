﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_res_records.aspx.cs" Inherits="bms.add_res_records" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Resident Records</title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="add1_res_record.css"/>
    <script>
         function clearLabelAfterDelay(mbox, delay) {
            setTimeout(function() {
                document.getElementById(mbox).innerText = '';
            }, delay);
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
                <i class="fas fa-bell"></i>
                <div class="user">
                    <img src="images/n2logo.jpg" alt="">
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
                    <a href="#">
                      <i class="fas fa-layer-group"></i>
                        <div>Blotters</div>
                    </a>
                </li>
                <li>
                    <a href="#">
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
            <div class="charts">
                <div class="chart">
                   <div class="f_info">
                     <h1 class="addresh1">Add Resident</h1>
                       <div class="res_data">
                                <label>First Name: </label>
                                <asp:TextBox ID="fname_tb" CssClass="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Middle Name: </label>
                                <asp:TextBox ID="mname_tb" CssClass="form-control" placeholder="Middle Name" runat="server"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Last Name: </label>
                                <asp:TextBox ID="lname_tb" CssClass="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Birthday: </label>
                                <asp:TextBox ID="dob_tb" CssClass="form-control" type="date" placeholder="YYYY-MM-DD" AutoPostBack="true" runat="server" OnTextChanged="dob_tb_TextChanged"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Age: </label>
                                <asp:TextBox ID="age_tb" CssClass="form-control" placeholder="Age" runat="server"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Sex</label>
                                <asp:DropDownList ID="sexlist" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">Select Sex</asp:ListItem>
                                    <asp:ListItem Text="M" />
                                    <asp:ListItem Text="F" />
                                </asp:DropDownList>
                       </div>
                   </div>                   
                </div>
                <div class="chart2">
                    <div class="f_info1">
                       <div class="res_data">
                                <label>Contact No.:</label>
                                <asp:TextBox ID="contact_tb" CssClass="form-control" placeholder="Contact No." runat="server" OnTextChanged="contact_tb_TextChanged"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Place of Birth:</label>
                                <asp:TextBox ID="pob_db" CssClass="form-control" placeholder="Place of Birth" runat="server"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Religion:</label>
                                <asp:TextBox ID="religion_tb" CssClass="form-control" placeholder="Religion" runat="server"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Civil Status:</label>
                                <asp:DropDownList ID="CSList" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">Select Status</asp:ListItem>
                                    <asp:ListItem Text="Single" />
                                    <asp:ListItem Text="Married" />
                                    <asp:ListItem Text="Separated" />
                                    <asp:ListItem Text="Annuled" />
                                    <asp:ListItem Text="Widow/er" />
                                </asp:DropDownList>
                       </div> 
                       <div class="res_data">
                                <label>Educational Attainment:</label>
                                <asp:DropDownList ID="EAList" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">Select Attainment</asp:ListItem>
                                    <asp:ListItem Text="Elementary School" />
                                    <asp:ListItem Text="High School (Junior)" />
                                    <asp:ListItem Text="High School (Senior)" />
                                    <asp:ListItem Text="College" />
                                    <asp:ListItem Text="College Graduate" />
                                    <asp:ListItem Text="Illiterate" />
                                </asp:DropDownList>
                       </div>
                   </div>
                </div>
                <div class="chart3">
                    <div class="f_info2">                       
                       <div class="res_data">
                                <label>Employment:</label>
                                <asp:DropDownList ID="employment" CssClass="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="employment_SelectedIndexChanged">
                                    <asp:ListItem Value="">Select Employment</asp:ListItem>
                                    <asp:ListItem Text="Employed" />
                                    <asp:ListItem Text="Self-Employed" />
                                    <asp:ListItem Text="Underemployed" />
                                    <asp:ListItem Text="Unemployed" />
                                </asp:DropDownList>
                       </div>
                       <div class="res_data">
                                <label>Employment Status:</label>
                                <asp:DropDownList ID="EmpList" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="">Select Status</asp:ListItem>
                                    <asp:ListItem Text="Regular" />
                                    <asp:ListItem Text="Part-Time" />
                                    <asp:ListItem Text="Casual" />
                                    <asp:ListItem Text="Unemployed" />
                                </asp:DropDownList>
                       </div>
                       <div class="res_data">
                                <label>Occupation:</label>
                                <asp:TextBox ID="occupation_tb" CssClass="form-control" placeholder="Occupation" runat="server"></asp:TextBox>
                       </div>
                       <div class="res_data">
                                <label>Income Level:</label>
                                <asp:TextBox ID="income_tb" CssClass="form-control" placeholder="Income" runat="server" OnTextChanged="income_tb_TextChanged"></asp:TextBox>
                       </div>                       
                   </div>
                </div>
                <div class="chart4">
                    <div class="f_info3">                       
                       
                       <div class="res_data" style="display: flex; flex-direction: column; margin-right: 30px;">
                                <label>Household:</label>
                            <div style="display: flex; align-items: center;">
                                <asp:DropDownList ID="HHList" CssClass="form-control" runat="server" style="margin-right: 10px;">
                                    <asp:ListItem Value="">Select Head</asp:ListItem>                                    
                                </asp:DropDownList>
                            </div>            
                       </div>                                            
                   </div>
                </div>
            </div>
            <div class="res_data" style="display: flex; justify-content: center; gap: 10px;">
                 <asp:Button ID="saveres_btn" CssClass="saveres_btn" runat="server" Text="Add" OnClick="saveres_btn_Click" />
                 <asp:Button ID="clear_btn" CssClass="clear_btn" runat="server" Text="Clear" /> 
                 <br />
                 <asp:Label ID="mbox" runat="server"></asp:Label>
            </div>  
        </div>
      </div>
    </form>
</body>    
</html>
