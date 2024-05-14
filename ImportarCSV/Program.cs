using System.IO;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hola! soy el importador de CSV");


List<PersonaImportada> personas = new List<PersonaImportada>();

using (var reader = new StreamReader(@".\personas1.csv"))
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

		personas.Add(unaPersona);
	}	
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


