using AutoMapper;
using BaristaAPI.Data;
using BaristaAPI.Models.Domain;
using BaristaAPI.Models.DTO;
using BaristaAPI.Repositories;
using BaristaAPI.Utills;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaristaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICafeRepository cafeRepository;

        public CafeController(IMapper mapper, ICafeRepository cafeRepository)
        {
            this.mapper = mapper;
            this.cafeRepository = cafeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCafeDto addCafeDto)
        {
            var cafeDomainModel = mapper.Map<Cafe>(addCafeDto);
            cafeDomainModel = await cafeRepository.CreateAsync(cafeDomainModel);

            return Ok(mapper.Map<CafeDto>(cafeDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? location)
        {
            var cafeDomain = await cafeRepository.GetAllAsync(location);

            return Ok(mapper.Map<List<CafeDto>>(cafeDomain));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var cafeDomain = await cafeRepository.GetByIdAsync(id);
            if (cafeDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CafeDto>(cafeDomain));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCafeDto updateCafeDto)
        {
            var cafeDomain = mapper.Map<Cafe>(updateCafeDto);

            cafeDomain = await cafeRepository.UpdateAsync(id, cafeDomain);

            if (cafeDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CafeDto>(cafeDomain));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var cafeDomain = await cafeRepository.DeleteAsync(id);

            if (cafeDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CafeDto>(cafeDomain));
        }
    }

        
}
