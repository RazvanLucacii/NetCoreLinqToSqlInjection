using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        void InsertDoctor(int id, string apellido, string especialidad, int salario, int idHospital);

        List<Doctor> GetDoctoresEspecialidad(string especialidad);

        void DeleteDoctor(int iddoctor);

        void ModificarDoctor(int idhospital, int iddoctor, string apellido, string especialidad, int salario);

        Doctor FindDoctor(int iddoctor);
    }
}
