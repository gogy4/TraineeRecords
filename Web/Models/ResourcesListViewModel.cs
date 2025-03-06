using Application.Dto;
using Domain.Entities;

namespace WebApplication1.Models;

public class ResourcesListViewModel
{
    public List<CurrentProject> Projects { get; set; }
    public List<InternshipDirection> Directions { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalProjects { get; set; }
    public int TotalDirections { get; set; }

    public ResourcesListViewModel(List<CurrentProject> projects, List<InternshipDirection> directions,
        int currentPage=1, int pageSize=10)
    {
        Projects = projects;
        Directions = directions;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalProjects = projects.Count;
        TotalDirections = directions.Count;
    }
}