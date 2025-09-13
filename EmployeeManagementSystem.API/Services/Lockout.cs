using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.API.Data;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.API.Services
{
    public class LockoutService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private const int MaxFailedAccessAttempts = 5; // Define a constant for max attempts

        public LockoutService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        
        public async Task<bool> CheckPasswordAndLockoutAsync(string email, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false; //gui error message invalid login
            }
            // Check if locked out permanently
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value == DateTimeOffset.MaxValue)
            {
                return false; //still need to display a gui msg, that the user is locked out
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                // Reset failed count and lockout cycle on success
                await _userManager.ResetAccessFailedCountAsync(user);
                user.LockoutCycleCount = 0;
                await _userManager.UpdateAsync(user);
                return true; //goes to user dashboard
            }
            else
            {//benefham dih bete3mel eh belzabt, bet3ed el trials beta3et el user w momken ne8ayar feeha ne5aleeha absat
                await _userManager.AccessFailedAsync(user);

                if (user.AccessFailedCount >= 5)
                {
                    // Lockout logic escalation
                    user.LockoutCycleCount++;
                    DateTimeOffset lockoutEnd;
                    switch (user.LockoutCycleCount)
                    {
                        case 1:
                            lockoutEnd = DateTimeOffset.UtcNow.AddMinutes(1);
                            break;
                        case 2:
                            lockoutEnd = DateTimeOffset.UtcNow.AddMinutes(5);
                            break;
                        case 3:
                            lockoutEnd = DateTimeOffset.UtcNow.AddMinutes(15); // Or whatever
                            break;
                        default:
                            lockoutEnd = DateTimeOffset.MaxValue; // Permanent lockout
                            break;
                    }
                    await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);//after end of lock, he is redirected to login page

                    // Reset failed count after lockout is set
                    await _userManager.ResetAccessFailedCountAsync(user);

                    await _userManager.UpdateAsync(user);

                    //var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                    //if (failedCount >= MaxFailedAccessAttempts)
                    //{
                    //    // Lock the user out for a specified duration (e.g., 5 minutes)
                    //    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));
                    //}
                }
                return false;
            }
        }
        

        public async Task<bool> IsLockedOutAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }

            return await _userManager.IsLockedOutAsync(user);//hena momken nezabatha mn el gui en bey-return time screen le moddet el lockout
                                                             //aw beywadi el user lel login page law mesh locked out
        }
    }
}