using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

namespace FacebookApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        public IActionResult test1()
        {
            string res = "";
            using (WebClient client = new WebClient())
            {
                res = client.DownloadString("http://graph.facebook.com/V2.6/rada.sangon?fields=id,name");
            }
            Task t = new Task(getFB);
            t.Start();
            return Json(new { result = "success", response = res });
        }

        private async Task<string> getFB()
        {
            var responseString = "";
            using (var client = new HttpClient())
            {
                
                HttpResponseMessage response = await client.GetAsync("http://graph.facebook.com/V2.6/me?fields=id,name");
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                }

            }
            return responseString;
        }
    }
}
