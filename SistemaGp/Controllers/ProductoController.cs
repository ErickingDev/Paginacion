using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGp.Datos;
using SistemaGp.Models;
using SistemaGp.Models.ViewModels;
using System.IO;
using System.Linq;

namespace SistemaGp.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _db;

        //para las imagenes
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //AQUI VAMOS A LLAMAR A LA LISTA
            //con el include traemos los datos de categoria
            //tipo aplicacion

            IEnumerable<Producto> lista = _db.Producto.Include(c => c.Categoria)
                                                      .Include(t => t.TipoAplicacion);

            return View(lista);
        }

        public IActionResult Upsert(int? Id)
        {
            //IEnumerable<SelectListItem> categoriaDropDown = _db.Categoria.Select(c => new SelectListItem
            //{
            //    Text = c.NombreCategoria, 
            //    Value = c.Id.ToString()
            //});

            ////este va ahcer el llenado y lo vamos a utilizar en la vista
            ////upsert
            //ViewBag.categoriaDropDown = categoriaDropDown;


            //Producto producto = new Producto();

            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _db.Categoria.Select(c => new SelectListItem
                {
                    Text = c.NombreCategoria,
                    Value = c.Id.ToString()
                }),

                TipoAplicacionLista = _db.TipoAplicacion.Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                })
            };

            if (Id == null)
            {
                //crear nuevo producto
                return View(productoVM);
            }
            //si tiene el Id quiere decir que se esta llamando del boton editar
            else
            {
                productoVM.Producto = _db.Producto.Find(Id);
                if (productoVM.Producto == null)
                {
                    //no encontro el producto
                    return NotFound();
                }
                //si si encontro el producto
                return View(productoVM);
            }

        }

        //para utilizar el webHostEnvironment con http

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productoVM.Producto.Id == 0)
                {
                    //crear
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productoVM.Producto.ImagenUrl = fileName + extension;
                    _db.Producto.Add(productoVM.Producto);
                }
                else
                {
                    //actualizar

                    var objProducto = _db.Producto.AsNoTracking().FirstOrDefault(p => p.Id == productoVM.Producto.Id);
                    if (files.Count > 0)
                    {
                        // Se carga nueva Imagen
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrar imagen anterior

                        var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);

                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        //finalizadon para borrar la imagen anterior

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productoVM.Producto.ImagenUrl = fileName + extension;
                    }//caso contrario si no esta cargardo imagen nueva.
                    else
                    {
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _db.Producto.Update(productoVM.Producto);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }// if del model isvalid

            //esto es por si caso falla y devolvemos las listas al usuario
            //se llenan nuevamente las listas
            productoVM.CategoriaLista = _db.Categoria.Select(c => new SelectListItem
            {
                Text = c.NombreCategoria,
                Value = c.Id.ToString()
            });

            productoVM.TipoAplicacionLista = _db.TipoAplicacion.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });

            return View(productoVM);
        }


        //metodo eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Producto producto = _db.Producto.Include(c => c.Categoria)
                                            .Include(t => t.TipoAplicacion)
                                            .FirstOrDefault(p => p.Id == Id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        //post de eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Producto producto)
        {
            if (producto == null)
            {
                return NotFound();
            }
            //Eliminar Imagen
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;


            //borrar imagen anterior

            var anteriorFile = Path.Combine(upload, producto.ImagenUrl);

            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }
            //finalizadon para borrar la imagen anterior

            _db.Producto.Remove(producto);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

    }
}
