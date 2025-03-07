using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.Dto;

public class TraineeDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string InternshipDirection { get; set; }
    [Required]
    public string CurrentProject { get; set; }

    public TraineeDto()
    {
        
    }
    public TraineeDto(Trainee trainee, string currentProject, string internshipDirection)
    {
        Id = trainee.Id;
        Name = trainee.Name;
        Surname = trainee.Surname;
        Gender = trainee.Gender;
        Email = trainee.Email;
        PhoneNumber = trainee.PhoneNumber;
        DateOfBirth = trainee.DateOfBirth;
        InternshipDirection = internshipDirection;
        CurrentProject = currentProject;
    }
}