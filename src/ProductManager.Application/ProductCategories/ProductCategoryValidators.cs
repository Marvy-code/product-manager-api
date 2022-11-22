namespace ProductManager.Application
{
    public class CreateProductCategoryInputValidator : AbstractValidator<CreateProductCategoryInput>
    {
        public CreateProductCategoryInputValidator()
        {
            RuleFor(category => category.Name).NotEmpty().WithMessage("{PropertyName} est obligatoire").MaximumLength(35)
                .WithMessage("{PropertyName} ne doit pas exceder 35 caracteres").NotNull();
            RuleFor(category => category.Description).MaximumLength(150)
                .WithMessage("La description ne doit pas exceder 150 caracteres");
        }
    }

    public class RenameProductCategoryInputValidator : AbstractValidator<RenameProductCategoryInput>
    {
        public RenameProductCategoryInputValidator()
        {
            RuleFor(category => category.NewName).NotEmpty().WithMessage("{PropertyName} est obligatoire").MaximumLength(35)
                .WithMessage("{PropertyName} ne doit pas exceder 35 caracteres").NotNull();
        }
    }
}