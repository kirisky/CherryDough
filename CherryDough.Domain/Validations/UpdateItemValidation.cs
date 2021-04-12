using CherryDough.Domain.Models;

namespace CherryDough.Domain.Validations
{
    public class UpdateItemValidation : ItemValidation<Item>
    {
        public UpdateItemValidation()
        {
            ValidateId();
            ValidateName();
            ValidateCategory();
            ValidateDescription();
        }
    }
}