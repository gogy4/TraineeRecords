namespace Application.Services;

public class EditResourceNameService(
    CurrentProjectService currentProjectService,
    InternshipDirectionsService internshipDirectionsService)
{
    public async Task ChangeResourceName(Guid resourceId, string resourceType, string newName)
    {
        if (newName is null || newName.Length == 0) throw new ArgumentException("Нельзя изменить на пустое название");
        if (resourceType == "Direction") await internshipDirectionsService.ChangeName(resourceId, newName);
        else await currentProjectService.ChangeName(resourceId, newName);
    }

    public async Task<string> GetResourceById(Guid resourceId, string resourceType)
    {
        return resourceType == "Direction"
            ? (await internshipDirectionsService.GetById(resourceId)).Name
            : (await currentProjectService.GetById(resourceId)).Name;
    }
}