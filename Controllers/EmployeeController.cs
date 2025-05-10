using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookXpertAPI.Models;
using BookXpertAPI.Data;

namespace BookXpertAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context) => _context = context;

        [HttpGet] public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees() => await _context.Employees.ToListAsync();

        [HttpGet("{id}")] public async Task<ActionResult<Employee>> GetEmployee(int id) => await _context.Employees.FindAsync(id);

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee emp)
        {
            if (_context.Employees.Any(e => e.Name == emp.Name)) return Conflict("Duplicate Name!");
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmployee", new { id = emp.Id }, emp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee emp)
        {
            if (id != emp.Id) return BadRequest();
            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("states")]
        public async Task<ActionResult<IEnumerable<State>>> GetStates() => await _context.States.ToListAsync();
    }
}
