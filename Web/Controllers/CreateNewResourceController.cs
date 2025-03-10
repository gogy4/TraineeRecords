﻿using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Route("CreateNewResource")]
public class CreateNewResourceController(ResourceServices resourceServices) : Controller
{
    [HttpPost]
    [Route("AddInternshipDirection")]
    public async Task<IActionResult> AddInternshipDirection([FromBody] string newDirection)
    {
        var directionId = await resourceServices.CreateInternshipDirection(newDirection);
        return Ok(new {id = directionId}); 
    }

    [HttpPost]
    [Route("AddCurrentProject")]
    public async Task<IActionResult> AddCurrentProject([FromBody] string newProject)
    {
        var projectId = await resourceServices.CreateCurrentProject(newProject);
        return Ok(new {id = projectId}); 
    }
}
