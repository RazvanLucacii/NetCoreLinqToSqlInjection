using Microsoft.AspNetCore.Http.HttpResults;
using NetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

#region PROCEDIMIENTOS ALMACENADOS

//INSERTAR PERSONAJE
//create or replace procedure SP_INSERT_PERSONAJE
//(p_idPersonaje PERSONAJES.IDPERSONAJE%TYPE, p_nombre PERSONAJES.PERSONAJE%TYPE, p_imagen PERSONAJES.IMAGEN%TYPE)
//as
//begin
//  insert into PERSONAJES VALUES(p_idPersonaje, p_nombre, p_imagen);
//commit;
//end;


//MODIFICAR PERSONAJE
//create or replace procedure SP_MODIFICAR_PERSONAJE
//(p_idPersonaje PERSONAJES.IDPERSONAJE%TYPE, p_nombre PERSONAJES.PERSONAJE%TYPE, p_imagen PERSONAJES.IMAGEN%TYPE)
//as
//begin
//  update PERSONAJES set PERSONAJE = p_nombre, IMAGEN = p_imagen
//  where IDPERSONAJE = p_idPersonaje;
//commit;
//end;


//DELETE PERSONAJE
//create or replace procedure SP_DELETE_PERSONAJE
//(p_idPersonaje PERSONAJES.IDPERSONAJE%TYPE)
//as
//begin
//  delete from PERSONAJES where IDPERSONAJE = p_idPersonaje;
//commit;
//end;

#endregion


namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryPersonajesOracle: IRepositoryPersonajes
    {
        private DataTable tablaPersonajes;
        private OracleCommand command;
        private OracleConnection connection;

        public RepositoryPersonajesOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True; User Id=SYSTEM; Password=oracle";
            this.connection = new OracleConnection(connectionString);
            this.command = new OracleCommand();
            this.command.Connection = this.connection;
            string sql = "select * from PERSONAJES";
            OracleDataAdapter adPj = new OracleDataAdapter(sql, this.connection);
            this.tablaPersonajes = new DataTable();
            adPj.Fill(this.tablaPersonajes);
        }

        public List<Personaje> GetPersonajes()
        {
            var consulta = from datos in this.tablaPersonajes.AsEnumerable()
                           select datos;
            List<Personaje> personajes = new List<Personaje>();
            foreach (var row in consulta)
            {
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = row.Field<int>("IDPERSONAJE");
                personaje.Nombre = row.Field<string>("PERSONAJE");
                personaje.Imagen = row.Field<string>("IMAGEN");
                personajes.Add(personaje);
            }
            return personajes;
        }

        public Personaje FindPersonaje(int idPersonaje)
        {
            var consulta = from datos in this.tablaPersonajes.AsEnumerable()
                           where datos.Field<int>("IDPERSONAJE") == idPersonaje
                           select datos;
            var row = consulta.First();
            Personaje personaje = new Personaje();
            personaje.IdPersonaje = row.Field<int>("IDPERSONAJE");
            personaje.Nombre = row.Field<string>("PERSONAJE");
            personaje.Imagen = row.Field<string>("IMAGEN");
            return personaje;
        }

        public void InsertPersonaje(int idPersonaje, string nombre, string imagen)
        {
            OracleParameter pamidPersonaje = new OracleParameter(":p_idPersonaje", idPersonaje);
            this.command.Parameters.Add(pamidPersonaje);
            OracleParameter pamNombre = new OracleParameter(":p_nombre", nombre);
            this.command.Parameters.Add(pamNombre);
            OracleParameter pamImagen = new OracleParameter(":p_imagen", imagen);
            this.command.Parameters.Add(pamImagen);
            this.command.CommandText = "SP_INSERT_PERSONAJE";
            this.command.CommandType = CommandType.StoredProcedure;
            this.connection.Open();
            int af = this.command.ExecuteNonQuery();
            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public void ModificarPersonaje(int idPersonaje, string nombre, string imagen)
        {
            OracleParameter pamidPersonaje = new OracleParameter(":p_idPersonaje", idPersonaje);
            this.command.Parameters.Add(pamidPersonaje);
            OracleParameter pamNombre = new OracleParameter(":p_nombre", nombre);
            this.command.Parameters.Add(pamNombre);
            OracleParameter pamImagen = new OracleParameter(":p_imagen", imagen);
            this.command.Parameters.Add(pamImagen);
            this.command.CommandText = "SP_MODIFICAR_PERSONAJE";
            this.command.CommandType = CommandType.StoredProcedure;
            this.connection.Open();
            int af = this.command.ExecuteNonQuery();
            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public void DeletePersonaje(int idPersonaje)
        {
            OracleParameter pamidPersonaje = new OracleParameter(":p_idPersonaje", idPersonaje);
            this.command.Parameters.Add(pamidPersonaje);
            this.command.CommandText = "SP_DELETE_PERSONAJE";
            this.command.CommandType = CommandType.StoredProcedure;
            this.connection.Open();
            this.command.ExecuteNonQuery();
            this.connection.Close();
            this.command.Parameters.Clear();
        }
    }
}
