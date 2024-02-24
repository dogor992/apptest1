using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quiz2Cenfotec.DataContracts;
using Quiz2Cenfotec.DatabaseContext;
using System.Reflection.Metadata;

namespace Quiz2Cenfotec.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {

            List<Cliente> clientes = new List<Cliente>();

            string sql = "EXEC paObtenerClientes";//procedimiento almacenado que trae los clientes
            clientes = _context.Clientes.FromSqlRaw<Cliente>(sql).ToList();

            return clientes != null ? 
                          View(clientes) :
                          Problem("Entity set 'ApplicationDbContext.Clientes'  is null.");
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellidos,Edad,Cedula,Preferencias")] Cliente cliente)
        {
            _dg_gen_resultado resultado = new _dg_gen_resultado();
            if (ModelState.IsValid)
            {
               resultado =  _context.InsertarCliente(cliente);
             
                if (resultado.CodError == "00")
                {
                    // El cliente se insertó correctamente. Puedes redirigir a una página de éxito o hacer lo que necesites.
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Ocurrió un error. Puedes manejarlo según tus necesidades.
                    ViewBag.ErrorMessage = resultado.Mensaje;
                    return View(cliente);
                }

            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellidos,Edad,Cedula,Preferencias")] Cliente cliente)
        {
            _dg_gen_resultado elResultado = new _dg_gen_resultado();
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(cliente);
                    //await _context.SaveChangesAsync();
                  elResultado = _context.ActualizarCliente(cliente);
                    if (elResultado.CodError == "00")
                    {
                        // El cliente se actualizó correctamente. Puedes redirigir a una página de éxito o hacer lo que necesites.
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Ocurrió un error. Puedes manejarlo según tus necesidades.
                        ViewBag.ErrorMessage = elResultado.Mensaje;
                        return View(cliente);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _dg_gen_resultado elResultado = new _dg_gen_resultado();
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                elResultado = _context.EliminarCliente(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
