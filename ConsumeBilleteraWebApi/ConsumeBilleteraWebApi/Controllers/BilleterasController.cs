using ConsumeBilleteraWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumeBilleteraWebApi.Controllers
{
    public class BilleterasController : Controller
    {
        // GET: Billeteras
        string BaseUrl = "http://localhost:34211";

        public async Task<ActionResult> Index()
        {
            List<Billetera> EmpInfo = new List<Billetera>();
            using (var transaction = new HttpClient())
            {
                transaction.BaseAddress = new Uri(BaseUrl);
                transaction.DefaultRequestHeaders.Clear();
                transaction.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Response = await transaction.GetAsync("list");
                if (Response.IsSuccessStatusCode)
                {
                    var EmpResponse = Response.Content.ReadAsStringAsync().Result;

                    EmpInfo = JsonConvert.DeserializeObject<List<Billetera>>(EmpResponse);
                }

                return View(EmpInfo);
            }

        }

        public async Task<ActionResult> balance()
        {
            Balance EmpInfo = new Balance();
            using (var transaction = new HttpClient())
            {
                transaction.BaseAddress = new Uri(BaseUrl);
                transaction.DefaultRequestHeaders.Clear();
                transaction.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Response = await transaction.GetAsync("get_balance");
                if (Response.IsSuccessStatusCode)
                {
                    var EmpResponse = Response.Content.ReadAsStringAsync().Result;

                    EmpInfo = JsonConvert.DeserializeObject<Balance>(EmpResponse);
                }

                return View(EmpInfo);
            }

        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Billetera billeteras)
        {
            using (var billetera = new HttpClient())
            {
                billetera.BaseAddress = new Uri(BaseUrl + "/api/Billeteras");
                var postTask = billetera.PostAsJsonAsync<Billetera>("billeteras", billeteras);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("balance");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta con el administrador");
            return View(billeteras);
        }

    }
}