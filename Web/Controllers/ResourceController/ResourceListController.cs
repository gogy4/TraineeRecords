using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Controllers;

public class ResourceListController(TraineeServices traineeServices, ResourceServices resourceServices, 
    DeleteResourceService deleteResourceService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string activeTab = "projects", string searchQuery = "",
        string sortOrder = "name", int page = 1, int pageSize = 5)
    {
        var result = await resourceServices.GetFilteredSortedPaged(searchQuery, sortOrder, page, pageSize);
        var traineeByDirection = await traineeServices.GetByDirection(result.Directions
            .Select(d => d.Id)
            .ToArray());
        var traineeByProject = await traineeServices.GetByProject(result.Projects
            .Select(p => p.Id)
            .ToArray());

        var model = new ResourcesListViewModel(
            activeTab, searchQuery, sortOrder,
            result.TotalPagesProjects, result.TotalPagesDirections,
            result.Projects, result.Directions,
            traineeByProject, traineeByDirection, TempData["Success"] as string, TempData["Error"] as string,
            page, pageSize
        );

        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid resourceId, string resourceType, string activeTab, string searchQuery, string sortOrder, int page, int pageSize)
    {
        var successMessage = resourceType == "Direction" ? "Направление успешно удалено" : "Проект успешно удален";
        var a = new CreateResourceController(traineeServices);
        try
        {
            await deleteResourceService.DeleteResource(resourceId, resourceType);
            TempData["Success"] = successMessage;
        }
        catch (ArgumentException e)
        {
            TempData["Error"] = e.Message;
        }

        return RedirectToAction("Index", new { activeTab, searchQuery, sortOrder, page, pageSize });
    }

}