using Microsoft.AspNetCore.Mvc;
using SistemaGp.Datos;
using SistemaGp.Models;

namespace SistemaGp.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoriaController(ApplicationDbContext db)
        {
            //esto es para poder acceder al dbcontext y sus datos
            _db = db;
        }

        public IActionResult Index()
        {
            //vamos a extraer los datos por medio de una lista
            IEnumerable<Categoria> lista = _db.Categoria;
            //IEnumerable<Categoria> lista = _db.Categoria; esto es lo que le vamos
            //a enviar al view

            return View(lista);
        }

        //metodo get para agregar categoria
        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                //solo si es verdadero
                _db.Categoria.Add(categoria);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(categoria);

        }

        //metodo get para Editar
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Categoria.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //metodo post para actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                //solo si es verdadero
                _db.Categoria.Update(categoria);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(categoria);

        }


        //metodo get para Eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Categoria.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //metodo post para Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Categoria categoria)
        {
            if (categoria == null)
            {
                return NotFound();

            }

            //solo si es verdadero
            _db.Categoria.Remove(categoria);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
