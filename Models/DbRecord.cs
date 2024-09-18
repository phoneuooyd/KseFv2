using KseF.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KseF.Models
{
	public class DbRecord : IDbRecord
	{
		[PrimaryKey]
		[Required]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime? CreatedOn { get; set; } = DateTime.Now;
		public DateTime? UpdatedOn { get; set; }
	}
}
