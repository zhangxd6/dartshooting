using System;
using Owin;
using System.Web.Http;
using DartShooting.Web.App_Start;
using System.Diagnostics;
using Swashbuckle.Application;
using System.Collections.ObjectModel;
using System.Fabric.Description;

namespace DartShooting.Web
{
    public interface IOwinAppBuilder
    {
        void Configuration(IAppBuilder appBuilder);

    }

    public class StartUp : IOwinAppBuilder
    {
        private ConfigurationSettings configSettings;

        public StartUp(ConfigurationSettings configSettings)
        {
            this.configSettings = configSettings;
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            try
            {
                config
                    .EnableSwagger(c =>
                    {
                        c.RootUrl(req =>
                        {
                            return req.RequestUri.GetLeftPart(UriPartial.Authority) + "/dartshooting";
                        });
                        c.SingleApiVersion("v1", "A title for your API");
                    })
                    .EnableSwaggerUi(
                    
                    );
                config.EnableCors();
                config.MapHttpAttributeRoutes();
                RouteConfig.RegisterRoutes(config.Routes);

                appBuilder.UseWebApi(config);
            }
            catch (Exception e)
            {

                Trace.WriteLine(e);
            }
        }
    }
}