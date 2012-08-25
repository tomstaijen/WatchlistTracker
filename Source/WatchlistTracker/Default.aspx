<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WatchlistTracker._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <div id="hallo">
        <div id="grid" data-role="grid" 
		data-columns='[{"field":"title"},{"field":"released","template":"#= kendo.toString(new Date(released*1000),\"dd MMMM yyyy\")#"}]'
        data-bind="source: watchlist">
  </div>
  <script type='text/javascript'>
    var apikey = "4d940e1f910c57cd7ba21537685cae69";
    var username = "tstaijen";
    var pwhash = "e38d785509fb6ee2ead53da872e39f5613b04e22";

    var viewModel = kendo.observable({
        watchlist: []
    });
  function updateWatchlist() {
    var data = {
        username: username,
        password: pwhash
    }
    $.ajax("http://api.trakt.tv/user/watchlist/movies.json/" + apikey + "/" + username, {
        type: "post",
        dataType: "jsonp",
        //data: JSON.stringify(data),
        success: function(data) {
            console.log(data);
			viewModel.set("watchlist", data);
        },
        error: function(a,b,c) {
			console.log(a);
         }
    });
}

$(function () {
    updateWatchlist();
    kendo.bind($("#hallo"), viewModel);
});  
</script>
</asp:Content>
