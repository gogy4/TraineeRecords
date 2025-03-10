using Application.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class DeleteResourceController(DeleteResourceService deleteResourceService, ResourceServices resourceServices) : Controller
{
    public async Task<IActionResult> Index(string resourceType)
    {
        var resources = await resourceServices.GetAll();
        var model = new DeleteResourceViewModel(resources.projects, resources.directions, TempData["Error"] as string,
            TempData["Success"] as string, resourceType);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id, string resourceType)
    {
        var success = resourceType == "Direction" ? "Направление успешно удалено" : "Проект успешно удален";
        try
        {
            await deleteResourceService.DeleteResource(id, resourceType);
            TempData["Success"] = success;
            return RedirectToAction("Index", new { resourceType });
        }
        catch (ArgumentException e)
        {
            TempData["Error"] = e.Message;
            return RedirectToAction("Index", new { resourceType });
        }
    }
}