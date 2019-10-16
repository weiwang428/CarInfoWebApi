﻿using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;
using CarInfoWebApplication.Models;

namespace CarInfoWebApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var container = this.BuildUnityContainer();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }

        //IUnityContainer BuildUnityContainer()
        //{
        //    var container = new UnityContainer();
        //    container.RegisterType<ICarRepository, CarRepository>();
        //    return container;
        //}


    }
}
