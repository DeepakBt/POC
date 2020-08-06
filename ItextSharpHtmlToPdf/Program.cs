using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.IO;

namespace ItextSharpHtmlToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Core 2.1 HTML to PDF ITextSharp");
            Program P = new Program();
            // int i = P.GeneratePDFFromHTML(@"D:\","test.pdf");
            P.GeneratePDFFromHTML("<HTML><body><div>test</div></body></HTML>", @"D:\test.pdf");
            Console.ReadLine();
        }

        public void GeneratePDFFromHTML(string HTMLString,string PDFSaveLocationFilePathWithName)
        {
            try
            {
                StringReader StrRead = new StringReader(HTMLString);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    htmlparser.Parse(StrRead);
                    pdfDoc.Close();

                    byte[] bytes = memoryStream.ToArray();
                    File.WriteAllBytes(PDFSaveLocationFilePathWithName, bytes);
                    memoryStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int GeneratePDFFromHTML(string TempFilePath, string AttachmentName)

        //{

        //    int iResult = 0; StringReader sr = null;

        //    string tempPath = TempFilePath + AttachmentName;

        //    try

        //    {





        //        //sr = new StringReader(Convert.ToString(dtResult.Rows[0][0]));

        //        sr = new StringReader(Convert.ToString("<HTML><body><div>ghhg</div></body></HTML>"));

        //        using (FileStream _fileStream = new FileStream(@"" + tempPath + "", FileMode.OpenOrCreate))

        //        {

        //            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 20f);

        //            PdfWriter writer = PdfWriter.GetInstance(document, _fileStream);

        //            document.Open();

        //            //Style

        //            iTextSharp.text.Font tableBodyfont = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 6);//body font



        //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);



        //            document.Close();

        //            writer.Close();

        //            iResult = 1;



        //            //Transfer Created file to FTP folder



        //            //iResult = TransferFileToFTP(tempPath, AttachmentPATH, AttachmentName);



        //            //Delete temporary created file

        //            if (iResult == 1)

        //            {

        //                File.Delete(tempPath);

        //            }
        //        }
        //    }
        //    catch (Exception oEX)
        //    {
        //        iResult = 0;
        //        Console.WriteLine(oEX.Message);
        //    }
        //    return iResult;

        //}
    }
}
