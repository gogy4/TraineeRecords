using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers;

public class ResourcesListController(
    CurrentProjectServices currentProjectServices,
    InternshipDirectionsServices internshipDirectionsServices,
    TraineeServices traineesServices)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string activeTab = "projects", string searchQuery = "",
        string sortOrder = "name", int page = 1, int pageSize = 5)
    {
        var projects = await currentProjectServices.GetByFilter(searchQuery);
        var directions = await internshipDirectionsServices.GetByFilter(searchQuery);
        var traineeByDirection = await traineesServices.GetByDirection(directions);
        var traineeByProject = await traineesServices.GetByProject(projects);
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

        var totalProjects = projects.Count;
        var totalDirections = directions.Count;

        var totalPagesProjects = (int)Math.Ceiling((double)totalProjects / pageSize);
        var totalPagesDirections = (int)Math.Ceiling((double)totalDirections / pageSize);

        var model = new ResourcesListViewModel(activeTab, searchQuery, sortOrder, totalPagesProjects, totalPagesDirections,
            projectPaged, directionPaged,  traineeByProject, traineeByDirection, page, pageSize);


        return View(model);
    }
}