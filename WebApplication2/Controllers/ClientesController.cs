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
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class ClientesController : Controller
    {
        private readonly PalestraContext _context;
        private readonly UserManager<AspNetUser> _userManager;

        public ClientesController(PalestraContext context, UserManager<AspNetUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Forbid();
            }

            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var result = await _userManager.FindByNameAsync(User.Identity.Name);
                if (result != null)
                {

                    var user_id = result.Id;
                    var clienti = _context.Clientes.Where(cl => cl.FkUtente == user_id).ToList();
                    return View(clienti);
                }
            }

            return Forbid();
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.FkUtenteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return PartialView("_Details", cliente);
        }

        // GET: Clientes/Create
        public async Task<IActionResult> CreateAsync()
        {
            
            var model = new ClientesV();
            string? id = await AuthenticationService.TakeIdByAuthenticationUserAsync(User, _userManager);
            if( id != null)
            {
                ViewData["FkUtente"] = id;
                return PartialView("_CreateCliente", model);
            }

            return Forbid();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientesV cliente)
        {
            if (ModelState.IsValid)
            {
                Cliente cl = new Cliente
                {
                    Nome = cliente.Nome,
                    Cognome = cliente.Cognome,
                    Email = cliente.Email,
                    HaveAccess = cliente.HaveAccess,
                    NumeroTelefono = cliente.NumeroTelefono,
                    FkUtente = cliente.FkUtente
                };

                _context.Add(cl);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cliente creato con successo!";
                return RedirectToAction(nameof(Index));
            }

            // Popola l'ErrorMessage con i dettagli degli errori di validazione
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage)
                                                 .ToList();

            TempData["ErrorMessage"] =  string.Join(", ", errorMessages);
            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            ClientesV cl = new ClientesV
            {
                Nome = cliente?.Nome,
                Cognome = cliente?.Cognome,
                Email = cliente?.Email,
                HaveAccess = cliente?.HaveAccess,
                NumeroTelefono = cliente?.NumeroTelefono,
                FkUtente = cliente?.FkUtente
            };

            ViewData["id"] = cliente.Id;
            return PartialView("_Edit", cl);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientesV cliente)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var client = await _context.Clientes.FindAsync(id);
                    if (client == null){

                        return NotFound();
                    }

                    client.Nome = cliente.Nome;
                    client.Cognome = cliente.Cognome;
                    client.NumeroTelefono = cliente.NumeroTelefono;
                    client.Email = cliente.Email;
                    client.HaveAccess = cliente.HaveAccess;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = "Cliente aggiornato con successo!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Si è verificato un errore. Aggiornamento dati non effettuato.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            string? idUser = await AuthenticationService.TakeIdByAuthenticationUserAsync(User, _userManager);
            if (cliente != null && cliente.FkUtente == idUser)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cliente eliminato con successo!";
            return Json(new { redirectToUrl = Url.Action("Index") });
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
