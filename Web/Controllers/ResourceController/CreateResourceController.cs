using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CreateResourceController(TraineeServices traineeServices) : CreateEditResource(traineeServices)
{
    public async Task<IActionResult> Index(string resourceType)
    {
        var trainee = await traineeServices.GetAll();
        var model = new OperationResourceViewModel(resourceType, TempData["Errors"] as string,
            TempData["Success"] as string, trainee);
        return View(model);
    }
}