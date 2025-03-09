using Domain.Entities;

namespace Application.Dto;

public class InternshipDirectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int CountTrainees { get; set; }

    public InternshipDirectionDto(InternshipDirection internshipDirection)
    {
        Id = internshipDirection.Id;
        Name = internshipDirection.Name;
        CountTrainees = internshipDirection.CountTrainees;
    }
}