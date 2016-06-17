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
        public IActionResult DetailsPdf()
        {
            //var htmlString = "";
            //var urlString = Request.Scheme +"://"+ Request.Host + Url.Action("DetailsPdfSrc","Home");
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
            //memberPicPath = Path.Combine(memberPicPath, "_2_1365148227.jpg");
            memberPicPath = Path.Combine(memberPicPath, "_677138-topic-ix-1.jpg");
            
            Image memberPic = Image.GetInstance(memberPicPath);
            memberPic.ScaleToFit(120f, 120f);
            memberPic.Border = Rectangle.BOX;
            memberPic.BorderColor = BaseColor.DARK_GRAY;
            memberPic.BorderWidth = 1f;
            //var w = memberPic.ScaledWidth / 2;
            memberPic.SetAbsolutePosition(document.PageSize.Width-30f-memberPic.ScaledWidth, document.PageSize.Height-130f);
            //png.ScaleToFit(140f, 120f);
            //png.SpacingBefore = 10f;
            //png.SpacingAfter = 1f;
            memberPic.Alignment = Element.ALIGN_RIGHT;

            Rectangle rect = new Rectangle(36, 36, 559, 806);
            rect.Border = Rectangle.BOX;
            rect.BorderColor = BaseColor.DARK_GRAY;
            rect.BorderWidth = 1f;


            document.Open();



            document.Add(rect);



            //document.Add(new Paragraph("Hello World"));
            //document.Add(new Paragraph(DateTime.Now.ToString()));
            document.Add(logo); document.Add(memberPic);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");

            //return Content(resp);
        }

    }
}
