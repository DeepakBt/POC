using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UploadMVC4.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase ReqExcelFile)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("AppId", "41203F44M9E21A450CM9F97C5799B6471E43");
                using (var form = new MultipartFormDataContent())
                {
                    ReqExcelFile.SaveAs(Server.MapPath(ReqExcelFile.FileName));
                    using (var fs = System.IO.File.Open(Server.MapPath(ReqExcelFile.FileName),FileMode.Open, FileAccess.Read))
                    {
                        using (var streamContent = new StreamContent(fs))
                        {
                            using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                            {
                                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                                form.Add(fileContent, "ReqExcelFile", ReqExcelFile.FileName);
                                HttpResponseMessage response = await httpClient.PostAsync("https://*&UY^Yccdcdcdcdcd", form);
                                string strResponse = response.Content.ReadAsStringAsync().Result;
                            }
                        }
                    }
                }
            }
            return View();
        }
    }
}