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
            //using (var client = new HttpClient())
            //{
            //    // using (var content = new MultipartFormDataContent())
            //    //  {
            //        var multipartContent = new MultipartFormDataContent();

            //        byte[] Bytes = new byte[ReqExcelFile.InputStream.Length + 1];
            //        ReqExcelFile.InputStream.Read(Bytes, 0, Bytes.Length);
            //        var fileContent = new ByteArrayContent(Bytes);
            //        //fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = ReqExcelFile.FileName };
            //        //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            //        //multipartContent.Add(fileContent);
            //        multipartContent.Add(fileContent, "XLSXFile",  ReqExcelFile.FileName);
            //        client.DefaultRequestHeaders.Add("AppId", "41203F44M9E21A450CM9F97C5799B6471E43");
            //        //client.DefaultRequestHeaders.Add("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //        //client.DefaultRequestHeaders.Contains.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        //var multipartContent = new MultipartFormDataContent();
            //        //multipartContent.Add(byteArrayContent, "csvFile", "filename");
            //        //var postResponse = await _client.PostAsync("offers", multipartContent);


            //        var requestUri = "https://api.moamc.com/LMS/api/Sales/UploadAIFNav";
            //        var result = client.PostAsync(requestUri, multipartContent).Result;
            //        string strRes = result.Content.ReadAsStringAsync().Result;
            //        //string response=result.Content.ReadAsStringAsync().ToString();
            //        if (result.StatusCode == System.Net.HttpStatusCode.Created)
            //        {
            //            //List<string> m = result.Content.ReadAsAsync<List<string>>().Result;

            //            //ViewBag.Success = m.FirstOrDefault();
            //            ViewBag.Success = "Success";
            //        }
            //        else
            //        {
            //            ViewBag.Failed = "Failed !" + result.Content.ToString();
            //        }
            //   // }
            //}

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
                                // "file" parameter name should be the same as the server side input parameter name
                                form.Add(fileContent, "ReqExcelFile", ReqExcelFile.FileName);
                                HttpResponseMessage response = await httpClient.PostAsync("https://api.f.com/LMS/api/Sales/UploadAIFNav", form);
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