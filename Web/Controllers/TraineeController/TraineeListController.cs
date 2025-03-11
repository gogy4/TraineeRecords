using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class TraineeListController(TraineeService traineeService, ResourceService resourceService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var trainees = await traineeService.GetAll();
        var resources = await resourceService.GetResourceProperties();
        var model = new TraineeListViewModel(trainees, resources);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Filter(string directionFilter, string projectFilter)
    {
        var filteredTrainees = await traineeService.GetByFilter(directionFilter, projectFilter);
        var resources = await resourceService.GetResourceProperties();
        var model = new TraineeListViewModel(filteredTrainees, resources, directionFilter, projectFilter);
        return View("Index", model);
    }
}