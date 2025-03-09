using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Controllers;

public class ResourcesListController(TraineeServices traineeServices, ResourceServices resourceServices) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string activeTab = "projects", string searchQuery = "", 
        string sortOrder = "name", int page = 1, int pageSize = 5)
    {
        var result = await resourceServices.GetFilteredSortedPaged(searchQuery, sortOrder, page, pageSize);
        var traineeByDirection = await traineeServices.GetByDirection(result.Directions);
        var traineeByProject = await traineeServices.GetByProject(result.Projects);

        var model = new ResourcesListViewModel(
            activeTab, searchQuery, sortOrder,
            result.TotalPagesProjects, result.TotalPagesDirections,
            result.Projects, result.Directions,
            traineeByProject, traineeByDirection,
            page, pageSize
        );

        return View(model);
    }
}