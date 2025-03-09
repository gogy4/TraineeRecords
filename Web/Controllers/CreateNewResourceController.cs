using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Route("CreateNewResource")]
public class CreateNewResourceController : Controller
{
    private readonly ResourceServices _resourceServices;

    public CreateNewResourceController(ResourceServices resourceServices)
    {
        _resourceServices = resourceServices;
    }

    [HttpPost]
    [Route("AddInternshipDirection")]
    public async Task<IActionResult> AddInternshipDirection([FromBody] string newDirection)
    {
        await _resourceServices.CreateInternshipDirection(newDirection);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("AddCurrentProject")]
    public async Task<IActionResult> AddCurrentProject([FromBody] string newProject)
    {
        await _resourceServices.CreateCurrentProject(newProject);
        return RedirectToAction("Index");
    }
}
