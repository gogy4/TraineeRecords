using Application.Dto;
using Domain.Entities;

namespace WebApplication1.Models
{
    public class ResourcesListViewModel
    {
        public List<CurrentProjectDto> Projects { get; set; }
        public List<InternshipDirectionDto> Directions { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public Dictionary<Guid, List<TraineeDto>> TraineesByProject { get; set; }
        public Dictionary<Guid, List<TraineeDto>> TraineesByDirection { get; set; }

        public int TotalPagesProjects { get; set; }
        public int TotalPagesDirections { get; set; }
        public string ActiveTab { get; set; }
        public string SearchQuery { get; set; }
        public string SortOrder { get; set; }

        public ResourcesListViewModel(string activeTab, string searchQuery, string sortOrder, int totalPagesProjects, int totalPagesDirections, List<CurrentProjectDto> projects, List<InternshipDirectionDto> directions,
            Dictionary<Guid, List<TraineeDto>> traineesByProject, Dictionary<Guid, List<TraineeDto>> traineesByDirection, int currentPage = 1, int pageSize = 10)
        {
            SortOrder = sortOrder;
            ActiveTab = activeTab;
            SearchQuery = searchQuery;
            Projects = projects;
            Directions = directions;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPagesProjects = totalPagesProjects;
            TotalPagesDirections = totalPagesDirections;
            TraineesByDirection = traineesByDirection;
            TraineesByProject = traineesByProject;
        }

        public ResourcesListViewModel()
        {
            
        }
    }
}