using BCP.Core.Dtos;
using BCP.Core.Entities;
using BCP.Core.Exceptions;
using BCP.Core.Repository;

namespace BCP.Core.Services;

public class BikeService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IBikeRepository _bikeRepository;

    public BikeService(IBrandRepository brandRepository, IBikeRepository bikeRepository)
    {
        _brandRepository = brandRepository;
        _bikeRepository = bikeRepository;
    }

    public async Task<Bike> InsertAsync(BikeInsertDto dto)
    {
        var brand = await _brandRepository.GetByIdAsync(dto.brandId) ?? throw new BrandNotFoundException();
        var bike = new Bike()
        {
            NumberPlate = dto.NumberPlate,
            Name = dto.Name,
            Brand = brand,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            Milage = dto.Milage,
            KmRun = dto.KmRun,
            BrandId = dto.brandId,
            Id = dto.Id,
            Rating = dto.Rating,

        };
        await _bikeRepository.InsertAsync(bike);
        return bike;
    }
   
    public async Task<BikeInsertDto> GetAsync(int id)
    {
        var bike = await _bikeRepository.GetByIdAsync(id);

        if (bike == null)
        {
            return null;
        }

        var brandDto = new BikeInsertDto
        {
            Id = bike.Brand.Id,
            Name = bike.Brand.Name
        };

        var bikeDto = new BikeInsertDto
        {
            Id = bike.Id,
            
            Name = bike.Name,
            NumberPlate = bike.NumberPlate,
            Description = bike.Description,
            KmRun = bike.KmRun,
            Milage = bike.Milage,
            Rating = bike.Rating,
            ImageUrl = bike.ImageUrl
            
        };

        return bikeDto;
    }
   
   
   
}