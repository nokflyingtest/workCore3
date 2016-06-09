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
    public class mem_productController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public mem_productController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: mem_product
        public IActionResult Index(string memberId)
        {
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

        // GET: mem_product/ListProduct
        public IActionResult ListProduct(string memberId)
        {
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

        // GET: mem_product/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mem_product = await _context.mem_product.SingleOrDefaultAsync(m => m.product_code == id);
            if (mem_product == null)
            {
                return NotFound();
            }

            return View(mem_product);
        }

        // GET: mem_product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: mem_product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_code,member_code,grow_area,id,rowversion,x_log,x_note,x_status")] mem_product mem_product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mem_product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mem_product);
        }

        // GET: mem_product/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mem_product = await _context.mem_product.SingleOrDefaultAsync(m => m.product_code == id);
            if (mem_product == null)
            {
                return NotFound();
            }
            return View(mem_product);
        }

        // POST: mem_product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("product_code,member_code,grow_area,id,rowversion,x_log,x_note,x_status")] mem_product mem_product)
        {
            if (id != mem_product.product_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mem_product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mem_productExists(mem_product.product_code))
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
            return View(mem_product);
        }

        // GET: mem_product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mem_product = await _context.mem_product.SingleOrDefaultAsync(m => m.product_code == id);
            if (mem_product == null)
            {
                return NotFound();
            }

            return View(mem_product);
        }

        // POST: mem_product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mem_product = await _context.mem_product.SingleOrDefaultAsync(m => m.product_code == id);
            _context.mem_product.Remove(mem_product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool mem_productExists(string id)
        {
            return _context.mem_product.Any(e => e.product_code == id);
        }
    }
}
