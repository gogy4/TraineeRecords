namespace Application.Dto;

public class ResourcePropertiesDto
{
    public List<CurrentProjectDto> Projects { get; set; }
    public List<InternshipDirectionDto> Directions { get; set; }
    public Dictionary<Guid, string> ProjectNames { get; set; }
    public Dictionary<Guid, string> DirectionNames { get; set; }

    public ResourcePropertiesDto(List<CurrentProjectDto> projects, List<InternshipDirectionDto> directions,
        Dictionary<Guid, string> projectNames, Dictionary<Guid, string> directionNames)
    {
        Projects = projects;
        Directions = directions;
        ProjectNames = projectNames;
        DirectionNames = directionNames;
    }
}