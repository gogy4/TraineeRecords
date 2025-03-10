using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class EditResourceNameController(
    ResourceServices resourceServices,
    TraineeServices traineeServices,
    EditResourceNameService editResourceNameService) : Controller
{
    public async Task<IActionResult> Index(string resourceType)
    {
        var resources = await resourceServices.GetAll();
        var trainees = await traineeServices.GetAll();
        
        var model = new OperationResourceViewModel(resourceType, TempData["Error"] as string,
            TempData["Success"] as string, trainees,resources.projects, resources.directions);
        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id, string resourceType, string newName)
    {
        try
        {
            if (resourceType == "Direction") await editResourceNameService.ChangeDirectionName(id, newName);
            else await editResourceNameService.ChangeProjectName(id, newName);
            TempData["Success"] = "Вы успешно изменили данные";
            return RedirectToAction("Index", new { resourceType });
        }

        catch (ArgumentException e)
        {
            TempData["Error"] = e.Message;
            return RedirectToAction("Index", new { resourceType });
        }
    }
}