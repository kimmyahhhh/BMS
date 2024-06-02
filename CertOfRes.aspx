<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertOfRes.aspx.cs" Inherits="bms.CertOfRes" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Certificate of Residency</title>
    <link rel="stylesheet" href="CertOfRes1.css">
    <script type="text/javascript">
        window.onload = function() {
            window.print();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="certificate">
                <div class="text">
                    <img src="images/guimba.jpg" alt="guimba" class="logo" />
                    <img src="images/n2logo.jpg" alt="n2" class="huhu" />
                    <p>Republic of the Philippines</p>
                    <p>Province of Nueva Ecija</p>
                    <p>Municipality of Guimba</p>
                    <br />
                </div>
                <div class="content" style="border-bottom: solid;">
                    <h2>OFFICE OF THE BARANGAY CAPTAIN</h2>
                    <h3>BARANGAY NARVACAN II</h3>
                </div>
                <div class="lahh">
                    <h4>BARANGAY RESIDENCY</h4>
                    <br />
                </div>
                <div class="cont">
                    <p style="margin-left: 25px;">To whom it may concern:</p>
                    <p style="margin-left: 25px; text-indent: 100px;">This is to certify that <asp:Label ID="lblName" runat="server" style="text-decoration: underline;"></asp:Label>, <asp:Label ID="lblAge" runat="server" style="text-decoration: underline;"></asp:Label> years old is a resident of this barangay, particularly in Purok <asp:Label ID="lblZone" runat="server" style="text-decoration: underline;"></asp:Label>, Brgy. Narvacan II, Guimba, Nueva Ecija, since <asp:Label ID="lblBirthday" runat="server" style="text-decoration: underline;" Text='<%# Eval("dob", "{0:dd/MM/yyyy}") %>'></asp:Label> up to present <asp:Label ID="lblYear" runat="server" style="text-decoration: underline;"></asp:Label> with <asp:Label ID="lblAge2" runat="server" style="text-decoration: underline;"></asp:Label> years of residency.</p>
                    <p style="margin-left: 25px; text-indent: 100px;">This is issued upon the request for any legal purposes it may serve.</p>
                    <p style="margin-left: 25px; text-indent: 100px;">Issued this <asp:Label ID="lblDay" runat="server" style="text-decoration: underline;"></asp:Label> day of <asp:Label ID="lblMonth" runat="server" style="text-decoration: underline;"></asp:Label> <asp:Label ID="lblYear2" runat="server" style="text-decoration: underline;"></asp:Label>.</p>
                </div>
                <div class="footer" style="margin-right: 600px;">
                    <p>PREPARED BY:</p>
                    <p style="text-decoration: underline;"><b>KIMBERLY RAFAEL</b></p>
                    <p>Barangay Secretary</p>
                </div>
                <div class="foot" style="margin-left: 600px;">
                    <p>APPROVED BY:</p>
                    <p style="text-decoration:underline;"><b>VON MAMAID</b></p>
                    <p>Barangay Captain</p>
                </div>
                <br />
                <div class="note" style="margin-right: 30px;">
                    <p style="font-size: 14px;">Not Valid Without Dry Seal</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
