using Application.Dto;
using Application.Services;
using Domain.Entities;
using FluentValidation;

namespace Application.DtoValidator
{
    public class TraineeCreateValidator : AbstractValidator<TraineeDto>
    {
        private readonly TraineeServices traineeServices;

        public TraineeCreateValidator(TraineeServices traineeServices)
        {
            this.traineeServices = traineeServices;

            RuleFor(t => t.Name)
                .Length(2, 50).WithMessage("Имя должно содержать от 2 до 50 символов");

            RuleFor(t => t.Surname)
                .Length(2, 50).WithMessage("Фамилия должна содержать от 2 до 50 символов");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+7\d{10}$").WithMessage("Номер телефона должен начинаться с +7 и содержать 10 цифр после него")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber))  
                .MustAsync(async (trainee, phone, token) => await traineeServices.PhoneNumberHaveNotUsed(phone, trainee.Id))
                .WithMessage("Данный номер телефона уже зарегистрирован")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));


            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email не является действительным")
                .MustAsync(async (trainee, email, token) => await traineeServices.EmailHaveNotUsed(email, trainee.Id))
                .WithMessage("Данная почта уже зарегистрирована");
        }
    }
}