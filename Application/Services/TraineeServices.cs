using Application.Dto;
using Application.DtoValidator;
using Domain.Entities;

namespace Application.Services;

public class TraineeServices(ITraineeRepository repository, ResourceServices resourceServices)
{
    public async Task Create(CreateTraineeDto traineeDto)
    {
        var validator = new TraineeCreateValidator(this);
        var result = await validator.ValidateAsync(traineeDto);
        if (!result.IsValid)
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        var ids = await resourceServices
            .GetIds(traineeDto.InternshipDirection, traineeDto.CurrentProject);
        var trainee = new Trainee(traineeDto.Name, traineeDto.Surname, traineeDto.Gender, traineeDto.Email,
            traineeDto.PhoneNumber, traineeDto.DateOfBirth, ids.internshipDirectionId, ids.currentProjectId);
        await repository.AddAsync(trainee);
    }

    public async Task<Trainee> GetById(Guid traineeId)
    {
        return await repository.GetByIdAsync(traineeId);
    }

    public async Task<bool> PhoneNumberHaveNotUsed(string phoneNumber)
    {
        return await repository.GetByPhoneNumberAsync(phoneNumber) is null;
    }

    public async Task<bool> EmailHaveNot(string email)
    {
        return await repository.GetByEmailAsync(email) is null;
    }
}