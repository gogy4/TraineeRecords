using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class EditResourceController(TraineeServices traineeServices, EditResourceNameService editResourceNameService) :
    CreateEditResource(traineeServices)
{
    public async Task<IActionResult> Index(Guid resourceId, string resourceType)
    {
        var trainees = await traineeServices.GetTraineeWithoutResource(resourceId, resourceType);
        var resourceName = await editResourceNameService.GetResourceById(resourceId, resourceType);
        var model = new OperationResourceViewModel(resourceType, TempData["Error"] as string,
            TempData["Success"] as string, trainees, resourceId: resourceId, resourceName: resourceName);
        return View(model);
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