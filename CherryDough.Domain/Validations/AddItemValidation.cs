using CherryDough.Domain.Models;

namespace CherryDough.Domain.Validations
{
    public class AddItemValidation : ItemValidation<Item>
    {
        public AddItemValidation()
        {
            ValidateId();
            ValidateName();
            ValidateCategory();
            ValidateDescription();
        }
    }
}