using Microsoft.AspNetCore.Http.HttpResults;
using NetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

#region PROCEDIMIENTOS ALMACENADOS

//DELETE DOCTOR
//create or replace procedure sp_delete_doctor
//(p_iddoctor DOCTOR.DOCTOR_NO%TYPE)
//as
//begin
//  delete from DOCTOR where DOCTOR_NO = p_iddoctor;
//commit;
//end;

//MODIFICAR DOCTOR
//create or replace procedure sp_modificar_doctor
//(p_idhospital DOCTOR.HOSPITAL_COD%TYPE, p_iddoctor DOCTOR.DOCTOR_NO%TYPE, p_apellido DOCTOR.APELLIDO%TYPE, p_especialidad DOCTOR.ESPECIALIDAD%TYPE
//, p_salario DOCTOR.SALARIO%TYPE)
//as
//begin
//  update DOCTOR set HOSPITAL_COD = p_idhospital, APELLIDO = p_apellido,
//  ESPECIALIDAD = p_especialidad, SALARIO = p_salario 
//  where DOCTOR_NO = p_iddoctor;
//commit;
//end;

#endregion

namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresOracle : IRepositoryDoctores
    {
        private DataTable tableDoctores;
        private OracleConnection connection;
        private OracleCommand command;

        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True; User Id=SYSTEM; Password=oracle";
            this.connection = new OracleConnection(connectionString);
            this.command = new OracleCommand();
            this.command.Connection = this.connection;
            string sql = "select * from DOCTOR";
            OracleDataAdapter adDoct = new OracleDataAdapter(sql, this.connection);
            this.tableDoctores = new DataTable();
            adDoct.Fill(this.tableDoctores);
        }

        public void DeleteDoctor(int iddoctor)
        {
            OracleParameter pamIdDoctor = new OracleParameter(":p_iddoctor", iddoctor);
            this.command.Parameters.Add(pamIdDoctor);
            this.command.CommandText = "sp_delete_doctor";
            this.command.CommandType = CommandType.StoredProcedure;
            this.connection.Open();
            this.command.ExecuteNonQuery();
            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
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

        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD").ToUpper() == especialidad.ToUpper()
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Doctor> doctores = new List<Doctor>();
                foreach(var row in consulta)
                {
                    Doctor doc = new Doctor
                    {
                        IdDoctor = row.Field<int>("DOCTOR_NO"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Especialidad = row.Field<string>("ESPECIALIDAD"),
                        Salario = row.Field<int>("SALARIO"),
                        IdHospital = row.Field<int>("HOSPITAL_COD")
                    };
                    doctores.Add (doc);
                }
                return doctores;
            }
        }

        public void InsertDoctor(int id, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (:idhospital, :iddoctor, :apellido, :especialidad, :salario)";

            OracleParameter pamIdHospital = new OracleParameter(":idhospital", idHospital);
            this.command.Parameters.Add(pamIdHospital);
            OracleParameter pamIdDoctor = new OracleParameter(":iddoctor", id);
            this.command.Parameters.Add(pamIdDoctor);
            OracleParameter pamApellido = new OracleParameter(":apellido", apellido);
            this.command.Parameters.Add(pamApellido);
            OracleParameter pamEspecialidad = new OracleParameter(":especialidad", especialidad);
            this.command.Parameters.Add(pamEspecialidad);
            OracleParameter pamSalario = new OracleParameter(":salario", salario);
            this.command.Parameters.Add(pamSalario);

            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;

            this.connection.Open();
            int af = this.command.ExecuteNonQuery();
            this.connection.Close();

            this.command.Parameters.Clear();
        }

        public void ModificarDoctor(int idhospital, int iddoctor, string apellido, string especialidad, int salario)
        {
            OracleParameter pamIdHospital = new OracleParameter(":p_idhospital", idhospital);
            this.command.Parameters.Add(pamIdHospital);
            OracleParameter pamIdDoctor = new OracleParameter(":p_iddoctor", iddoctor);
            this.command.Parameters.Add(pamIdDoctor);
            OracleParameter pamApellido = new OracleParameter(":p_apellido", apellido);
            this.command.Parameters.Add(pamApellido);
            OracleParameter pamEspecialidad = new OracleParameter(":p_especialidad", especialidad);
            this.command.Parameters.Add(pamEspecialidad);
            OracleParameter pamSalario = new OracleParameter(":p_salario", salario);
            this.command.Parameters.Add(pamSalario);

            this.command.CommandText = "sp_modificar_doctor";
            this.command.CommandType = CommandType.StoredProcedure;

            this.connection.Open();
            this.command.ExecuteNonQuery();
            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public Doctor FindDoctor(int iddoctor)
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == iddoctor
                           select datos;
            var row = consulta.First();
            Doctor doctor = new Doctor();
            doctor.IdHospital = row.Field<int>("HOSPITAL_COD");
            doctor.IdDoctor = row.Field<int>("DOCTOR_NO");
            doctor.Apellido = row.Field<string>("APELLIDO");
            doctor.Especialidad = row.Field<string>("ESPECIALIDAD");
            doctor.Salario = row.Field<int>("SALARIO");
            return doctor;
        }
    }
}
