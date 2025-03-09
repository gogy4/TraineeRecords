using Domain.Entities;
using System.Collections.Generic;
using Application.Dto;


public class ResourceResultPageDto(
    List<CurrentProjectDto> projects,
    List<InternshipDirectionDto> directions,
    int totalPagesProjects,
    int totalPagesDirections)
{
    public List<CurrentProjectDto> Projects { get; } = projects;
    public List<InternshipDirectionDto> Directions { get; } = directions;
    public int TotalPagesProjects { get; } = totalPagesProjects;
    public int TotalPagesDirections { get; } = totalPagesDirections;
}