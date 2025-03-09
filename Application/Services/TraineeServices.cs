using Application.Dto;
using Application.DtoValidator;
using Domain.Entities;

namespace Application.Services;

public class TraineeServices(ITraineeRepository repository, ResourceServices resourceServices)
{
    public async Task Create(TraineeDto traineeDto)
    {
        var validator = new TraineeCreateValidator(this);
        var result = await validator.ValidateAsync(traineeDto);
        if (!result.IsValid)
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));

        var ids = await resourceServices.GetIds(traineeDto.InternshipDirection, traineeDto.CurrentProject);
        await resourceServices.ChangeCountTrainees(null, null, ids.internshipDirectionId, ids.currentProjectId);

        var trainee = new Trainee(traineeDto.Name, traineeDto.Surname, traineeDto.Gender, traineeDto.Email,
            traineeDto.PhoneNumber, traineeDto.DateOfBirth, ids.internshipDirectionId, ids.currentProjectId);

        await repository.AddAsync(trainee);
    }


    public async Task Edit(TraineeDto traineeDto)
    {
        var validator = new TraineeCreateValidator(this);
        var result = await validator.ValidateAsync(traineeDto);
        if (!result.IsValid)
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        var trainee = await GetById(traineeDto.Id);
        var oldIds = (trainee.CurrentProjectId, trainee.InternshipDirectionId);
        var newIds = await resourceServices.GetIds(traineeDto.InternshipDirection, traineeDto.CurrentProject);
        if (oldIds != newIds)
            await resourceServices.ChangeCountTrainees(oldIds.Item2,
                oldIds.Item1, newIds.internshipDirectionId, newIds.currentProjectId);


        trainee.Edit(traineeDto.Name, traineeDto.Surname, traineeDto.Gender, traineeDto.Email, traineeDto.PhoneNumber,
            traineeDto.DateOfBirth, newIds.internshipDirectionId, newIds.currentProjectId);

        await repository.UpdateAsync(trainee);
    }


    public async Task<List<Trainee>> GetByFilter(string directionFilter, string currentProjectId)
    {
        var (projectId, directionId) = await resourceServices.GetIds(directionFilter, currentProjectId);
        return await repository.GetByResourceIds(directionId, projectId);
    }

    public async Task<Dictionary<Guid, List<Trainee>>> GetByDirection(List<InternshipDirection> directions)
    {
        var traineeList = new Dictionary<Guid, List<Trainee>>();
        foreach (var direction in directions)
        {
            var trainee = await repository.GetByResourceIds(direction.Id, Guid.Empty);
            traineeList[direction.Id] = trainee;
        }

        return traineeList;
    }
    
    public async Task<Dictionary<Guid, List<Trainee>>> GetByProject(List<CurrentProject> projects)
    {
        var traineeList = new Dictionary<Guid, List<Trainee>>();
        foreach (var project in projects)
        {
            var trainee = await repository.GetByResourceIds(Guid.Empty, project.Id);
            traineeList[project.Id] = trainee;
        }

        return traineeList;
    }
    
    public async Task<Trainee> GetById(Guid traineeId)
    {
        return await repository.GetByIdAsync(traineeId);
    }

    public async Task<bool> PhoneNumberHaveNotUsed(string phoneNumber, Guid traineeId)
    {
        var trainee = await repository.GetByPhoneNumberAsync(phoneNumber);
        return trainee is null || trainee.Id == traineeId;
    }

    public async Task<bool> EmailHaveNot(string email, Guid traineeId)
    {
        var trainee = await repository.GetByEmailAsync(email);
        return trainee is null || trainee.Id == traineeId;
    }

    public async Task<List<Trainee>> GetAll()
    {
        return await repository.GetAllAsync();
    }
}