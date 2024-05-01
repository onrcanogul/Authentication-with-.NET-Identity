using Business.DTOs.Role;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class RolesController : Controller
    {
        readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            await _roleService.CreateRoleAsync(name);
            return RedirectToAction("getallusers", "users");
        }
        
    }
}
