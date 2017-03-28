using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace ExpenseTracker.WebClient.Helpers
{
    public static class ExpenseTrackerHttpClient
    {
        public static HttpClient GetClient(string requestVersion = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ExpenseTrackerConstants.ExpenseTrackerAPI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (!String.IsNullOrEmpty(requestVersion))
            {
                // thru a custom request header
                // client.DefaultRequestHeaders.Add("api-version", requestVersion);

                // thru content negotiation
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.expensetracker.v"
                    + requestVersion + "json"));
            }

            return client;
        }
    }
}