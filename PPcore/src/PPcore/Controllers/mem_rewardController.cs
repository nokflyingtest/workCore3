using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;

namespace PPcore.Controllers
{
    public class mem_rewardController : Controller
    {
        private PalangPanyaDBContext _context;

        public mem_rewardController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: mem_reward
        public IActionResult Index(string memberId, string v)
        {
            List<Models.mem_reward> mem_rewards = new List<Models.mem_reward>();
            var member = _context.member.Single(m => m.id == new Guid(memberId));
            mem_rewards = _context.mem_reward.Where(m => m.member_code == member.member_code).OrderBy(m => m.rec_no).ToList();

            ViewBag.memberId = memberId;
            if (!String.IsNullOrEmpty(v)) { ViewBag.isViewOnly = 1; } else { ViewBag.isViewOnly = 0; }
            return View(mem_rewards);
        }

        // GET: mem_reward/Details/5
        public IActionResult Details(string id)
        {
            if (id != null)
            {
                mem_reward mr = _context.mem_reward.Single(m => m.id == new Guid(id));
                if (mr != null)
                {
                    return Json(new { id = mr.id, rec_no = mr.rec_no, reward_desc = mr.reward_desc });
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

        // POST: mem_reward/Create
        [HttpPost]
        public IActionResult Create(string memberId, int rec_no, string reward_desc)
        {
            var member = _context.member.Single(m => m.id == new Guid(memberId));

            var mem_reward = new mem_reward();
            mem_reward.member_code = member.member_code;
            mem_reward.rec_no = rec_no;
            mem_reward.reward_desc = reward_desc;
            mem_reward.x_status = "Y";

            _context.mem_reward.Add(mem_reward);
            _context.SaveChanges();

            return Json(new { result = "success" });
        }

        // GET: mem_reward/Edit/5
        public IActionResult Edit(string id)
        {
            return Details(id);
        }

        // POST: mem_reward/Edit/5
        [HttpPost]
        public IActionResult Edit(string memberId, string id, int rec_no, string reward_desc)
        {
            var member = _context.member.Single(m => m.id == new Guid(memberId));
            var mem_reward = _context.mem_reward.Single(m => m.id == new Guid(id));
            mem_reward.reward_desc = reward_desc;
            _context.Update(mem_reward);

            _context.SaveChanges();

            return Json(new { result = "success" });
        }
    }
}
