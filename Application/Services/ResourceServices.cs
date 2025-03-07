using System.ComponentModel.Design;
using Domain.Entities;

namespace Application.Services;

public class ResourceServices(
    CurrentProjectServices currentProjectServices,
    InternshipDirectionsServices internshipDirectionsService)
{
    public async Task ChangeCountTrainees(Guid? oldInternshipDirectionId, Guid? oldCurrentProjectId,
        Guid? newInternshipDirectionId, Guid? newCurrentProjectId)
    {
        if (oldCurrentProjectId.HasValue && oldCurrentProjectId != newCurrentProjectId)
        {
            var oldProject = await currentProjectServices.GetById(oldCurrentProjectId.Value);
            if (oldProject != null)
            {
                oldProject.DecreaseTraineeCount();
                await currentProjectServices.Update(oldProject);
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
            var newProject = await currentProjectServices.GetById(newCurrentProjectId.Value);
            if (newProject != null)
            {
                newProject.IncreaseTraineeCount();
                await currentProjectServices.Update(newProject);
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


    public async Task<(List<string> internShipDirectionNames, List<string> currentProjectNames)> GetNameResources()
    {
        var internShipDirectionNames = (await internshipDirectionsService.GetAll())
            .Select(d => d.Name)
            .ToList();
        var currentProjectNames = (await currentProjectServices.GetAll())
            .Select(p => p.Name)
            .ToList();

        return (internShipDirectionNames, currentProjectNames);
    }

    public async Task CreateInternshipDirection(string internshipDirectionName)
    {
        await internshipDirectionsService.Create(internshipDirectionName);
    }

    public async Task CreateCurrentProject(string projectName)
    {
        await currentProjectServices.Create(projectName);
    }

    public async Task<(Guid currentProjectId, Guid internshipDirectionId)> GetIds(string internshipDirectionName,
        string currentProjectName)
    {
        var project = await currentProjectServices.GetByName(currentProjectName);
        var internshipDirection = await internshipDirectionsService.GetByName(internshipDirectionName);
        var projectId = project is null ? Guid.Empty : project.Id;
        var internshipDirectionId = internshipDirection is null ? Guid.Empty : internshipDirection.Id;
        return (projectId, internshipDirectionId);
    }
}