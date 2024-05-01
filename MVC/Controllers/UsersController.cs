using Business.DTOs.Role;
using Business.DTOs.User;
using Business.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GetAllUsersDto users = await _userService.GetUsersAsync();
            return View(users);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            bool result = await _userService.CreateUserAsync(user);
            return RedirectToAction(result ? "Index" : "Error");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            GetAllUsersDto users = await _userService.GetUsersAsync();
            
            return View(users);
        }
        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetUser(string id)
        {
            GetUserByIdDto user = await _userService.GetUserByIdAsync(id);
            List<RoleDto> roles = await _roleService.GetAllRoles();
            ViewBag.Roles = roles;
            return View(user);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser()
        {            
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            bool result = await _userService.DeleteUser(id);
            TempData["deleteResult"] = result;
            return RedirectToAction("GetAllUsers");
        }      
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignRole(string id, List<string> selectedRoles)
        {
            await _userService.AssignRoleToUser(id, selectedRoles);
            return RedirectToAction("GetAllUsers");
        }
        
        
    }
}
