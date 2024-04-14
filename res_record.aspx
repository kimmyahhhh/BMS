<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="res_record.aspx.cs" Inherits="bms.res_record" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="res_record.css"/>
    
</head>
<body>
    <form id="form1" runat="server">
       <div class="container">
        <div class="topbar">
            <div class="logo">
                <h2>Brand.</h2>
            </div>
            <div class="search">
                <asp:TextBox ID="search" runat="server" type="text" placeholder="Search"></asp:TextBox>
                <label for="search"><i class="fas fa-search"></i></label>
            </div>
            <i class="fas fa-bell"></i>
            <div class="user">
                <img src="img/user.png" alt=""/>
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
                    <a href="#">
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
                        <div class="number">1217</div>
                        <div class="card-name">Students</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-user-graduate"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">42</div>
                        <div class="card-name">Teachers</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-chalkboard-teacher"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">68</div>
                        <div class="card-name">Employees</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-users"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">$4500</div>
                        <div class="card-name">Earnings</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-dollar-sign"></i>
                    </div>
                </div>
            </div>
            <div class="charts">
                <div class="chart">
                    
                        <table style="width:100%;">
                            <tr>
                                 <th>#</th>
                                 <th>Nom</th>
                                 <th>Prenom</th>
                                 <th>identifiant </th>
                                 <th>date de naissance </th>
                                 <th>salaire</th>
                            </tr>

                            <tr>
                                 <td>1</td>
                                 <td>Jean</td>
                                 <td>leBon</td>
                                 <td>1368</td>
                                 <td>18 Nov 1962</td>
                                 <td>5000$</td>
                            </tr>

                            <tr>
                                 <td>2</td>
                                 <td>Von</td>
                                 <td>leBron</td>
                                 <td>4587</td>
                                 <td>13 Nov 2003</td>
                                 <td>14$</td>
                            </tr> 
                        </table>
                        
                   
                </div>
                <div class="chart doughnut-chart">
                    <h2>Employees</h2>
                    <div>
                        <canvas id="doughnut"></canvas>
                    </div>
                </div>
            </div>
            </div>
       </div>
        
 
     </form>
   
    <script src="res_record.js"></script>
</body>
</html>