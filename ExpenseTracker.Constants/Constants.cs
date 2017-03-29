using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseTracker
{
    public class ExpenseTrackerConstants
    {


        public const string ExpenseTrackerAPI = "http://api.iulitab.com/";
        public const string ExpenseTrackerClient = "http://localhost:27470/";
        public const string ExpenseTrackerMobile = "ms-app://s-1-15-2-467734538-4209884262-1311024127-1211083007-3894294004-443087774-3929518054/";

        public const string IdSrvIssuerUri = "https://identity.iulitab.com/embedded";

        public const string IdSrv = "https://identity.iulitab.com/identity";
        public const string IdSrvToken = IdSrv + "/connect/token";
        public const string IdSrvAuthorize = IdSrv + "/connect/authorize";
    }
}
