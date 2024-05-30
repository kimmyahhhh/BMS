<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_blttr.aspx.cs" Inherits="blotter.admin_blttr" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="stylesheet" href="admin_blttr_css.css"/>
     <%-- <script type="text/javascript" src="Scripts.js"></script> --%>
    
</head>
<body>
    <form id="form1" runat="server">
       <div class="container">
        <div class="topbar">
            <div class="logo">
                <h2>Brand.</h2>
            </div>
          
            <div class="search">
                <asp:TextBox ID="TextBox1" runat="server" type="text" placeholder="Search" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
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
                        <h1 class="resinfo_lbl">Blotter</h1>                        
                        <%-- <asp:Button ID="btnBltrModal" Class="btnBltrModal" runat="server" Text="Add Report" OnClientClick="openModal(); return false;"/>--%>
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
            <%--<center><div class="charts2">
                <div class="chart">
            
                  </div>
                  </div>
                </center> --%>
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
         <%--<div id="AddModal" class="modal">
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
        </div>--%>

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

          <div class="button-container">
            <asp:Button ID="btnSetActive" runat="server" Text="Set Active" OnClick="btnSetActive_Click" OnClientClick="return confirmStatusChange();" />
            </div>
                 
                </div>
            </div>
        </div>  

                <%--blur effect --%>
                <div id="overlay"></div>
        </div>
    

     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>

        <%--Add report modal script --%>
       <%--  function openModal() {
            document.getElementById('AddModal').style.display = "block";
            document.getElementById('overlay').style.display = 'block';

        }

        function closeModal() {
            document.getElementById('AddModal').style.display = "none";
            document.getElementById('overlay').style.display = 'none';

        }

        function addUser() {
            var res = $('#<%= res_tb.ClientID %>').val();
            var compla = $('#<%= compla_tb.ClientID %>').val();
            var compli = $('#<%= compli_tb.ClientID %>').val();
            var date = $('#<%= txtDate.ClientID %>').val();
            var time = $('#<%= txtTime.ClientID %>').val();
            var susp = $('#<%= susp_tb.ClientID %>').val();

            if (res.trim() === '' || compla.trim() === '' || compli.trim() === '' || date.trim() === '' || time.trim() === '' || susp.trim() === '') {
                alert('Please fill the empty fields.');
                return;
            }

            

            var enteredDate = new Date(date + ' ' + time); 
            var currentDate = new Date();
           

            if (enteredDate > currentDate) {
                alert('The date cannot be in the future.');
                return;
            }

            if (enteredDate.toDateString() === currentDate.toDateString() && enteredDate > currentDate) {
                alert('The time cannot be in the future for the current date.');
                return;
        } 

          

            $.ajax({
                type: 'POST',
                url: 'blttr.aspx/AddUser',
                data: JSON.stringify({ res: res, compla: compla, compli: compli, date: date, time: time, susp: susp }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    alert('User added successfully.');
                    closeModal();

                    $('#<%= res_tb.ClientID %>').val();
                    $('#<%= compla_tb.ClientID %>').val();
                    $('#<%= compli_tb.ClientID %>').val();
                    $('#<%= txtDate.ClientID %>').val();
                    $('#<%= txtTime.ClientID %>').val();
                    $('#<%= susp_tb.ClientID %>').val();
                  
                    location.reload(); 
                },
                error: function (xhr, status, error) {
                    alert('An error occurred while adding user: ' + xhr.responseText);
                }
            }); --%>
        
         <%--View report modal script --%>

        function showModal2() {
            document.getElementById('ViewModal').style.display = "block";
            document.getElementById('overlay').style.display = 'block';
        }

        function closeModal2() {
            document.getElementById('ViewModal').style.display = "none";
            document.getElementById('overlay').style.display = 'none';
        }

         <%--verify update script --%>

        function confirmStatusChange() {
            return confirm("Are you sure you want to change the status?");
        }

        function showModal() {
            document.getElementById('AddModal').style.display = 'block';
            document.getElementById('overlay').style.display = 'block';
        }

        function closeModal() {
            document.getElementById('AddModal').style.display = 'none';
            document.getElementById('overlay').style.display = 'none';
        }


    </script>
    </form>
</body>
</html>
