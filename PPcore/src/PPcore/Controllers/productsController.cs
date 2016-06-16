using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPcore.Models;
using Newtonsoft.Json;

namespace PPcore.Controllers
{
    public class productsController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public productsController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: mem_product/ListProduct
        public IActionResult DetailsAsTableList(string memberId)
        {
            ViewBag.product_group = new SelectList(_context.product_group.OrderBy(p => p.product_group_desc), "product_group_code", "product_group_desc", "1");

            List<ViewModels.mem_product.mem_productViewModel> mem_productViewModels = new List<ViewModels.mem_product.mem_productViewModel>();
            var member = _context.member.Single(m => m.id == new Guid(memberId));
            var mem_products = _context.mem_product.Where(m => m.member_code == member.member_code).OrderBy(m => m.rec_no).ToList();
            foreach (var mp in mem_products)
            {
                var mem_productViewModel = new ViewModels.mem_product.mem_productViewModel();
                mem_productViewModel.mem_product = mp;
                var product = _context.product.Single(p => p.product_code == mp.product_code);
                mem_productViewModel.product = product;
                mem_productViewModel.product_group_desc = _context.product_group.Single(p => p.product_group_code == product.product_group_code).product_group_desc;
                mem_productViewModel.product_type_desc = _context.product_type.Single(p => (p.product_type_code == product.product_type_code) && (p.product_group_code == product.product_group_code)).product_type_desc;
                mem_productViewModels.Add(mem_productViewModel);
            }
            ViewBag.memberId = memberId;
            //ViewBag.course_grade = new SelectList(_context.course_grade, "cgrade_code", "cgrade_desc");(x.Body.Scopes.Count > 5) && (x.Foo == "test")
            return View(mem_productViewModels);
        }

        [HttpGet]
        public IActionResult DetailsAsJsonList(string product_group_code, string product_type_code, string pattern)
        {
            if (product_type_code == null)
            {
                return NotFound();
            }
            //product_type_code = product_type_code.PadRight(3);
            //_context.Database.ExecuteSqlCommand("INSERT INTO product (product_group_code,product_type_code,product_desc,x_status) VALUES ('" + product_group_code + "','" + product_type_code + "','" + product_desc + "','Y')");
            var sqlProduct = "SELECT * FROM product WHERE product_group_code = '"+ product_group_code + "'";
            //var products = _context.product.FromSql(sqlProduct).ToList();
            //if (pattern.Trim() == "") pattern = "%";
            var products = _context.product.Where(pr => (pr.product_group_code == product_group_code) && (pr.product_type_code == product_type_code) && (pr.x_status == "Y")).OrderBy(pr => pr.rec_no).ToList();
            if (products == null)
            {
                return NotFound();
            }
            List<productItem> p = new List<productItem>();
            foreach (var product in products)
            {
                p.Add(new productItem { rec_no = product.rec_no, product_code = product.product_code, product_desc = product.product_desc });
            }
            string pjson = JsonConvert.SerializeObject(p);
            return Json(pjson);
        }

        // GET: products/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string product_code, string product_desc, string product_group_code, string product_type_code)
        {
            _context.Database.ExecuteSqlCommand("INSERT INTO product (product_group_code,product_type_code,product_code,product_desc,x_status) VALUES ('" + product_group_code + "','" + product_type_code + "','" + product_code + "','" + product_desc + "','Y')");
            //var cP = _context.product.Count();
            var cR = _context.product.Where(pr => (pr.product_group_code == product_group_code) && (pr.product_type_code == product_type_code)).Count();
            return Json(new { result = "success", rec_no = cR, product_code = product_code, product_desc = product_desc });
        }

        private class productItem
        {
            public int rec_no;
            public string product_code;
            public string product_desc;
        }
    }
}
