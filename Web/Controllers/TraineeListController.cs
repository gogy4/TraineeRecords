using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class TraineeListController(TraineeServices traineeServices, CurrentProjectServices projectServices, InternshipDirectionsServices directionsServices) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var trainees = await traineeServices.GetAll();
        var projects = await projectServices.GetAll();
        var directions = await directionsServices.GetAll();
        
        var model = new TraineeListViewModel(trainees, directions, projects);
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Filter(string directionFilter, string projectFilter)
    {
        var filteredTrainees = await traineeServices.GetByFilter(directionFilter, projectFilter);
        var projects = await projectServices.GetByFilter(projectFilter);
        var directions = await directionsServices.GetByFilter(directionFilter);
        var model = new TraineeListViewModel(filteredTrainees, directions, projects);
        return View("Index", model);
    }
}