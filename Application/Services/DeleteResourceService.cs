using Domain.Entities;

namespace Application.Services;

public class DeleteResourceService(TraineeServices traineeServices, CurrentProjectServices currentProjectServices, InternshipDirectionsServices internshipDirectionsServices)
{
    private async Task DeleteProject(Guid projectId)
    {
        var project = (await traineeServices.GetByProject(projectId))[projectId];
        if (project is null || project.Count == 0) await currentProjectServices.Delete(projectId);
        else throw new ArgumentException("Нельзя удалить проект, который выполняют стажеры");
    }
    
    private async Task DeleteDirection(Guid directionId)
    {
        var project = (await traineeServices.GetByDirection(directionId))[directionId];
        if (project is null || project.Count == 0) await internshipDirectionsServices.Delete(directionId);
        else throw new ArgumentException("Нельзя удалить направление, если на него записаны стажеры");
    }

    public async Task DeleteResource(Guid resourceId, string resourceType)
    {
        if (resourceType == "Direction")
        {
            await DeleteDirection(resourceId);
        }
        else await DeleteProject(resourceId);
    }
}