using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Pdf;
using MigraDoc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MigraDoc.DocumentObjectModel.Shapes;

namespace PPtest.Controllers
{
    public class PdfSharpTestController : Controller
    {
        private IHostingEnvironment _env;

        public PdfSharpTestController(IHostingEnvironment env)
        {
            _env = env;
        }

        public ActionResult TestMigraDoc1()
        {

            Document document = new Document();

            Section section = document.AddSection();

            Paragraph paragraph = section.AddParagraph();

            paragraph.Format.Font.Color = Color.FromCmyk(100, 30, 20, 50);

            paragraph.AddFormattedText("Hello,??? World!", TextFormat.Bold);

            var memberPicPath = Path.Combine(_env.WebRootPath, "images_member");
            memberPicPath = Path.Combine(memberPicPath, "_2_1365148227.jpg");
            //memberPicPath = Path.Combine(memberPicPath, "_677138-topic-ix-1.jpg");
            Image imgMember = section.AddImage(memberPicPath);
            imgMember.Height = "5cm";
            imgMember.LockAspectRatio = true;
            imgMember.RelativeVertical = RelativeVertical.Line;
            imgMember.RelativeHorizontal = RelativeHorizontal.Margin;
            imgMember.Top = ShapePosition.Top;
            imgMember.Left = ShapePosition.Right;
            imgMember.WrapFormat.Style = WrapStyle.Through;
            

            document.UseCmykColor = true;
            const bool unicode = false;
            const PdfFontEmbedding embedding = PdfFontEmbedding.Always;
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            //const string filename = "HelloWorld.pdf";
            //pdfRenderer.PdfDocument.Save(filename);
            MemoryStream m = new MemoryStream();
            pdfRenderer.PdfDocument.Save(m,false);
            return new FileStreamResult(m, "application/pdf");
        }


    }

}