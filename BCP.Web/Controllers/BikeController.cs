using System.Diagnostics;
using BCP.ApiModels.Bike;
using BCP.ApiModels.Review;
using BCP.Core.Dtos;
using BCP.Core.Enums;
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
    public async Task<IActionResult> InsertBike(BikeInsertApiModel model)
    {
        var dto = new BikeInsertDto
        {
            brandId = model.brandId,
            Name = model.Name,
            NumberPlate = model.NumberPlate,
            Description = model.Description,
            KmRun = model.KmRun,
            Milage = model.Milage,
            ImageUrl = model.ImageUrl,
            Price = model.Price
            
            
        };
        var bike = await _bikeService.InsertAsync(dto);
        return Ok(bike);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBike(int id)
    {
        var bike = await _bikeRepository.GetByIdAsync(id);
        if (bike == null)
        {
            return NotFound();
        }

        var bikeModel = new BikeModel()
        {
            brandId = bike.BrandId,
            Description = bike.Description,
            KmRun = bike.KmRun,
            Milage = bike.Milage,
            ImageUrl = bike.ImageUrl,
            NumberPlate = bike.NumberPlate,
            Name = bike.Name,
            Id = bike.BrandId,
            Price = bike.Price

        };
        return Ok(bike);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBikes()
    {
        var bikes = await _bikeRepository.GetAllAsync();
        return Ok(bikes);
    }


    [HttpPut("{id}/rental-status")]
    public async Task<IActionResult> UpdateBikeRentalStatus(int id)
    {
        var bike = await _bikeRepository.GetByIdAsync(id);
        if (bike == null)
        {
            return NotFound();
        }

        bike.RentalStatus = BikeRentalStatus.Available; // set rental status to Available
        await _bikeRepository.UpdateAsync(bike);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBike(int id, BikeUpdateApiModel model)
    {
        var bike = await _bikeRepository.GetByIdAsync(id);
        if (bike == null)
        {
            return NotFound();
        }

        bike.Name = model.Name ;
        bike.Description = model.Description ;
        bike.KmRun = model.KmRun ;
        bike.Milage = model.Milage ;
        bike.NumberPlate = model.NumberPlate;
        bike.ImageUrl = model.ImageUrl;
        bike.Price = model.Price ;

        await _bikeRepository.UpdateAsync(bike);

        return Ok(bike);
    }
}

  
  

    
