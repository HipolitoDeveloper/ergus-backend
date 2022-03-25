using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("fabricante")]
    public class Producer : BaseModel
    {
        public Producer() { }

        public Producer(int id, string code)
        {
            Id = id;
            Code = code;
        }

        #region [ Propriedades ]

        [Column("fab_id")]
        public int Id { get; private set; }

        [Column("fab_codigo")]
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
