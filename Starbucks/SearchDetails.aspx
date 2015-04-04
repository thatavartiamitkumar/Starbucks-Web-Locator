<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchDetails.aspx.cs" Inherits="Starbucks.SearchDetails" MasterPageFile="MasterPage.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:Label ID="InsertLable" runat="server" Font-Names="BookMan Old Style" Font-Size="24pt" ForeColor="BLUE"
        Style="z-index: 101; left: 45px; position: absolute; top: 100px" Text="Search for a location "></asp:Label>
    <asp:Label  CssClass="MessageStyle" ID="DeletelblMessage" runat="server"  Font-Bold="True" Font-Names="BookMan Old Style" Font-Size="24pt" ForeColor="Green"></asp:Label>
    <asp:Label  CssClass="MessageStyle" ID="ErrorMessageID" runat="server" Font-Bold="True" Font-Names="BookMan Old Style" Font-Size="20pt" ForeColor="Red"></asp:Label>
    

    <table style="z-index: 102; left: 45px; position: relative;top:60px">

        <tr>
            <td class="auto-style1">
                <asp:Label ID="StreetLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Street"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="StreetTextBox" runat="server"> </asp:TextBox>
            </td>
            <td class="auto-style1">
                <asp:Label ID="CityLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="City"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CityTextBox" runat="server" ></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="auto-style1">
                <asp:Label ID="StateLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="State"></asp:Label>
            </td>
            <td>
     <asp:DropDownList ID="ddlState" runat="server" Style="margin-left: 0px" Width="150px">
                <asp:ListItem Selected="True">Select..</asp:ListItem>
                <asp:ListItem Value="AL">Alabama</asp:ListItem>
                <asp:ListItem Value="AK	">Alaska	</asp:ListItem>
                <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                <asp:ListItem Value="CA">California</asp:ListItem>
                <asp:ListItem Value="CO">Colorado</asp:ListItem>
                <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                <asp:ListItem Value="DE">Delaware</asp:ListItem>
                <asp:ListItem Value="FL">Florida</asp:ListItem>
                <asp:ListItem Value="GA">Georgia</asp:ListItem>
                <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                <asp:ListItem Value="ID">Idaho</asp:ListItem>
                <asp:ListItem Value="IL">Illinois</asp:ListItem>
                <asp:ListItem Value="IN">Indiana</asp:ListItem>
                <asp:ListItem Value="IA">Iowa</asp:ListItem>
                <asp:ListItem Value="KS">Kansas</asp:ListItem>
                <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                <asp:ListItem Value="ME">Maine</asp:ListItem>
                <asp:ListItem Value="MD">Maryland</asp:ListItem>
                <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                <asp:ListItem Value="MI">Michigan</asp:ListItem>
                <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                <asp:ListItem Value="MO">Missouri</asp:ListItem>
                <asp:ListItem Value="MT">Montana</asp:ListItem>
                <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                <asp:ListItem Value="NV">Nevada</asp:ListItem>
                <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                <asp:ListItem Value="NY">New York</asp:ListItem>
                <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                <asp:ListItem Value="OH">Ohio</asp:ListItem>
                <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                <asp:ListItem Value="OR">Oregon</asp:ListItem>
                <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                <asp:ListItem Value="TX">Texas</asp:ListItem>
                <asp:ListItem Value="UT">Utah</asp:ListItem>
                <asp:ListItem Value="VT">Vermont</asp:ListItem>
                <asp:ListItem Value="VA">Virginia</asp:ListItem>
                <asp:ListItem Value="WA">Washington</asp:ListItem>
                <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                <asp:ListItem Value="DC">Washington, D.C.</asp:ListItem>
            </asp:DropDownList>
    <br />
    <br />
            </td>
            <td class="auto-style1">
                <asp:Label ID="countryLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Country"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="countryTextBox" runat="server" ></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="auto-style1">
                <asp:Label ID="zipLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="zipcode"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="zipTextBox" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="Search_Click" Width="100px" />
            </td>
            <td>
                <asp:Button ID="Clear" runat="server" OnClick="Clear_Click" Text="Clear" Width="100px" /></td>
        </tr>
    </table>

    <asp:GridView ID="GridViewSearch" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Style="z-index: 104; left: 45px; position: relative; top: 90px" AllowSorting="True" OnSorting="GridViewSearch_Sorting">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxDelete" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
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
    <asp:Button ID="DeleteButton" runat="server" Text="Delete" EnableTheming="True" OnClick="DeleteButton_Click" Style="z-index:104; top:100px;position:relative;left:45px" Width="100px"></asp:Button>


    







</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            width: 120px;
        }
    </style>

    <style type="text/css">
        .TextBoxStyle {
            height: 25px;
            width: 150px;
        }
    </style>

    <style>
        .MessageStyle {
            left:45px;
            top:155px;
            position:absolute;
            z-index:101;
        }
    </style>
    
</asp:Content>