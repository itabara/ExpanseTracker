using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentityServer3.Core.Configuration;
using System.IO;

[assembly: OwinStartup(typeof(ExpenseTracker.IdSrv.Startup))]

namespace ExpenseTracker.IdSrv
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    IssuerUri = ExpenseTrackerConstants.IdSrvIssuerUri,

                    Factory = new IdentityServerServiceFactory().UseInMemoryUsers(Config.Users.Get())
                    .UseInMemoryClients(Config.Clients.Get()).UseInMemoryScopes(Config.Scopes.Get()),

                    SigningCertificate = LoadCertificate()
                });
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(String.Format(@"{0}\bin\..\Certificates\{1}",
                AppDomain.CurrentDomain.BaseDirectory,
                ExpenseTrackerConstants.SslCertificateFile), this.GetPFXPasswd(), X509KeyStorageFlags.MachineKeySet);
        }

        private string GetPFXPasswd()
        {
            var lines = File.ReadLines(String.Format(@"{0}\bin\..\Certificates\{1}",
                AppDomain.CurrentDomain.BaseDirectory,
                ExpenseTrackerConstants.SslCertificateFilePasswd)).Take(1).ToArray();
            return lines.Count() > 0 ? lines[0] : string.Empty;
        }
    }
}