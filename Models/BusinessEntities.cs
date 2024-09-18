using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Models;
using SQLite;

namespace KseF.Models
{
    public class BusinessEntities : DbRecord
	{
		public string NazwaSkrocona { get; set; } = "";
        public string? NazwaPelna { get; set; } = "";
        public string Nip { get; set; } = "";

		public string Ulica { get; set; } = "";
		public string NrDomu { get; set; } = "";
		public string? NrLokalu { get; set; } = "";
		public string KodPocztowy { get; set; } = "";
		public string Miejscowosc { get; set; } = "";
		public string? AdresSiedziby { get; set; } = "";
		public string? AdresKorespondencyjny { get; set; } = "";
		public string? NrRachunku { get; set; } = "";
		public string? NrTelefonu { get; set; } = "";
		public string? AdresEmail { get; set; } = "";
		public string? Notatki { get; set; } = "";
	}

    public class MyBusinessEntities : BusinessEntities
    {
		public string? Regon { get; set; } = "";
		public string? Krs { get; set; } = "";
		public string? Bdo { get; set; } = "";

		public bool IsPodmiot { get; set; } = false;
		public bool IsTP { get; set; } = false;
		public bool IsDrukujStopke { get; set; } = true;

		public string? KodUS { get; set; }
		public string? ImieOsFiz { get; set; }
		public string? NazwiskoOsFiz { get; set; }
		public DateTime? DataUrodzeniaOF { get; set; }
		public EnumLibrary.FormaOpodatkowania? FormaOpodatkowania { get; set; } = EnumLibrary.FormaOpodatkowania.ZasadyOgole;
		public string? StopkaFaktury { get; set; } = "";

		public string? TokenKSeF { get; set; } = "D4A0E2EDD1E74E13C693F143338FFA142BA004FA4FCCA1276A963120A6C84B29";
	}

    public class ClientEntities : BusinessEntities
    {
		[ForeignKey("MyBusinessEntityId")]
		public virtual Guid MyBusinessEntityId { get; set; }

		public string? NrKlienta { get; set; } = "";
		public string? Imie { get; set; } = "";
		public string? Nazwisko { get; set; } = "";
	}
}
