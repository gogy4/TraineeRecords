using Domain.Entities;

namespace WebApplication1.Models;

public class TraineeListViewModel
{
    public List<Trainee> Trainees { get; set; }
    public List<InternshipDirection> Directions { get; set; }
    public List<CurrentProject> Projects { get; set; }
    public (List<string> directionsName, List<string> projectsName) ResourcesName { get; set; }


    public TraineeListViewModel(List<Trainee> trainees, List<InternshipDirection> directions, List<CurrentProject> projects, (List<string> directionsName, List<string> projectsName) resourcesName)
    {
        Trainees = trainees;
        Directions = directions;
        Projects = projects;
        ResourcesName = resourcesName;
    }
}