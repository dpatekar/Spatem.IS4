﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spatem.Core.Models;
using Spatem.Identity.IdentityServer;
using System.Threading.Tasks;

namespace Spatem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Custom user registration method
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };

            var result = await _userManager.CreateAsync(user, model.Password);
            // TODO: define roles externally
            string role = "User";

            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                await _userManager.AddToRoleAsync(user, role);
                //await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(JwtClaimTypes.GivenName, model.FirstName));
                //await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(JwtClaimTypes.FamilyName, model.LastName));

                return Ok(new ProfileViewModel(user));
            }

            return BadRequest(result.Errors);
        }
    }
}