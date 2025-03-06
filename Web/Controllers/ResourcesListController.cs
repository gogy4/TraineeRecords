using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ResourcesListController(
    CurrentProjectServices currentProjectServices,
    InternshipDirectionsServices internshipDirectionsServices) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string activeTab = "projects")
    {
        var projects = await currentProjectServices.GetAll();
        var directions = await internshipDirectionsServices.GetAll();
        var model = new ResourcesListViewModel(projects, directions);
        
        ViewBag.ActiveTab = activeTab;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> FilterAndSort(string searchQuery, string sortOrder = "name", string activeTab = "projects")
    {
        var projects = await currentProjectServices.GetByFilter(searchQuery);
        var directions = await internshipDirectionsServices.GetByFilter(searchQuery);

        if (sortOrder == "trainees")
        {
            projects = projects.OrderByDescending(p => p.CountTrainees).ToList();
            directions = directions.OrderByDescending(d => d.CountTrainees).ToList();
        }
        else
        {
            projects = projects.OrderBy(p => p.Name).ToList();
            directions = directions.OrderBy(d => d.Name).ToList();
        }

        var model = new ResourcesListViewModel(projects, directions);
        ViewBag.ActiveTab = activeTab;

        return View("Index", model);
    }

    [HttpGet]
    public async Task<IActionResult> Paginate(int page = 1, int pageSize = 10, string searchQuery = "", string sortOrder = "name", string activeTab = "projects")
    {
        var projects = await currentProjectServices.GetByFilter(searchQuery);
        var directions = await internshipDirectionsServices.GetByFilter(searchQuery);

        if (sortOrder == "trainees")
        {
            projects = projects.OrderByDescending(p => p.CountTrainees).ToList();
            directions = directions.OrderByDescending(d => d.CountTrainees).ToList();
        }
        else
        {
            projects = projects.OrderBy(p => p.Name).ToList();
            directions = directions.OrderBy(d => d.Name).ToList();
        }

        var projectPaged = projects.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var directionPaged = directions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var model = new ResourcesListViewModel(projectPaged, directionPaged, page, pageSize);
        ViewBag.ActiveTab = activeTab;

        return View("Index", model);
    }
}
