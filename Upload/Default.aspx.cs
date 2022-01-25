using Rest;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI;
using RestClient = RestSharp.RestClient;

namespace Upload
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public async Task uploadAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("AppId", "41203F44M9E21A450CM9F97C5799B6471E43");
                using (var form = new MultipartFormDataContent())
                {


                    if (FileUpload1.HasFile)
                    {
                        FileUpload1.SaveAs(Server.MapPath(FileUpload1.FileName));
                        try
                        {
                            using (var fs = System.IO.File.Open(Server.MapPath(FileUpload1.FileName), FileMode.Open, FileAccess.Read))
                            {
                                using (var streamContent = new StreamContent(fs))
                                {
                                    using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                                    {
                                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                                        // "file" parameter name should be the same as the server side input parameter name
                                        form.Add(fileContent, "ReqExcelFile", FileUpload1.FileName);
                                        HttpResponseMessage response = await httpClient.PostAsync("https://api.moamc.com/LMS/api/Sales/UploadAIFNav", form);
                                        //HttpResponseMessage response = await httpClient.PostAsync("https://api.f.com/LMS/api/Sales/UploadAIFNav", form).Result;
                                        string strResponse = response.Content.ReadAsStringAsync().Result;
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }


                    }
                    //ReqExcelFile.SaveAs(Server.MapPath(ReqExcelFile.FileName));
                    //using (var fs = System.IO.File.Open(Server.MapPath(ReqExcelFile.FileName), FileMode.Open, FileAccess.Read))
                    //{
                    //    using (var streamContent = new StreamContent(fs))
                    //    {
                    //        using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                    //        {
                    //            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    //            // "file" parameter name should be the same as the server side input parameter name
                    //            form.Add(fileContent, "ReqExcelFile", ReqExcelFile.FileName);
                    //            HttpResponseMessage response = await httpClient.PostAsync("https://api.f.com/LMS/api/Sales/UploadAIFNav", form);
                    //            string strResponse = response.Content.ReadAsStringAsync().Result;
                    //        }
                    //    }
                    //}
                }
            }
        }

        public async void Upload2()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("AppId", "41203F44M9E21A450CM9F97C5799B6471E43");
            MultipartFormDataContent form = new MultipartFormDataContent();
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath(FileUpload1.FileName));
                HttpResponseMessage response = await httpClient.PostAsync("https://api.f.com/LMS/api/Sales/UploadAIFNav", form);

                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                string sd = response.Content.ReadAsStringAsync().Result;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath(FileUpload1.FileName));
                    var client = new RestClient("https://api.moamc.com/LMS/api/Sales/UploadAIFSIP");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("AppId", "41203F44M9E21A450CM9F97C5799B6471E43");
                    request.AddHeader("ReqExcelFile", "ReqExcelFile");
                    request.AddFile("ReqExcelFile", Server.MapPath(FileUpload1.FileName));
                    IRestResponse response = client.Execute(request);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
