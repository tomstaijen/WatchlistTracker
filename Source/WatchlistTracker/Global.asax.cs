﻿using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.WebApi;
using Raven.Client;
using Raven.Client.Document;
using SignalR;
using WatchlistTracker.Connections;
using WatchlistTracker.Logic;

namespace WatchlistTracker
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapConnection<EchoConnection>("echo", "signal/echo/{*operation}");

            // WebAPI Routes
            var route = RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var configuration = GlobalConfiguration.Configuration;

            // Make JSon default formatting
            var xmlFormatter = configuration.Formatters.OfType<XmlMediaTypeFormatter>().First();
            configuration.Formatters.Remove(xmlFormatter);

            // Initialize autofac
            var container = Bootstrap();
            // autofac for ASP.Net WebForms
            _containerProvider = new ContainerProvider(container);
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        private IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            // RavenDB
            builder.Register(c =>
                                 {
                                     var docStore = new DocumentStore
                                                        {
                                                            ConnectionStringName = "CLOUDBIRD_CONNECTION_STRING"
                                                        };
                                     docStore.Initialize();
                                     return docStore;
                                 }).SingleInstance();

            builder.Register(c => c.Resolve<DocumentStore>().OpenSession()).As<IDocumentSession>().InstancePerLifetimeScope();
            
            // Logics
            builder.RegisterType<UserRepository>().InstancePerLifetimeScope();

            // Controllers
            builder.RegisterApiControllers(Assembly.GetCallingAssembly());

            return builder.Build();
        }
    }
}

