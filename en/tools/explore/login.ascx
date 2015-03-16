<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="SkyServer.en.tools.explore.WebUserControl1" %>
<div id="loginbox">
    <% if (loggedin) { %>
    <p>Logged in as:</p>
    <p class="username">dmedv</p>
        <asp:Button ID="Button1" runat="server" Text="Fake logout" OnClick="logUserOut" />
        <% } else { %>
    <p class="username">Not logged in</p>
    <asp:Button ID="loginbutton" runat="server" Text="Fake login" OnClick="logUserIn" />
        <%} %>

</div>