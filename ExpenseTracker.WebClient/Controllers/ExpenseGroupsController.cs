using ExpenseTracker.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using ExpenseTracker.WebClient.Helpers;
using ExpenseTracker.WebClient.Models;
using PagedList;
using Marvin.JsonPatch;

namespace ExpenseTracker.WebClient.Controllers
{
    public class ExpenseGroupsController : Controller
    {

        public async Task<ActionResult> Index(int? page=1)
        {
            var client = ExpenseTrackerHttpClient.GetClient();
            var model = new ExpenseGroupsViewModel();

            HttpResponseMessage egsResponse = await client.GetAsync("api/expensegroupstatusses");
            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();
                var listExpenseGroupsStatuses = JsonConvert.DeserializeObject<IEnumerable<ExpenseGroupStatus>>(content);
                model.ExpenseGroupStatusses = listExpenseGroupsStatuses;
            }
            else
            {
                return Content("An error occurred");
            }

            HttpResponseMessage response = await client.GetAsync("api/expensegroups?sort=expensegroupstatusid,title&page=" + page + "&pagesize=5");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var pagingInfo = HeaderParser.FindAndParsePagingInfo(response.Headers);


                var expenseGroups = JsonConvert.DeserializeObject<IEnumerable<ExpenseGroup>>(content);

                var pagedExpeseGroupList = new StaticPagedList<ExpenseGroup>(expenseGroups, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);

                model.ExpenseGroups = pagedExpeseGroupList;
                model.PagingInfo = pagingInfo;
            }
            else
            {
                return Content("An error occurred");
            }

            return View(model);
        }

 
        // GET: ExpenseGroups/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var client = ExpenseTrackerHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/expensegroups/" + id
                + "?fields=id,description,title,expenses");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<ExpenseGroup>(content);
                return View(model);
            }

            return Content("An error occurred");
        }

        // GET: ExpenseGroups/Create
 
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExpenseGroup expenseGroup)
        {
            try
            {
                var client = ExpenseTrackerHttpClient.GetClient();
                // an expense group is created with status "Open", for the current user

                expenseGroup.ExpenseGroupStatusId = 1;
                expenseGroup.UserId = @"http://expensetrackeridsrv3/embedded_1";

                var serializedItemToCreate = JsonConvert.SerializeObject(expenseGroup);


                var response = await client.PostAsync("/api/expensegroups",
                    new StringContent(serializedItemToCreate,
                    System.Text.Encoding.Unicode,
                    "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("An error occurred");
                }
            }
            catch
            {
                return Content("An error occurred");
            }
        }

        // GET: ExpenseGroups/Edit/5
 
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var client = ExpenseTrackerHttpClient.GetClient();

                HttpResponseMessage response = await client.GetAsync("api/expensegroups/" + id 
                    + "?fields=id,title,description");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<ExpenseGroup>(content);
                    return View(model);
                }

                return Content("An error occurred");

            }
            catch
            {
                return Content("An error occurred");
            }
        }

        // POST: ExpenseGroups/Edit/5   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ExpenseGroup expenseGroup)
        {
            try
            {
                var client = ExpenseTrackerHttpClient.GetClient();

                JsonPatchDocument<DTO.ExpenseGroup> patchDoc = new JsonPatchDocument<ExpenseGroup>();
                patchDoc.Replace(eg => eg.Title, expenseGroup.Title);
                patchDoc.Replace(eg => eg.Description, expenseGroup.Description);

                // serialize and PUT or better PATCH
                var serializedItemToUpdate = JsonConvert.SerializeObject(patchDoc);

                //var serializedItemToUpdate = JsonConvert.SerializeObject(expenseGroup);

                //var response = await client.PutAsync("api/expensegroups/" + id,
                //    new StringContent(serializedItemToUpdate,
                //    System.Text.Encoding.Unicode, "application/json"));

                // use PATCH instead of PUT
                var response = await client.PatchAsync("api/expensegroups/" + id,
                    new StringContent(serializedItemToUpdate,
                    System.Text.Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return Content("An error occurred");
            }
            catch
            {
                return Content("An error occurred");
            }
        }
         
        // POST: ExpenseGroups/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var client = ExpenseTrackerHttpClient.GetClient();
                var response = await client.DeleteAsync("api/expensegroups/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return Content("An error occurred");
            }
            catch
            {
                return Content("An error occurred");
            }
        }
    }
}
