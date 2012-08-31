<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Profile.aspx.cs" Inherits="WatchlistTracker.Account.Profile" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
                    <h2>
                        Edit your Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                <asp:Label ID="UserName" runat="server" CssClass="textEntry"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
<%--                            <p>--%>
<%--                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>--%>
<%--                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>--%>
<%--                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" --%>
<%--                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." --%>
<%--                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>--%>
<%--                            </p>--%>
<%--                            <p>--%>
<%--                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>--%>
<%--                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>--%>
<%--                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" --%>
<%--                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" --%>
<%--                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>--%>
<%--                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" --%>
<%--                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."--%>
<%--                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>--%>
<%--                            </p>--%>
                             <p>
                                <asp:Label ID="TraktUsernameLabel" runat="server" AssociatedControlID="UserName">Trakt User Name:</asp:Label>
                                <asp:TextBox ID="TraktUsername" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TraktUsername" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="Trakt User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="TraktPasswordLabel" runat="server" AssociatedControlID="TraktPassword">Trakt Password:</asp:Label>
                                <asp:TextBox ID="TraktPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TraktPassword" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="Trakt Api Key is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="TraktApiKeyLabel" runat="server" AssociatedControlID="TraktApiKey">Trakt Api Key:</asp:Label>
                                <asp:TextBox ID="TraktApiKey" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TraktApiKey" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="Trakt Api Key is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>

                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" Text="Create User" 
                                ValidationGroup="RegisterUserValidationGroup" 
                                onclick="CreateUserButton_Click1"/>
                        </p>
                    </div>
</asp:Content>
