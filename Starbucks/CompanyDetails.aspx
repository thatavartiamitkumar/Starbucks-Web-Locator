<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyDetails.aspx.cs" Inherits="Starbucks.CompanyDetails" MasterPageFile="MasterPage.Master" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>



<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  
      <asp:Label ID="InsertLable" runat="server" Font-Names="BookMan Old Style" Font-Size="24pt" ForeColor="BLUE"
        Style="z-index: 101; left: 45px; position: absolute; top: 100px" Text="Insert a location "></asp:Label>

    <asp:Label  CssClass="MessageStyle" ID="InsertlblMessage" runat="server" Font-Bold="True" Font-Names="BookMan Old Style" Font-Size="20pt" ForeColor="Green"></asp:Label>
     <asp:Label  CssClass="MessageStyle" ID="ErrorMessageID" runat="server" Font-Bold="True" Font-Names="BookMan Old Style" Font-Size="20pt" ForeColor="Red"></asp:Label>
    <asp:ValidationSummary CssClass="MessageStyle"  ID="validation" runat="server" DisplayMode="BulletList" ForeColor="Red"/>
    
    <table style="z-index: 102; left: 45px; color: white; font-family: 'Lucida Console'; position: relative; top: 30px" id="TABLE1">
        <tr>
            <td class="auto-style1">
                <asp:Label ID="StreetLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Street"></asp:Label>
                <asp:Label runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td class="TextBoxStyle">
                <asp:TextBox ID="StreetTextBox" runat="server" > </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="StreetTextBox" ForeColor="Red" ErrorMessage="Please Enter Street" Display="None" ></asp:RequiredFieldValidator>
            </td>
          
            <td class="auto-style1">
                <asp:Label ID="CityLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="City"></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td class="TextBoxStyle">
                <asp:TextBox ID="CityTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="CityTextBox" ForeColor="Red" ErrorMessage="Please Enter City" Display="None" ></asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td class="auto-style1">
                <asp:Label ID="StateLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="State"></asp:Label>
                 <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td class="TextBoxStyle">
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
                 <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td class="TextBoxStyle">
                <asp:TextBox ID="CountryTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="CountryTextBox" ForeColor="Red" ErrorMessage="Please Enter Country" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>

            <td class="auto-style1">
                <asp:Label ID="zipLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="zipcode"></asp:Label>
                 <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td class="TextBoxStyle">
                <asp:TextBox ID="zipTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="zipTextBox"  ForeColor="Red" ErrorMessage="Please Enter Zipcode" Display="None"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style1">
                <asp:Label ID="PhoneLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Phone"></asp:Label>
            </td>
            <td class="TextBoxStyle">
                <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox>

            </td>
        </tr>

        <%--<tr>

            <td class="auto-style1">
                <asp:Label ID="LongLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Longitude"></asp:Label>
            </td>
            <td class="TextBoxStyle">
                <asp:TextBox ID="LongTextBox" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style1">
                <asp:Label ID="LatLabel" runat="server" Style="text-align: center" Font-Names="Arial" Font-Size="Medium" ForeColor="Black" Text="Latitude"></asp:Label>
            </td>
            <td class="TextBoxStyle">
                <asp:TextBox ID="LatTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>--%>

        <tr>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
            <td></td>

        </tr>
        <tr>
            <td></td>
            <td></td>
            <td class="auto-style1"></td>
            <td></td>

        </tr>
        <tr>

            <td class="auto-style1">
                <asp:Button ID="SaveButton" runat="server" Text="Save" Width="100px" OnClick="Save_Click" />
               
            </td>
            <td>
                <asp:Button ID="ClearButton" runat="server" Text="Cancel" Width="100px" OnClick="Clear_Click" /></td>
        </tr>





    </table>

    <div>
        <cc1:GMap ID="GMap1" runat="server" Visible="false" OnClick="GMap1_Click" />

    </div>

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
            top:10px;
            position:relative;
            z-index:101;
        }
    </style>
    
</asp:Content>

