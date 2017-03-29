using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ExpenseTracker.IdSrv.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get(){

            return new List<InMemoryUser>(){
                new InMemoryUser{
                    Username = "Iulian", Password = "passwd", Subject = "1",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Iulian"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Tabara"),
                    }
                },
                new InMemoryUser{
                    Username = "Emilian", Password = "passwd", Subject = "2",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Emilian"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Tabara"),
                    }
                },
                new InMemoryUser{
                    Username = "Darius", Password = "passwd", Subject = "3",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Darius"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Tabara"),
                    }
                },
                new InMemoryUser{
                    Username = "Rebeca", Password = "passwd", Subject = "4",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Rebeca"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Tabara"),
                    }
                },
            };
        }
    }
}