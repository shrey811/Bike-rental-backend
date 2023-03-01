// // using BCP.ApiModels;
// // using BCP.Core.Dtos;
// // using BCP.Core.Repository;
// // using BCP.Core.Services;
// // using Microsoft.AspNetCore.Mvc;
// //
// // namespace BCP.Controllers;
// //
// // [ApiController]
// // [Route("api/rental")]
// // public class RentalController : ControllerBase
// // {
// //     private readonly RentalService _rentalService;
// //     private readonly IRentRepository _rentRepository;
// //
// //
// //     public RentalController(RentalService rentalService, IRentRepository rentRepository)
// //     {
// //         _rentalService = rentalService;
// //         _rentRepository = rentRepository;
// //     }
// //
// //     [HttpPost]
// //     public async Task<IActionResult> RentBike(RentalInsertViewModel model)
// //     {
// //         var dto = new RentalDto
// //         {
// //             BikeId = model.BikeId,
// //              Remarks = model.Remarks
// //         };
// //         // var rental = await _rentalService.RentBikeAsync(dto.BikeId,dto.UserId);
// //         return Ok(rental);
// //     }
// //
// //     [HttpGet("{id}")]
// //     public async Task<IActionResult> GetRental(int id)
// //     {
// //         var rental = await _rentalService.GetRentalAsync(id);
// //         if (rental == null)
// //         {
// //             return NotFound();
// //         }
// //         return Ok(rental);
// //     }
// //
// //     [HttpGet]
// //     public async Task<IActionResult> GetAllRentals()
// //     {
// //         var rentals = await _rentalRepository.GetAllAsync();
// //         return Ok(rentals);
// //     }
// //
// //     [HttpPut("{id}/return")]
// //     public async Task<IActionResult> ReturnBike(int id, [FromBody] RentalReturnViewModel model)
// //     {
// //         var dto = new RentalReturnDto
// //         {
// //             RentalId = id,
// //             ReturnDate = model.ReturnDate,
// //             DamageDescription = model.DamageDescription
// //         };
// //         var rental = await _rentalService.ReturnBikeAsync(dto);
// //         return Ok(rental);
// //     }
// // }
//
// using BCP.Core.Dtos;
// using BCP.Core.Entities;
// using BCP.Core.Repository;
// using BCP.Core.Services;
// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// namespace BCP.API.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class RentController : ControllerBase
//     {
//         private readonly IRentRepository _rentService;
//         private readonly IBikeRepository _bikeRepository;
//
//         public RentController(IRentRepository rentService, IBikeRepository bikeRepository)
//         {
//             _rentService = rentService;
//             _bikeRepository = bikeRepository;
//         }
//
//         [HttpGet("rented")]
//         public async Task<ActionResult<IEnumerable<RentDto>>> GetRentedBikes()
//         {
//             var rentedBikes = await _rentService.GetAllRentedAsync();
//             return Ok(rentedBikes);
//         }
//
//         [HttpPost("rent")]
//         public async Task<ActionResult<Bike>> RentBike(int bikeId, int userId)
//         {
//             var bike = await _bikeRepository.GetByIdAsync(bikeId);
//             if (bike == null)
//             {
//                 return NotFound();
//             }
//
//             if (bike.Status != BikeRentalStatus.Available)
//             {
//                 return BadRequest("Bike is not available for rent");
//             }
//
//             var rentEntry = new RentEntry
//             {
//                 BikeId = bikeId,
//                 UserId = userId,
//                 Status = BikeRentalStatus.Rented
//             };
//
//             await _rentService.RentBikeAsync(rentEntry);
//
//             bike.Status = BikeRentalStatus.Rented;
//             await _bikeRepository.UpdateAsync(bike);
//
//             return bike;
//         }
//
//         [HttpPost("return")]
//         public async Task<ActionResult<Bike>> ReturnBike(int bikeId, int userId)
//         {
//             var bike = await _bikeRepository.GetByIdAsync(bikeId);
//             if (bike == null)
//             {
//                 return NotFound();
//             }
//
//             if (bike.Status != BikeRentalStatus.Rented)
//             {
//                 return BadRequest("Bike is not currently rented");
//             }
//
//             var rentEntry = await _rentService.GetActiveRentalByBikeIdAsync(bikeId);
//             if (rentEntry == null)
//             {
//                 return BadRequest("No active rental found for this bike");
//             }
//
//             if (rentEntry.UserId != userId)
//             {
//                 return BadRequest("This rental is associated with a different user");
//             }
//
//             rentEntry.Status = BikeRentalStatus.Returned;
//             rentEntry.ReturnedDateTime = DateTime.Now;
//
//             await _rentService.ReturnBikeAsync(rentEntry);
//
//             bike.Status = BikeRentalStatus.Available;
//             await _bikeRepository.UpdateAsync(bike);
//
//             return bike;
//         }
//     }
// }
