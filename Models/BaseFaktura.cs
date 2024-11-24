using KseF.Models.Invoice_FA_2;
using Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KseF.Models
{
    public class BaseFaktura : DbRecord
	{
        [ForeignKey("MyBusinessEntity.Id")]
		public Guid BusinessEntityId { get; set; }

        [ForeignKey("ClientEntity.Id")]
        public Guid ClientEntityId { get; set; }

		public string? IdNabywcy { get; set; } = "01";
		public string? NumerFaktury { get; set; } = "";
        public string? DaneSprzedawcy { get; set; } = "";
        public string? DaneNabywcy { get; set; } = "";
        public string? UlicaNabywcy { get; set; }
        public string? NrDomuNabywcy { get; set; }
        public string? NrLokaluNabywcy { get; set; }
        public string? KodPocztowyNabywcy { get; set; }
        public string? MiejscowoscNabywcy { get; set; }
        public string? NabywcaAdresL1 { get; set; }
        public string? NabywcaAdresL2 { get; set; }
        public string? UlicaSprzedawcy { get; set; }
        public string? NrDomuSprzedawcy { get; set; }
        public string? NrLokaluSprzedawcy { get; set; }
        public string? KodPocztowySprzedawcy { get; set; }
        public string? MiejscowoscSprzedawcy { get; set; }
        public string? SprzedawcaAdresL1 { get; set; }
        public string? SprzedawcaAdresL2 { get; set; }
        public string? NipSprzedawcy { get; set; }
        public string? NazwaSprzedawcy { get; set; } = "Sprzedawca1";
        public string? EmailSprzedawcy { get; set; }
        public string? TelefonSprzedawcy { get; set; }
		public string? NipNabywcy { get; set; }
        public string? NazwaNabywcy { get; set; }
        public string? EmailNabywcy { get; set; }
        public string? TelefonNabywcy { get; set; }
        public string? NrKlienta { get; set; }

        public DateTime DataWystawienia { get; set; } = DateTime.Now.Date;
        public DateTime DataSprzedazy { get; set; } = DateTime.Now.Date;
        public DateTime DataWprowadzenia { get; set; } = DateTime.Now.Date;
        public DateTime TerminPlatnosci { get; set; } = DateTime.Now.Date;
        public EnumLibrary.TypFaktury? TypFaktury { get; set; }

        [Ignore]
        public ICollection<PozycjeFaktury> PozycjeFaktury { get; set; }
		public string? KodWaluty { get; set; }
		public decimal ProcentVatNaliczonego { get; set; }
        public decimal ProcentKosztow { get; set; }
        public decimal CenaNettoRazem { get; set; }
        public decimal CenaBruttoRazem { get; set; }
        [Ignore]
        public ICollection<Rata>? ZaplaconeRaty { get; set; }
        public decimal DoZapłatyPozostało { get; set; }

        public bool IsTP { get; set; }
        public bool IsZakupSrodkowTrwalych { get; set; }
        public bool IsWewnątrzwspólnotowaDostawaTowarow { get; set; }
        public bool IsWewnątrzwspólnotoweNabycieTowaru { get; set; }
        public bool IsKorekta { get; set; } = false;
        public string? PrzyczynaKorekty { get; set; } = "";

        [Ignore]
		public virtual BaseFaktura? KorektaDoFaktury { get; set; }
        public Guid? KorektaDoFakturyId { get; set; }

		public string? Notatki { get; set; } = "";
        public string? FormaPlatnosci { get; set; } = "";
        public string? NrKontaBankowego { get; set; } = "";
        public string NrFakturyKSeF { get; set; } = "";
        public string URLFakturyKSeF { get; set; } = "";
        public string XMLFakturyKSeF { get; set; } = "";
        public string NumerReferencyjnyKSeF { get; set; } = "";
        public int? StatusKSeF { get; set; }
        public DateTime? DataFakturyKSeF { get; set; }

    }
    public class PozycjeFaktury
    {
        public int? TowarId { get; set; } = 0;
        public int NrPozycji { get; set; }
        public bool IsKor { get; set; }
        public string? NazwaTowaruUslugi { get; set; }
        public string Opis { get; set; } = "";
        public EnumLibrary.RodzajPozycji RodzajPozycji { get; set; }
        public EnumLibrary.JednostkaMiary JednostkaMiary { get; set; }
        public PodatekVat Vat { get; set; }
        public decimal CenaJednostkowaNetto { get; set; }
        public decimal CenaJednostkowaBrutto { get; set; }
        public decimal CenaJednostkowaVat { get; set; }
        public decimal IloscTowaruUslugi { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscVat { get; set; }
        public decimal WartoscBrutto { get; set; } 
        
        public int GTU { get; set; }
        public decimal? StawkaRyczaltu { get; set; }
    }    
    public class PodatekVat
    {
        public string NazwaStawki { get; set; }
        public int WysokośćPodatku { get; set; }
    }
    public class Rata
    {   
        public Guid RataId { get; set; }
        public Guid FakturaId { get; set; }
        public decimal KwotaRaty { get; set; }
        public DateTime DataZapłatyRaty { get; set; }
    }

    public class InvoiceHeader
    {
        public string ReferenceNumber { get; set; }
        public string KsefReferenceNumber { get; set; }
        public DateTime InvoicingDate { get; set; }
        public DateTime AcquisitionTimestamp { get; set; }
        public string IssuedByName { get; set; }
        public string IssuedByNIP { get; set; }
        public string IssuedToName { get; set; }
        public string IssuedToNIP { get; set; }
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal Vat { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
    }
}
