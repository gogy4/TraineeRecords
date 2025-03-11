using Application.Services;

public class DeleteResourceService(
    CurrentProjectService currentProjectService,
    InternshipDirectionsService internshipDirectionsService)
{
    private async Task DeleteProject(Guid projectId)
    {
        var project = await currentProjectService.GetById(projectId);
        if (project is null || project.CountTrainees == 0) await currentProjectService.Delete(projectId);
        else throw new ArgumentException("Нельзя удалить проект, который выполняют стажеры");
    }

    private async Task DeleteDirection(Guid directionId)
    {
        var direction = await internshipDirectionsService.GetById(directionId);
        if (direction is null || direction.CountTrainees == 0) await internshipDirectionsService.Delete(directionId);
        else throw new ArgumentException("Нельзя удалить направление, если на него записаны стажеры");
    }

    public async Task DeleteResource(Guid resourceId, string resourceType)
    {
        if (resourceType == "Direction")
            await DeleteDirection(resourceId);
        else await DeleteProject(resourceId);
    }
}