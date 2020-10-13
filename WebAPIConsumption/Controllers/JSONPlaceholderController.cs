using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPIConsumption.Models;

namespace WebAPIConsumption.Controllers
{
    public class JSONPlaceholderController : Controller
    {
        // GET: JSONPlaceholder
        public ActionResult Index()
        {
            IEnumerable<JSONPlaceholder> jsonPlaceholder = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/posts/1/");
                var responseTask = client.GetAsync("comments");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<JSONPlaceholder>>();
                    readJob.Wait();
                    jsonPlaceholder = readJob.Result;
                }
                else
                {
                    jsonPlaceholder = Enumerable.Empty<JSONPlaceholder>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
            }
            return View(jsonPlaceholder);
        }
    }
}