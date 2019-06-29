using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TA.Models; //dodato zbog validacije brojeva i klase ValidacijaSifara gde je odradena custom validacija

namespace TA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            //dodato zbog validacije brojeva
            //DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(ValidacijaSifara), typeof(ValidacijaSifaraValidator));
        }
    }
}
