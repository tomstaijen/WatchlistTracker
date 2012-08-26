<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WatchlistTracker._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div class="k-content">

    <div id="hallo">
        <div id="watchlist" 
            style="max-height: 400;"
            data-role="list" 
		    data-template="movie_list_template"
            data-scrollable="true"
            data-bind="source: watchlist"></div>
<%--        <div id="releases" --%>
<%--            data-role="grid" --%>
<%--            style="max-height: 400;"--%>
<%--            data-columns='[{"field":"title"},{"field":"released","template":"#= kendo.toString(new Date(released*1000),\"dd MMMM yyyy\")#"},{"field":"quality"}]'--%>
<%--		    data-scrollable="true"--%>
<%--            data-bind="source: releases"></div>--%>
  </div>

  <script id="movie_list_template" type="template">
    <div class="movie">
            <img src="${images.poster}" alt="${title} image" />
            <h3>${title}</h3>
            <dl>
                <dt>Release Date:</dt>
                <dd>${kendo.toString(new Date(1345186800*1000), "dd MMMM yyyy")}</dd>
            </dl>
        </div>
  </script>

  <script type='text/javascript'>
    var apikey = "4d940e1f910c57cd7ba21537685cae69";
    var username = "tstaijen";
    var pwhash = "e38d785509fb6ee2ead53da872e39f5613b04e22";

    var viewModel = kendo.observable({
        watchlist: [],
        releases: [],
        qualities: ['CAM', 'TS', 'SCREENER', 'DVDSCR', 'DVDRIP', 'DVDR', 'HDRIP', 'BRRIP', 'HDTV', 'BluRay', 'PDTV']
    });

function updateReleases() {
    $.ajax("https://api.twitter.com/1/statuses/user_timeline.json?include_entities=true&include_rts=true&screen_name=releaselognet&count=250&page=2", {
        type: "get",
        dataType: "jsonp",
        success: function (data) {
            console.log(data);
            var result = new Array();
            for (var i = 0; i < data.length; i++) {
                result.push(releaseFromTwitter(data[i]));
            }
            viewModel.set("releases", result);
        }
    });
}

function releaseFromTwitter(item) {
    var release = {
        title: item.text.substring(0, item.text.indexOf(':')),
        released: new Date(item.created_at),
        quality: ''
    };
    release.quality = detectQuality(release.title);
    return release;
}

function detectQuality(title) {
    var q = viewModel.get("qualities");

    title = title.toLowerCase();

    for (var i = 0; i < q.length; i++) {
        if (title.indexOf(q[i].toLowerCase()) > -1) {
            return q[i];
        }
    }
    return 'Unknown';
}

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
    updateReleases();
    kendo.bind($("#hallo"), viewModel);
});  
</script>

    <style scoped>
        .movie
        {
        	float: left;
        	width: 800px;
        	height: 110px;
        	margin: 10px;
        	padding: 5px;
            box-shadow: inset 0 0 20px rgba(0,0,0,0.2);
            border-radius: 20px;

        }
        .movie img
        {
        	float: left;
        	width: 110px;
        	height: 110px;
        	-webkit-border-radius: 15px;
            -webkit-border-top-right-radius: 0;
            -moz-border-radius: 15px;
            -moz-border-radius-topright: 0;
            border-radius: 15px;
            border-top-right-radius: 0;

        }
        .movie h3
        {
        	margin: 0;
        	padding: 10px 10px 10px 15px;
        	color: #fff;
        	font-size: 1em;
        	float: left;
        	max-width: 600px;
        	text-transform: uppercase;
        	background-color: rgba(0,0,0,0.4);
        	-moz-box-shadow: inset 0 0 20px rgba(0,0,0,0.2);
            -webkit-box-shadow: inset 0 0 20px rgba(0,0,0,0.2);
            box-shadow: inner 0 0 20px rgba(0,0,0,0.2);
            -moz-border-radius-topright: 10px;
            -moz-border-radius-bottomright: 10px;
            -webkit-border-top-right-radius: 10px;
            -webkit-border-bottom-right-radius: 10px;
            border-top-right-radius: 10px;
            border-bottom-right-radius: 10px;
        }
        .movie dl
        {
            float: left;
            margin: 15px 0 0 0;
        }
        .movie dt, dd
        {
        	float: left;
        	margin: 0;
        	padding: 0;
        	width: 120px;
        }
        .movie dt
        {
        	clear: left;
        	width: 120px;
        	padding: 0 5px 0 15px;
        	text-align: right;
        	opacity: 0.6;
        }
        .k-listview:after, .movie dl:after
        {
        	content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }
        .k-listview
        {
        	border: 0;
        	padding: 0 0 20px 0;
        	min-width: 0;
        }
    </style>
</div>
</asp:Content>
