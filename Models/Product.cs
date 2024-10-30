using Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KseF.Models.Invoice_FA_2
{
	public class Product : DbRecord
	{
		public string Nazwa { get; set; }
		public string? Opis { get; set; }
		public decimal Cena { get; set; }
		public string Kategoria { get; set; }
		public EnumLibrary.JednostkaMiary? JednostkaMiary { get; set; }
		public EnumLibrary.RodzajPozycji? RodzajPozycji { get; set; }
		public EnumLibrary.StawkiPodatkuPL StawkaPodatku { get; set; }
		public int? GTU { get; set; }

    }
}
