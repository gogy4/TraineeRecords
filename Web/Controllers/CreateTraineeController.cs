using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

[Route("CreateTrainee")]
public class CreateTraineeController(TraineeServices traineeServices, ResourceServices resourceServices)
    : Controller
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var (directionNames, projectNames) = await resourceServices.GetNameResources();
        var traineeViewModel = new CreateTraineeViewModel(directionNames, projectNames, 
            TempData["Errors"] as string, TempData["Success"] as string);
        return View(traineeViewModel);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTrainee(CreateTraineeDto traineeDto)
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

    [HttpPost]
    [Route("AddInternshipDirection")]
    public async Task<IActionResult> AddInternshipDirection([FromBody] string newDirection)
    {
        await resourceServices.CreateInternshipDirection(newDirection);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("AddCurrentProject")]
    public async Task<IActionResult> AddCurrentProject([FromBody] string newProject)
    {
        await resourceServices.CreateCurrentProject(newProject);
        return RedirectToAction("Index");
    }
}