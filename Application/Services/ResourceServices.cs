using System.ComponentModel.Design;
using Domain.Entities;

namespace Application.Services;

public class ResourceServices(
    CurrentProjectServices currentProjectServices,
    InternshipDirectionsServices internshipDirectionsService)
{
    public async Task<(Guid currentProjectId, Guid internshipDirectionId)> GetIds(string internshipDirectionName,
        string currentProjectName)
    {
        var project = await currentProjectServices.GetByName(currentProjectName);

        if (project is null)
        {
            project = await currentProjectServices.Create(currentProjectName);
        }
        else
        {
            project.IncrementTrainees();
            await currentProjectServices.Update(project);
        }

        var direction = await internshipDirectionsService.GetByName(internshipDirectionName);

        if (direction is null)
        {
            direction = await internshipDirectionsService.Create(internshipDirectionName);
        }
        else
        {
            direction.IncrementTrainees();
            await internshipDirectionsService.Update(direction);
        }

        return (project.Id, direction.Id);
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

    public async Task<InternshipDirection> CreateInternshipDirection(string internshipDirectionName)
    {
        var internshipDirection = await internshipDirectionsService.Create(internshipDirectionName);
        return internshipDirection;
    }

    public async Task<CurrentProject> CreateCurrentProject(string projectName)
    {
        var project = await currentProjectServices.Create(projectName);
        return project;
    }
}