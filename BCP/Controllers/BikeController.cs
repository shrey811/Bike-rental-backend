using BCP.ApiModels;
using BCP.Core.Dtos;
using BCP.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Controllers;

[ApiController]
[Route("api/bike")]
public class BikeController : ControllerBase
{
    private readonly BikeService _bikeService;

    public BikeController(BikeService bikeService)
    {
        _bikeService = bikeService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertBike(BikeInsertViewModel model)
    {
        var dto = new BikeInsertDto
        {
            BrandId = model.BrandId,
            Name = model.Name,
            NumberPlate = model.NumberPlate
        };
        var bike = await _bikeService.InsertAsync(dto);
        return Ok(bike);
    }
}