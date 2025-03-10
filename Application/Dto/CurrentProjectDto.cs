using Domain.Entities;

namespace Application.Dto;

public class CurrentProjectDto(CurrentProject currentProject)
{
    public Guid Id { get; set; } = currentProject.Id;
    public string Name { get; set; } = currentProject.Name;
    public int CountTrainees { get; set; } = currentProject.CountTrainees;
}