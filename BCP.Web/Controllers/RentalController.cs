        
         
         using BCP.Core.Dtos;
         using BCP.Core.Entities;
         using BCP.Core.Repository;
         using BCP.Core.Services;
         using Microsoft.AspNetCore.Mvc;
         using System.Collections.Generic;
         using System.Threading.Tasks;
         using BCP.ApiModels;
         using BCP.ApiModels.Rent;
         using BCP.Core.Enums;
         
         namespace BCP.API.Controllers
         {
             [ApiController]
             [Route("api/[controller]")]
             public class RentController : ControllerBase
             {
                 private readonly RentalService _rentService;
                 private readonly IBikeRepository _bikeRepository;
                 private readonly IRentRepository _rentRepository;
         
                 public RentController(RentalService rentService, IBikeRepository bikeRepository, IRentRepository rentRepository)
                 {
                     _rentService = rentService;
                     _bikeRepository = bikeRepository;
                     _rentRepository = rentRepository;
                 }
         
                 [HttpGet("rented")]
                 public async Task<ActionResult<IEnumerable<RentDto>>> GetRentedBikes()
                 {
                     var rentedBikes = await _rentRepository.GetAllRentedAsync();
                     var model = rentedBikes.Select(r => new RentInsertResponseModel()
                     {
                         User = $"{r.RentedBy.FirstName} {r.RentedBy.LastName}",
                         BikeName = r.Bike.Name,
                         BrandName = r.Bike.Brand.Name,
                         RentedOn = r.RentedOn,
                         RentedUntil = r.RentedUntil,
                         Id = r.Id
                     });
                     return Ok(model);
                 }
         
                 [HttpPost("rent")]
                 public async Task<ActionResult<Bike>> RentBike(RentalInsertApiModel model)
                 {
                     var bike = await _bikeRepository.GetByIdAsync(model.BikeId);
                     if (bike == null)
                     {
                         return NotFound();
                     }
         
                     if (!bike.RentalStatus.Equals(BikeRentalStatus.Available))
                     {
                         return BadRequest("Bike is not available for rent");
                     }
         
                     var rentEntry = new RentDto()
                     {
                         BikeId = bike.Id,
                         UserId = model.UserId,
                         RentedUntil = model.RentedUntil,
                         DateTime = model.DateTime
                     };
         
                     await _rentService.RentBikeAsync(rentEntry);
         
                     bike.RentalStatus = BikeRentalStatus.Rented;
                     await _bikeRepository.UpdateAsync(bike);
         
                     return bike;
                 }
         
                 // [HttpPost("return")]
                 // public async Task<ActionResult<Bike>> ReturnBike(ReturnApiModel model)
                 // {
                 //     var bike = await _bikeRepository.GetByIdAsync(model.BikeId);
                 //     await _rentService.ReturnBikeAsync(bike)
                 //     var dto = new RentalReturnDto
                 // }
             }
         }
         //public async Task<ActionResult<Bike>> ReturnBike(ReturnApiModel model)
        // {
        //     var bike = await _bikeRepository.GetByIdAsync(model.BikeId);
        //     await _rentService.ReturnBikeAsync(bike)
        //     var dto = new RentalReturnDto
        // }
        
