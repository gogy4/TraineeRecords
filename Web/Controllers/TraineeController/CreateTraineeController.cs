using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

[Route("")]
public class CreateTraineeController(TraineeService traineeService, ResourceService resourceService) : Controller
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var resourceProperties = await resourceService.GetResourceProperties();
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
            await traineeService.Create(traineeDto);
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