using Application.Dto;
using Domain.Entities;

namespace WebApplication1.Models;

public class TraineeListViewModel
{
    public List<TraineeListDto> Trainees { get; set; }
    public Dictionary<Guid, (string direction, string project)> TraineeResources { get; set; } = new();


    public TraineeListViewModel(List<TraineeListDto> trainees, ResourcePropertiesDto resourceProperties)
    {
        Trainees = trainees;
        InitTraineeResources(resourceProperties);
    }

    private void InitTraineeResources(ResourcePropertiesDto resourceProperties)
    {
        var projectNames = resourceProperties.ProjectNames;
        var directionNames = resourceProperties.DirectionNames;

        foreach (var trainee in Trainees)
            TraineeResources[trainee.Id] = (directionNames[trainee.InternshipDirectionId],
                projectNames[trainee.CurrentProjectId]);
    }

    public TraineeListViewModel()
    {
        
    }
}