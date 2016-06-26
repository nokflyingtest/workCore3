using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPcore.Models;
using System.Data.SqlClient;
using PPcore.Services;

namespace PPcore.Controllers
{
    public class registersController : Controller
    {
        private readonly PalangPanyaDBContext _context;
        private readonly IEmailSender _emailSender;

        public registersController(PalangPanyaDBContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // POST: registers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(string birthdate, string cid_card, string email, string fname, string lname, string mobile)
        {
            DateTime bd = Convert.ToDateTime(birthdate);
            //birthdate = (bd.Year).ToString() + bd.Month.ToString() + bd.Day.ToString();
            birthdate = (bd.Year).ToString() + bd.ToString("MMdd");
            string password = cid_card.Substring(cid_card.Length - 4);
            try
            {
                _context.Database.ExecuteSqlCommand("INSERT INTO member (member_code,cid_card,birthdate,fname,lname,mobile,email,x_status,password) VALUES ('"+ cid_card + "','" + cid_card + "','" + birthdate + "',N'" + fname + "',N'" + lname + "','"+mobile+"','"+email+"','Y','"+password+"')");
                _emailSender.SendEmailAsync(email, "username and password", "Username: " + cid_card + "\nPassword: " + password);
            }
            catch (SqlException ex)
            {
                var errno = ex.Number; var msg = "";
                if (errno == 2627) //Violation of primary key. Handle Exception
                {
                    msg = "duplicate";
                }
                return Json(new { result = "fail", error_code = errno, error_message = msg });
            }
            catch (Exception ex)
            {
                var errno = ex.HResult; var msg = "";
                if (ex.InnerException.Message.IndexOf("PRIMARY KEY") != -1)
                {
                    msg = "duplicate";
                }
                return Json(new { result = "fail", error_code = errno, error_message = msg });
            }

            return Json(new { result = "success" });
        }

        private bool memberExists(string id)
        {
            return _context.member.Any(e => e.member_code == id);
        }
    }
}
