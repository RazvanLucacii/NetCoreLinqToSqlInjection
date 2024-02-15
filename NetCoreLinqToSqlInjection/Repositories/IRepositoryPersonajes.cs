using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryPersonajes
    {
        List<Personaje> GetPersonajes();

        void InsertPersonaje(int idPersonaje, string nombre, string imagen);

        void DeletePersonaje(int idPersonaje);

        void ModificarPersonaje(int idPersonaje, string nombre, string imagen);

        Personaje FindPersonaje(int idPersonaje);
    }
}
