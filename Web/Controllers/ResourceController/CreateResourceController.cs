using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CreateResourceController(TraineeService traineeService) : CreateEditResource(traineeService)
{
    public async Task<IActionResult> Index(string resourceType)
    {
        var trainee = await traineeService.GetAll();
        var model = new OperationResourceViewModel(resourceType, TempData["Errors"] as string,
            TempData["Success"] as string, trainee);
        return View(model);
    }
}