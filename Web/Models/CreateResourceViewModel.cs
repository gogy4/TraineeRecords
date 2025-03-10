using Application.Dto;

namespace WebApplication1.Models;

public class CreateResourceViewModel
{
    public string ResourceType { get; set; }
    
    public string? Errors { get; set; }
    public string? Success { get; set; }
    public List<TraineeDto> Trainees { get; set; }

    public CreateResourceViewModel()
    {
        
    }

    public CreateResourceViewModel(List<TraineeDto> trainees, string resourceType, string? errors, string? success)
    {
        ResourceType = resourceType;
        Errors = errors;
        Success = success;
        Trainees = trainees;
    }
}