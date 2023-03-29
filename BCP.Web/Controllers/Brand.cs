using BCP.Core.Dtos;
using BCP.Core.Entities;
using BCP.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BCP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll()
        {
            var brands = await _brandRepository.GetAllAsync();
            var dtos = new List<BrandDto>();
            foreach (var brand in brands)
            {
                var dto = new BrandDto
                {
                    Id = brand.Id,
                    BrandName = brand.Name,
                    Manufacturer = brand.Manufacturer
                };
                dtos.Add(dto);
            }
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDto>> Create(BrandDto dto)
        {
            var brand = new Brand
            {
                Name = dto.BrandName,
                Manufacturer = dto.Manufacturer
            };
            var createdBrand = await _brandRepository.InsertAsync(brand);
            var createdDto = new BrandDto
            {
                Id = createdBrand.Id,
                BrandName = createdBrand.Name,
                Manufacturer = createdBrand.Manufacturer
            };
            return CreatedAtAction(nameof(GetById), new { brandId = createdDto.Id }, createdDto);
        }

        [HttpGet("{brandId:int}", Name = nameof(GetById))]
        public async Task<ActionResult<BrandDto>> GetById(int brandId)
        {
            var brand = await _brandRepository.GetByIdAsync(brandId);
            if (brand == null)
            {
                return NotFound();
            }
            var dto = new BrandDto
            {
                Id = brand.Id,
                BrandName = brand.Name,
                Manufacturer = brand.Manufacturer
            };
            return Ok(dto);
        }
    }
}
