using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PPcore.Controllers
{
    public class membersController : Controller
    {
        private PalangPanyaDBContext _context;
        private IConfiguration _configuration;
        private IHostingEnvironment _env;

        private void prepareViewBag()
        {
            ViewBag.images_upload = _configuration.GetSection("Paths").GetSection("images_upload").Value;
            ViewBag.images_member = _configuration.GetSection("Paths").GetSection("images_member").Value;

            //ViewBag.sex = new SelectList(new[] { new { Value = "F", Text = "Female" }, new { Value = "M", Text = "Male" }, new { Value = "A", Text = "Alternative" } }, "Value", "Text", "F");
            ViewBag.sex = new SelectList(new[] { new { Value = "F", Text = "หญิง" }, new { Value = "M", Text = "ชาย" } }, "Value", "Text", "F");

            ViewBag.cid_type = new SelectList(new[] { new { Value = "C", Text = "บัตรประชาชน" }, new { Value = "H", Text = "สำเนาทะเบียนบ้าน" }, new { Value = "P", Text = "Passport" } }, "Value", "Text", "F");
            ViewBag.marry_status = new SelectList(new[] { new { Value = "N", Text = "โสด" }, new { Value = "Y", Text = "สมรส" } }, "Value", "Text");
            ViewBag.zone = new SelectList(new[] { new { Value = "M", Text = "กลาง" }, new { Value = "N", Text = "เหนือ" }, new { Value = "E", Text = "ตะวันออก" }, new { Value = "W", Text = "ตะวันตก" }, new { Value = "S", Text = "ใต้" }, new { Value = "L", Text = "ตะวันออกเฉียงเหนือ" } }, "Value", "Text");

            ViewBag.mem_group = new SelectList(_context.mem_group.OrderBy(g => g.mem_group_code), "mem_group_code", "mem_group_desc");
            ViewBag.mem_type = new SelectList(_context.mem_type.OrderBy(t => t.mem_group_code).OrderBy(t => t.mem_type_code), "mem_type_code", "mem_type_desc", "3  ");
            ViewBag.mem_level = new SelectList(_context.mem_level.OrderBy(t => t.mlevel_code), "mlevel_code", "mlevel_desc", "3  ");
            ViewBag.mem_status = new SelectList(_context.mem_status.OrderBy(s => s.mstatus_code), "mstatus_code", "mstatus_desc", "3  ");

            ViewBag.ini_country = new SelectList(_context.ini_country.OrderBy(c => c.country_code), "country_code", "country_desc", "66");
            ViewBag.ini_province = new SelectList(_context.ini_province.OrderBy(p => p.province_code), "province_code", "pro_desc", "10000000");

        }

        public membersController(PalangPanyaDBContext context, IConfiguration configuration, IHostingEnvironment env)
        {
            _context = context;
            _configuration = configuration;
            _env = env;
        }

        // GET: members
        public IActionResult Index()
        {
            var palangPanyaDBContext = _context.member; //.Include(m => m.ini_list_zip).Include(m => m.mem_).Include(m => m.mlevel_codeNavigation).Include(m => m.mstatus_codeNavigation);
            ViewBag.countRecords = palangPanyaDBContext.Count();
            return View(palangPanyaDBContext.OrderByDescending(m => m.rowversion).ToList());
        }

        // GET: members/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            member member = _context.member.Single(m => m.id == new Guid(id));
            if (member == null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(member.mem_photo))
            {
                //pic_image m = _context.pic_image.Single(i => i.image_code == member.mem_photo);
                //ViewBag.memPhoto = m.image_name;
                ViewBag.memPhoto = member.mem_photo;
            }

            if (!String.IsNullOrEmpty(member.cid_card_pic))
            {
                //pic_image c = _context.pic_image.Single(i => i.image_code == member.cid_card_pic);
                //ViewBag.cidCardPhoto =c.image_name;
                ViewBag.cidCardPhoto = member.cid_card_pic;
            }
            ViewBag.memberId = id;
            ViewBag.TabNoData = "";
            prepareViewBag();
            return View(member);
        }

        // GET: members/DetailsPdf/5
        public FileStreamResult DetailsPdf(String id)
        {
            member m = _context.member.SingleOrDefault(mb => mb.id == new Guid(id));

            var fname = m.fname; var lname = m.lname;
            var sex = m.sex == "F"? "หญิง" : "ชาย";
            var nationality = m.nationality;

            var birthdate = String.Format("{0:d MMMM yyyy}",m.birthdate);
            DateTime today = DateTime.Today;
            int year = 0;
            Int32.TryParse(String.Format("{0:yyyy}", m.birthdate),out year);
            int age = today.Year - year + 543;
            if (m.birthdate > today.AddYears(-age)) age--;
            var current_age = age.ToString();

            var cid_card = m.cid_card.Substring(0,7) + "XXXXXX";

            var marry_status = m.marry_status == "N"? "โสด" :"สมรส";

            var religion = m.religion;
            var tel = m.tel != null ? m.tel.Substring(0,1) : "";
            var mobile = m.mobile;
            var fax = m.fax; var email = m.email;
            var social_app_data = m.social_app_data;
            var text_address = m.texta_address + " " + m.textb_address + " " + m.textc_address;

            List<listTraining> train = new List<listTraining>();
            train.Add(new listTraining { rec_no = "1", code = "10110", desc = "การแปรรูปผลิตภัณฑ์", grade = "Good" });

            List<listInfo> visit = new List<listInfo>();
            visit.Add(new listInfo { rec_no = "1", desc = "โครงการดูงานการส่งออกสินค้าต่างประเทศ ที่ประเทศมาเลเซีย" });

            List<listInfo> social = new List<listInfo>();
            social.Add(new listInfo { rec_no = "1", desc = "ปลูกป่าที่จังหวัดเชียงใหม่" });
            social.Add(new listInfo { rec_no = "2", desc = "ให้ความรู้เกี่ยวกับการเกษตรแบบเศรษฐกิจพอเพียงให้กับศูนย์ส่งเสริมการเกษตร จังหวัดน่าน" });

            List<listInfo> reward = new List<listInfo>();
            reward.Add(new listInfo { rec_no = "1", desc = "รางวัลโครงการผู้นำตัวอย่างเศรษฐกิจพอเพียง ปี พ.ศ.2558" });

            List<listInfo> education = new List<listInfo>();
            education.Add(new listInfo { rec_no = "1", desc = "ระดับมัธยมศึกษา โรงเรียนหอการค้า สาขาพาณิชยกรรม" });
            education.Add(new listInfo { rec_no = "2", desc = "ระดับอุดมศึกษา มหาวิทยาลัยสุโขทัยธรรมมาธิราช สาขาการเกษตร" });

            var medical_history = "หอบหืด"; var blood_group = "โอ"; var restrict_food = "อาหารทะเล";
            var hobby = "เพาะพันธุ์กล้วยไม้"; var special_skill = "เพาะพันธุ์กล้วยไม้หายาก";

            List<listWork> work = new List<listWork>();
            work.Add(new listWork { rec_no = "1", company = "บริษัท เกษตรพัฒนา (Kasadpattana Co.,Ltd.)", position = "วิทยากร (Lecturer)", year = "พ.ศ.2559", address = "55/621 โครงการสุโขทัย อเวนิว 99 ถ.บอนด์สตรีท ต.บางพูด อ.ปากเกร็ด จ.นนทบุรี 11120" });



            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.A4, 25, 25, 50, 30);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            var logoPath = Path.Combine(_env.WebRootPath, "images");
            logoPath = Path.Combine(logoPath, "logo_t.png");
            Image logo = Image.GetInstance(logoPath);
            logo.ScalePercent(50);
            logo.Alignment = Element.ALIGN_CENTER;

            var memPhoto =  m.mem_photo != null ? m.mem_photo : "dummy_person_0.jpg";
            var memberPicPath = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_member").Value), memPhoto);

            Image memberPic = Image.GetInstance(memberPicPath);
            memberPic.ScaleToFit(120f, 120f);
            memberPic.Border = Rectangle.BOX;
            memberPic.BorderColor = BaseColor.DARK_GRAY;
            memberPic.BorderWidth = 1f;
            memberPic.SetAbsolutePosition(document.PageSize.Width - 30f - memberPic.ScaledWidth, document.PageSize.Height - 130f);
            memberPic.Alignment = Element.ALIGN_RIGHT;

            Rectangle rect = new Rectangle(30, 36, 565, 706);
            rect.Border = Rectangle.BOX;
            rect.BorderColor = BaseColor.DARK_GRAY;
            rect.BorderWidth = 1f;

            var fontPath = Path.Combine(_env.WebRootPath, "fonts/THSarabunNew.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font cn = new Font(bf, 16);
            Font cnb = new Font(bf, 16, Font.BOLD);
            Font cni = new Font(bf, 16, Font.ITALIC);

            document.Open();
            document.Add(rect);
            document.Add(logo); document.Add(memberPic);

            PdfPTable table = new PdfPTable(2);
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            //table.TotalWidth = document.PageSize.Width;
            table.TotalWidth = 535f;
            table.LockedWidth = true;
            table.DefaultCell.VerticalAlignment = 1;
            table.DefaultCell.PaddingLeft = 15f;
            //table.DefaultCell.PaddingTop = 30f;
            float[] widths = new float[] { 135f, 400f };
            table.SetWidths(widths);
            table.SpacingBefore = 50f;

            //new paragraph Member Info
            PdfPCell cell = new PdfPCell(new Phrase("ข้อมูลตามบัตรประชาชน", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
            //Member Info Row 1
            table.AddCell(cell);
            PdfPTable memberInfoRow1 = new PdfPTable(6);
            memberInfoRow1.DefaultCell.Border = Rectangle.NO_BORDER;
            memberInfoRow1.TotalWidth = 400f;
            memberInfoRow1.LockedWidth = true;
            memberInfoRow1.DefaultCell.VerticalAlignment = 1;
            memberInfoRow1.SetWidths(new float[] { 15f, 135f, 20f, 30f, 30f, 55f });
            memberInfoRow1.AddCell(new PdfPCell(new Phrase("ชื่อ", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow1.AddCell(new PdfPCell(new Phrase(fname + " " + lname, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow1.AddCell(new PdfPCell(new Phrase("เพศ", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow1.AddCell(new PdfPCell(new Phrase(sex, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow1.AddCell(new PdfPCell(new Phrase("สัญชาติ", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow1.AddCell(new PdfPCell(new Phrase(nationality, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(memberInfoRow1); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);
            //Member Info Row 2
            table.AddCell("");
            PdfPTable memberInfoRow2 = new PdfPTable(5);
            memberInfoRow2.DefaultCell.Border = Rectangle.NO_BORDER;
            memberInfoRow2.TotalWidth = 400f;
            memberInfoRow2.LockedWidth = true;
            memberInfoRow2.DefaultCell.VerticalAlignment = 1;
            memberInfoRow2.SetWidths(new float[] { 60f, 95f, 20f, 15f, 105f });
            memberInfoRow2.AddCell(new PdfPCell(new Phrase("วัน/เดือน/ปี เกิด", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow2.AddCell(new PdfPCell(new Phrase(birthdate, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow2.AddCell(new PdfPCell(new Phrase("อายุ", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow2.AddCell(new PdfPCell(new Phrase(current_age, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow2.AddCell(new PdfPCell(new Phrase("ปี", cnb)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(memberInfoRow2); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);
            //Member Info Row 3
            table.AddCell("");
            PdfPTable memberInfoRow3 = new PdfPTable(6);
            memberInfoRow3.DefaultCell.Border = Rectangle.NO_BORDER;
            memberInfoRow3.TotalWidth = 400f;
            memberInfoRow3.LockedWidth = true;
            memberInfoRow3.DefaultCell.VerticalAlignment = 1;
            memberInfoRow3.SetWidths(new float[] { 78f, 77f, 40f, 25f, 25f, 50f });
            memberInfoRow3.AddCell(new PdfPCell(new Phrase("หมายเลขบัตรประชาชน", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow3.AddCell(new PdfPCell(new Phrase(cid_card, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow3.AddCell(new PdfPCell(new Phrase("สถานภาพ", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow3.AddCell(new PdfPCell(new Phrase(marry_status, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow3.AddCell(new PdfPCell(new Phrase("ศาสนา", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow3.AddCell(new PdfPCell(new Phrase(religion, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(memberInfoRow3); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);
            //Member Info Row 4
            table.AddCell("");
            PdfPTable memberInfoRow4 = new PdfPTable(4);
            memberInfoRow4.DefaultCell.Border = Rectangle.NO_BORDER;
            memberInfoRow4.TotalWidth = 400f;
            memberInfoRow4.LockedWidth = true;
            memberInfoRow4.DefaultCell.VerticalAlignment = 1;
            memberInfoRow4.SetWidths(new float[] { 83f, 77f, 80f, 65f });
            memberInfoRow4.AddCell(new PdfPCell(new Phrase("หมายเลขโทรศัพท์มือถือ", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow4.AddCell(new PdfPCell(new Phrase(mobile, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow4.AddCell(new PdfPCell(new Phrase("หมายเลขโทรศัพท์บ้าน", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow4.AddCell(new PdfPCell(new Phrase(tel, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(memberInfoRow4); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);
            //Member Info Row 5
            table.AddCell("");
            PdfPTable memberInfoRow5 = new PdfPTable(4);
            memberInfoRow5.DefaultCell.Border = Rectangle.NO_BORDER;
            memberInfoRow5.TotalWidth = 400f;
            memberInfoRow5.LockedWidth = true;
            memberInfoRow5.DefaultCell.VerticalAlignment = 1;
            memberInfoRow5.SetWidths(new float[] { 25f, 135f, 23f, 122f });
            memberInfoRow5.AddCell(new PdfPCell(new Phrase("แฟ็กส์", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow5.AddCell(new PdfPCell(new Phrase(fax, cni)) { Border = Rectangle.NO_BORDER });
            memberInfoRow5.AddCell(new PdfPCell(new Phrase("อีเมล", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow5.AddCell(new PdfPCell(new Phrase(email, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(memberInfoRow5); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);
            //Member Info Row 6
            table.AddCell("");
            PdfPTable memberInfoRow6 = new PdfPTable(2);
            memberInfoRow6.DefaultCell.Border = Rectangle.NO_BORDER;
            memberInfoRow6.TotalWidth = 400f;
            memberInfoRow6.LockedWidth = true;
            memberInfoRow6.DefaultCell.VerticalAlignment = 1;
            memberInfoRow6.SetWidths(new float[] { 30f, 285f });
            memberInfoRow6.AddCell(new PdfPCell(new Phrase("Social", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow6.AddCell(new PdfPCell(new Phrase(social_app_data, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(memberInfoRow6); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);
            //Member Info Row 7
            table.AddCell("");
            PdfPTable memberInfoRow7 = new PdfPTable(2);
            memberInfoRow7.DefaultCell.Border = Rectangle.NO_BORDER;
            memberInfoRow7.TotalWidth = 400f;
            memberInfoRow7.LockedWidth = true;
            memberInfoRow7.DefaultCell.VerticalAlignment = 1;
            memberInfoRow7.SetWidths(new float[] { 30f, 285f });
            memberInfoRow7.AddCell(new PdfPCell(new Phrase("ที่อยู่", cnb)) { Border = Rectangle.NO_BORDER });
            memberInfoRow7.AddCell(new PdfPCell(new Phrase(text_address, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(memberInfoRow7); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);
            //Member Info Row 8
            //table.AddCell("");
            //PdfPTable memberInfoRow8 = new PdfPTable(2);
            //memberInfoRow8.DefaultCell.Border = Rectangle.NO_BORDER;
            //memberInfoRow8.TotalWidth = 400f;
            //memberInfoRow8.LockedWidth = true;
            //memberInfoRow8.DefaultCell.VerticalAlignment = 1;
            //memberInfoRow8.SetWidths(new float[] { 30f, 285f });
            //memberInfoRow8.AddCell("");
            //memberInfoRow8.AddCell(new PdfPCell(new Phrase(textb_address, cni)) { Border = Rectangle.NO_BORDER });
            //cell = new PdfPCell(memberInfoRow8); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            //table.AddCell(cell);

            //Line
            table.AddCell(" ");
            //Line
            PdfPTable line = new PdfPTable(3);
            line.DefaultCell.Border = Rectangle.NO_BORDER;
            line.DefaultCell.FixedHeight = 1f;
            line.SpacingBefore = 10f;
            line.TotalWidth = 400f;
            line.LockedWidth = true;
            line.SetWidths(new float[] { 5f, 295f, 15f });
            line.AddCell("");
            cell = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, FixedHeight = 1f };
            cell.BorderWidthTop = 1f;
            line.AddCell(cell);
            line.AddCell("");
            table.AddCell(line);

            //new paragraph Training Info
            cell = new PdfPCell(new Phrase("ประวัติการฝึกอบรม", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            if (train.Count > 0)
            {
                for (var i = 0; i < train.Count; i++)
                {
                    PdfPTable rowz = new PdfPTable(7);
                    //rowz.DefaultCell.Border = Rectangle.NO_BORDER;
                    rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.TotalWidth = 400f; rowz.LockedWidth = true;
                    rowz.SetWidths(new float[] { 8f, 12f, 30f, 22f, 70f, 27f, 40f });
                    rowz.AddCell(new PdfPCell(new Phrase(train[0].rec_no + ".", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase("รหัส", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(train[0].code, cni)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase("หลักสูตร", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(train[0].desc, cni)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase("ระดับเกรด", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(train[0].grade, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);
                    table.AddCell("");
                }
            }

            //Line
            line = new PdfPTable(3);
            line.DefaultCell.Border = Rectangle.NO_BORDER;
            line.DefaultCell.FixedHeight = 1f;
            line.SpacingBefore = 10f;
            line.TotalWidth = 400f;
            line.LockedWidth = true;
            line.SetWidths(new float[] { 5f, 295f, 15f });
            line.AddCell("");
            cell = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, FixedHeight = 1f };
            cell.BorderWidthTop = 1f;
            line.AddCell(cell);
            line.AddCell("");
            table.AddCell(line);

            //new paragraph
            cell = new PdfPCell(new Phrase("ประวัติการดูงาน", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            if (visit.Count > 0)
            {
                for (var i = 0; i < visit.Count; i++)
                {
                    PdfPTable rowz = new PdfPTable(2);
                    rowz.DefaultCell.Border = Rectangle.NO_BORDER;
                    rowz.TotalWidth = 400f;
                    rowz.LockedWidth = true;
                    rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.SetWidths(new float[] { 8f, 180f });
                    rowz.AddCell(new PdfPCell(new Phrase(visit[0].rec_no + ".", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(visit[0].desc, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);
                    table.AddCell("");
                }
            }

            //Line
            line = new PdfPTable(3);
            line.DefaultCell.Border = Rectangle.NO_BORDER;
            line.DefaultCell.FixedHeight = 1f;
            line.SpacingBefore = 10f;
            line.TotalWidth = 400f;
            line.LockedWidth = true;
            line.SetWidths(new float[] { 5f, 295f, 15f });
            line.AddCell("");
            cell = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, FixedHeight = 1f };
            cell.BorderWidthTop = 1f;
            line.AddCell(cell);
            line.AddCell("");
            table.AddCell(line);

            //new paragraph Social Info
            cell = new PdfPCell(new Phrase("ประสบการณ์ช่วยเหลือสังคม", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            if (social.Count > 0)
            {
                for (var i = 0; i < social.Count; i++)
                {
                    PdfPTable rowz = new PdfPTable(2);
                    rowz.DefaultCell.Border = Rectangle.NO_BORDER;
                    rowz.TotalWidth = 400f;
                    rowz.LockedWidth = true;
                    rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.SetWidths(new float[] { 8f, 180f });
                    rowz.AddCell(new PdfPCell(new Phrase(social[i].rec_no + ".", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(social[i].desc, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);
                    table.AddCell("");
                }
            }

            //Line
            line = new PdfPTable(3);
            line.DefaultCell.Border = Rectangle.NO_BORDER;
            line.DefaultCell.FixedHeight = 1f;
            line.SpacingBefore = 10f;
            line.TotalWidth = 400f;
            line.LockedWidth = true;
            line.SetWidths(new float[] { 5f, 295f, 15f });
            line.AddCell("");
            cell = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, FixedHeight = 1f };
            cell.BorderWidthTop = 1f;
            line.AddCell(cell);
            line.AddCell("");
            table.AddCell(line);

            //new paragraph
            cell = new PdfPCell(new Phrase("รางวัลเชิดชูเกียรติ", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            if (reward.Count > 0)
            {
                for (var i = 0; i < reward.Count; i++)
                {
                    PdfPTable rowz = new PdfPTable(2);
                    rowz.DefaultCell.Border = Rectangle.NO_BORDER;
                    rowz.TotalWidth = 400f;
                    rowz.LockedWidth = true;
                    rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.SetWidths(new float[] { 8f, 180f });
                    rowz.AddCell(new PdfPCell(new Phrase(reward[i].rec_no + ".", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(reward[i].desc, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);
                    table.AddCell("");
                }
            }

            //Line
            line = new PdfPTable(3);
            line.DefaultCell.Border = Rectangle.NO_BORDER;
            line.DefaultCell.FixedHeight = 1f;
            line.SpacingBefore = 10f;
            line.TotalWidth = 400f;
            line.LockedWidth = true;
            line.SetWidths(new float[] { 5f, 295f, 15f });
            line.AddCell("");
            cell = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, FixedHeight = 1f };
            cell.BorderWidthTop = 1f;
            line.AddCell(cell);
            line.AddCell("");
            table.AddCell(line);

            //new paragraph
            cell = new PdfPCell(new Phrase("การศึกษา", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            if (education.Count > 0)
            {
                for (var i = 0; i < education.Count; i++)
                {
                    PdfPTable rowz = new PdfPTable(2);
                    rowz.DefaultCell.Border = Rectangle.NO_BORDER;
                    rowz.TotalWidth = 400f;
                    rowz.LockedWidth = true;
                    rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.SetWidths(new float[] { 8f, 180f });
                    rowz.AddCell(new PdfPCell(new Phrase(education[i].rec_no + ".", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(education[i].desc, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);
                    table.AddCell("");
                }
            }

            //Line
            //table.AddCell(" ");
            //Line
            line = new PdfPTable(3);
            line.DefaultCell.Border = Rectangle.NO_BORDER;
            line.DefaultCell.FixedHeight = 1f;
            line.SpacingBefore = 10f;
            line.TotalWidth = 400f;
            line.LockedWidth = true;
            line.SetWidths(new float[] { 5f, 295f, 15f });
            line.AddCell("");
            cell = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, FixedHeight = 1f };
            cell.BorderWidthTop = 1f;
            line.AddCell(cell);
            line.AddCell("");
            table.AddCell(line);

            //new paragraph Health Info
            cell = new PdfPCell(new Phrase("ข้อมูลสุขภาพ", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;

            //Health Info Row 1
            table.AddCell(cell);
            PdfPTable healthInfoRow1 = new PdfPTable(2);
            healthInfoRow1.DefaultCell.Border = Rectangle.NO_BORDER;
            healthInfoRow1.TotalWidth = 400f;
            healthInfoRow1.LockedWidth = true;
            healthInfoRow1.DefaultCell.VerticalAlignment = 1;
            healthInfoRow1.SetWidths(new float[] { 30f, 165f });
            healthInfoRow1.AddCell(new PdfPCell(new Phrase("โรคประจำตัว", cnb)) { Border = Rectangle.NO_BORDER });
            healthInfoRow1.AddCell(new PdfPCell(new Phrase(medical_history, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(healthInfoRow1); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);

            //Health Info Row 2
            table.AddCell("");
            PdfPTable healthInfoRow2 = new PdfPTable(4);
            healthInfoRow2.DefaultCell.Border = Rectangle.NO_BORDER;
            healthInfoRow2.TotalWidth = 400f;
            healthInfoRow2.LockedWidth = true;
            healthInfoRow2.DefaultCell.VerticalAlignment = 1;
            healthInfoRow2.SetWidths(new float[] { 30f, 50f, 45f, 70f });
            healthInfoRow2.AddCell(new PdfPCell(new Phrase("หมู่โลหิต", cnb)) { Border = Rectangle.NO_BORDER });
            healthInfoRow2.AddCell(new PdfPCell(new Phrase(blood_group, cni)) { Border = Rectangle.NO_BORDER });
            healthInfoRow2.AddCell(new PdfPCell(new Phrase("งานอดิเรก", cnb)) { Border = Rectangle.NO_BORDER });
            healthInfoRow2.AddCell(new PdfPCell(new Phrase(hobby, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(healthInfoRow2); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);

            //Health Info Row 3
            table.AddCell("");
            PdfPTable healthInfoRow3 = new PdfPTable(4);
            healthInfoRow3.DefaultCell.Border = Rectangle.NO_BORDER;
            healthInfoRow3.TotalWidth = 400f;
            healthInfoRow3.LockedWidth = true;
            healthInfoRow3.DefaultCell.VerticalAlignment = 1;
            healthInfoRow3.SetWidths(new float[] { 30f, 50f, 45f, 70f });
            healthInfoRow3.AddCell(new PdfPCell(new Phrase("อาหารที่แพ้", cnb)) { Border = Rectangle.NO_BORDER });
            healthInfoRow3.AddCell(new PdfPCell(new Phrase(restrict_food, cni)) { Border = Rectangle.NO_BORDER });
            healthInfoRow3.AddCell(new PdfPCell(new Phrase("ความสามารถพิเศษ", cnb)) { Border = Rectangle.NO_BORDER });
            healthInfoRow3.AddCell(new PdfPCell(new Phrase(special_skill, cni)) { Border = Rectangle.NO_BORDER });
            cell = new PdfPCell(healthInfoRow3); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
            table.AddCell(cell);

            //Line
            table.AddCell(" "); //table.AddCell(" ");
            //Line
            line = new PdfPTable(3);
            line.DefaultCell.Border = Rectangle.NO_BORDER;
            line.DefaultCell.FixedHeight = 1f;
            line.SpacingBefore = 10f;
            line.TotalWidth = 400f;
            line.LockedWidth = true;
            line.SetWidths(new float[] { 5f, 295f, 15f });
            line.AddCell("");
            cell = new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, FixedHeight = 1f };
            cell.BorderWidthTop = 1f;
            line.AddCell(cell);
            line.AddCell("");
            table.AddCell(line);

            //new paragraph
            cell = new PdfPCell(new Phrase("ข้อมูลสถานที่ทำงาน", cnb));
            cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            if (work.Count > 0)
            {
                for (var i = 0; i < work.Count; i++)
                {
                    PdfPTable rowz = new PdfPTable(3);
                    rowz.DefaultCell.Border = Rectangle.NO_BORDER; rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.TotalWidth = 400f; rowz.LockedWidth = true;
                    rowz.SetWidths(new float[] { 8f, 40f, 150f });
                    rowz.AddCell(new PdfPCell(new Phrase(work[i].rec_no + ".", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase("ชื่อสถานที่ทำงาน", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(work[i].company, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);

                    if (i == 0)
                    {
                        cell = new PdfPCell(new Phrase("และประวัติการทำงาน", cnb));
                        cell.HorizontalAlignment = 2; cell.Border = Rectangle.NO_BORDER;
                        table.AddCell(cell);
                    }
                    else
                    {
                        table.AddCell("");
                    }

                    rowz = new PdfPTable(5);
                    rowz.DefaultCell.Border = Rectangle.NO_BORDER; rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.TotalWidth = 400f; rowz.LockedWidth = true;
                    rowz.SetWidths(new float[] { 8f, 20f, 90f, 25f, 40f });
                    rowz.AddCell("  ");
                    rowz.AddCell(new PdfPCell(new Phrase("ตำแหน่ง", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(work[i].position, cni)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase("ปีที่ทำงาน", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(work[i].year, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);
                    table.AddCell("");

                    rowz = new PdfPTable(3);
                    rowz.DefaultCell.Border = Rectangle.NO_BORDER; rowz.DefaultCell.VerticalAlignment = 1;
                    rowz.TotalWidth = 400f; rowz.LockedWidth = true;
                    rowz.SetWidths(new float[] { 10f, 18f, 200f });
                    rowz.AddCell(new PdfPCell(new Phrase(" ", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase("ที่อยู่", cnb)) { Border = Rectangle.NO_BORDER });
                    rowz.AddCell(new PdfPCell(new Phrase(work[i].address, cni)) { Border = Rectangle.NO_BORDER });
                    cell = new PdfPCell(rowz); cell.HorizontalAlignment = 0; cell.Border = Rectangle.NO_BORDER; cell.PaddingLeft = 30f;
                    table.AddCell(cell);
                    table.AddCell("");
                }
            }





            document.Add(table);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

        // GET: members/Create
        public IActionResult Create()
        {
            prepareViewBag();
            return View();
        }

        // POST: members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("member_code,birthdate,building,cid_card,cid_card_pic,cid_type,country_code,current_age,district_code,email,fax,floor,fname,h_no,lane,latitude,lname,longitude,lot_no,marry_status,mem_group_code,mem_photo,mem_type_code,mlevel_code,mobile,mstatus_code,nationality,parent_code,place_name,province_code,religion,room,rowversion,sex,social_app_data,street,subdistrict_code,tel,texta_address,textb_address,textc_address,village,x_log,x_note,x_status,zip_code,zone")] member member)


        public IActionResult Create(member member)
        {
            if (ModelState.IsValid)
            {
                member.x_status = "Y";
                if ((!string.IsNullOrEmpty(member.mem_photo)) && (member.mem_photo.Substring(0, 1) != "M"))
                {
                    var fileName = member.mem_photo.Substring(9);
                    var fileExt = Path.GetExtension(fileName);
                    pic_image m = new pic_image();
                    m.image_code = "M" + DateTime.Now.ToString("yyMMddhhmmssfffffff") + fileExt;
                    m.x_status = "Y";
                    m.image_name = fileName;

                    m.ref_doc_type = "member";
                    m.ref_doc_code = member.member_code;
                    fileName = m.image_code;
                    _context.pic_image.Add(m);
                    _context.SaveChanges();

                    var s = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_upload").Value), member.mem_photo);
                    var d = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_member").Value), fileName);
                    System.IO.File.Copy(s, d, true);
                    System.IO.File.Delete(s);
                    clearImageUpload();

                    member.mem_photo = m.image_code;
                }
                if ((!string.IsNullOrEmpty(member.cid_card_pic)) && (member.cid_card_pic.Substring(0, 1) != "C"))
                {
                    var fileName = member.cid_card_pic.Substring(9);
                    var fileExt = Path.GetExtension(fileName);
                    pic_image pic_image = new pic_image();
                    pic_image.image_code = "C" + DateTime.Now.ToString("yyMMddhhmmssfffffff") + fileExt;
                    pic_image.x_status = "Y";
                    pic_image.image_name = fileName;
                    pic_image.ref_doc_type = "cidcard";
                    pic_image.ref_doc_code = member.member_code;
                    fileName = pic_image.image_code;
                    _context.pic_image.Add(pic_image);
                    _context.SaveChanges();

                    var s = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_upload").Value), member.cid_card_pic);
                    var d = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_member").Value), fileName);
                    System.IO.File.Copy(s, d, true);
                    System.IO.File.Delete(s);
                    clearImageUpload();

                    member.cid_card_pic = pic_image.image_code;
                }
                _context.member.Add(member);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            prepareViewBag();
            return View(member);
        }

        // GET: members/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            member member = _context.member.Single(m => m.id == new Guid(id));
            if (member == null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(member.mem_photo))
            {
                //pic_image m = _context.pic_image.Single(i => i.image_code == member.mem_photo);
                //ViewBag.memPhoto = m.image_name;
                ViewBag.memPhoto = member.mem_photo;
            }

            if (!String.IsNullOrEmpty(member.cid_card_pic))
            {
                //pic_image c = _context.pic_image.Single(i => i.image_code == member.cid_card_pic);
                //ViewBag.cidCardPhoto =c.image_name;
                ViewBag.cidCardPhoto = member.cid_card_pic;
            }

            ViewBag.memberId = id;

            //For dropdown province; we need to manually assign it, then prepare viewbag for javascript
            ViewBag.zipCode = member.zip_code;
            ViewBag.subdistrictCode = member.subdistrict_code;
            ViewBag.districtCode = member.district_code;
            //ViewBag.provinceCode = member.province_code;



            prepareViewBag();
            clearImageUpload();
            return View(member);
        }

        // POST: members/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("member_code,birthdate,building,cid_card,cid_card_pic,cid_type,country_code,current_age,district_code,email,fax,floor,fname,h_no,id,lane,latitude,lname,longitude,lot_no,marry_status,mem_group_code,mem_photo,mem_type_code,mlevel_code,mobile,mstatus_code,nationality,parent_code,place_name,province_code,religion,room,rowversion,sex,social_app_data,street,subdistrict_code,tel,texta_address,textb_address,textc_address,village,x_log,x_note,x_status,zip_code,zone")] member member)
        {
            if (ModelState.IsValid)
            {
                member.x_status = "Y";

                if ((!string.IsNullOrEmpty(member.mem_photo)) && (member.mem_photo.Substring(0,1) != "M"))
                {
                    var fileName = member.mem_photo.Substring(9);
                    var fileExt = Path.GetExtension(fileName);
                    pic_image m = new pic_image();
                    m.image_code = "M" + DateTime.Now.ToString("yyMMddhhmmssfffffff") + fileExt;
                    m.x_status = "Y";
                    m.image_name = fileName;

                    m.ref_doc_type = "member";
                    m.ref_doc_code = member.member_code;
                    fileName = m.image_code;
                    _context.pic_image.Add(m);
                    _context.SaveChanges();

                    var s = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_upload").Value), member.mem_photo);
                    var d = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_member").Value), fileName);
                    System.IO.File.Copy(s, d, true);
                    System.IO.File.Delete(s);
                    clearImageUpload();

                    member.mem_photo = m.image_code;
                }
                if ((!string.IsNullOrEmpty(member.cid_card_pic)) && (member.cid_card_pic.Substring(0, 1) != "C"))
                {
                    var fileName = member.cid_card_pic.Substring(9);
                    var fileExt = Path.GetExtension(fileName);
                    pic_image pic_image = new pic_image();
                    pic_image.image_code = "C" + DateTime.Now.ToString("yyMMddhhmmssfffffff") + fileExt;
                    pic_image.x_status = "Y";
                    pic_image.image_name = fileName;
                    pic_image.ref_doc_type = "cidcard";
                    pic_image.ref_doc_code = member.member_code;
                    fileName = pic_image.image_code;
                    _context.pic_image.Add(pic_image);
                    _context.SaveChanges();

                    var s = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_upload").Value), member.cid_card_pic);
                    var d = Path.Combine(Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_member").Value), fileName);
                    System.IO.File.Copy(s, d, true);
                    System.IO.File.Delete(s);
                    clearImageUpload();

                    member.cid_card_pic = pic_image.image_code;
                }


                _context.Update(member);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            prepareViewBag();
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> uploadMemPhoto(ICollection<IFormFile> fileMemPhoto)
        {
            var uploads = Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_upload").Value);
            var filePrefix = DateTime.Now.ToString("ddhhmmss") + "_";
            var fileName = ""; var fileExt = "";
            foreach (var file in fileMemPhoto)
            {
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileExt = Path.GetExtension(fileName);
                    fileName = fileName.Substring(0, (fileName.Length <= (50 - fileExt.Length) ? fileName.Length : (50 - fileExt.Length))) + fileExt;
                    using (var SourceStream = file.OpenReadStream())
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, filePrefix + fileName), FileMode.Create))
                        {
                            await SourceStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
            return Json(new { result = "success", uploads = uploads, fileName = filePrefix + fileName });
        }


        [HttpPost]
        public async Task<IActionResult> uploadCidCardPhoto(ICollection<IFormFile> fileCidCardPhoto)
        {
            var uploads = Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_upload").Value);
            var filePrefix = DateTime.Now.ToString("ddhhmmss") + "_";
            var fileName = ""; var fileExt = "";

            foreach (var file in fileCidCardPhoto)
            {
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileExt = Path.GetExtension(fileName);
                    fileName = fileName.Substring(0, (fileName.Length <= (50 - fileExt.Length) ? fileName.Length : (50 - fileExt.Length))) + fileExt;
                    using (var SourceStream = file.OpenReadStream())
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, filePrefix + fileName), FileMode.Create))
                        {
                            await SourceStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
            return Json(new { result = "success", uploads = uploads, fileName = filePrefix + fileName });
        }

        private void clearImageUpload()
        {
            var uploads = Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_upload").Value);

            string[] dirs = Directory.GetFiles(uploads);
            System.Diagnostics.Debug.WriteLine("The number of files starting with c is {0}.", dirs.Length);
            foreach (string dir in dirs)
            {
                var f = dir;
                System.Diagnostics.Debug.WriteLine(dir);
            }
        }

        [HttpGet]
        public IActionResult ListTabNoData(string memberId)
        {
            var memberCode = _context.member.SingleOrDefault(m => m.id == new Guid(memberId)).member_code;
            List<tab> t = new List<tab>();
            if (_context.mem_train_record.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_train_record", value = 0 });
            }
            if (_context.mem_site_visit.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_site_visit", value = 0 });
            }
            if (_context.mem_social.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_social", value = 0 });
            }
            if (_context.mem_reward.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_reward", value = 0 });
            }
            if (_context.mem_education.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_education", value = 0 });
            }
            if (_context.mem_health.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_health", value = 0 });
            }
            if (_context.mem_worklist.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_worklist", value = 0 });
            }
            if (_context.mem_product.Where(mt => mt.member_code == memberCode).Count() == 0)
            {
                t.Add(new tab { name = "mem_product", value = 0 });
            }
            return Json(JsonConvert.SerializeObject(t));
        }

        private class listTraining
        {
            public string rec_no { get; set; }
            public string code { get; set; }
            public string desc { get; set; }
            public string grade { get; set; }
        }
        private class listInfo
        {
            public string rec_no { get; set; }
            public string desc { get; set; }
        }
        private class listWork
        {
            public string rec_no { get; set; }
            public string company { get; set; }
            public string position { get; set; }
            public string year { get; set; }
            public string address { get; set; }
        }
    }

    //Json for javascript to check which tabs are no data; see ListTabNoData action in this class
    public class tab
    {
        public string name;
        public int value;
    }
}
