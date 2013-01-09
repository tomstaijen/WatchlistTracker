<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WatchlistTracker._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

  <script type="text/javascript">
      $(function () {
          var connection = $.connection('/signal/echo');

          connection.received(function (data) {
              console.log(data);
              $('#messages').append('<li>' + data + '</li>');
          });

          connection.start().done(function () {
              $("#broadcast").click(function () {
                  data = {
                      text: $('#msg').val()
                  }
                  connection.send(data);
              });
          });

      });
  </script>

  <input type="text" id="msg" />
  <input type="button" id="broadcast" value="broadcast" />

  <ul id="messages">
  </ul>


<div class="k-content">

  <a class="k-button"><span id="reload" class="k-icon k-i-refresh"></span></a>
   <div id="hallo">
    <div id="collected-wrapper" style="width: 850px; float:left; ">
        <h2>Collected Movies</h2>
        <div id="collected" data-role="list" data-template="collected_item_template" data-bind="source: collected">
        </div>
    </div>
    <div id="potentials-wrapper" data-bind="visible: potentialsVisible" style="width: 850px; float: left;">        
        <h2>Potentials</h2>
        <div id="potentials" 
            data-role="list" 
		    data-template="collected_item_template"
            data-bind="source: potentials" style="width: 850px;">
        </div>
    </div>
        <div id="futures_wrapper" data-bind="visible: futuresVisible" style="width: 850px">        
        <h2>Not yet released</h2>
        <div id="futures" 
            data-role="list" 
		    data-template="collected_item_template"
            data-bind="source: futures">
        </div>
    </div>
  </div>
  <script id="collected_item_template" type="template">
        <div class="movie" style="float: left;">
            <img src="${images.poster}" alt="${title}" />
            # if( Releases ) { # 
                RELEASES
            # } #
            <a class="k-button" data-bind="click: Collected">Collected</a>
            <a class="k-button" data-bind="click: Seen">Seen</a>
        </div>
  </script>

  <script id="movie_list_template" type="template">
    <div class="movie">
            <img src="${images.poster}" alt="${title} Poster" />
            <h3>${title} (${year}) <a href="http://www.imdb.com/title/${imdb_id}" rel="external" class="k-button">IMDb</a></h3>
            <div><span class="k-button" data-bind="active: InCollection">Collection</span></div>
            <dl>
                <dt>Tagline</dt>
                <dd>#= NotNull(tagline) #</dd>
                <dt>Trakt Rating</dt>
                <dd>${ratings.percentage} (${ratings.votes} votes)</dd>
                <dt>Duration</dt>
                <dd>${runtime} minutes</dd>
                <dt>Release Date:</dt>
                <dd>#= kendo.toString(new Date(ReleaseDate), "dd MMMM yyyy") #</dd>
                <dt>Genres:</dt>
                 <dd>#= Join(genres) #</dd>
                <dt>Releases</dt>
                <dd>
                 </dd>
            </dl>
        </div>
  </script>
  <script type="template" id="releases_templates">
      # if( Releases ) { #
                    <div 
                        data-role="grid" 
                        data-bind="source: Releases"
                        data-columns='[ {"field":"Title"},
                                        {"field":"ReleaseName"},
                                        {"field":"Group"},
                                        {"title" : "Tags", "template": "\\#= PrintTags(Tags) \\#" },
                                        {"field":"Distance"} ]'>
                    </div>
                    # } #
  </script>

  <script type='text/javascript'>
      var traktUser = "<%= User== null?"":User.TraktUser.UserName %>";
      var traktPass = "<%= User== null?"":User.TraktUser.PasswordHash %>";
      
      kendo.data.binders.active = kendo.data.Binder.extend({
          refresh: function () {
              var value = this.bindings["active"].get();
              if (value) {
                  $(this.element).addClass("k-state-active");
              } else {
                  $(this.element).removeClass("k-state-active");
              }
          }
      });

      var viewModel = kendo.observable({
          watchlist: [],
          releases: [],

          collectedVisible : function () {
              return this.get("watchlist").length > 0;
          },
          potentialsVisible : function () {
              return this.get("watchlist").length > 0;
          },
          futuresVisible : function () {
              return this.get("watchlist").length > 0;
          },

          collected: function() {
              var watchlist = this.get("watchlist");
              var result = new Array();
              for (var i = 0; i < watchlist.length; i++) {
                  if (watchlist[i].InCollection)
                      result.push(watchlist[i]);
              }
              return result;
          },
          
          potentials : function() {
              var watchlist = this.get("watchlist");
              var result = new Array();
              var now = new Date();
              for (var i = 0; i < watchlist.length; i++) {
                  if (!watchlist[i].InCollection && watchlist[i].ReleaseDate < now)
                      result.push(watchlist[i]);
              }
              return result;

          },
           
          futures: function() {
              var watchlist = this.get("watchlist");
              var result = new Array();
              var now = new Date();
              for (var i = 0; i < watchlist.length; i++) {
                  console.log(watchlist[i].ReleaseDate);
                  if (!watchlist[i].InCollection && watchlist[i].ReleaseDate > now )
                      result.push(watchlist[i]);
              }
              return result;

          }

      });

      function NotNull(value) {
          if (value)
              return value;
          return '';
      }


      function Join(data) {
          if (!data)
              return '';
          return data.join(', ');
      }

      function updateWatchlist() {
          $("#reload").removeClass("k-i-refresh");
          $("#reload").addClass("k-loading");
          $.ajax("/api/releases/getwatchlist", {
              type: "get",
              dataType: "json",
              success: function (data) {
                  console.log(data);
                  viewModel.set("watchlist", data);
              },
              error: function (a, b, c) {
                  console.log(a);
              },
              complete: function () {
                  $("#reload").removeClass("k-loading");
                  $("#reload").addClass("k-i-refresh");
              }
        
          });
      }

      $("#reload").click(function (e) {
          e.preventDefault();
          updateWatchlist();
      });

      $(function () {
          kendo.bind($("#hallo"), viewModel);
          updateWatchlist();
      });
  </script>

    <style>
        .movie
        {
            width: 150px;
        }
        .movie img
        {
        	float: left;
        	width: 138px;
            height: 203px;
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
        	width: 500px;
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
