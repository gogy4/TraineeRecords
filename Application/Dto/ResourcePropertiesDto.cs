namespace Application.Dto;

public class ResourcePropertiesDto(
    List<CurrentProjectDto> projects,
    List<InternshipDirectionDto> directions,
    Dictionary<Guid, string> projectNames,
    Dictionary<Guid, string> directionNames)
{
    public List<CurrentProjectDto> Projects { get; set; } = projects;
    public List<InternshipDirectionDto> Directions { get; set; } = directions;
    public Dictionary<Guid, string> ProjectNames { get; set; } = projectNames;
    public Dictionary<Guid, string> DirectionNames { get; set; } = directionNames;
}