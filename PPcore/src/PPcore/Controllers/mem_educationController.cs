using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;

namespace PPcore.Controllers
{
    public class mem_educationController : Controller
    {
        private PalangPanyaDBContext _context;

        public mem_educationController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: mem_education
        public IActionResult Index(string memberId, string v)
        {
            List<Models.mem_education> mem_educations = new List<Models.mem_education>();
            var member = _context.member.Single(m => m.id == new Guid(memberId));
            mem_educations = _context.mem_education.Where(m => m.member_code == member.member_code).OrderBy(m => m.rec_no).ToList();

            ViewBag.memberId = memberId;
            if (!String.IsNullOrEmpty(v)) { ViewBag.isViewOnly = 1; } else { ViewBag.isViewOnly = 0; }
            return View(mem_educations);
        }

        // GET: mem_education/Details/5
        public IActionResult Details(string id)
        {
            if (id != null)
            {
                mem_education me = _context.mem_education.Single(m => m.id == new Guid(id));
                if (me != null)
                {
                    return Json(new { id = me.id, rec_no = me.rec_no, colledge_name = me.colledge_name, degree = me.degree, faculty = me.faculty});
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

        // POST: mem_education/Create
        [HttpPost]
        public IActionResult Create(string memberId, int rec_no, string colledge_name, string degree, string faculty)
        {
            var member = _context.member.Single(m => m.id == new Guid(memberId));

            var mem_education = new mem_education();
            mem_education.member_code = member.member_code;
            mem_education.rec_no = rec_no;
            mem_education.colledge_name = colledge_name;
            mem_education.degree = degree;
            mem_education.faculty = faculty;
            mem_education.x_status = "Y";

            _context.mem_education.Add(mem_education);
            _context.SaveChanges();

            return Json(new { result = "success" });
        }

        // GET: mem_education/Edit/5
        public IActionResult Edit(string id)
        {
            return Details(id);
        }

        // POST: mem_education/Edit/5
        [HttpPost]
        public IActionResult Edit(string memberId, string id, int rec_no, string colledge_name, string degree, string faculty)
        {
            var member = _context.member.Single(m => m.id == new Guid(memberId));
            var mem_education = _context.mem_education.Single(m => m.id == new Guid(id));
            mem_education.colledge_name = colledge_name;
            mem_education.degree = degree;
            mem_education.faculty = faculty;
            _context.Update(mem_education);

            _context.SaveChanges();

            return Json(new { result = "success" });
        }
    }
}
