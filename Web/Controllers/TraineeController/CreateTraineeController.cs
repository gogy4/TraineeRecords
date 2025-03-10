using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

[Route("")]
public class CreateTraineeController(TraineeServices traineeServices, ResourceServices resourceServices)
    : Controller
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var resourceProperties = await resourceServices.GetResourceProperties();
        var traineeViewModel = new TraineeViewModel(resourceProperties.DirectionNames.Values.ToList(),
            resourceProperties.ProjectNames.Values.ToList(),
            TempData["Errors"] as string, TempData["Success"] as string);
        return View(traineeViewModel);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTrainee(TraineeDto traineeDto)
    {
        try
        {
            await traineeServices.Create(traineeDto);
            TempData["Success"] = "Стажер успешно создан!";
            return RedirectToAction("Index");
        }
        catch (ArgumentException e)
        {
            TempData["Errors"] = e.Message;
            return RedirectToAction("Index");
        }
    }
}