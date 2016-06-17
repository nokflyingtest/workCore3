using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;

namespace PPtest.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

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

        public IActionResult DetailsPdfSrc()
        {
            return View();
        }

        public async Task<IActionResult> iTextDetailsPdf()
        {
            var htmlString = "";
            var urlString = Request.Scheme + "://" + Request.Host + Url.Action("DetailsPdfSrc", "Home");

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(urlString))
            using (HttpContent content = response.Content)
            {
                htmlString = await content.ReadAsStringAsync();
            }

            ////Create a byte array that will eventually hold our final PDF
            //Byte[] bytes;

            ////Boilerplate iTextSharp setup here
            ////Create a stream that we can write to, in this case a MemoryStream
            //MemoryStream ms;
            //using (ms = new MemoryStream())
            //{

            //    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
            //    using (var doc = new Document())
            //    {

            //        //Create a writer that's bound to our PDF abstraction and our stream
            //        using (var writer = PdfWriter.GetInstance(doc, ms))
            //        {

            //            //Open the document for writing
            //            doc.Open();

            //            //Our sample HTML and CSS
            //            var example_html = @"<p>This <em>is </em><span class=""headline"" style=""text-decoration: underline;"">some</span> <strong>sample <em> text</em></strong><span style=""color: red;"">!!!</span></p>";
            //            var example_css = @".headline{font-size:200%}";

            //            /**************************************************
            //             * Example #1                                     *
            //             *                                                *
            //             * Use the built-in HTMLWorker to parse the HTML. *
            //             * Only inline CSS is supported.                  *
            //             * ************************************************/

            //            //Create a new HTMLWorker bound to our document
            //            //using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
            //            //{

            //            //    //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
            //            //    using (var sr = new StringReader(example_html))
            //            //    {

            //            //        //Parse the HTML
            //            //        htmlWorker.Parse(sr);
            //            //    }
            //            //}

            //            /**************************************************
            //             * Example #2                                     *
            //             *                                                *
            //             * Use the XMLWorker to parse the HTML.           *
            //             * Only inline CSS and absolutely linked          *
            //             * CSS is supported                               *
            //             * ************************************************/

            //            //XMLWorker also reads from a TextReader and not directly from a string
            //            using (var srHtml = new StringReader(example_html))
            //            {

            //                //Parse the HTML
            //                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
            //            }

            //            /**************************************************
            //             * Example #3                                     *
            //             *                                                *
            //             * Use the XMLWorker to parse HTML and CSS        *
            //             * ************************************************/

            //            //In order to read CSS as a string we need to switch to a different constructor
            //            //that takes Streams instead of TextReaders.
            //            //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
            //            using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
            //            {
            //                using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_html)))
            //                {

            //                    //Parse the HTML
            //                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
            //                }
            //            }


            //            doc.Close();
            //        }
            //    }

            //    //After all of the PDF "stuff" above is done and closed but **before** we
            //    //close the MemoryStream, grab all of the active bytes from the stream
            //    bytes = ms.ToArray();
            //}

            //Now we just need to do something with those bytes.
            //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
            //You could also write the bytes to a database in a varbinary() column (but please don't) or you
            //could pass them to another function for further PDF processing.

            //var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            //System.IO.File.WriteAllBytes(testFile, bytes);

            //MemoryStream workStream = new MemoryStream();
            //Document document = new Document(PageSize.A4, 25, 25, 50, 30);
            //PdfWriter.GetInstance(document, workStream).CloseStream = false;
            //byte[] byteInfo = workStream.ToArray();
            //workStream.Write(byteInfo, 0, byteInfo.Length);
            //workStream.Position = 0;

            //return new FileStreamResult(workStream, "application/pdf");

            Byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        var example_html = @"<p>This <em>is </em><span class=""headline"" style=""text-decoration: underline;"">some</span> <strong>sample <em> text</em></strong><span style=""color: red;"">!!!</span></p>";
                        var example_css = @".headline{font-size:200%}";
                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlString)))
                            {
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            }
                        }
                        doc.Close();
                    }
                }
                bytes = ms.ToArray();
            }


            MemoryStream m = new MemoryStream(bytes);
            return new FileStreamResult(m, "application/pdf");

            //return Content(htmlString);
        }

        public IActionResult iTextDetailsPdfTest()
        {
            var htmlString = "";
            var urlString = Request.Scheme + "://" + Request.Host + Url.Action("DetailsPdfSrc", "Home");



            //Create a byte array that will eventually hold our final PDF
            Byte[] bytes;

            //Boilerplate iTextSharp setup here
            //Create a stream that we can write to, in this case a MemoryStream
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document())
                {

                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {

                        //Open the document for writing
                        doc.Open();

                        //Our sample HTML and CSS
                        var example_html = @"<p>This <em>is </em><span class=""headline"" style=""text-decoration: underline;"">some</span> <strong>sample <em> text</em></strong><span style=""color: red;"">!!!</span></p>";
                        var example_css = @".headline{font-size:200%}";

                        /**************************************************
                         * Example #1                                     *
                         *                                                *
                         * Use the built-in HTMLWorker to parse the HTML. *
                         * Only inline CSS is supported.                  *
                         * ************************************************/

                        //Create a new HTMLWorker bound to our document
                        //using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
                        //{

                        //    //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
                        //    using (var sr = new StringReader(example_html))
                        //    {

                        //        //Parse the HTML
                        //        htmlWorker.Parse(sr);
                        //    }
                        //}

                        /**************************************************
                         * Example #2                                     *
                         *                                                *
                         * Use the XMLWorker to parse the HTML.           *
                         * Only inline CSS and absolutely linked          *
                         * CSS is supported                               *
                         * ************************************************/

                        //XMLWorker also reads from a TextReader and not directly from a string
                        using (var srHtml = new StringReader(example_html))
                        {

                            //Parse the HTML
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }

                        /**************************************************
                         * Example #3                                     *
                         *                                                *
                         * Use the XMLWorker to parse HTML and CSS        *
                         * ************************************************/

                        //In order to read CSS as a string we need to switch to a different constructor
                        //that takes Streams instead of TextReaders.
                        //Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_html)))
                            {

                                //Parse the HTML
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            }
                        }


                        doc.Close();
                    }
                }

                //After all of the PDF "stuff" above is done and closed but **before** we
                //close the MemoryStream, grab all of the active bytes from the stream
                bytes = ms.ToArray();
            }

            //Now we just need to do something with those bytes.
            //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
            //You could also write the bytes to a database in a varbinary() column (but please don't) or you
            //could pass them to another function for further PDF processing.
            var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            System.IO.File.WriteAllBytes(testFile, bytes);





            return Content(htmlString);
        }

        public IActionResult iTextDetailsPdfIText()
        {
            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.A4, 25, 25, 50, 30);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            //PdfWriter p = PdfWriter.GetInstance(document, workStream);
            var logoPath = Path.Combine(_env.WebRootPath, "images");
            logoPath = Path.Combine(logoPath, "logo_t.png");
            Image logo = Image.GetInstance(logoPath);
            logo.ScalePercent(50);
            //png.ScaleToFit(140f, 120f);
            //png.SpacingBefore = 10f;
            //png.SpacingAfter = 1f;
            logo.Alignment = Element.ALIGN_CENTER;

            var memberPicPath = Path.Combine(_env.WebRootPath, "images_member");
            memberPicPath = Path.Combine(memberPicPath, "_2_1365148227.jpg");
            //memberPicPath = Path.Combine(memberPicPath, "_677138-topic-ix-1.jpg");

            Image memberPic = Image.GetInstance(memberPicPath);
            memberPic.ScaleToFit(120f, 120f);
            memberPic.Border = Rectangle.BOX;
            memberPic.BorderColor = BaseColor.DARK_GRAY;
            memberPic.BorderWidth = 1f;
            //var w = memberPic.ScaledWidth / 2;
            memberPic.SetAbsolutePosition(document.PageSize.Width - 30f - memberPic.ScaledWidth, document.PageSize.Height - 130f);
            //png.ScaleToFit(140f, 120f);
            //png.SpacingBefore = 10f;
            //png.SpacingAfter = 1f;
            memberPic.Alignment = Element.ALIGN_RIGHT;

            Rectangle rect = new Rectangle(30, 36, 565, 706);
            rect.Border = Rectangle.BOX;
            rect.BorderColor = BaseColor.DARK_GRAY;
            rect.BorderWidth = 1f;


            document.Open();
            document.Add(rect);
            document.Add(logo); document.Add(memberPic);


            document.Add(new Paragraph("ข้อมูลตามบัตรประชาชน"));
            document.Add(new Paragraph(DateTime.Now.ToString()));

            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");


        }
    }
}
