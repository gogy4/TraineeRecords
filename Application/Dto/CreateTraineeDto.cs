using System.ComponentModel.DataAnnotations;

namespace Application.Dto;

public class CreateTraineeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public string InternshipDirection { get; set; }
    public string CurrentProject { get; set; }
}