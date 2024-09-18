using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KseF.Interfaces
{
	public interface IDbRecord
	{
		[PrimaryKey]
		[Required]
		public Guid Id { get; set; }
		public DateTime? CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}
}
