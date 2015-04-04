<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Master.aspx.cs" Inherits="Starbucks.Master" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="Master" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" Height="100px" ImageUrl="~/Images/Untitled-2.png" Width="1270px" ImageAlign="Top" />
      <div>
          </div>
     <table style="height: 40px; width: 7px">

            <tr>
                <td>
                     <asp:Menu ID="Menu1" runat="server" Font-Names="Aharoni" ForeColor="BLUE" Style="z-index:104 ; left: 50px;top:auto; position: absolute;
                         top: 55px" Width="70px" Height="20px" Font-Bold="True" Font-Size="Large" >
                         <Items>
                             <asp:MenuItem Text="HOME" Value="New Item" NavigateUrl="~/Home.aspx"></asp:MenuItem>
                         </Items>                     
                     </asp:Menu>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Menu ID="Menu5" runat="server" Font-Names="Aharoni" ForeColor="BLUE" Style="z-index:104 ; left: 130px;top:auto; position: absolute;
                         top: 55px" Width="70px" Height="20px" Font-Bold="True" Font-Size="Large" >
                         <Items>
                             <asp:MenuItem Text="INSERT" Value="New Item" NavigateUrl="~/Home.aspx"></asp:MenuItem>
                         </Items>                     
                     </asp:Menu>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Menu ID="Menu2" runat="server" Font-Names="Aharoni" ForeColor="BLUE" Style="z-index: 104; left: 230px;top:auto; position: absolute;
                         top: 55px" Width="70px" Height="20px" Font-Bold="True" Font-Size="Large"  >
                         <Items>
                             <asp:MenuItem Text="UPDATE" Value="Update" NavigateUrl="~/UpdateDetails.aspx" ></asp:MenuItem>
                         </Items>                     
                     </asp:Menu>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Menu ID="Menu3" runat="server" Font-Names="Aharoni" ForeColor="BLUE" Style="z-index: 104; left: 330px;top:auto; position: absolute;
                         top: 55px" Width="70px" Height="20px" Font-Bold="True" Font-Size="Large"  >
                         <Items>
                             <asp:MenuItem Text="SEARCH" Value="Search" NavigateUrl="~/SearchDetails.aspx"></asp:MenuItem>
                         </Items>                     
                     </asp:Menu>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Menu ID="Menu4" runat="server" Font-Names="Aharoni" ForeColor="BLUE" Style="z-index: 104; left: 430px;top:auto; position: absolute;
                         top: 55px" Width="70px" Height="20px" Font-Bold="True" Font-Size="Large"  >
                         <Items>
                             <asp:MenuItem Text="DELETE" Value="Sort" NavigateUrl="~/SortDetails.aspx"></asp:MenuItem>
                         </Items>                     
                     </asp:Menu>
                </td>
            </tr>
       </table>
    </div>
     <div style="margin-left:0%">
             &nbsp;&nbsp;
     </div>
    </form>
</body>
</html>
