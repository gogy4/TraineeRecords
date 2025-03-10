using Application.Dto;

namespace WebApplication1.Models;

public class CreateInternshipDirectionViewModel
{
    public List<TraineeDto> Trainees { get; set; }
    public string InternshipDirection { get; set; }
    public Guid? TraineeId { get; set; }

    public CreateInternshipDirectionViewModel()
    {
        
    }

    public CreateInternshipDirectionViewModel(List<TraineeDto> trainees, Guid? traineeId = null, string internshipDirection = null)
    {
        Trainees = trainees;
        TraineeId = traineeId;
        InternshipDirection = internshipDirection;
    }
}
