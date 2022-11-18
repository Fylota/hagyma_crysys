using System.Net.Mime;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Models.Auth;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CaffController : ControllerBase
{
    public CaffController(ICaffService caffService, ICommentService commentService, ILogger<CaffController> logger)
    {
        CaffService = caffService;
        CommentService = commentService;
        Logger = logger;
    }

    private ICaffService CaffService { get; }
    private ICommentService CommentService { get; }
    private ILogger<CaffController> Logger { get; }

    [HttpGet]
    [Route("getImage")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaffDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CaffDetails>> GetImage([FromQuery] string imageId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var result = await CaffService.GetImageAsync(imageId, userId);
        if (result == null) return NotFound();
        return result;
    }

    [HttpGet]
    [Route("downloadImage")]
    [Produces("image/caff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DownloadImage([FromQuery] string imageId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        try
        {
            var result = await CaffService.DownloadImageAsync(imageId, userId);
            return File(result.Item1, "image/caff", result.Item2);
        }
        catch (ImageNotFoundException)
        {
            return NotFound("Image not found");
        }
        catch (UserNotFoundException)
        {
            return Unauthorized();
        }
        catch (NotAllowedException)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("listImages")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CaffItem>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<CaffItem>>> GetImages()
    {
        return await CaffService.GetImagesAsync();
    }

    [HttpGet]
    [Route("purchasedImages")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CaffItem>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<CaffItem>>> GetPurchasedImages()
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        return await CaffService.GetPurchasedImagesAsync(userId);
    }

    [HttpGet]
    [Route("uploadedImages")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CaffItem>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<CaffItem>>> GetUploadedImages()
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        return await CaffService.GetUploadedImagesAsync(userId);
    }

    [HttpPost]
    [Route("addComment")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Comment))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Comment>> AddComment([FromQuery] string imageId, [FromBody] CommentRequest comment)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        try
        {
            var result = await CommentService.AddCommentAsync(imageId, userId, comment);
            return Ok(result);
        }
        catch (ImageNotFoundException)
        {
            return NotFound("Image not found");
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpDelete]
    [Route("deleteComment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteComment([FromQuery] string commentId)
    {
        try
        {
            await CommentService.DeleteCommentAsync(commentId);
            return Ok();
        }
        catch (CommentNotFoundException)
        {
            return NotFound("Comment not found");
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    [Route("deleteImage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteImage([FromQuery] string imageId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var image = await CaffService.DeleteImageAsync(imageId, userId, User.IsInRole(AuthRoles.Admin.ToString()));
        if (image == null) return NotFound();
        return Ok();
    }

    [HttpPost]
    [Route("uploadImage")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CaffDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CaffDetails>> UploadImage([FromForm] CaffUploadRequest uploadRequest)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var file = Request.Form.Files.First();
        try
        {
            var result = await CaffService.UploadImage(userId, uploadRequest);
            return Created($"/api/caff/getImage?imageId={result.Id}", result);
        }
        catch (Exception e)
        {
            Logger.LogError("{}",e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}