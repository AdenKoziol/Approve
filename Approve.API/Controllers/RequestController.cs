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
    [Route("/Requests")]
    public class RequestController : ControllerBase
    {
        [HttpGet("GetAllRequests/{Completed}")]
        public Task<IEnumerable<MRequest>> GetAllRequests(bool Completed)
        {
            return RRequest.GetAllRequests(Completed);
        }

        [HttpGet("ID/{ID}")]
        public Task<MRequest> GetRequestByID(int ID)
        {
            return RRequest.GetRequestByID(ID);
        }

        [HttpGet("GetNextRequestID")]
        public Task<Int32> GetNextRequestID()
        {
            return RRequest.GetNextRequestID();
        }

        [HttpDelete]
        public void DeleteRequest(MRequest request)
        {
            REmail.DeleteEmails(request.ID);
            RRequest.DeleteRequest(request);
        }

        [HttpPost]
        public void CreateRequest(MRequest request)
        {
            RRequest.CreateRequest(request);
        }

        [HttpPut]
        public void UpdateRequest(MRequest request)
        {
            RRequest.UpdateRequest(request);
        }
    }
}
