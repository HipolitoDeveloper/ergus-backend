using Ergus.Backend.Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("usuario_token")]
    public class UserToken
    {
        [Column("usut_id")]
        public int Id { get; set; }

        [Column("usut_schema_tipo")]
        public string SchemaType { get; set; } = String.Empty;

        [Column("usut_token_tipo")]
        public string TokenType { get; set; } = String.Empty;

        [Column("usu_id")]
        public int UserId { get; set; }

        [Required]
        [Column("usut_token_valor")]
        public string TokenValue { get; set; } = String.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        //public override IEntityKeyMap GetEntityKeyMap()
        //{
        //    return new EntityKeyMap
        //    {
        //        IdColumn = "usut_id",
        //    };
        //}
    }
}
