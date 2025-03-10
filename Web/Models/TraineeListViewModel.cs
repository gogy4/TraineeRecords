using Application.Dto;
using Domain.Entities;

namespace WebApplication1.Models;

public class TraineeListViewModel
{
    public List<TraineeDto> Trainees { get; set; }
    public Dictionary<Guid, (string direction, string project)> TraineeResources { get; set; } = new();


    public TraineeListViewModel(List<TraineeDto> trainees, ResourcePropertiesDto resourceProperties)
    {
        Trainees = trainees;
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