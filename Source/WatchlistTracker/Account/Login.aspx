<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="WatchlistTracker.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.
    </p>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <div class="accountInfo" id ="loginForm">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <p>
                        <span>Username:</span>
                        <input data-bind="value: username" />
                    </p>
                    <p>
                        <span>Password:</span>
                        <input data-bind="value: password" type="password"/>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <button data-bind="click: onClick">Submit</button>
                </p>
            </div>
            
<script>
    var viewmodel = kendo.observable({
        username: '',
        password: '',
        onClick: function (e) {
            e.preventDefault();
            $.ajax("/api/user/login/",
            {
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ username: this.get('username'), password: this.get('password') }),
                success: function (data, a, b) {
                    if (data.Success) {
                        window.location = data.Redirect;
                    }
                    else {
                        jError("Login failure: " + data.Reason);
                    }
                },
                error: function (a, b, c) {
                    jError("An error occurred: " + a.responseText);
                    console.log(a);
                }
            });

        }
    });

    kendo.bind($("#loginForm"), viewmodel);
</script>            

</asp:Content>
