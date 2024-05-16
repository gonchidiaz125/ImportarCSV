using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportarCSV
{
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
}
