using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportarCSV
{
	public class PersonaRepository
	{
		string connectionString = "Server=localhost;Database=ImportarCsvBD;Trusted_Connection=True;MultipleActiveResultSets=true";

		public int Insert(Persona persona)
		{
			int idInsertado = 0;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO Personas (Documento, Nombre, SegundoNombre, Apellido) VALUES (@Documento, @Nombre, @SegundoNombre, @Apellido); SELECT SCOPE_IDENTITY();";
				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@Documento", persona.Documento);
				command.Parameters.AddWithValue("@Nombre", persona.Nombre);
				command.Parameters.AddWithValue("@SegundoNombre", persona.SegundoNombre); // Aquí se corrigió el parámetro
				command.Parameters.AddWithValue("@Apellido", persona.Apellido);

				connection.Open();
				idInsertado = Convert.ToInt32(command.ExecuteScalar());
			}

			return idInsertado; // Aquí se devuelve el ID insertado
		}
		public bool ExistePersonaPorDocumento(int documento)
		{
			bool existe = false;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = "SELECT COUNT(1) FROM Personas WHERE Documento = @Documento";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Documento", documento);

				connection.Open();
				existe = Convert.ToBoolean(command.ExecuteScalar());
			}

			return existe;
		}
	}
}
