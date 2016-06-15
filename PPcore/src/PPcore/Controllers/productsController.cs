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

        [HttpGet]
        public IActionResult DetailsAsList(string product_group_code, string product_type_code, string pattern)
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
        public IActionResult Create(string product_desc, string product_group_code, string product_type_code)
        {
            _context.Database.ExecuteSqlCommand("INSERT INTO product (product_group_code,product_type_code,product_desc,x_status) VALUES ('"+ product_group_code + "','"+ product_type_code + "','"+ product_desc + "','Y')");
            var cP = _context.product.Count();
            var cR = _context.product.Where(pr => (pr.product_group_code == product_group_code) && (pr.product_type_code == product_type_code)).Count();
            return Json(new {result = "success", rec_no = cR, product_code = cP, product_desc = product_desc });
        }

        private class productItem
        {
            public int rec_no;
            public string product_code;
            public string product_desc;
        }
    }
}
