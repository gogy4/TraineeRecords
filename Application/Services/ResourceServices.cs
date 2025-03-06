using System.ComponentModel.Design;
using Domain.Entities;

namespace Application.Services;

public class ResourceServices(
    CurrentProjectServices currentProjectServices,
    InternshipDirectionsServices internshipDirectionsService)
{
    public async Task ChangeCountTrainees(string internshipDirectionName,
        string currentProjectName)
    {
        var (internshipDirectionId, currentProjectId) = await GetIds(internshipDirectionName, currentProjectName);
        var project = await currentProjectServices.GetById(currentProjectId);
        if (project is null)
        {
            await currentProjectServices.Create(currentProjectName);
        }
        else
        {
            project.IncrementTrainees();
            await currentProjectServices.Update(project);
        }

        var direction = await internshipDirectionsService.GetById(internshipDirectionId);
        if (direction is null)
        {
            await internshipDirectionsService.Create(internshipDirectionName);
        }
        else
        {
            direction.IncrementTrainees();
            await internshipDirectionsService.Update(direction);
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

    public async Task<(Guid currentProjectId, Guid internshipDirectionId)> GetIds(string internshipDirectionName, string currentProjectName)
    {
        var project = await currentProjectServices.GetByName(currentProjectName);
        var internshipDirection = await internshipDirectionsService.GetByName(internshipDirectionName);
        var projectId = project is null ? Guid.Empty : project.Id;
        var internshipDirectionId = internshipDirection is null ? Guid.Empty : internshipDirection.Id;
        return (projectId, internshipDirectionId);
    }
}