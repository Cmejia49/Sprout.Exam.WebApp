using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.DataAccess;
using Sprout.Exam.Business.EmployeeManager;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
          _employeeService = employeeService;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetAsync();
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);  
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {

            var emp = await _employeeService.GetByIdAsync(input.Id);
            if(emp == null)
            {
                return NotFound();
            }

            var updated = await _employeeService.UpdateAsync(input);
            if (updated)
            {
                return Ok(input);
            }
           return BadRequest();
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var id =  await _employeeService.CreateAsync(input);
            return Created($"/api/employees/{id}", id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteByIdAsync(id);
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate/{absentDays}/{workedDays}")]
        public async Task<IActionResult> Calculate(int id,decimal absentDays,decimal workedDays)
        {

            var emp = await _employeeService.GetByIdAsync(id);
            if(emp == null)
            {
                return NotFound();
            }
            var typeId = (EmployeeType)emp.EmployeeTypeId;
            var result = await _employeeService.CalculateAsync(typeId,absentDays, workedDays);
            return Ok(result);
        }

    }
}
