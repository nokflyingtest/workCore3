using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPcore.Models;

namespace PPcore.Controllers
{
    public class registersController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public registersController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // POST: registers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("member_code,birthdate,building,cid_card,cid_card_pic,cid_type,country_code,current_age,district_code,email,fax,floor,fname,h_no,id,lane,latitude,lname,longitude,lot_no,marry_status,mem_group_code,mem_photo,mem_type_code,mlevel_code,mobile,mstatus_code,nationality,parent_code,place_name,province_code,religion,room,rowversion,sex,social_app_data,street,subdistrict_code,tel,texta_address,textb_address,textc_address,village,x_log,x_note,x_status,zip_code,zone")] member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        private bool memberExists(string id)
        {
            return _context.member.Any(e => e.member_code == id);
        }
    }
}
