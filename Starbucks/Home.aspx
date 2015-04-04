<%@ Page Language="C#" CodeBehind="Home.aspx.cs" Inherits="Starbucks.Home" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> STARBUCKS AND DUNKIN DONUTS</title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div style="margin-left: 340px">
        <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Font-Size="XX-Large" ForeColor="#009900" Height="58px" OnTextChanged="TextBox1_TextChanged" style="text-align: center; margin-right: 0px;" Width="600px">WELCOME TO STARBUCKS LOCATOR</asp:TextBox>
           </div>
           <div>
        <asp:Menu ID="Menu2" runat="server" StaticSubMenuIndent="10px" Style="z-index: 104; left: 540px; position: absolute;top:200px" BackColor="White" DynamicHorizontalOffset="2" DynamicVerticalOffset="2" Font-Names="Verdana" Font-Size="Medium" ForeColor="#284E98">
            <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
            <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
            <StaticMenuItemStyle VerticalPadding="20px" HorizontalPadding="5px"/>
                           
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="20px" />
            <DynamicMenuStyle BackColor="White" />
            <DynamicSelectedStyle BackColor="White" />
                        
            <Items>
                <asp:MenuItem Text="Insert a Location" Value="Insert" NavigateUrl="~/CompanyDetails.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Update a Location" Value="Update" NavigateUrl="~/UpdateDetails.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Search and Delete a Location" Value="Search" NavigateUrl="~/SearchDetails.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Visualize" Value="Sort" NavigateUrl="VisualizeLocation.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Check a Location" Value="Sort" NavigateUrl="Decision.aspx"></asp:MenuItem>
            </Items>
            <StaticSelectedStyle BackColor="#507CD1" />
        </asp:Menu>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Starbucks-reusable-cups2-e1363669717430-2.jpg" style="width:1200px; height:500px" BorderStyle="None"/>
   </div>
    </form>
</body>
</html>
