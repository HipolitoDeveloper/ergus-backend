using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("grade")]
    public class Grid : BaseModel, IGrid, IGeneric
    {
        public Grid() { }

        public Grid(int id, string code, string externalCode, string name)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("gd_id")]
        public int Id { get; private set; }

        [Column("gd_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("gd_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("gd_nome")]
        public string Name { get; private set; } = String.Empty;


        [Column("gd_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("gd_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("gd_removido")]
        public bool WasRemoved { get; private set; }

        [Column("gd_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("gd_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Grid Criar(string code, string externalCode, string name)
        {
            var grid = new Grid();

            grid.Id = 0;
            grid.Code = code;
            grid.ExternalCode = externalCode;
            grid.Name = name;
            grid.RemovedId = 0;
            grid.CreatedDate = DateTime.UtcNow;
            grid.UpdatedDate = DateTime.UtcNow;
            grid.WasRemoved = false;

            return grid;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new GridModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Grid newGrid)
        {
            this.Id = newGrid.Id;
            this.Code = newGrid.Code;
            this.ExternalCode = newGrid.ExternalCode;
            this.Name = newGrid.Name;
            this.UpdatedDate = newGrid.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class GridModelValidation : AbstractValidator<Grid>
    {
        public GridModelValidation()
        {
            Include(new GridValidation());

            RuleFor(x => x.CreatedDate)
                .IsValidDateTime(true).WithMessage("A Data de Criação está inválida");

            RuleFor(x => x.UpdatedDate)
                .IsValidDateTime(true).WithMessage("A Data de Atualização está inválida");

            When(x => x.WasRemoved, () =>
            {
                RuleFor(x => x.RemovedDate)
                    .IsValidDateTime(true).WithMessage("A Data de Exclusão está inválida");

                RuleFor(x => x.RemovedId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("O Código da Exclusão é obrigatório")
                    .GreaterThan(0).WithMessage("O Código da Exclusão está inválido");
            });

            RuleFor(x => x)
                .NotEmpty()
                .IsGridCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");
        }
    }
}
