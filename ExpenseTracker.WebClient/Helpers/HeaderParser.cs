using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace ExpenseTracker.WebClient.Helpers
{
    public class HeaderParser
    {
        public static PagingInfo FindAndParsePagingInfo(HttpResponseHeaders responseHeaders)
        {
            if (responseHeaders.Contains("X-Pagination"))
            {
                var pageInfo = responseHeaders.First(p => p.Key == "X-Pagination").Value;

                return JsonConvert.DeserializeObject<PagingInfo>(pageInfo.First());
            }

            return null;
        }
    }
}