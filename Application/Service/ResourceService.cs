using System.ComponentModel.Design;
using Application.Dto;
using Domain.Entities;

namespace Application.Services;

public class ResourceService(
    CurrentProjectService currentProjectService,
    InternshipDirectionsService internshipDirectionsService)
{
    public async Task ChangeCountTrainees(Guid? oldInternshipDirectionId, Guid? oldCurrentProjectId,
        Guid? newInternshipDirectionId, Guid? newCurrentProjectId)
    {
        if (oldCurrentProjectId.HasValue && oldCurrentProjectId != newCurrentProjectId)
        {
            var oldProject = await currentProjectService.GetById(oldCurrentProjectId.Value);
            if (oldProject != null)
            {
                oldProject.DecreaseTraineeCount();
                await currentProjectService.Update(oldProject);
            }
        }

        if (oldInternshipDirectionId.HasValue && oldInternshipDirectionId != newInternshipDirectionId)
        {
            var oldDirection = await internshipDirectionsService.GetById(oldInternshipDirectionId.Value);
            if (oldDirection != null)
            {
                oldDirection.DecreaseTraineeCount();
                await internshipDirectionsService.Update(oldDirection);
            }
        }

        if (newCurrentProjectId.HasValue && oldCurrentProjectId != newCurrentProjectId)
        {
            var newProject = await currentProjectService.GetById(newCurrentProjectId.Value);
            if (newProject != null)
            {
                newProject.IncreaseTraineeCount();
                await currentProjectService.Update(newProject);
            }
        }

        if (newInternshipDirectionId.HasValue && oldInternshipDirectionId != newInternshipDirectionId)
        {
            var newDirection = await internshipDirectionsService.GetById(newInternshipDirectionId.Value);
            if (newDirection != null)
            {
                newDirection.IncreaseTraineeCount();
                await internshipDirectionsService.Update(newDirection);
            }
        }
    }

    private async Task<(List<CurrentProjectDto> projects, List<InternshipDirectionDto> directions)> GetAll()
    {
        var projects = (await currentProjectService.GetAll())
            .Select(p => new CurrentProjectDto(p))
            .ToList();
        var directions = (await internshipDirectionsService.GetAll())
            .Select(d => new InternshipDirectionDto(d))
            .ToList();

        return (projects, directions);
    }

    public async Task<Guid> CreateInternshipDirection(string internshipDirectionName)
    {
        if (await internshipDirectionsService.GetByName(internshipDirectionName) is not null)
            throw new ArgumentException("Такое направление уже существует");
        return await internshipDirectionsService.Create(internshipDirectionName);
    }

    public async Task<Guid> CreateCurrentProject(string projectName)
    {
        if (await currentProjectService.GetByName(projectName) is not null)
            throw new ArgumentException("Такой проект уже существует");
        return await currentProjectService.Create(projectName);
    }

    public async Task<(Guid currentProjectId, Guid internshipDirectionId)> GetIds(string internshipDirectionName,
        string currentProjectName)
    {
        var project = await currentProjectService.GetByName(currentProjectName);
        var internshipDirection = await internshipDirectionsService.GetByName(internshipDirectionName);
        var projectId = project is null ? Guid.Empty : project.Id;
        var internshipDirectionId = internshipDirection is null ? Guid.Empty : internshipDirection.Id;
        return (projectId, internshipDirectionId);
    }

    private async Task<(List<CurrentProjectDto> projects, List<InternshipDirectionDto> directions)> Sort(
        string sortOrder, List<CurrentProjectDto> projects, List<InternshipDirectionDto> directions)
    {
        switch (sortOrder)
        {
            case "trainees_desc":
                projects = projects.OrderBy(p => p.CountTrainees).ToList();
                directions = directions.OrderBy(d => d.CountTrainees).ToList();
                break;
            case "trainees":
                projects = projects.OrderByDescending(p => p.CountTrainees).ToList();
                directions = directions.OrderByDescending(d => d.CountTrainees).ToList();
                break;
            case "name_desc":
                projects = projects.OrderByDescending(p => p.Name).ToList();
                directions = directions.OrderByDescending(d => d.Name).ToList();
                break;
            default:
                projects = projects.OrderBy(p => p.Name).ToList();
                directions = directions.OrderBy(d => d.Name).ToList();
                break;
        }

        return (projects, directions);
    }

    public async Task<ResourceResultPageDto> GetFilteredSortedPaged(
        string searchQuery, string sortOrder, int page, int pageSize)
    {
        var projects = (await currentProjectService.GetByFilter(searchQuery))
            .Select(p => new CurrentProjectDto(p))
            .ToList();
        var directions = (await internshipDirectionsService.GetByFilter(searchQuery))
            .Select(d => new InternshipDirectionDto(d))
            .ToList();

        (projects, directions) = await Sort(sortOrder, projects, directions);

        var totalProjects = projects.Count;
        var totalDirections = directions.Count;

        return new ResourceResultPageDto(
            projects.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            directions.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            (int)Math.Ceiling((double)totalProjects / pageSize),
            (int)Math.Ceiling((double)totalDirections / pageSize)
        );
    }

    public async Task<ResourcePropertiesDto> GetResourceProperties(string dirFilter = null, string projectFilter = null)
    {
        var (projects, directions) = await GetAll();
        var directionsName = new Dictionary<Guid, string>();
        var projectsName = new Dictionary<Guid, string>();
        foreach (var project in projects.Where(p => p.Name == projectFilter || projectFilter == null))
            projectsName[project.Id] = project.Name;
        foreach (var direction in directions.Where(p => p.Name == dirFilter || dirFilter == null))
            directionsName[direction.Id] = direction.Name;
        var modelDto = new ResourcePropertiesDto(projects, directions, projectsName, directionsName);
        return modelDto;
    }

    private async Task<bool> ResourceExists(string directionName = null, string projectName = null)
    {
        if (directionName is not null) return await internshipDirectionsService.GetByName(directionName) is null;
        return await currentProjectService.GetByName(projectName) is null;
    }
}