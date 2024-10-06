using Approve.API.Models;
using Approve.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Approve.API.Controllers
{
    [ApiController]
    [Route("/Emails")]
    public class EmailController : ControllerBase
    {
        [HttpGet("ID/{ID}")]
        public Task<MEmail> GetEmailByID(int ID)
        {
            return REmail.GetEmailByID(ID);
        }

        [HttpGet("RequestID/{RequestID}")]
        public Task<IEnumerable<MEmail>> GetEmailsByRequestID(int RequestID)
        {
            return REmail.GetEmailsByRequestID(RequestID);
        }

        [HttpGet("GetNextEmailID")]
        public Task<Int32> GetNextEmailID()
        {
            return REmail.GetNextEmailID();
        }

        [HttpPost]
        public void CreateEmail(MEmail email)
        {
            REmail.CreateEmail(email);
        }

        [HttpPut]
        public void UpdateEmail(int emailID)
        {
            REmail.UpdateEmail(emailID);
        }

        [HttpGet("Approve")]
        public void ApproveEmail(int emailID)
        {
            REmail.UpdateEmail(emailID);
        }

        [HttpGet("EmailClick")]
        public IActionResult EmailClick(int emailID)
        {
            try
            {
                MEmail email = REmail.GetEmailByID(emailID).Result;
                MRequest request = RRequest.GetRequestByID(email.RequestID).Result;

                return Content("<html><body style='background-color: #252526; color: #FFFFFF;'>" +
                    $"<h3>Request #: {request.ID}</h3>" +
                    $"<p>Machine: {request.Machine.Name}</p>" +
                    $"<p>Description: {request.Description}</p>" +
                    $"<p>Date Posted: {request.DatePosted}</p>" +
                    $"<p>Posted By: {request.Poster.Name}</p>" +
                    $@"
                    <a href='https://localhost:44387/Emails/Approve?emailID={email.ID}' 
                       style='display: inline-block; padding: 10px 20px; font-size: 16px; font-weight: bold; color: #fff; 
                              background-color: #4CAF50; text-align: center; text-decoration: none; border-radius: 5px;'>
                       Approve
                    </a>" +
                    "</body></html>", "text/html");
            }
            catch
            {
                return Content("<html><body><h3>Request Was Deleted</h3></body></html>", "text/html");
            }
        }
    }
}
