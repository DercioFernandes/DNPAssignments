﻿using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPIv1.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic logic;

    public PostController(IPostLogic logic)
    {
        this.logic = logic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDTO dto)
    {
        try
        {
            Post post = await logic.CreateAsync(dto);
            return Created($"/users/{post.idPost}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] int? idCreator)
    {
        try
        {
            SearchPostParametersDTO parameters = new(idCreator);
            IEnumerable<Post> posts = await logic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult<Post>> UpdateAsync([FromBody] PostUpdateDTO dto)
    {
        try
        {
            Post post = await logic.UpdateAsync(dto);
            return Created($"/users/{post.idPost}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await logic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{idPost:int}")]
    public async Task<ActionResult<PostBasicDTO>> GetById([FromRoute] int idPost)
    {
        try
        {
            PostBasicDTO result = await logic.GetByIdAsync(idPost);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}