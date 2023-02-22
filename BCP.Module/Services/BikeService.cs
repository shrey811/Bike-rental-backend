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
        var brand = await _brandRepository.GetByIdAsync(dto.BrandId) ?? throw new BrandNotFoundException();
        var bike = new Bike()
        {
            NumberPlate = dto.NumberPlate,
            Name = dto.Name,
            Brand = brand
        };
        await _bikeRepository.InsertAsync(bike);
        return bike;
    }
}