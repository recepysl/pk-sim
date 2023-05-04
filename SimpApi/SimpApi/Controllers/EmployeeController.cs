using Microsoft.AspNetCore.Mvc;
using SimpApi.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpApi.Controllers;




[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{ 
    public EmployeeController()
    {

    }


    [HttpPost]
    public ActionResult<Employee> Post ([FromBody] Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.Email))
        {
            return BadRequest("Invalid email");
        }
        if (string.IsNullOrWhiteSpace(employee.Name))
        {
            return BadRequest("Invalid name");
        }
        if (employee.HourlySalary < 10 || employee.HourlySalary > 50)
        {
            return BadRequest("Invalid HourlySalary");
        }

        return Ok(employee);
    }



    [HttpPut]
    public Employee Put([FromBody] Employee employee)
    {
        return employee;
    }

}
