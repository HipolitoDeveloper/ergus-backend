using Ergus.Backend.Infrastructure.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("empresa")]
    public class Company : BaseModel, IGeneric
    {
        public Company() { }

        public Company(int id, string code)
        {
            Id = id;
            Code = code;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("emp_id")]
        public int Id { get; private set; }

        [Column("emp_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("emp_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("emp_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("emp_removido")]
        public bool WasRemoved { get; private set; }

        [Column("emp_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("emp_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public override bool EhValido()
        {
            return true;
        }

        #endregion [ FIM - Metodos ]
    }
}
