using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.ModelsV;

namespace WebApplication2.Controllers
{
    public class SchedumsController : Controller
    {
        private readonly PalestraContext _context;
        private readonly UserManager<AspNetUser> _userManager;

        public SchedumsController(PalestraContext context, UserManager<AspNetUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Schedums
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if( user != null)
                {
                    var user_id = user.Id;
                    var schede = await _context.Scheda.Where(s => s.FkUtente == user_id).ToListAsync();
                    
                    return View(schede);
                }
            }
            return Forbid();
        }

        // GET: Schedums/Create
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if( user != null)
                {
                    var user_id = user.Id;
                    var clienti = await _context.Clientes.Where(fkU => fkU.FkUtente == user_id).ToListAsync();
                    ViewData["clienti"] = clienti;
                    return View();
                }
               
            }

            return Forbid();
        }

        // POST: Schedums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataInizio,DataFine,Note,FkUtente,FkCliente")] Schedum schedum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schedum);
        }

        // GET: Schedums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedum = await _context.Scheda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedum == null)
            {
                return NotFound();
            }

            return View(schedum);
        }

        // GET: Schedums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedum = await _context.Scheda.FindAsync(id);
            if (schedum == null)
            {
                return NotFound();
            }
            return View(schedum);
        }

        // POST: Schedums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataInizio,DataFine,Note,FkUtente,FkCliente")] Schedum schedum)
        {
            if (id != schedum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchedumExists(schedum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schedum);
        }

        // GET: Schedums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedum = await _context.Scheda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedum == null)
            {
                return NotFound();
            }

            return View(schedum);
        }

        // POST: Schedums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedum = await _context.Scheda.FindAsync(id);
            if (schedum != null)
            {
                _context.Scheda.Remove(schedum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchedumExists(int id)
        {
            return _context.Scheda.Any(e => e.Id == id);
        }
    }
}
