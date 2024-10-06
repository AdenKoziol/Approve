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
    [Route("/Teams")]
    public class TeamController : ControllerBase
    {
        [HttpGet]
        public Task<IEnumerable<MTeam>> GetAllTeams()
        {
            return RTeam.GetAllTeams();
        }

        [HttpGet("ID/{ID}")]
        public Task<MTeam> GetTeamByID(int ID) 
        {
            return RTeam.GetTeamByID(ID);
        }

        [HttpGet("Name/{Name}")]
        public Task<MTeam> GetTeamByName(string Name)
        {
            return RTeam.GetTeamByName(Name);
        }

        [HttpGet("GetNextTeamID")]
        public Task<Int32> GetNextTeamID()
        {
            return RTeam.GetNextTeamID();
        }

        [HttpDelete]
        public void DeleteTeam(MTeam team)
        {
            RTeam.DeleteTeam(team);
        }

        [HttpPost]
        public void CreateTeam(MTeam team) 
        {
            RTeam.CreateTeam(team);
        }

        [HttpPut]
        public void UpdateTeam(MTeam team)
        {
            RTeam.UpdateTeam(team);
        }
    }
}
