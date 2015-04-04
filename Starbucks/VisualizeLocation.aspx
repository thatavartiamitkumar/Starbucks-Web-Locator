<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisualizeLocation.aspx.cs" Inherits="Starbucks.VisualizeLocation" MasterPageFile="MasterPage.Master" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <asp:Label ID="VizLable" runat="server" Font-Names="BookMan Old Style" Font-Size="24pt" ForeColor="BLUE"
        Style="z-index: 101; left: 45px; position: absolute; top: 100px" Text="Visualize a location "></asp:Label>


    <asp:Label ID="CityLabel" runat="server" Text="Enter the City"  ></asp:Label>
    &nbsp;<asp:TextBox ID="CityTextBox" runat="server" Style="margin-left: 5px" Width="150px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="StateLabel" runat="server" Text="Enter State"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
    <asp:Label ID="ZipcodeLabel" runat="server" Text="Enter Zipcode"></asp:Label>
    &nbsp;<asp:TextBox ID="ZipTextBox" runat="server" Style="margin-left: 3px" Width="150px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Style="margin-left: 6px" Text="Submit" Width="94px" />
    &nbsp;&nbsp;&nbsp;
            <asp:Button ID="ClearButton" runat="server" OnClick="ClearButton_Click" Text="Clear" Width="103px" />
    <br />


    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            <asp:Label ID="LabelMap" runat="server" Font-Bold="False" Font-Names="BookMan Old Style" ForeColor="Red" Text=" "></asp:Label>
    <cc1:GMap ID="GMap1" runat="server" Height="1000px" Width="1000px"  />
</asp:Content>
