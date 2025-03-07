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
        var directionsName = new List<string>();
        var projectsName = new List<string>();
        foreach (var t in trainees)
        {
            directionsName.Add((await directionsServices.GetById(t.InternshipDirectionId)).Name);
            projectsName.Add((await projectServices.GetById(t.CurrentProjectId)).Name);
        }
        var resourcesName = (directionsName, projectsName);

        var model = new TraineeListViewModel(trainees, directions, projects, resourcesName);
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Filter(string directionFilter, string projectFilter)
    {
        var filteredTrainees = await traineeServices.GetByFilter(directionFilter, projectFilter);
        var projects = await projectServices.GetAll();
        var directions = await directionsServices.GetAll();
    
        var directionsName = new List<string>();
        var projectsName = new List<string>();

        foreach (var t in filteredTrainees)
        {
            directionsName.Add((await directionsServices.GetById(t.InternshipDirectionId)).Name);
            projectsName.Add((await projectServices.GetById(t.CurrentProjectId)).Name);
        }
    
        var resourcesName = (directionsName, projectsName);
    
        ViewBag.SelectedDirection = directionFilter;
        ViewBag.SelectedProject = projectFilter;

        var model = new TraineeListViewModel(filteredTrainees, directions, projects, resourcesName);
        return View("Index", model);
    }


}