using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PDFDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoPDFController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetPDF()
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            var CoverImage = Path.Combine(imagePath, "NTCover.jpg");
            DocumentBuilder.New().AddSection().AddImage(imagePath, new XSize(100, 100)).SetAlignment(HorizontalAlignment.Center).ToDocument().Build("DemoPDF.pdf");
            return Ok();
        }
    }
}
