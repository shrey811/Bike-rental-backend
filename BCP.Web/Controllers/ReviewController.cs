using BCP.ApiModels.Review;
using BCP.Core.Repository;
using BCP.Core.Services;
using BCP.Dtos.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Controllers;

[ApiController]
[Route("api/review")]

public class ReviewController : ControllerBase
{
    private readonly ReviewService _reviewService;
    private readonly  IReviewRepository _reviewRepository;

    public ReviewController(ReviewService reviewService, IReviewRepository reviewRepository)
    {
        _reviewService = reviewService;
        _reviewRepository = reviewRepository;
    }
    [HttpPost]
    public async Task<IActionResult> PostReview(ReviewApiModel model)
    {
        var dto = new ReviewDto()
        {
            Message = model.Message,
            Rating = model.Rating,
            BikeId = model.BikeId
        };
        var review = await _reviewService.InsertAsync(dto);
        return Ok(review);
    }
    [HttpGet("{bikeId:int}")]
    public async Task<IActionResult> GetById(int bikeId)
    {
        var bike = await _reviewRepository.GetByIdAsync(bikeId);
        return Ok(bike);
    }

    [HttpGet] 
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _reviewRepository.GetAllAsync());
    }
}