using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using UnitedWomenMVC.Models;

namespace UnitedWomenMVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services

            //Web API routes

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }
            );
            //ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Members>("Members");
            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
