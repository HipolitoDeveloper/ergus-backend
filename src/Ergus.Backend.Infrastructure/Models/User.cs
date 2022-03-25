using Ergus.Backend.Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("usuario")]
    public class User
    {
        [Column("usu_id")]
        public int Id       { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("usu_nome")]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        [Column("usu_login")]
        public string Login { get; set; } = String.Empty;

        [Required]
        [MaxLength(200)]
        [Column("usu_senha")]
        public string Password { get; set; } = String.Empty;

        [Required]
        [MaxLength(150)]
        [Column("usu_email")]
        public string Email { get; set; } = String.Empty;

        //public override IEntityKeyRemovableCodeMap GetEntityKeyRemovableCodeMap()
        //{
        //    return new EntityKeyRemovableCodeMap
        //    {
        //        IdColumn = "usu_id",
        //        UpdateDateColumn = "usu_dt_alt",
        //        CreatedDateColumn = "usu_dt_inc",
        //        RemovalDateColumn = "usu_dt_rem",
        //        RemovedIdColumn = null,
        //        RemovedColumn = "usu_removido",
        //        CodeColumn = "usu_codigo",
        //    };
        //}
    }
}
