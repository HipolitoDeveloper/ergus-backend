﻿using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.WebApi.Catalogo.Models.Addresses.Request;

namespace Ergus.Backend.WebApi.Catalogo.Models.Producers.Request
{
    public class ProducerUpdateRequest : BaseModel, IProducer<AddressUpdateRequest>
    {
        public int Id                   { get; set; }
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public string? Name             { get; set; } = string.Empty;
        public string? Email            { get; set; } = string.Empty;
        public string? Contact          { get; set; } = string.Empty;
        public string? Site             { get; set; } = string.Empty;
        public string? FiscalDocument   { get; set; } = string.Empty;
        public string? Document         { get; set; } = string.Empty;
        public bool Active              { get; set; }
        public int? AddressId           { get; set; }
        public string? PersonType       { get; set; } = string.Empty;

        public AddressUpdateRequest? Address    { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new ProducerUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ProducerUpdateRequestValidation : AbstractValidator<ProducerUpdateRequest>
    {
        public ProducerUpdateRequestValidation()
        {
            Include(new ProducerValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
