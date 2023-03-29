using BCP.Core.Entities;
using BCP.Core.Repository;
using BCP.Dtos.Review;

namespace BCP.Core.Services;

public class ReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    public async Task<Review> InsertAsync(ReviewDto dto)
    {
        var review = new Review()
        {
            Message = dto.Message,
            Rating = dto.Rating,
            BikeId = dto.BikeId
        };
        await _reviewRepository.InsertAsync(review);
        return review;
    }
   
}