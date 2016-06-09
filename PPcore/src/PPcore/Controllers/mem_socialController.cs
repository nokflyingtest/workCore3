using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;

namespace PPcore.Controllers
{
    public class mem_socialController : Controller
    {
        private PalangPanyaDBContext _context;

        public mem_socialController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: mem_social
        public IActionResult Index(string memberId, string v)
        {
            List<Models.mem_social> mem_socials = new List<Models.mem_social>();
            var member = _context.member.Single(m => m.id == new Guid(memberId));
            mem_socials = _context.mem_social.Where(m => m.member_code == member.member_code).OrderBy(m => m.rec_no).ToList();

            ViewBag.memberId = memberId;
            if (!String.IsNullOrEmpty(v)) { ViewBag.isViewOnly = 1; } else { ViewBag.isViewOnly = 0; }
            return View(mem_socials);
        }

        // GET: mem_social/Details/5
        public IActionResult Details(string id)
        {
            if (id != null)
            {
                mem_social ms = _context.mem_social.Single(m => m.id == new Guid(id));
                if (ms != null)
                {
                    return Json(new { id = ms.id, rec_no = ms.rec_no, social_desc = ms.social_desc});
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // POST: mem_social/Create
        [HttpPost]
        public IActionResult Create(string memberId, int rec_no, string social_desc)
        {
            var member = _context.member.Single(m => m.id == new Guid(memberId));

            var mem_social = new mem_social();
            mem_social.member_code = member.member_code;
            mem_social.rec_no = rec_no;
            mem_social.social_desc = social_desc;
            mem_social.x_status = "Y";

            _context.mem_social.Add(mem_social);
            _context.SaveChanges();

            return Json(new { result = "success" });
        }

        // GET: mem_social/Edit/5
        public IActionResult Edit(string id)
        {
            return Details(id);
        }

        // POST: mem_social/Edit/5
        [HttpPost]
        public IActionResult Edit(string memberId, string id, int rec_no, string social_desc)
        {
            var member = _context.member.Single(m => m.id == new Guid(memberId));
            var mem_social = _context.mem_social.Single(m => m.id == new Guid(id));
            mem_social.social_desc = social_desc;
            _context.Update(mem_social);

            _context.SaveChanges();

            return Json(new { result = "success" });
        }
    }
}
