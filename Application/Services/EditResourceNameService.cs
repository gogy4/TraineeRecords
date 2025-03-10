namespace Application.Services;

public class EditResourceNameService(CurrentProjectServices currentProjectServices, InternshipDirectionsServices internshipDirectionsServices)
{
    public async Task ChangeProjectName(Guid projectId, string newName)
    {
        if (newName is null || newName.Length == 0) throw new ArgumentException("Нельзя изменить на пустое название");
        await currentProjectServices.ChangeName(projectId, newName);
    }
    
    public async Task ChangeDirectionName(Guid directionId, string newName)
    {
        if (newName is null || newName.Length == 0) throw new ArgumentException("Нельзя изменить на пустое название");

        await internshipDirectionsServices.ChangeName(directionId, newName);
    }
}