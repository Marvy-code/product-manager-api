namespace ProductManager.Application
{
    public class CreateProductInputValidator : AbstractValidator<CreateProductInput>
    {
        public CreateProductInputValidator()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("{PropertyName} est obligatoire").MaximumLength(35)
                .WithMessage("{PropertyName} ne doit pas exceder 35 caracteres").NotNull();
            RuleFor(product => product.Description).MaximumLength(150)
                .WithMessage("La description ne doit pas exceder 150 caracteres");

            RuleFor(product => product.Price).NotNull().GreaterThan(0).LessThanOrEqualTo(100_000m);
            RuleFor(product => product.Quantity).NotNull().GreaterThan(0);
        }
    }
}