using Domain.Entities;

namespace Application.Dto;

public class InternshipDirectionDto(InternshipDirection internshipDirection)
{
    public Guid Id { get; set; } = internshipDirection.Id;
    public string Name { get; set; } = internshipDirection.Name;
    public int CountTrainees { get; set; } = internshipDirection.CountTrainees;
}