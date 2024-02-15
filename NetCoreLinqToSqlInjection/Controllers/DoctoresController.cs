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

        public IActionResult DoctoresEspecialidad()
        {
            List<Doctor> doctores = this.repoDoc.GetDoctores();
            return View(doctores);
        }

        [HttpPost]
        public IActionResult DoctoresEspecialidad(string especialidad)
        {
            List<Doctor> doctores = this.repoDoc.GetDoctoresEspecialidad(especialidad);
            if (doctores == null)
            {
                ViewData["MENSAJE"] = "No existen doctores con esa especialidad";
                return View();
            }
            else
            {
                return View(doctores);
            }
        }

        public IActionResult DeleteDoctor(int iddoctor)
        {
            this.repoDoc.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }

        public IActionResult ModificarDoctor(int idDoctor)
        {
            Doctor doctor = this.repoDoc.FindDoctor(idDoctor);
            return View(doctor);
        }

        [HttpPost]
        public IActionResult ModificarDoctorPost(Doctor doctor)
        {
            this.repoDoc.ModificarDoctor(doctor.IdHospital, doctor.IdDoctor, doctor.Apellido, doctor.Especialidad, doctor.Salario);
            return RedirectToAction("Index");
        }
    }
}
