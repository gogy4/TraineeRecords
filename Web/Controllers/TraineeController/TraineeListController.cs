using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class TraineeListController(TraineeServices traineeServices, ResourceServices resourceServices) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var trainees = await traineeServices.GetAll();
        var resources = await resourceServices.GetResourceProperties();
        var model = new TraineeListViewModel(trainees, resources);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Filter(string directionFilter, string projectFilter)
    {
        var filteredTrainees = await traineeServices.GetByFilter(directionFilter, projectFilter);
        var resources = await resourceServices.GetResourceProperties();
        var model = new TraineeListViewModel(filteredTrainees, resources, directionFilter, projectFilter);
        return View("Index", model);
    }
}