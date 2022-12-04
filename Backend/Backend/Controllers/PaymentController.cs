using System.Net.Mime;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PaymentController : ControllerBase
{
    public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
    {
        PaymentService = paymentService;
        Logger = logger;
    }

    private ILogger<PaymentController> Logger { get; }
    private IPaymentService PaymentService { get; }

    [HttpPost]
    [Route("purchase")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> PurchaseImage([FromQuery] string imageId)
    {
        var userId = User.GetUserId();
        if (userId == null)
        {
            Logger.LogInformation("Unauthorized user tried to purchase image with id: {}.", imageId);
            return Unauthorized();
        }

        try
        {
            var result = await PaymentService.BuyImageAsync(imageId, userId);
            Logger.LogInformation("user with id: {} purchased image with id: {}.", User.GetUserId(), imageId);
            return Ok(result);
        }
        catch (ImageNotFoundException)
        {
            Logger.LogInformation("user with id: {} tried to purchase image with id: {}.", User.GetUserId(), imageId);
            return NotFound("Image not found");
        }
        catch (UserNotFoundException)
        {
            Logger.LogInformation("Unauthorized user tried to purchase image with id: {}.", imageId);
            return Unauthorized();
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}