using Domain.Entities;

namespace Application.Dto;

public class CurrentProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CountTrainees { get; set; }

    public CurrentProjectDto(CurrentProject currentProject)
    {
        Id = currentProject.Id;
        Name = currentProject.Name;
        CountTrainees = currentProject.CountTrainees;
    }
}