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
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized call for image with id: {}", imageId);
            return Unauthorized();
        }

        var result = await CaffService.GetImageAsync(imageId, userId);
        if (result == null)
        {
            Logger.LogInformation("User with id:{} requested image with id:{}, result: not found.", userId, imageId);
            return NotFound();
        }

        Logger.LogInformation("User with id:{} requested image with id:{}, result: found.", userId, imageId);
        return result;
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
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized user tried to upload an image.");
            return Unauthorized();
        }

        try
        {
            var result = await CaffService.UploadImage(userId, uploadRequest);
            if (result == null)
            {
                Logger.LogInformation("{} user make a bad upload image request.", userId);
                return BadRequest();
            }

            Logger.LogInformation("{} user uploaded file with id: {}", userId, result.Id);
            return Created($"/api/caff/getImage?imageId={result.Id}", result);
        }
        catch (InvalidCaffException)
        {
            Logger.LogInformation("{} user uploaded file a invalid Caff file.", userId);
            return BadRequest("Caff file is not valid");
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
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
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized user tried to comment to file with id: {}.", imageId);
            return Unauthorized();
        }

        try
        {
            var result = await CommentService.AddCommentAsync(imageId, userId, comment);
            Logger.LogInformation("user with id: {} added comment {} to image {}.", userId, result.Id, imageId);
            return Ok(result);
        }
        catch (ImageNotFoundException)
        {
            Logger.LogInformation("user with id: {} tried to comment to non-existent image.", userId);
            return NotFound("Image not found");
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
        Logger.LogInformation("user with id: {} queried every image.", User.GetUserId());
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
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized user query purchased images.");
            return Unauthorized();
        }

        Logger.LogInformation("user with id: {} queried their every purchased image.", User.GetUserId());
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
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized user query uploaded images.");
            return Unauthorized();
        }

        Logger.LogInformation("user with id: {} queried their every uploaded image", User.GetUserId());
        return await CaffService.GetUploadedImagesAsync(userId);
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
            Logger.LogInformation("user with id: {} deleted comment with id: {}.", User.GetUserId(), commentId);
            return Ok();
        }
        catch (CommentNotFoundException)
        {
            Logger.LogInformation("user with id: {} tried to delete comment with id: {}.", User.GetUserId(), commentId);
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
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized user tried to delete image with id: {}.", imageId);
            return Unauthorized();
        }

        var image = await CaffService.DeleteImageAsync(imageId, userId, User.IsInRole(AuthRoles.Admin.ToString()));
        if (image == null)
        {
            Logger.LogInformation("user with id: {} tried to delete image with id: {}.", User.GetUserId(), imageId);
            return NotFound();
        }

        Logger.LogInformation("user with id: {} deleted image with id: {}.", User.GetUserId(), imageId);
        return Ok();
    }

    [HttpGet]
    [Route("downloadImage")]
    [Produces("image/caff")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DownloadImage([FromQuery] string imageId)
    {
        var userId = User.GetUserId();
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized user tried download image with id: {}.", imageId);
            return Unauthorized();
        }

        try
        {
            var result = await CaffService.DownloadImageAsync(imageId, userId);
            Logger.LogInformation("user with id: {} downloaded image with id: {}.", User.GetUserId(), imageId);
            return File(result.Item1, "image/caff", result.Item2);
        }
        catch (ImageNotFoundException)
        {
            Logger.LogInformation("user with id: {} tried to download image with id: {}.", User.GetUserId(), imageId);
            return NotFound("Image not found");
        }
        catch (UserNotFoundException)
        {
            Logger.LogInformation("Unauthorized user tried to download image with id: {}.", imageId);
            return Unauthorized();
        }
        catch (NotAllowedException)
        {
            Logger.LogInformation(
                "user with id: {} tried to download image with id: {}. Rejected due to authorization.", User.GetUserId(), imageId);
            return Unauthorized();
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}