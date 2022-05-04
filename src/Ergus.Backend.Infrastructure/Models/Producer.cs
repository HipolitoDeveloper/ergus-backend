using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("fabricante")]
    public class Producer : BaseModel, IProducer<Address>, IGeneric
    {
        public Producer() { }

        public Producer(int id, string code, string externalCode, string name, string email, string contact, string site, string fiscalDocument, string document, TipoPessoa? personType, bool active, IAddress? address)
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
                var localAddress = (Address)address;

                if (!address.OnlyId)
                    this.Address = localAddress;

                this.AddressId = localAddress.Id;
            }
        }

        #region [ Propriedades ]

        [Column("fab_id")]
        public int Id { get; private set; }

        [Column("fab_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("fab_codigo_ext", TypeName = SqlUtils.VARCHAR)]
        public string ExternalCode { get; set; } = string.Empty;

        [Column("fab_nome", TypeName = SqlUtils.VARCHAR)]
        public string Name { get; set; }

        [Column("fab_email", TypeName = SqlUtils.VARCHAR)]
        public string Email { get; set; } = string.Empty;

        [Column("fab_contato", TypeName = SqlUtils.VARCHAR)]
        public string Contact { get; set; } = string.Empty;

        [Column("fab_site", TypeName = SqlUtils.VARCHAR)]
        public string Site { get; set; } = string.Empty;

        [Column("fab_documento_fiscal", TypeName = SqlUtils.VARCHAR)]
        public string FiscalDocument { get; set; } = string.Empty;

        [Column("fab_documento", TypeName = SqlUtils.VARCHAR)]
        public string Document { get; set; } = string.Empty;

        [Column("fab_tipo_pessoa")]
        public string? PersonType { get; private set; } = TipoPessoa.Nenhum.DescriptionAttr();

        [Column("fab_ativo")]
        public bool Active { get; set; }

        [Column("end_id")]
        [ForeignKey(nameof(Address))]
        public int? AddressId { get; set; }

        [Column("fab_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("fab_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("fab_removido")]
        public bool WasRemoved { get; private set; }

        [Column("fab_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("fab_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        public virtual Address? Address { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Producer Criar(string code, string externalCode, string name, string email, string contact, string site, string fiscalDocument, string document, TipoPessoa? personType, IAddress? address)
        {
            var producer = new Producer();

            producer.Id = 0;
            producer.Code = code;
            producer.ExternalCode = externalCode;
            producer.Name = name;
            producer.Email = email;
            producer.Contact = contact;
            producer.Site = site;
            producer.FiscalDocument = fiscalDocument;
            producer.Document = document;
            producer.PersonType = personType.DescriptionAttr();
            producer.Active = true;
            producer.RemovedId = 0;
            producer.CreatedDate = DateTime.UtcNow;
            producer.UpdatedDate = DateTime.UtcNow;
            producer.WasRemoved = false;

            if (address != null)
            {
                var localAddress = (Address)address;

                if (!address.OnlyId)
                    producer.Address = localAddress;

                producer.AddressId = localAddress.Id;
            }

            producer.Active = true;

            return producer;
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
            ValidationResult = new ProducerModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Producer newProducer)
        {
            this.Id = newProducer.Id;
            this.Code = newProducer.Code;
            this.ExternalCode = newProducer.ExternalCode;
            this.Name = newProducer.Name;
            this.Email = newProducer.Email;
            this.Contact = newProducer.Contact;
            this.Site = newProducer.Site;
            this.FiscalDocument = newProducer.FiscalDocument;
            this.Document = newProducer.Document;
            this.PersonType = newProducer.PersonType;
            this.AddressId = newProducer.AddressId;
            this.UpdatedDate = newProducer.UpdatedDate;
            this.Active = newProducer.Active;

            if (newProducer.Address == null)
            {
                this.Address = null;
                this.AddressId = newProducer.AddressId;
            }
            else
            {
                if (this.Address == null)
                    this.Address = new Address();

                this.Address.MergeToUpdate(newProducer.Address);
            }
        }

        #endregion [ FIM - Metodos ]
    }

    public class ProducerModelValidation : AbstractValidator<Producer>
    {
        public ProducerModelValidation()
        {
            Include(new ProducerValidation());

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
                .IsProducerCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.AddressId.HasValue, () =>
            {
                RuleFor(x => x.AddressId)
                    .AddressExists().WithMessage(x => $"O AddressId {x.AddressId} não faz referência a nenhum endereço no banco de dados");
            });
        }
    }
}
