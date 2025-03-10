using Application.Dto;

namespace WebApplication1.Models;

public class TraineeViewModel
{
    public List<string>? InternshipDirections { get; set; }
    public List<string>? Projects { get; set; }
    public string? Errors { get; set; }
    public string? Success { get; set; }
    public TraineeDto? Trainee { get; set; }

    public TraineeViewModel(List<string> internshipDirections, List<string> projects,
        string? errors, string? success, TraineeDto traineeDto = null)
    {
        Trainee = traineeDto;
        InternshipDirections = internshipDirections;
        Projects = projects;
        Errors = errors;
        Success = success;
    }

    public TraineeViewModel()
    {
    }
}