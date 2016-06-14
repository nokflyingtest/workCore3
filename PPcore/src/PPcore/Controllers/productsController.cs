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
        public IActionResult SelectBy(string product_group_code, string product_type_code)
        {
            if (product_type_code == null)
            {
                return NotFound();
            }
            //product_type_code = product_type_code.PadRight(3);
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

        // GET: products
        public async Task<IActionResult> Index()
        {
            return View(await _context.product.ToListAsync());
        }

        // GET: products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var product = await _context.product.SingleOrDefaultAsync(m => m.product_code == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_code,id,product_desc,product_group_code,product_type_code,rec_no,rowversion,x_log,x_note,x_status")] product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var product = await _context.product.SingleOrDefaultAsync(m => m.product_code == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("product_code,id,product_desc,product_group_code,product_type_code,rec_no,rowversion,x_log,x_note,x_status")] product product)
        {
            if (id != product.product_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!productExists(product.product_code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var product = await _context.product.SingleOrDefaultAsync(m => m.product_code == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.product.SingleOrDefaultAsync(m => m.product_code == id);
            _context.product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool productExists(int id)
        {
            return _context.product.Any(e => e.product_code == id);
        }

        private class productItem
        {
            public int rec_no;
            public int product_code;
            public string product_desc;
        }
    }
}
