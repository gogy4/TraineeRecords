using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Route("CreateTrainee")]
public class CreateTraineeController(TraineeServices traineeServices, ResourceServices resourceServices)
    : Controller
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var (directionNames, projectNames) = await resourceServices.GetNameResources();
        ViewBag.InternshipDirections = directionNames;
        ViewBag.Projects = projectNames;
        return View();
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTrainee(CreateTraineeDto traineeDto)
    {
        var ids = await resourceServices.GetIds(traineeDto.InternshipDirection, traineeDto.CurrentProject);
        await traineeServices.Create(
            traineeDto.Name, traineeDto.Surname, traineeDto.Gender, traineeDto.Email,
            traineeDto.PhoneNumber, traineeDto.DateOfBirth,
            ids.internshipDirectionId, ids.currentProjectId
        );

        return RedirectToAction("Index");
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