<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tests.aspx.cs" Inherits="SitefinityWebApp.Tests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="TestCreateButton" OnClick="TestCreateButton_Click" Text="Test Create" />
        </div>
        <div>
            <asp:Button runat="server" ID="TestGetButton" OnClick="TestGetButton_Click" Text="Test Get" />
        </div>
        <div>
            <asp:Button runat="server" ID="TestUpdate" OnClick="TestUpdate_Click" Text="Test Update"  />
        </div>
    </form>
</body>
</html>
