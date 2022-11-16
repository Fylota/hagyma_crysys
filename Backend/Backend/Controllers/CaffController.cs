using System.Net.Mime;
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
    private ICaffService CaffService { get; }

    public CaffController(ICaffService caffService)
    {
        CaffService = caffService;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UploadImage([FromBody] CaffUploadRequest uploadRequest)
    {
        return await Task.FromResult(Ok());
    }

    [HttpGet]
    [Route("downloadImage")]
    [Produces("image/caff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<FileInfo>> DownloadImage([FromQuery] string imageId)
    {
        return await Task.FromResult(Ok());
    }

    [HttpPost]
    [Route("addComment")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> AddComment([FromQuery] string imageId, [FromBody] Comment comment)
    {
        return await Task.FromResult(Ok());
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
        return await Task.FromResult(Ok());
    }
}