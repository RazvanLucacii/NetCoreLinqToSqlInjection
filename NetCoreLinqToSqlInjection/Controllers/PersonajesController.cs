using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class PersonajesController : Controller
    {
        private IRepositoryPersonajes repoPj;

        public PersonajesController(IRepositoryPersonajes repo)
        {
            this.repoPj = repo;
        }
        public IActionResult Index()
        {
            List<Personaje> personajes = this.repoPj.GetPersonajes();
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Personaje personaje)
        {
            this.repoPj.InsertPersonaje(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }
    }
}
