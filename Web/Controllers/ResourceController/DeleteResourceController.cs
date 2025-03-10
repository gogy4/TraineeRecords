using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class DeleteResourceController(DeleteResourceService deleteResourceService, ResourceServices resourceServices)
    : Controller
{
    public async Task<IActionResult> Index(string resourceType)
    {
        var resources = await resourceServices.GetAll();
        var model = new OperationResourceViewModel(resourceType, TempData["Error"] as string,
            TempData["Success"] as string, projects: resources.projects, directions: resources.directions);
        return View(model);
    }
    

    
}