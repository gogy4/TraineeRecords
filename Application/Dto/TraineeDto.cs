using Domain.Entities;

namespace Application.Dto;

public class TraineeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public Guid InternshipDirectionId { get; set; }
    public Guid CurrentProjectId { get; set; }
    
    
    public string InternshipDirectionName { get; set; }
    public string CurrentProjectName { get; set; }


    public TraineeDto(Trainee trainee, string directionName = null, string projectName = null)
    {
        Id = trainee.Id;
        Name = trainee.Name;
        Surname = trainee.Surname;
        Gender = trainee.Gender;
        DateOfBirth = trainee.DateOfBirth;
        Email = trainee.Email;
        PhoneNumber = trainee.PhoneNumber;
        InternshipDirectionId = trainee.InternshipDirectionId;
        CurrentProjectId = trainee.CurrentProjectId;
        InternshipDirectionName = directionName;
        CurrentProjectName = projectName;
    }

    public TraineeDto()
    {
        
    }
}