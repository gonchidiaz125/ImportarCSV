using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hola! soy el importador de CSV");

List<PersonaImportada> personasImportadas = new List<PersonaImportada>();

using (var reader = new StreamReader(@"C:\desarrollo\EjemploArchivoCsv\ImportarCSV\personas1.csv"))
{
    while (!reader.EndOfStream)
    {
        var linea = reader.ReadLine();
        var valores = linea.Split(',');

        var unaPersona = new PersonaImportada()
        {
            Documento = valores[0],
            Nombre = valores[1],
            SegundoNombre = valores[2],
            Apellido = valores[3],
        };

        personasImportadas.Add(unaPersona);
    }
}

List<Persona> personas = new List<Persona>();

var numeroDeFila = 0;

foreach (var personaImportada in personasImportadas)
{
    if (numeroDeFila > 0)
    {
        int numero;
        bool DocumentoCorrecto = Int32.TryParse(personaImportada.Documento, out numero);
        if (DocumentoCorrecto)
        {
            var persona = new Persona()
            {
                Id = 0,
                Documento = numero,
                Nombre = personaImportada.Nombre,
                SegundoNombre = personaImportada.SegundoNombre,
                Apellido = personaImportada.Apellido
            };

            personas.Add(persona);
        }
        else
        {
            Console.WriteLine("El numero de Documento no es correcto");
        }
    }
    numeroDeFila++;
}

foreach (var persona in personas)
{
    Console.WriteLine($"Documento: {persona.Documento}  Nombre: {persona.Nombre} {persona.SegundoNombre} Apellido: {persona.Apellido}");
}

PersonaLogic personaLogic = new PersonaLogic();
foreach (var persona in personas)
{
    int idInsertado = personaLogic.InsertarPersona(persona);
    
}

Console.WriteLine("");
Console.WriteLine("Fin");

public class PersonaImportada
{
    public string Documento { get; set; }
    public string Nombre { get; set; }
    public string SegundoNombre { get; set; }
    public string Apellido { get; set; }
}

public class Persona
{
    public Persona()
    {
        Nombre = string.Empty;
        SegundoNombre = string.Empty;
        Apellido = string.Empty;
    }
    public int Id { get; set; }
    public int Documento { get; set; }
    public string Nombre { get; set; }
    public string SegundoNombre { get; set; }
    public string Apellido { get; set; }
}

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

public class PersonaLogic
{
    private readonly PersonaRepository personaRepository;

    public PersonaLogic()
    {
        this.personaRepository = new PersonaRepository();
    }

    public int InsertarPersona(Persona persona)
    {
        if (!personaRepository.ExistePersonaPorDocumento(persona.Documento))
        {
            return personaRepository.Insert(persona);
        }
        else
        {
            Console.WriteLine($"La persona con documento {persona.Documento} ya existe.");
            return 0; 
        }
    }
}


