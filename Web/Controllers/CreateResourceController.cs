using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CreateResourceController(TraineeServices traineeServices) : Controller
{
    public async Task<IActionResult> Index(string resourceType)
    {
        var trainee = await traineeServices.GetAll();
        var model = new CreateResourceViewModel(trainee, resourceType, TempData["Success"] as string,
            TempData["Errors"] as string);
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Create(Guid traineeId, Guid resourceId, string resourceType)
    {
        var success = resourceType == "Direction" ? "Направление успешно создано" : "Проект успешно создан";
        if (traineeId == Guid.Empty)
        {
            TempData["Success"] = success;
            return RedirectToAction("Index", new { resourceType });
        }

        try
        {
            await traineeServices.CreateResource(traineeId, resourceId, resourceType);
            TempData["Success"] = success;
            return RedirectToAction("Index", new { resourceType });
        }

        catch (ArgumentException e)
        {
            TempData["Errors"] = e.Message;
            return RedirectToAction("Index", new { resourceType });
        }
    }
}