using Microsoft.AspNetCore.Mvc;
using SistemaGp.Datos;
using SistemaGp.Models;

namespace SistemaGp.Controllers
{
    public class TipoAplicacionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TipoAplicacionController(ApplicationDbContext db)
        {
            //esto es para poder acceder al dbcontext y sus datos
            _db = db;
        }

        public IActionResult Index()
        {
            //vamos a extraer los datos por medio de una lista
            IEnumerable<TipoAplicacion> lista = _db.TipoAplicacion;
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
        public IActionResult Crear(TipoAplicacion tipoAplicacion)
        {

            _db.TipoAplicacion.Add(tipoAplicacion);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //metodo get para Editar
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.TipoAplicacion.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //metodo post para actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(TipoAplicacion tipoAplicacion)
        {
            if (ModelState.IsValid)
            {
                //solo si es verdadero
                _db.TipoAplicacion.Update(tipoAplicacion);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(tipoAplicacion);

        }


        //metodo get para Eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.TipoAplicacion.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //metodo post para Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(TipoAplicacion tipoAplicacion)
        {
            if (tipoAplicacion == null)
            {
                return NotFound();

            }

            //solo si es verdadero
            _db.TipoAplicacion.Remove(tipoAplicacion);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
