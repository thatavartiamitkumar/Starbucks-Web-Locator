<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SortDetails.aspx.cs" Inherits="Starbucks.SortDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  <div>
       <img src="Images/dunkin-donuts.png" style="width:100%;height: 90px;"/>
   </div>
        <div style="margin-left:50%">
             <img src="Images/Starbucks-store.png" />
        </div>
        <asp:Menu ID="Menu2" runat="server"
        Font-Names="Bookman Old Style" ForeColor="Red" Style="z-index: 104; left: 30px; position: absolute;
        top: 200px" Width="184px" Height="256px">
        <Items>
         <asp:MenuItem Text="Welcome" Value="New Item" NavigateUrl="~/Home.aspx">
            </asp:MenuItem>
            <asp:MenuItem Text="Insert a Location" Value="Insert" NavigateUrl="~/CompanyDetails.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Update a Location" Value="Update" NavigateUrl="~/UpdateDetails.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Search and Delete a Location" Value="Search" NavigateUrl="~/SearchDetails.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Search and Sort a Location" Value="Sort" NavigateUrl="~/SortDetails.aspx"></asp:MenuItem>
        </Items>
    </asp:Menu>
        <asp:Label ID="SearchLabel" runat="server" Font-Names="BookMan Old Style" Font-Size="24pt" ForeColor="Red"
        Style="z-index: 101; left: 250px; position: absolute; top: 100px" Text="Sort based on the Required Fields "></asp:Label>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
    <table style="z-index: 102; left: 280px; position: absolute; top: 150px">
   <tr>
       <td class="auto-style1">
        <asp:Label ID="CompanyLabel" runat="server" style="text-align:center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Company"></asp:Label>
       </td>
       <td>
          <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" Width="157px" Height="30px">
              <asp:ListItem>...Select...</asp:ListItem>
              <asp:ListItem>Starbucks</asp:ListItem>
              <asp:ListItem>DunkinDonuts</asp:ListItem>
          </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCompany" ErrorMessage="Choose one Company" ForeColor="Red" Visible="False"></asp:RequiredFieldValidator>
       </td>
   </tr>
   <tr>
       <td class="auto-style1">
          <asp:Label ID="StreetLabel" runat="server" style="text-align:center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Street"></asp:Label>
       </td>
       <td>
           <asp:TextBox ID="StreetTextBox" runat="server" Height="30px" Width="150px"> </asp:TextBox>
       </td>
   </tr>
   <tr>
         <td class="auto-style1">
             <asp:Label ID="CityLabel" runat="server" style="text-align:center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="City"></asp:Label>
         </td>
         <td>
             <asp:TextBox ID="CityTextBox" runat="server" Height="25px" Width="150px"></asp:TextBox>
         </td>
   </tr>
   <tr>
         <td class="auto-style1">
             <asp:Label ID="StateLabel" runat="server" style="text-align:center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="State"></asp:Label>
         </td>
         <td>
             <asp:TextBox ID="StateTextBox" runat="server" Height="25px" Width="150px"></asp:TextBox>
         </td>
   </tr>
   <tr>
         <td class="auto-style1">
             <asp:Label ID="countryLabel" runat="server" style="text-align:center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Country"></asp:Label>
         </td>
         <td>
             <asp:TextBox ID="countryTextBox" runat="server" Height="25px" Width="150px"></asp:TextBox>
         </td>
   </tr>
   <tr>
         <td class="auto-style1">
             <asp:Label ID="zipLabel" runat="server" style="text-align:center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="zipcode"></asp:Label>
         </td>
         <td>
             <asp:TextBox ID="zipTextBox" runat="server" Height="25px" Width="150px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="zipTextBox" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
         </td>
   </tr>
        <tr>
            <td>

                <asp:Label ID="SortLabel" runat="server" ForeColor="Black" Text="Sort By"></asp:Label>

            </td>
            <td>

                <asp:DropDownList ID="ddlSort" runat="server" Height="30px"  Width="157px">
                    <asp:ListItem>Select Column</asp:ListItem>
                    <asp:ListItem>company</asp:ListItem>
                    <asp:ListItem>street</asp:ListItem>
                    <asp:ListItem>city</asp:ListItem>
                    <asp:ListItem>state</asp:ListItem>
                    <asp:ListItem>country</asp:ListItem>
                    <asp:ListItem>zipcode</asp:ListItem>
                    <asp:ListItem>phone</asp:ListItem>
                    <asp:ListItem>longitutde</asp:ListItem>
                    <asp:ListItem>latitude</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>

                <asp:Label ID="OrderLabel" runat="server" ForeColor="Black" Text="Order By"></asp:Label>

            </td>
            <td>

                <asp:DropDownList ID="ddlOrder" runat="server" Height="30px" Width="157px">
                    <asp:ListItem>Order</asp:ListItem>
                    <asp:ListItem>Ascending</asp:ListItem>
                    <asp:ListItem>Descending</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="UpdateButton" runat="server" Text="Search" OnClick="Sort_Click" Width="100px" />
                </td>
            <td >
                <asp:Button ID="Clear" runat="server" OnClick="Clear_Click"  Text="Clear" Width="100px" /></td>
        </tr>
    </table>
        
        <asp:GridView ID="GridViewSort" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" style="z-index: 104; left: 30px; position: absolute; top: 550px"  AutoGenerateColumns="False" >
            <Columns>
                 <asp:BoundField DataField="Company" HeaderText="Company" />
                 <asp:BoundField DataField="Street" HeaderText="Street" />
                 <asp:BoundField DataField="City" HeaderText="City" />
                 <asp:BoundField DataField="State" HeaderText="State" />
                 <asp:BoundField DataField="zipcode" HeaderText="Zipcode"  />
                 <asp:BoundField DataField="phone" HeaderText="Phone" />
                 <asp:BoundField DataField="longitude" HeaderText="Longitude" />
                 <asp:BoundField DataField="latitude" HeaderText="Latitude" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
        
        
        </form>
    </body>
    </html>

