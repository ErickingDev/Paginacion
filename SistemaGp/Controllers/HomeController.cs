using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGp.Datos;
using SistemaGp.Models;
using SistemaGp.Models.ViewModels;
using SistemaGp.Utilidades;
using System.Diagnostics;

namespace SistemaGp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //el db context para homevm
        private readonly ApplicationDbContext _db;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult About()
        {

            return View();
        }
        public IActionResult Team()
        {

            return View();
        }
        public IActionResult Shop()
        {

			HomeVM homeVM = new HomeVM()
			{
			    
			    Productos = _db.Producto.Include(c => c.Categoria).Include(t => t.TipoAplicacion),
				Categorias = _db.Categoria
			};

			return View(homeVM);
		}

        public IActionResult Privacy()
        {
            HomeVM homeVM = new HomeVM()
            {
                Productos = _db.Producto.Include(c => c.Categoria).Include(t => t.TipoAplicacion),
                Categorias = _db.Categoria
            };

            return View(homeVM);
        }


        public IActionResult Detalle(int Id)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null
                && HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }

            DetalleVM detalleVM = new DetalleVM()
            {
                Producto = _db.Producto.Include(c => c.Categoria).Include(t => t.TipoAplicacion)
                                       .Where(p => p.Id == Id).FirstOrDefault(),
                //Producto = _productoRepo.ObtenerPrimero(p => p.Id == Id, incluirPropiedades: "Categoria,TipoAplicacion"),
                ExisteEnCarro = false
            };

            foreach (var item in carroComprasLista)
            {
                //esto quiere decir que el producto esta agregado al carro de compras
                //y se activara el boton agregar carro si seleccionamos el mismo producto
                if (item.ProductoId == Id)
                {
                    detalleVM.ExisteEnCarro = true;
                }
            }

            return View(detalleVM);
        }

        //este detalle de tipo post es para el carrito de compras.
        [HttpPost, ActionName("Detalle")]
        //Detalle esto es cuando se invoque que va a llamar al post
        //para que al momento de agregar al carrito de comparas el icono cambien a (1)
        public IActionResult DetallePost(int Id)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null
                && HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            carroComprasLista.Add(new CarroCompra { ProductoId = Id });
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);

            return RedirectToAction(nameof(Index));
        }

        //este metodo es para cuando ya tenemos un producto agregado al carro y 
        //queremos eliminarlo.
        public IActionResult RemoverDeCarro(int Id)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null
                && HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }

            var productoARemover = carroComprasLista.SingleOrDefault(x => x.ProductoId == Id);
            if (productoARemover != null)
            {
                carroComprasLista.Remove(productoARemover);
            }

            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}