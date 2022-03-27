using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("anuncio_sku")]
    public class AdvertisementSku : BaseModel
    {
        public AdvertisementSku() { }

        public AdvertisementSku(int id, string code)
        {
            Id = id;
            Code = code;
        }

        #region [ Propriedades ]

        [Column("asku_id")]
        public int Id { get; private set; }

        [Column("asku_codigo")]
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
