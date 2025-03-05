using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class CreateTraineeController(TraineeServices traineeServices, GetResourceIdsService resourceIdsService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string name, string surname, string gender, string email, string phoneNumber,
        DateTime dateOfBirth, string internshipDirection, string currentProject)
    {
        var ids = await resourceIdsService.GetIds(internshipDirection, currentProject);
        var trainee = await traineeServices.Create(name, surname, gender, email, phoneNumber, dateOfBirth,
            ids.internshipDirectionId, ids.currentProjectId);
        return RedirectToAction("Index");
    }
}