using CherryDough.Domain.Commands.Validations;

namespace CherryDough.Domain.Commands
{
    public class AddItemCommand : ItemCommand
    {
        public AddItemCommand(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Category = category;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}