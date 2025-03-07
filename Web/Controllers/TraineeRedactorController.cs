using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class TraineeRedactorController(
    TraineeServices traineeServices,
    CurrentProjectServices projectServices,
    InternshipDirectionsServices directionsServices,
    ResourceServices resourceServices) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(Guid traineeId)
    {
        var trainee = await traineeServices.GetById(traineeId);
        var direction = (await directionsServices.GetById(trainee.InternshipDirectionId)).Name;
        var project = (await projectServices.GetById(trainee.CurrentProjectId)).Name;
        var (directionNames, projectNames) = await resourceServices.GetNameResources();
        
        var traineeDto = new TraineeDto(trainee, project, direction);
        var model = new EditTraineeViewModel(traineeDto, directionNames, projectNames, 
            TempData["Errors"] as string, TempData["Success"] as string);
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditTraineeViewModel editTrainee)
    {
        var trainee = editTrainee.Trainee;
        try
        {
            await traineeServices.Edit(trainee);
            TempData["Success"] = "Стажер успешно изменен!";
            return RedirectToAction("Index", new { traineeId = trainee.Id });
        }
        catch (ArgumentException e)
        {
            TempData["Errors"] = e.Message;
            return RedirectToAction("Index", new { traineeId = trainee.Id });
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
