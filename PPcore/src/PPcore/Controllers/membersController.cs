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

            ViewBag.sex = new SelectList(new[] { new { Value = "F", Text = "Female" }, new { Value = "M", Text = "Male" }, new { Value = "A", Text = "Alternative" } }, "Value", "Text", "F");
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
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();
            document.Add(new Paragraph("Hello World"));
            document.Add(new Paragraph(DateTime.Now.ToString()));
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
            return Json(JsonConvert.SerializeObject(t));
        }
    }

    //Json for javascript to check which tabs are no data; see ListTabNoData action in this class
    public class tab
    {
        public string name;
        public int value;
    }
}
