using Application.Dto;
using Domain.Entities;

namespace WebApplication1.Models
{
    public class ResourcesListViewModel
    {
        public List<CurrentProject> Projects { get; set; }
        public List<InternshipDirection> Directions { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<Trainee> TraineesByProject  { get; set; }
        public List<Trainee> TraineesByDirection  { get; set; }

        public int TotalPagesProjects { get; set; }
        public int TotalPagesDirections { get; set; }
        public string ActiveTab { get; set; }
        public string SearchQuery { get; set; }
        public string SortOrder { get; set; }

        public ResourcesListViewModel(string activeTab, string searchQuery, string sortOrder, int totalPagesProjects, int totalPagesDirections, List<CurrentProject> projects, List<InternshipDirection> directions,
            List<Trainee> traineesByProject, List<Trainee> traineesByDirection, int currentPage = 1, int pageSize = 10)
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