using Domain.Entities;

namespace Application.Dto;

public class TraineeListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public Guid InternshipDirectionId { get; set; }
    public Guid CurrentProjectId { get; set; }

    public TraineeListDto(Trainee trainee)
    {
        Id = trainee.Id;
        Name = trainee.Name;
        Surname = trainee.Surname;
        Email = trainee.Email;
        PhoneNumber = trainee.PhoneNumber;
        InternshipDirectionId = trainee.InternshipDirectionId;
        CurrentProjectId = trainee.CurrentProjectId;
    }
}