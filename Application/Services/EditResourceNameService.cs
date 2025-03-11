namespace Application.Services;

public class EditResourceNameService(CurrentProjectServices currentProjectServices, InternshipDirectionsServices internshipDirectionsServices)
{
    public async Task ChangeResourceName(Guid resourceId, string resourceType, string newName)
    {
        if (newName is null || newName.Length == 0) throw new ArgumentException("Нельзя изменить на пустое название");
        if (resourceType == "Direction") await internshipDirectionsServices.ChangeName(resourceId, newName);
        else await currentProjectServices.ChangeName(resourceId, newName);
    }
    
    public async Task<string> GetResourceById(Guid resourceId, string resourceType)
    {
        return resourceType == "Direction"
            ? (await internshipDirectionsServices.GetById(resourceId)).Name
            : (await currentProjectServices.GetById(resourceId)).Name;
    }
}