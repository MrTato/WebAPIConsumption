using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPIConsumption.Models;

namespace WebAPIConsumption.Controllers
{
    public class AgifyController : Controller
    {
        // GET: Agify
        public ActionResult Index()
        {
            IEnumerable<Agify> jsonPlaceholder = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.agify.io?name=");
                var responseTask = client.GetAsync("michael");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Agify>>();
                    readJob.Wait();
                    jsonPlaceholder = readJob.Result;
                }
                else
                {
                    jsonPlaceholder = Enumerable.Empty<Agify>();
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
            }
            return View(jsonPlaceholder);
        }
    }
}