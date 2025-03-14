﻿using Application.Dto;
using Application.DtoValidator;
using Domain.Entities;

namespace Application.Services;

public class TraineeService(ITraineeRepository repository, ResourceService resourceService)
{
    public async Task Create(TraineeDto traineeDto)
    {
        var validator = new TraineeCreateValidator(this);
        var result = await validator.ValidateAsync(traineeDto);
        if (!result.IsValid)
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));

        var ids = await resourceService.GetIds(traineeDto.InternshipDirectionName, traineeDto.CurrentProjectName);
        await resourceService.ChangeCountTrainees(null, null, ids.InternshipDirectionId, ids.CurrentProjectId);

        var trainee = new Trainee(traineeDto.Name, traineeDto.Surname, traineeDto.Gender, traineeDto.Email,
            traineeDto.PhoneNumber, traineeDto.DateOfBirth, ids.InternshipDirectionId ?? Guid.Empty,
            ids.CurrentProjectId ?? Guid.Empty);

        await repository.AddAsync(trainee);
    }

    public async Task Edit(TraineeDto traineeDto)
    {
        ArgumentNullException.ThrowIfNull(traineeDto);
        if (traineeDto.Id == Guid.Empty) throw new ArgumentNullException("Такого стажера не существует");

        var validator = new TraineeCreateValidator(this);
        var result = await validator.ValidateAsync(traineeDto);
        if (!result.IsValid)
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        var trainee = await GetById(traineeDto.Id);
        var oldIds = (trainee.CurrentProjectId, trainee.InternshipDirectionId);
        var newIds = await resourceService.GetIds(traineeDto.InternshipDirectionName, traineeDto.CurrentProjectName);
        if (oldIds != newIds)
            await resourceService.ChangeCountTrainees(oldIds.InternshipDirectionId,
                oldIds.CurrentProjectId, newIds.InternshipDirectionId, newIds.CurrentProjectId);

        var a = newIds.InternshipDirectionId ?? Guid.Empty;
        var d = newIds.CurrentProjectId ?? Guid.Empty;
        trainee.Edit(traineeDto.Name, traineeDto.Surname, traineeDto.Gender, traineeDto.Email, traineeDto.PhoneNumber,
            traineeDto.DateOfBirth, newIds.InternshipDirectionId ?? Guid.Empty, newIds.CurrentProjectId ?? Guid.Empty);

        await repository.UpdateAsync(trainee);
    }


    public async Task<List<TraineeDto>> GetByFilter(string directionFilter, string currentProjectId)
    {
        var (projectId, directionId) = await resourceService.GetIds(directionFilter, currentProjectId);
        return (await repository.GetByResourceIds(directionId, projectId))
            .Select(t => new TraineeDto(t))
            .ToList();
    }

    public async Task<Dictionary<Guid, List<TraineeDto>>> GetByDirection(params Guid[] directionIds)
    {
        var traineeList = new Dictionary<Guid, List<TraineeDto>>();
        foreach (var directionId in directionIds)
        {
            var trainee = (await repository.GetByResourceIds(directionId, Guid.Empty))
                .Select(t => new TraineeDto(t))
                .ToList();
            traineeList[directionId] = trainee;
        }

        return traineeList;
    }

    public async Task<Dictionary<Guid, List<TraineeDto>>> GetByProject(params Guid[] projectIds)
    {
        var traineeList = new Dictionary<Guid, List<TraineeDto>>();
        foreach (var projectId in projectIds)
        {
            var trainee = (await repository.GetByResourceIds(Guid.Empty, projectId))
                .Select(t => new TraineeDto(t))
                .ToList();
            ;
            traineeList[projectId] = trainee;
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

    public async Task<bool> EmailHaveNotUsed(string email, Guid traineeId)
    {
        var trainee = await repository.GetByEmailAsync(email);
        return trainee is null || trainee.Id == traineeId;
    }

    public async Task<List<TraineeDto>> GetAll()
    {
        return (await repository.GetAllAsync())
            .Select(t => new TraineeDto(t))
            .ToList();
    }

    public async Task<(ResourcePropertiesDto resourcePropertiesDto, TraineeDto traineeDto)> GetTraineeWithResources(
        Guid traineeId)
    {
        var trainee = await GetById(traineeId);
        var resourcePropertiesDto = await resourceService.GetResourceProperties();
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

    public async Task EditTraineeResource(Guid traineeId, Guid resourceId, string resourceType)
    {
        var trainee = await GetById(traineeId);
        var traineeDto = new TraineeDto(trainee);

        switch (resourceType)
        {
            case "Direction":
                traineeDto.InternshipDirectionId = resourceId;
                break;
            case "Project":
                traineeDto.CurrentProjectId = resourceId;
                break;
        }

        await Edit(traineeDto);
    }

    public async Task<List<TraineeDto>> GetTraineeWithoutResource(Guid resourceId, string resourceType)
    {
        return resourceType == "Direction"
            ? (await GetAll())
            .Where(t => t.InternshipDirectionId != resourceId)
            .ToList()
            : (await GetAll())
            .Where(t => t.CurrentProjectId != resourceId)
            .ToList();
    }
}