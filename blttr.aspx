<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="blttr.aspx.cs" Inherits="blotter.WebForm1" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="Style.css"/>
    <script src="Scripts.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
       <div class="container">
        <div class="topbar">
            <div class="logo">
                <h2>Brand.</h2>
            </div>
            <div class="search">
               
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
                        <h1 class="resinfo_lbl">Blotter</h1>                        
                        <asp:Button ID="btnBltrModal" Class="btnBltrModal"  runat="server" Text="Add Report" OnClientClick="showModal(); return false;" OnClick="btnShowModal_Click" />
                     </div>
                </div>
            </div>
            <center>
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <div class="number">1217</div>
                        <div class="card-name">Scheduled Cases</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-user-graduate"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">42</div>
                        <div class="card-name">Active Cases</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-chalkboard-teacher"></i>
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number">68</div>
                        <div class="card-name">Settled Cases</div>
                    </div>
                    <div class="icon-box">
                        <i class="fas fa-users"></i>
                    </div>
                </div>
                
            </div>
                </center>
            <center><div class="charts2">
                <div class="chart">
                    <div class="search">
                <asp:TextBox ID="TextBox1" runat="server" type="text" placeholder="Search"></asp:TextBox>
                <label for="search"><i class="fas fa-search"></i></label>
            </div>

                    </div>
                </div></center>


            <center>
            <div class="charts">
                <div class="chart">
                    <center>
                    
          <asp:GridView ID="GridView1" runat="server" Font-Size="0.8em" AutoGenerateColumns="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowPaging="true"
                OnPageIndexChanging="OnPageIndexChanging" PageSize="2">
    <Columns>
               
               
                <asp:BoundField DataField="complainant" HeaderText="complainant" />
                <asp:BoundField DataField="compliance" HeaderText="complaince" />
                <asp:BoundField DataField="Suspect" HeaderText="Suspect" />
                <asp:BoundField DataField="started" HeaderText="started" />
                <asp:BoundField DataField="ended" HeaderText="ended" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                 <asp:Templatefield HeaderText="Action" >
                                <ItemTemplate >
                                    <asp:Button ID="btnBltrModal2"  Class="btnBltrModal2" runat="server" Text="Active" />
                                    
                                </ItemTemplate>
                                    </asp:Templatefield>

        <asp:Templatefield HeaderText="View Details" >
                                <ItemTemplate >
                                    <asp:Button ID="btnBltrModal3"  Class="btnBltrModal3" runat="server" Text="View" />
                                    
                                </ItemTemplate>
                                    </asp:Templatefield>

                

       
    </Columns>
</asp:GridView>
                        </center>
                        
                   
                </div>
                
                </div>
                </center>
           
            </div>
       
        <div id="modal" class="modal">
              <div class="modh"><span class="close" onclick="closeModal();">&times;</span>
                <h2>Blotter Report</h2></div>
            <div class="modal-content">
              
                <div class="form-container">
                    <div class="form-group">

            <label for="res_tb">Resident ID:</label>
                <asp:TextBox ID="res_tb" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="compla_tb">Complainant:</label>
                <asp:TextBox ID="compla_tb" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="compli_tb">Compliance:</label>
                <asp:TextBox ID="compli_tb" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="susp_tb">Suspect:</label>
                <asp:TextBox ID="susp_tb" runat="server" ></asp:TextBox>
            </div>
           
                    <br />
                    <br />

            <div class="button-container">
                <asp:Button ID="submitButton" CssClass="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
            </div>
                </div>
            </div>
        </div>
       
        <div id="overlay"></div>
 
     </form>
   
   
</body>
</html>
