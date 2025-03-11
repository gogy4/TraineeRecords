using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class TraineeRedactorController(TraineeService traineeService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(Guid traineeId)
    {
        var (resourcesProperties, traineeDto) = await traineeService.GetTraineeWithResources(traineeId);
        var model = new TraineeViewModel(resourcesProperties.DirectionNames.Values.ToList(),
            resourcesProperties.ProjectNames.Values.ToList(),
            TempData["Errors"] as string, TempData["Success"] as string, traineeDto);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TraineeViewModel editTrainee)
    {
        var trainee = editTrainee.Trainee;
        try
        {
            await traineeService.Edit(trainee);
            TempData["Success"] = "Стажер успешно изменен!";
            return RedirectToAction("Index", new { traineeId = trainee.Id });
        }
        catch (ArgumentException e)
        {
            TempData["Errors"] = e.Message;
            return RedirectToAction("Index", new { traineeId = trainee.Id });
        }
    }
}