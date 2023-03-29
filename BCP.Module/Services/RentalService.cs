using BCP.Core.Dtos;
using BCP.Core.Entities;
using BCP.Core.Enums;
using BCP.Core.Exceptions;
using BCP.Core.Repository;

namespace BCP.Core.Services
{
    public class RentalService
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRentRepository _rentRepository;


        public RentalService(IBikeRepository bikeRepository, IUserRepository userRepository,IRentRepository rentRepository)
        {
            _bikeRepository = bikeRepository;
            _userRepository = userRepository;
            _rentRepository = rentRepository;
         
        }

        public async Task RentBikeAsync(RentDto dto)
        {
            var bike = await _bikeRepository.GetByIdAsync(dto.BikeId) ?? throw new Exception();
            var user = await _userRepository.GetByIdAsync(dto.UserId) ?? throw new UserNotFoundException();

            if (bike.RentalStatus.Equals(BikeRentalStatus.Rented))
            {
                throw new BikeAlreadyInUseException();
            }

            bike.RentalStatus = BikeRentalStatus.Rented;

            var rentalEntry = new RentEntry()
            {
                RentedBy = user,
                Bike = bike,
                Remarks = dto.Remarks,
                RentedUntil = dto.RentedUntil,
                RentedOn = dto.RentedOn ?? DateTime.UtcNow,
                Price = dto.Price,
                
            };
            
            await _bikeRepository.UpdateAsync(bike);
            await _rentRepository.InsertAsync(rentalEntry);
            // await _rentRepository.SendOtpEmailAsync(rentalEntry);
            await _rentRepository.SendRentalEmailAsync(user, bike);
        }

        public async Task ReturnBikeAsync(ReturnDto dto)
        {
            var bike = await _bikeRepository.GetByIdAsync(dto.BikeId) ?? throw new BikeNotFoundException();

            if (!bike.RentalStatus.Equals(BikeRentalStatus.Rented))
            {
                throw new BikeNotRentedException();
            }

            var rentEntry = await _rentRepository.GetRentedEntryByBikeId(dto.BikeId) ?? throw new BikeNotRentedException();
                            
            bike.RentalStatus = BikeRentalStatus.Available;
            rentEntry.Status = BikeRentalStatus.Available;

            await _bikeRepository.UpdateAsync(bike);
            await _rentRepository.UpdateAsync(rentEntry);
        }
    }
}
