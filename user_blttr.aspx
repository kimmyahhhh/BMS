<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_blttr.aspx.cs" Inherits="blotter.user_blttr" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="user_blttr_css.css"/>
    
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
                    <a href="residentdashboard.aspx">
                        <i class="fas fa-th-large"></i>
                        <div>Dashboard</div>
                    </a>
                </li>
                <li>
                    <a href="res_record.aspx">
                        <i class="fas fa-user-friends"></i>
                        <div>Personal Info</div>
                    </a>
                </li>                
                <li>
                    <a href="#">
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
            </ul>
        </div>    
        <div class="main">
            <div class="hcards">
                <div class="hcards-content">
                    <div class="add-res">
                        <h1 class="resinfo_lbl">Blotter</h1>                        
                        <asp:Button ID="btnBltrModal" Class="btnBltrModal" runat="server" Text="Add Report" OnClientClick="openModal(); return false;"/>
                     </div>
                </div>
            </div>
            <center>
            <div class="cards">
                <div class="card">
                    <div class="card-content">
                        <div class="number" ><asp:Label ID="lblTotalCount" runat="server"></asp:Label></div>
                        <div class="card-name">Select All</div>
                    </div>
                    <div class="icon-box">
                        <asp:LinkButton ID="lnkPending" runat="server" OnClick="lnkAll_Click">
                     <i class="fas fa-users"></i>
                     </asp:LinkButton>
                      
                    </div>
                </div>

                <div class="card">
                    <div class="card-content">
                        <div class="number"><asp:Label ID="lblPendingCount" runat="server"></asp:Label></div>
                        <div class="card-name">Scheduled Cases</div>
                    </div>
                    <div class="icon-box">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkPending_Click">
                     <i class="fas fa-users"></i>
                     </asp:LinkButton>
                      
                </div>
            </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number"><asp:Label ID="lblActiveCount" runat="server"></asp:Label></div>
                        <div class="card-name">Active Cases</div>
                    </div>
                    <div class="icon-box">                       
                        <asp:LinkButton ID="lnkActive" runat="server" OnClick="lnkActive_Click">
                      <i class="fas fa-chalkboard-teacher" ></i>
                      </asp:LinkButton>
                       
                    </div>
                </div>
                <div class="card">
                    <div class="card-content">
                        <div class="number"><asp:Label ID="lblEndedCount" runat="server"></asp:Label></div>
                        <div class="card-name">Settled Cases</div>
                    </div>
                    <div class="icon-box">                        
                       <asp:LinkButton ID="lnkEnded" runat="server" OnClick="lnkEnded_Click">
                      <i class="fas fa-users" ></i>
                      </asp:LinkButton>
                        
                    </div>
                </div>                
            </div>
                </center>
            <center><div class="charts2">
                <div class="chart">
            <div class="search">
    <asp:TextBox ID="TextBox1" runat="server" type="text" placeholder="Search" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
    <label for="search"><i class="fas fa-search"></i></label>
                       
             </div>
                  </div>
              </div>
          </center>
      <center>
            <div class="charts">
                <div class="chart">                    
          <asp:GridView ID="GridView1" CssClass="tb" runat="server" Font-Size="0.8em" AutoGenerateColumns="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowPaging="true"
                OnPageIndexChanging="OnPageIndexChanging" PageSize="10" OnRowCommand="GridView1_RowCommand">
                <Columns>
                <asp:BoundField DataField="complainant" HeaderText="Complainant" />
                <asp:BoundField DataField="compliance" HeaderText="Complaince" />
                <asp:BoundField DataField="date_data" HeaderText="Date" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="time_data" HeaderText="Time" />
                <asp:BoundField DataField="Suspect" HeaderText="Suspect" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:Templatefield HeaderText="View Details" >
                <ItemTemplate>
                <asp:Button ID="btnBltrModal3"  Class="btnBltrModal3" runat="server" Text="View" CommandName="ViewDetails" CommandArgument='<%# Eval("blotter_id") %>'  />
                </ItemTemplate>
                </asp:Templatefield>  
                </Columns>
                </asp:GridView> 
            </div>
        </div>
     </center>
           
            </div>
       <%--Add report Modal --%>
        <div id="AddModal" class="modal">
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
            <div class="form-group">
                <label for="txtDate">Date:</label>
                 <asp:TextBox ID="txtDate" runat="server" TextMode="Date"></asp:TextBox>
            </div>
             <div class="form-group">
                <label for="txtTime">Time:</label>
            <asp:TextBox ID="txtTime" runat="server" TextMode="Time"></asp:TextBox>
            </div>

            
           
                    <br />
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

            <div class="button-container">
                 <asp:Button ID="submitButton" CssClass="submitButton"  Text="Submit" runat="server"  OnClientClick="addUser(); return false;"/>
            </div>
                </div>
            </div>
        </div>

           <%--View details modal Modal --%>

           <div id="ViewModal" class="modal">
              <div class="modh"><span class="close" onclick="closeModal2();">&times;</span>
                <h2>Blotter Report</h2></div>
            <div class="modal-content">              
            <div class="form-container">

              <div class="form-group">
            <label for="blott_tbv">Blotter ID:</label>
                <asp:TextBox ID="blott_tbv" runat="server" ></asp:TextBox>
            </div>
             <div class="form-group">
            <label for="res_tbv">Resident ID:</label>
                <asp:TextBox ID="res_tbv" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="compla_tbv">Complainant:</label>
                <asp:TextBox ID="compla_tbv" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="compli_tbv">Compliance:</label>
                <asp:TextBox ID="compli_tbv" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDatev">Date:</label>
                 <asp:TextBox ID="txtDatev" runat="server"></asp:TextBox>
            </div>
             <div class="form-group">
                <label for="txtTimev">Time:</label>
            <asp:TextBox ID="txtTimev" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="start_tbv">Case Started:</label>
            <asp:TextBox ID="start_tbv" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="end_tbv">Case Ended:</label>
            <asp:TextBox ID="end_tbv" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="susp_tbv">Suspect:</label>
                <asp:TextBox ID="susp_tbv" runat="server" ></asp:TextBox>               
            </div>
                <div class="form-group">
                <label for="status_tbv">Status:</label>                
                <asp:TextBox ID="status_tbv" runat="server" ReadOnly="true"></asp:TextBox><br />
            </div>
                 
                </div>
            </div>
        </div>  

                <%--blur effect --%>
                <div id="overlay"></div>
        </div>
    
    </form>
</body>
</html>

