using BCP.ApiModels;
using BCP.Core.Dtos;
using BCP.Core.Repository;
using BCP.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Controllers;

[ApiController]
[Route("api/bike")]
public class BikeController : ControllerBase
{
    private readonly BikeService _bikeService;
    private IBikeRepository _bikeRepository;

    public BikeController(BikeService bikeService, IBikeRepository bikeRepository)
    {
        _bikeRepository = bikeRepository;
        _bikeService = bikeService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertBike(BikeInsertViewModel model)
    {
        var dto = new BikeInsertDto
        {
            brandId = model.brandId,
            Name = model.Name,
            NumberPlate = model.NumberPlate,
            Description = model.Description,
            KmRun = model.KmRun,
            Milage = model.Milage,
            Rating = model.Rating,
            ImageUrl = model.ImageUrl
            
        };
        var bike = await _bikeService.InsertAsync(dto);
        return Ok(bike);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBike(int id)
    {
        var bike = await _bikeService.GetAsync(id);
        if (bike == null)
        {
            return NotFound();
        }
        return Ok(bike);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBikes()
    {
        var bikes = await _bikeRepository.GetAllAsync();
        return Ok(bikes);
    }
  
    
}