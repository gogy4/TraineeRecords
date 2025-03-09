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


    public async Task<List<TraineeListDto>> GetByFilter(string directionFilter, string currentProjectId)
    {
        var (projectId, directionId) = await resourceServices.GetIds(directionFilter, currentProjectId);
        return (await repository.GetByResourceIds(directionId, projectId))
            .Select(t => new TraineeListDto(t))
            .ToList();
    }

    public async Task<Dictionary<Guid, List<TraineeListDto>>> GetByDirection(List<InternshipDirectionDto> directions)
    {
        var traineeList = new Dictionary<Guid, List<TraineeListDto>>();
        foreach (var direction in directions)
        {
            var trainee = (await repository.GetByResourceIds(direction.Id, Guid.Empty))
                .Select(t => new TraineeListDto(t))
                .ToList();
            traineeList[direction.Id] = trainee;
        }

        return traineeList;
    }

    public async Task<Dictionary<Guid, List<TraineeListDto>>> GetByProject(List<CurrentProjectDto> projects)
    {
        var traineeList = new Dictionary<Guid, List<TraineeListDto>>();
        foreach (var project in projects)
        {
            var trainee = (await repository.GetByResourceIds(Guid.Empty, project.Id))
                .Select(t => new TraineeListDto(t))
                .ToList();
            ;
            traineeList[project.Id] = trainee;
        }

        return traineeList;
    }

    private async Task<Trainee> GetById(Guid traineeId)
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

    public async Task<List<TraineeListDto>> GetAll()
    {
        return (await repository.GetAllAsync())
            .Select(t => new TraineeListDto(t))
            .ToList();
    }

    public async Task<(ResourcePropertiesDto resourcePropertiesDto, TraineeDto traineeDto)> GetTraineeWithResources(Guid traineeId)
    {
        var trainee = await GetById(traineeId);
        var resourcePropertiesDto = await resourceServices.GetResourceProperties();
        var direction = resourcePropertiesDto.DirectionNames
            .Where(kv => kv.Key == trainee.InternshipDirectionId)
            .Select(kv => kv.Value)
            .FirstOrDefault();
        
        var project = resourcePropertiesDto.ProjectNames
            .Where(kv => kv.Key == trainee.CurrentProjectId)
            .Select(kv => kv.Value)
            .FirstOrDefault();
        
        var traineeDto = new TraineeDto(trainee, project, direction);
        return (resourcePropertiesDto, traineeDto);
    }
}