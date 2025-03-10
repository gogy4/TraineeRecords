using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class EditResourceController(
    TraineeServices traineeServices,
    CurrentProjectServices currentProjectServices,
    InternshipDirectionsServices internshipDirectionsServices, EditResourceNameService editResourceNameService,
    DeleteResourceService deleteResourceService) : Controller
{
    public async Task<IActionResult> Index(Guid resourceId, string resourceType)
    {
        var trainees = await traineeServices.GetTraineeWithoutResource(resourceId, resourceType);
        var resourceName = resourceType == "Direction"
            ? (await internshipDirectionsServices.GetById(resourceId)).Name
            : (await currentProjectServices.GetById(resourceId)).Name;
        var model = new OperationResourceViewModel(resourceType, TempData["Error"] as string,
            TempData["Success"] as string, trainees, resourceId: resourceId, resourceName: resourceName);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid traineeId, Guid resourceId, string resourceType)
    {
        var success = "Стажеры успешно изменены";
        if (traineeId == Guid.Empty)
        {
            TempData["Success"] = success;
            return RedirectToAction("Index", new { resourceId, resourceType });
        }

        try
        {
            await traineeServices.CreateResource(traineeId, resourceId, resourceType);
            TempData["Success"] = success;
            return RedirectToAction("Index", new { resourceId, resourceType });
        }

        catch (ArgumentException e)
        {
            TempData["Errors"] = e.Message;
            return RedirectToAction("Index", new { resourceId, resourceType });
        }
    }
    
    public async Task<IActionResult> EditName(Guid resourceId, string resourceType, string newName)
    {
        try
        {
            await editResourceNameService.ChangeResourceName(resourceId, resourceType, newName);
            TempData["Success"] = "Вы успешно изменили данные";
            return RedirectToAction("Index", new { resourceId, resourceType });
        }

        catch (ArgumentException e)
        {
            TempData["Error"] = e.Message;
            return RedirectToAction("Index", new { resourceId, resourceType });
        }
    }
    
}