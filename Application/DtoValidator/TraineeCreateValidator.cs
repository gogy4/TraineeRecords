using Application.Dto;
using Application.Services;
using Domain.Entities;

namespace Application.DtoValidator;
using FluentValidation;

public class TraineeCreateValidator : AbstractValidator<CreateTraineeDto>
{
    private TraineeServices traineeServices;
    public TraineeCreateValidator(TraineeServices traineeServices)
    {
        this.traineeServices = traineeServices;

        RuleFor(t => t.Name)
            .Length(2, 50).WithMessage("Имя должно содержать от 2 до 50 символов");
        RuleFor(t => t.Surname)
            .Length(2, 50).WithMessage("Имя должно содержать от 2 до 50 символов");
        
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+7\d{10}$").WithMessage("Номер телефона должен начинаться с +7 и содержать 10 цифр после него")
            .MustAsync((phone, token) => ValidateUniqueField(phone, traineeServices.GetByPhoneNumber))
            .WithMessage("Данный номер телефона уже зарегистрирован");
        
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email не является действительным")
            .MustAsync((email, token) => ValidateUniqueField(email, traineeServices.GetByEmail))
            .WithMessage("Данная почта уже зарегистрирована");
    }
    
    private async Task<bool> ValidateUniqueField(string value, Func<string, Task<Trainee?>> getCustomerByField)
    {
        var customer = await getCustomerByField(value);
        return customer is null;
    }
}