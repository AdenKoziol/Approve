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
    [Route("/Machines")]
    public class MachineController : ControllerBase
    {
        [HttpGet]
        public Task<IEnumerable<MMachine>> GetAllMachines()
        {
            return RMachine.GetAllMachines();
        }

        [HttpGet("ID/{ID}")]
        public Task<MMachine> GetMachineById(int ID) 
        { 
            return RMachine.GetMachineByID(ID);
        }

        [HttpGet("Name/{Name}")]
        public Task<MMachine> GetMachineByName(string Name)
        {
            return RMachine.GetMachineByName(Name);
        }

        [HttpGet("GetNextMachineID")]
        public Task<Int32> GetNextMachineID()
        {
            return RMachine.GetNextMachineID();
        }

        [HttpDelete]
        public void DeleteMachine(MMachine machine)
        {
            RMachine.DeleteMachine(machine);
        }

        [HttpPost]
        public void CreateMachine(MMachine machine)
        {
            RMachine.CreateMachine(machine);
        }

        [HttpPut]
        public void UpdateMachine(MMachine machine)
        {
            RMachine.UpdateMachine(machine);
        }
    }
}