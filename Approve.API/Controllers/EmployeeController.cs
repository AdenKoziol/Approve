using Approve.API.Models;
using Approve.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Approve.API.Controllers
{
    [ApiController]
    [Route("/Employees")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public Task<IEnumerable<MEmployee>> GetAllEmployees()
        {
            return REmployee.GetAllEmployees();
        }

        [HttpGet("ID/{ID}")]
        public Task<MEmployee> GetEmployeeByID(int ID)
        {
           return REmployee.GetEmployeeByID(ID);
        }

        [HttpGet("Name/{Name}")]
        public Task<MEmployee> GetEmployeeByName(string Name)
        {
            return REmployee.GetEmployeeByName(Name);
        }

        [HttpGet("Team/{Team}")]
        public Task<IEnumerable<MEmployee>> GetEmployeesByTeam(string Team)
        {
            return REmployee.GetEmployeesByTeam(Team);
        }

        [HttpGet("NextEmployeeID")]
        public Task<Int32> GetNextEmployeeID()
        {
            return REmployee.GetNextEmployeeID();
        }

        [HttpDelete]
        public void DeleteEmployee(MEmployee employee)
        {
            REmployee.DeleteEmployee(employee);
        }

        [HttpPost]
        public void CreateEmployee(MEmployee employee)
        {
            REmployee.CreateEmployee(employee);
        }

        [HttpPut]
        public void UpdateEmployee(MEmployee employee)
        {
            REmployee.UpdateEmployee(employee);
        }
    }
}