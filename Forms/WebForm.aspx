<%@ Page Language="C#" UnobtrusiveValidationMode="None" AutoEventWireup="true" Async="true" CodeBehind="WebForm.aspx.cs" Inherits="CloudComDevs.ShoppingCartDemo.Web.Forms.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>apyment Gateway | </title>
    <link type="text/css" href="../Content/style.css" />
</head>
<body>
    <div id="header">


        <a href="#" class="float">
            <img src="/images/logo.jpg" alt="" width="171" height="73" /></a>
    </div>
    <form id="form1" runat="server">
        <span class="payment-errors"></span>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ShowSummary="true" />

        <fieldset>
            <legend>Payment</legend>

            <table>
                <tr>
                    <td>Card Number
                    </td>

                    <td>

                        <asp:TextBox runat="server" ID="cardNumberTextBox" MaxLength="16" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Required card number" Text="*" ControlToValidate="cardNumberTextBox"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ControlToValidate="cardNumberTextBox" ValidationExpression="^\d{16,}$"
                             ErrorMessage="Invalid card number"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Name
                    </td>

                    <td>
                        <asp:TextBox runat="server" ID="nameTextBox" MaxLength="30" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="Required card holder name" Text="*" ControlToValidate="nameTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Expiary(mm/yyyy)
                    </td>

                    <td>
                        <asp:DropDownList runat="server" ID="monthDropDown">
                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                            <asp:ListItem Text="02" Value="02"></asp:ListItem>
                            <asp:ListItem Text="03" Value="03"></asp:ListItem>
                            <asp:ListItem Text="04" Value="04"></asp:ListItem>
                            <asp:ListItem Text="05" Value="05"></asp:ListItem>
                            <asp:ListItem Text="06" Value="06"></asp:ListItem>
                            <asp:ListItem Text="07" Value="07"></asp:ListItem>
                            <asp:ListItem Text="08" Value="08"></asp:ListItem>
                            <asp:ListItem Text="09" Value="09"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                        /

                    <asp:DropDownList runat="server" ID="yearDropdown">
                        <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                        <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                        <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                        <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                        <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>CSV Number
                    </td>

                    <td>
                        <asp:TextBox runat="server" ID="cvcTextBox" MaxLength="3" Width="50px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="Required CVC number" Text="*" ControlToValidate="cvcTextBox"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ControlToValidate="cvcTextBox" ValidationExpression="^\d{3,}$"
                             ErrorMessage="Invalid CSV Number"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>

                    <td>

                        <asp:Button runat="server" ID="submitButton" Text="Pay" CausesValidation="true" OnClick="submitButton_Click" />
                    </td>
                </tr>
            </table>

        </fieldset>
    </form>
</body>
</html>
