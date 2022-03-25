using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("fornecedor")]
    public class Provider : BaseModel
    {
        public Provider() { }

        public Provider(int id, string code)
        {
            Id = id;
            Code = code;
        }

        #region [ Propriedades ]

        [Column("for_id")]
        public int Id { get; private set; }

        [Column("for_codigo")]
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
