using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using EmployeeManagementSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/bonus")]
    public class BonusController : ControllerBase
    {
        private readonly AddBonus _AddBonus;

        public BonusController(AddBonus AddBonus)
        {
            _AddBonus = AddBonus;
        }

        
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CheckIn([FromBody] BonusApproval request)
        {

            int EmployeeID = request.EmployeeID;
            int BonusTypeID = request.BonusTypeID;
            decimal Amount = request.Amount;
            string Reason = request.reason;


            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var ManagerID = int.Parse(employeeIdClaim);

            bool bonusadded = await _AddBonus.CreateBonus(EmployeeID, ManagerID, BonusTypeID, Amount, Reason);
            if (bonusadded)
            {
                return Ok();
            }

            return BadRequest(new { message = "Failed to add bonus, try again later!" });
        }
    }
}
