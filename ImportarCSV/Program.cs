using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
	Console.WriteLine($"Documento: {persona.Documento}  Nommbre: {persona.Nombre} {persona.SegundoNombre} Apellido: {persona.Apellido}");
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
	public Persona() { 
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
