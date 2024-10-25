using System.Runtime.InteropServices;
using System.Text.Json;

namespace MitarbeiterManagement
{
	public class Mitarbeiter
	{
		public string Vorname { get; set; }
		public string Nachname { get; set; }
		public bool IstAngemeldet { get; set; } = false;

		public override string ToString()
		{
			return $"{Vorname} {Nachname}";
		}
	}

	public class MitarbeiterVerwaltung
	{
		private List<Mitarbeiter> mitarbeiterListe = new List<Mitarbeiter>();

		public void MitarbeiterHinzufuegen(string vorname, string nachname)
		{
			foreach (Mitarbeiter m in mitarbeiterListe)
			{
				if (m.Vorname == vorname && m.Nachname == nachname)
					return;
			}
            Console.Write("ist Mitarbeiter angemeldet? (j/n) ");
			string istangemeldet=Console.ReadLine();
			mitarbeiterListe.Add(new Mitarbeiter { Vorname = vorname, Nachname = nachname, IstAngemeldet =istangemeldet .ToLower()=="j" });
		}


		public void MitAnzeigen(bool angemeldete )
		{
			if (angemeldete)
			Console.WriteLine("Angemeldete Mitarbeiter:"); 
			else 
				Console.WriteLine("Abgemeldete Mitarbeiter:");
			foreach (var m in mitarbeiterListe.Where(m => (angemeldete ? m.IstAngemeldet : !m.IstAngemeldet)))
			{
				Console.WriteLine(m);
			}
		}


		public void Speichern()
		{
			string json = JsonSerializer.Serialize(mitarbeiterListe);
			File.WriteAllText("mitarbeiter.json", json);
			Console.WriteLine("Daten wurden in mitarbeiter.json gespeichert.");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var verwaltung = new MitarbeiterVerwaltung();


			while (true)
			{
                Console.WriteLine("[1] Mitarbeiter hinzufügen");         
				Console.WriteLine("[2] Mitarbeiter angemeldete anzeigen");
				Console.WriteLine("[3] Mitarbeiter abgemeldete anzeigen");
				//Console.WriteLine("[0] Mitarbeiter entfernen");
				Console.Write("Was möchtest du machen? ");
				switch (Console.ReadLine())
				{
					case "1":
                        Console.Write("Vorname: " );
						string vorname=Console.ReadLine();
						Console.Write("Nachname: ");
						string nachrname = Console.ReadLine();
						verwaltung.MitarbeiterHinzufuegen(vorname, nachrname);
						break;
					case "2":
						verwaltung.MitAnzeigen(true);
						break;
					case "3":
						verwaltung.MitAnzeigen(false);
						break;
					
				}


			}

			verwaltung.Speichern();
		}
	}
}

