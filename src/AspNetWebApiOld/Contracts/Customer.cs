using FluentValidation.Attributes;
using SampleWebApi.Validators;

namespace SampleWebApi.Contracts
{
    [Validator(typeof(CustomerValidator))]
    public class Customer
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public decimal Discount { get; set; }
        public string Address { get; set; }
    }
}