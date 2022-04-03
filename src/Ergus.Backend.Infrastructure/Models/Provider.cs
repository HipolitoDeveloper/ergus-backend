using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("fornecedor")]
    public class Provider : BaseModel, IProvider<Address>, IGeneric
    {
        public Provider() { }

        public Provider(int id, string code, string externalCode, string name, string email, string contact, string site, string fiscalDocument, string document, TipoPessoa? personType, bool active, 
            IAddress? address)
        {
            this.Id = id;
            this.Code = code;
            this.ExternalCode = externalCode;
            this.Name = name;
            this.Email = email;
            this.Contact = contact;
            this.Site = site;
            this.FiscalDocument = fiscalDocument;
            this.Document = document;
            this.PersonType = personType.DescriptionAttr();
            this.Active = active;

            if (address != null)
            {
                this.Address = (Address)address;
                this.AddressId = this.Address.Id;
            }
        }

        #region [ Propriedades ]

        [Column("for_id")]
        public int Id { get; private set; }

        [Column("for_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("for_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("for_nome")]
        public string Name { get; private set; }

        [Column("for_email")]
        public string Email { get; private set; } = string.Empty;

        [Column("for_contato")]
        public string Contact { get; private set; } = string.Empty;

        [Column("for_site")]
        public string Site { get; private set; } = string.Empty;

        [Column("for_documento_fiscal")]
        public string FiscalDocument { get; private set; } = string.Empty;

        [Column("for_documento")]
        public string Document { get; private set; } = string.Empty;

        [Column("for_tipo_pessoa")]
        public string? PersonType { get; private set; } = TipoPessoa.Nenhum.DescriptionAttr();

        [Column("for_ativo")]
        public bool Active { get; private set; }

        [Column("end_id")]
        [ForeignKey(nameof(Address))]
        public int? AddressId { get; private set; }

        [Column("for_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("for_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("for_removido")]
        public bool WasRemoved { get; private set; }

        [Column("for_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("for_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        public virtual Address? Address { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Provider Criar(string code, string externalCode, string name, string email, string contact, string site, string fiscalDocument, string document, TipoPessoa personType, 
            IAddress? address)
        {
            var provider = new Provider();

            provider.Id = 0;
            provider.Code = code;
            provider.ExternalCode = externalCode;
            provider.Name = name;
            provider.Email = email;
            provider.Contact = contact;
            provider.Site = site;
            provider.FiscalDocument = fiscalDocument;
            provider.Document = document;
            provider.PersonType = personType.DescriptionAttr();
            provider.RemovedId = 0;
            provider.CreatedDate = DateTime.UtcNow;
            provider.UpdatedDate = DateTime.UtcNow;
            provider.WasRemoved = false;

            if (address != null)
            {
                provider.Address = (Address)address;
                provider.AddressId = provider.Address.Id;
            }

            provider.Active = true;

            return provider;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
            this.Active = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new ProviderModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Provider newProvider)
        {
            this.Id = newProvider.Id;
            this.Code = newProvider.Code;
            this.ExternalCode = newProvider.ExternalCode;
            this.Name = newProvider.Name;
            this.Email = newProvider.Email;
            this.Contact = newProvider.Contact;
            this.Site = newProvider.Site;
            this.FiscalDocument = newProvider.FiscalDocument;
            this.Document = newProvider.Document;
            this.PersonType = newProvider.PersonType;
            this.AddressId = newProvider.AddressId;
            this.UpdatedDate = newProvider.UpdatedDate;
            this.Active = newProvider.Active;

            if (newProvider.Address == null)
                this.Address = null;
            else
            {
                if (this.Address == null)
                    this.Address = new Address();

                this.Address.MergeToUpdate(newProvider.Address);
            }
        }

        #endregion [ FIM - Metodos ]
    }

    public class ProviderModelValidation : AbstractValidator<Provider>
    {
        public ProviderModelValidation()
        {
            Include(new ProviderValidation());

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
                .IsProviderCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");
        }
    }
}
