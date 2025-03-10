using Application.Dto;

namespace WebApplication1.Models;

public class DeleteResourceViewModel
{
    public List<CurrentProjectDto> Projects { get; set; }
    public List<InternshipDirectionDto> Directions { get; set; }
    public string Errors { get; set; }
    public string Success { get; set; }
    
    public string ResourceType { get; set; }

    public DeleteResourceViewModel()
    {
    }

    public DeleteResourceViewModel(List<CurrentProjectDto> projects, List<InternshipDirectionDto> directions,
        string? errors, string? success, string? resourceType)
    {
        Projects = projects;
        Directions = directions;
        Errors = errors;
        Success = success;
        ResourceType = resourceType;
    }
}