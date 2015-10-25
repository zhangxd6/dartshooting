using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services;
using Microsoft.Owin.Hosting;
using System.Fabric.Description;
using System.Diagnostics;

namespace DartShooting.Web
{
    internal class OwinCommunicationListener : ICommunicationListener
    {
        private IOwinAppBuilder startup;
        private string appRoot;
        private string listeningAddress;
        private IDisposable serverHandle;

        public OwinCommunicationListener(string appRoot, IOwinAppBuilder startup)
        {
            this.appRoot = appRoot;
            this.startup = startup;
        }

        public void Initialize(ServiceInitializationParameters serviceInitializationParameters)
        {
            Trace.WriteLine("Initialize");

            EndpointResourceDescription serviceEndpoint = serviceInitializationParameters.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            int port = serviceEndpoint.Port;
            string root = String.IsNullOrWhiteSpace(this.appRoot) ? string.Empty : this.appRoot.TrimEnd('/') + "/";
            this.listeningAddress =string.Format( "http://+:{0}/{1}",port,root);
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            Trace.WriteLine("Opening on " + this.listeningAddress);

            try
            {
                serverHandle = WebApp.Start(listeningAddress, appBuilder => startup.Configuration(appBuilder));
                string resultAddress = this.listeningAddress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);
                ServiceEventSource.Current.Message(string.Format("listening on {0}",resultAddress));
                return Task.FromResult(resultAddress);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);

                this.StopWebServer();
                throw;
            }
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            Trace.WriteLine("Close");
            this.StopWebServer();
            return Task.FromResult(true);
        }

        public void Abort()
        {
            Trace.WriteLine("Abort");
            this.StopWebServer();
        }

        private void StopWebServer()
        {
            if (this.serverHandle != null)
            {
                try
                {
                    this.serverHandle.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    // no-op
                }
            }
        }
    }
}