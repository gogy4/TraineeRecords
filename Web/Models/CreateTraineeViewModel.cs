using Application.Dto;

namespace WebApplication1.Models;

public class CreateTraineeViewModel
{
    public List<string>? InternshipDirections { get; set; }
    public List<string>? Projects { get; set; }
    public string? Errors { get; set; }
    public string? Success { get; set; }

    public CreateTraineeViewModel(List<string> internshipDirections, List<string> projects,
        string? errors, string? success)
    {
        InternshipDirections = internshipDirections;
        Projects = projects;
        Errors = errors;
        Success = success;
    }
}