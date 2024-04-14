<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="bms.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="dashboard.css">
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
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <div class="number">5</div>
                        <div class="card-name">Resident</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-user-graduate"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">3</div>
                        <div class="card-name">Male</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-chalkboard-teacher"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">2</div>
                        <div class="card-name">Female</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-users"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">$4500</div>
                        <div class="card-name">Revenues</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-dollar-sign"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">2</div>
                        <div class="card-name">Blotter</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-dollar-sign"></i>
                    </div>
                </div>
            </div>
            <div class="charts">
                <div class="chart">
                    <h2>Earnings (past 12 months)</h2>
                    <div>
                        <canvas id="lineChart"></canvas>
                    </div>
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
</body>
</html>
