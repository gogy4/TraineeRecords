using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public abstract class CreateEditResource(TraineeServices traineeServices) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Operate(Guid traineeId, Guid resourceId, string resourceType)
    {
        var success = "Операция прошла успешно";
        if (traineeId == Guid.Empty)
        {
            TempData["Success"] = success;
            return RedirectToAction("Index", new { resourceId, resourceType });
        }

        try
        {
            await traineeServices.EditTraineeResource(traineeId, resourceId, resourceType);
            TempData["Success"] = success;
            return RedirectToAction("Index", new { resourceId, resourceType });
        }

        catch (ArgumentException e)
        {
            TempData["Errors"] = e.Message;
            return RedirectToAction("Index", new { resourceId, resourceType });
        }
    }
}