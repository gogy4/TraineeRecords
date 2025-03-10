using Application.Dto;

namespace WebApplication1.Models;

public class OperationResourceViewModel
{
    public string ResourceType { get; set; }
    public string? Errors { get; set; }
    public string? Success { get; set; }
    public List<TraineeDto> Trainees { get; set; }
    public List<CurrentProjectDto> Projects { get; set; }
    public List<InternshipDirectionDto> Directions { get; set; }
    public Guid? ResourceId { get; set; }
    public string ResourceName { get; set; }

    public OperationResourceViewModel()
    {
    }

    public OperationResourceViewModel(string resourceType, string errors, string success, 
        List<TraineeDto> trainees = null, List<CurrentProjectDto> projects = null,
        List<InternshipDirectionDto> directions = null, Guid? resourceId = null, string resourceName = null)
    {
        ResourceType = resourceType;
        Errors = errors;
        Success = success;
        Trainees = trainees;
        Projects = projects;
        Directions = directions;
        ResourceId = resourceId;
        ResourceName = resourceName;
    }
}