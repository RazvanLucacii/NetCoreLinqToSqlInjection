using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        private IRepositoryDoctores repoDoc;

        public DoctoresController(IRepositoryDoctores repo)
        {
            repoDoc = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repoDoc.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            this.repoDoc.InsertDoctor(doctor.IdDoctor, doctor.Apellido, doctor.Especialidad
                , doctor.Salario, doctor.IdHospital);
            return RedirectToAction("Index");
        }
    }
}
