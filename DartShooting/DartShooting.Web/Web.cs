using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Services;
using System.Fabric.Description;
using System.Collections.ObjectModel;

namespace DartShooting.Web
{
    public class Web : StatelessService
    {
        protected override ICommunicationListener CreateCommunicationListener()
        {
            ConfigurationSettings configSettings =
       this.ServiceInitializationParameters.CodePackageActivationContext.GetConfigurationPackageObject("Config").Settings;
            KeyedCollection<string, ConfigurationProperty> parameters = configSettings.Sections["WebServiceConfig"].Parameters;
            string appRoot = "dartShooting";
            if (parameters.Contains("appRoot"))
            {
                appRoot = parameters["appRoot"].Value;
            }

            // TODO: Replace this with an ICommunicationListener implementation if your service needs to handle user requests.
            return new OwinCommunicationListener(appRoot, new StartUp(configSettings)); ;
        }
    }
}
