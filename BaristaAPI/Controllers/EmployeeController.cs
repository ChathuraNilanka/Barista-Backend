using AutoMapper;
using BaristaAPI.Models.Domain;
using BaristaAPI.Models.DTO;
using BaristaAPI.Repositories;
using BaristaAPI.Utills;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaristaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            this.mapper = mapper;
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddEmployeeDto addEmployeeDto)
        {
            var employueDomainModel = mapper.Map<Employee>(addEmployeeDto);

            employueDomainModel = await employeeRepository.CreateAsync(employueDomainModel);

            return Ok(mapper.Map<EmployeeDto>(employueDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? cafe)
        {
            var employeesDomain = await employeeRepository.GetAllAsync(cafe);
            var employeeDtos = mapper.Map<List<EmployeeDto>>(employeesDomain);
            var orderdEmployees = employeeDtos.OrderByDescending(e => e.DaysWorked).ToList();
            return Ok(orderdEmployees);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var employeesDomain = await employeeRepository.GetByIdAsync(id);
            if (employeesDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<EmployeeDto>(employeesDomain));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var employeesDomain = mapper.Map<Employee>(updateEmployeeDto);

            employeesDomain = await employeeRepository.UpdateAsync(id, employeesDomain);

            if (employeesDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<EmployeeDto>(employeesDomain));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var employeesDomain = await employeeRepository.DeleteAsync(id);

            if (employeesDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<EmployeeDto>(employeesDomain));
        }
    }

    
}
