using Application.Dto;
using Domain.Entities;

namespace WebApplication1.Models;

public class TraineeListViewModel
{
    public List<TraineeDto> Trainees { get; set; }
    public Dictionary<Guid, (string direction, string project)> TraineeResources { get; set; } = new();
    public List<string> CurrentProjects { get; set; } 
    public List<string> InternshipDirections { get; set; } 
    public string SelectedDirection { get; set; }
    public string SelectedProject { get; set; }
    


    public TraineeListViewModel(List<TraineeDto> trainees, ResourcePropertiesDto resourceProperties, string selectedDirection = null, string selectedProject = null)
    {
        Trainees = trainees;
        CurrentProjects = resourceProperties.ProjectNames.Values.ToList();
        InternshipDirections = resourceProperties.DirectionNames.Values.ToList();
        SelectedDirection = selectedDirection;
        SelectedProject = selectedProject;
        InitTraineeResources(resourceProperties);
    }

    private void InitTraineeResources(ResourcePropertiesDto resourceProperties)
    {
        var projectNames = resourceProperties.ProjectNames;
        var directionNames = resourceProperties.DirectionNames;

        foreach (var trainee in Trainees)
        {
            var directionName = directionNames.GetValueOrDefault(trainee.InternshipDirectionId);
            var projectName = projectNames.GetValueOrDefault(trainee.CurrentProjectId);

            TraineeResources[trainee.Id] = (directionName, projectName);
        }
    }

    public TraineeListViewModel()
    {
    }
}