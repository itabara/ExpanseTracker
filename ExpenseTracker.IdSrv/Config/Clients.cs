using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.IdSrv.Config
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client()
                {
                    Enabled = true,
                    ClientName = "ExpenseTracker MVC Client (Hybrid flow)",
                    ClientId = "mvc",
                    Flow = Flows.Hybrid,
                    RequireConsent = true,

                    RedirectUris = new List<string>()
                    {
                        ExpenseTrackerConstants.ExpenseTrackerClient
                    }
                }
            };
        }
    }
}