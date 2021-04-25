namespace CherryDough.Domain.Commands.Validations
{
    public class AddItemCommandValidation : ItemValidation<AddItemCommand>
    {
        public AddItemCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateDescription();
            ValidateCategory();
        }
    }
}