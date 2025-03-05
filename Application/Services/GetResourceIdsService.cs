using System.ComponentModel.Design;
using Domain.Entities;

namespace Application.Services;

public class GetResourceIdsService(
    ICurrentProjectRepository currentProjectRepository,
    IInternshipDirectionRepository internshipDirectionRepository)
{
    public async Task<(Guid currentProjectId, Guid internshipDirectionId)> GetIds(string internshipDirectionName,
        string currentProjectName)
    {
        var project = (await currentProjectRepository.GetByNameAsync(currentProjectName))
            .FirstOrDefault();
        
        if (project is null)
        {
            project = new CurrentProject(currentProjectName);
            await currentProjectRepository.AddAsync(project);
        }
        else 
        {
            project.IncrementTrainees();
            await currentProjectRepository.UpdateAsync(project);
        }

        var direction = (await internshipDirectionRepository.GetByNameAsync(internshipDirectionName))
            .FirstOrDefault();
        
        if (direction is null)
        {
            direction = new InternshipDirection(internshipDirectionName);
            await internshipDirectionRepository.AddAsync(direction);
        }
        else
        {
            direction.IncrementTrainees();
            await internshipDirectionRepository.UpdateAsync(direction);
        }

        return (project.Id, direction.Id);
    }
}