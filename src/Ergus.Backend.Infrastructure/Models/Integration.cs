using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("integracao")]
    public class Integration : BaseModel
    {
        public Integration() { }

        public Integration(int id, string code)
        {
            Id = id;
            Code = code;
        }

        #region [ Propriedades ]

        [Column("int_id")]
        public int Id { get; private set; }

        [Column("int_codigo")]
        public string Code { get; private set; } = string.Empty;

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public override bool EhValido()
        {
            return true;
        }

        #endregion [ FIM - Metodos ]
    }
}
