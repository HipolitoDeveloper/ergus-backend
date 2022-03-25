using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("metadado")]
    public class Metadata : BaseModel
    {
        public Metadata() { }

        public Metadata(int id)
        {
            Id = id;
        }

        #region [ Propriedades ]

        [Column("md_id")]
        public int Id { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public override bool EhValido()
        {
            return true;
        }

        #endregion [ FIM - Metodos ]
    }
}
