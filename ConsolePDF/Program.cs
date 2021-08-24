using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
using System;
using System.IO;

namespace ConsolePDF
{
    class Program
    {
        static void Main(string[] args)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            var CoverImage = Path.Combine(imagePath, "NTCover.jpg");
            var PassPic = Path.Combine(imagePath, "EIN.jpg");
            DocumentBuilder.New().AddSection().AddImageToSection(CoverImage, new XSize(1000,250), ScalingMode.UserDefined)
                    .AddImage(PassPic, new XSize(120, 120), ScalingMode.UserDefined).ToDocument().Build("DemoPDF.pdf");
            Console.WriteLine("PDF Generated");
            Console.ReadLine();
        }
    }
}
