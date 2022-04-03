using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("endereco")]
    public class Address : BaseModel, IAddress, IGeneric
    {
        public Address() { }

        public Address(int? id, string code, string externalCode, string cityCode, string district, string complement, string number, string reference, string zipCode, string addressValue)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            CityCode = cityCode;
            District = district;
            Complement = complement;
            Number = number;
            Reference = reference;
            ZipCode = zipCode;
            AddressValue = addressValue;
        }

        #region [ Propriedades ]

        [Column("end_id")]
        public int? Id { get; private set; }

        [Column("end_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("end_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("mun_codigo")]
        public string CityCode { get; private set; }

        [Column("end_bairro")]
        public string District { get; private set; } = string.Empty;

        [Column("end_complemento")]
        public string Complement { get; private set; }

        [Column("end_numero")]
        public string Number { get; private set; }

        [Column("end_referencia")]
        public string Reference { get; private set; }

        [Column("end_cep")]
        public string ZipCode { get; private set; }

        [Column("end_logradouro")]
        public string AddressValue { get; private set; }

        [Column("end_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("end_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("end_removido")]
        public bool WasRemoved { get; private set; }

        [Column("end_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("end_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Address Criar(string code, string externalCode, string cityCode, string district, string complement, string number, string reference, string zipCode, string addressValue)
        {
            var address = new Address();

            address.Id = null;
            address.Code = code;
            address.ExternalCode = externalCode;
            address.CityCode = cityCode;
            address.District = district;
            address.Complement = complement;
            address.Number = number;
            address.Reference = reference;
            address.ZipCode = zipCode;
            address.AddressValue = addressValue;
            address.RemovedId = 0;
            address.CreatedDate = DateTime.UtcNow;
            address.UpdatedDate = DateTime.UtcNow;
            address.WasRemoved = false;

            return address;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new AddressModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Address newAddress)
        {
            this.Id = newAddress.Id;
            this.Code = newAddress.Code;
            this.ExternalCode = newAddress.ExternalCode;
            this.CityCode = newAddress.CityCode;
            this.District = newAddress.District;
            this.Complement = newAddress.Complement;
            this.Number = newAddress.Number;
            this.Reference = newAddress.Reference;
            this.ZipCode = newAddress.ZipCode;
            this.AddressValue = newAddress.AddressValue;
            this.UpdatedDate = newAddress.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class AddressModelValidation : AbstractValidator<Address>
    {
        public AddressModelValidation()
        {
            Include(new AddressValidation());

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
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .IsAddressCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");
        }
    }
}
