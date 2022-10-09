using AutoMapper;
using Employee.Core.DTOs.Employee;
using Employee.Services.UOW;

namespace Employee.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        #region Props
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<IActionResult> GetAllAysnc()
        {
            var rates = await _unitOfWork.Employee.GetAllAsync(
                orderBy: r => r.Id,
                orderByDirection: SD.Descending);

            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(rates));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(id);

            if (emp is null)
                return NotFound($"No employee was found with ID: {id}");

            return Ok(_mapper.Map<EmployeeDto>(emp));
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateAsync([FromBody] EmployeeCreationDto dto)
        {
            if (await _unitOfWork.Employee.IsValidAsync(ex => ex.Email == dto.Email))
                return BadRequest("Email Id already exists!");

            //mapping 
            var emp = _mapper.Map<Employee.Core.Entities.Models.Employee>(dto);
            emp.CreatedAt = DateTime.UtcNow;
            emp.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Employee.AddAsync(emp);
            _unitOfWork.Save();

            return Ok(emp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeUpdateDto dto)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(id);

            if (emp is null)
                return NotFound($"No employee was found with ID: {id}");

            if (await _unitOfWork.Employee.IsValidAsync(ex => ex.Email == dto.Email & ex.Id !=id))
                return BadRequest("Email  Id already exists!");
            emp.Email = dto.Email;
            emp.Name = dto.Name;
            emp.Phone = dto.Phone;
            emp.Address = dto.Address;
            emp.Sallary = dto.Sallary;
            emp.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Employee.Update(emp);
            _unitOfWork.Save();

            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(id);

            if (emp is null)
                return NotFound($"No employee was found with ID: {id}");

            _unitOfWork.Employee.Delete(emp);
            _unitOfWork.Save();
            return Ok();
        }
        #endregion Actions
    }
}
