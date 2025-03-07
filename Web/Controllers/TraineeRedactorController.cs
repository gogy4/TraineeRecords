using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class TraineeRedactorController(
    TraineeServices traineeServices,
    CurrentProjectServices projectServices,
    InternshipDirectionsServices directionsServices) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(Guid traineeId)
    {
        var trainee = await traineeServices.GetById(traineeId);
        var direction = (await directionsServices.GetById(trainee.InternshipDirectionId)).Name;
        var project = (await projectServices.GetById(trainee.CurrentProjectId)).Name;
        var model = new TraineeDto(trainee, direction, project);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TraineeDto traineeDto)
    {
        try
        {
            await traineeServices.Edit(traineeDto);
            TempData["Success"] = "Стажер успешно изменен!";
            return View("Index", traineeDto);
        }
        catch (ArgumentException e)
        {
            TempData["Errors"] = e.Message;
            return View("Index", traineeDto);
        }
    }
}