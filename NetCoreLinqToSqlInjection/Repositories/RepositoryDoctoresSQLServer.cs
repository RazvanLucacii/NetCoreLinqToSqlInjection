using NetCoreLinqToSqlInjection.Models;
using System.Data;
using System.Data.SqlClient;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer : IRepositoryDoctores
    {
        private DataTable tablaDoctores;
        private SqlConnection connection;
        private SqlCommand command;

        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;
            this.tablaDoctores = new DataTable();
            string sql = "select * from DOCTOR";
            SqlDataAdapter adDoc = new SqlDataAdapter(sql, this.connection);
            adDoc.Fill(this.tablaDoctores);
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD"),

                };  
                doctores.Add(doc);
            }
            return doctores;
        }

        public void InsertDoctor(int id, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (@idhospital, @iddoctor, @apellido, @especialidad, @salario)";
            this.command.Parameters.AddWithValue("@idhospital", idHospital);
            this.command.Parameters.AddWithValue("@iddoctor", id);
            this.command.Parameters.AddWithValue("@apellido", apellido);
            this.command.Parameters.AddWithValue("@especialidad", especialidad);
            this.command.Parameters.AddWithValue("@salario", salario);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;
            this.connection.Open();
            int af = this.command.ExecuteNonQuery();
            this.connection.Close();
            this.command.Parameters.Clear();
        }
    }
}
