
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind=“blttr.aspx.cs" Inherits=“bms.blotter” %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="blttr.css">
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
                <img src="narvacan2.png" alt="cha">
            </div>
        </div>
        <div class="sidebar" >
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
                    <a href="cblttr.html">
                      <i class="fas fa-layer-group"></i>
                        <div>Blotters</div>
                    </a>
                </li>
                       <a href="userIOC.html">
                   <i class="fa-regular fa-file"></i>
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
                        <div class="number">0</div>
                        <div class="card-name">Active Cases</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas   fa-list-ul"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">42</div>
                        <div class="card-name">Settled Cases</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-list-check"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">18</div>
                        <div class="card-name">Scheduled Cases</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-calendar-days"""></i>
                    </div>
                </div>
                
            
                        <canvas id="lineChart"></canvas>
                    </div>
                           
            
        <div class="charts">
                <div class="chart">
                      <h2>Records</h2>
                      </div>
                      <canvas id="lineChart"></canvas>
                    </div>
              
</div>
    </div>
    </form>
</body>
</html>